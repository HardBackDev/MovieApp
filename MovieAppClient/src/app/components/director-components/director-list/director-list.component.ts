import { HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Director } from 'src/app/models/director-models/director';
import { Metadata } from 'src/app/models/metadata';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DirectorService } from 'src/app/services/director.service';

@Component({
  selector: 'app-director-list',
  templateUrl: './director-list.component.html',
  styleUrls: ['./director-list.component.css']
})
export class DirectorListComponent {
  directors: Director[]
  metadata: Metadata
  searchedName: string = ''
  isLoading: boolean = true


  constructor(private service: DirectorService, public auth: AuthenticationService){}

  ngOnInit(){
    this.getDirectorsByParams(1, false);
    document.onscroll = (e) => {
      const scrollY = window.scrollY + document.body.clientHeight;
      const height = document.body.scrollHeight;
  
      if(this.metadata && this.metadata.CurrentPage < this.metadata.TotalPages  && scrollY >= height && !this.isLoading) {
        this.getDirectorsByParams(this.metadata.CurrentPage + 1, true)
      }
    }
  }

  getDirectorsByParams(pageNumber: number, concat: boolean = false){
    this.isLoading = true
    this.service.getDirectors(`pageSize=3&searchedName=${this.searchedName}&pageNumber=${pageNumber}`)
    .subscribe((res : HttpResponse<Director[]>) => {
      this.metadata = JSON.parse(res.headers.get('X-Pagination'))
      if(concat){
        this.directors = this.directors.concat(res.body)
      }
      else
        this.directors = res.body
      this.isLoading = false
    })
  }

  deleteDirector(id: string){
    this.service.deleteDirector(id)
    .subscribe(r => this.getDirectorsByParams(1))
  }

  searchDirectors(name: string, e){
    e.preventDefault()
    this.searchedName = name
    this.getDirectorsByParams(1)
  }

}
