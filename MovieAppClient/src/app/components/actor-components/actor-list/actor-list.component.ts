import { HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Actor } from 'src/app/models/actor-models/actor';
import { Metadata } from 'src/app/models/metadata';
import { ActorService } from 'src/app/services/actor.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-actor-list',
  templateUrl: './actor-list.component.html',
  styleUrls: ['./actor-list.component.css']
})
export class ActorListComponent {
  actors: Actor[]
  metadata: Metadata
  searchedName: string = ''
  isLoading: boolean = true


  constructor(private service: ActorService, public auth: AuthenticationService){}

  ngOnInit(){
    this.getActorsByParams(1, false);
    document.onscroll = (e) => {
      const scrollY = window.scrollY + document.body.clientHeight;
      const height = document.body.scrollHeight;
  
      if(this.metadata && this.metadata.CurrentPage < this.metadata.TotalPages  && scrollY >= height && !this.isLoading) {
        this.getActorsByParams(this.metadata.CurrentPage + 1, true)
      }
    }
  }

  getActorsByParams(pageNumber: number, concat: boolean = false){
    this.isLoading = true
    this.service.getActors(`pageSize=6&searchedName=${this.searchedName}&pageNumber=${pageNumber}`)
    .subscribe((res : HttpResponse<Actor[]>) => {
      this.metadata = JSON.parse(res.headers.get('X-Pagination'))
      if(concat){
        this.actors = this.actors.concat(res.body)
      }
      else
        this.actors = res.body
      this.isLoading = false
    })
  }

  deleteActor(id: string){
    this.service.deleteActor(id)
    .subscribe(r => this.getActorsByParams(1))
  }

  searchActors(name: string, e){
    e.preventDefault()
    this.searchedName = name
    this.getActorsByParams(1)
  }

}
