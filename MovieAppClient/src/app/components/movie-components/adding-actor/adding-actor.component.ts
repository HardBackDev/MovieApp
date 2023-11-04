import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Actor } from 'src/app/models/actor-models/actor';
import { Metadata } from 'src/app/models/metadata';
import { ActorService } from 'src/app/services/actor.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-adding-actor',
  templateUrl: './adding-actor.component.html',
  styleUrls: ['./adding-actor.component.css']
})
export class AddingActorComponent {
  route: ActivatedRoute = inject(ActivatedRoute);
  actors: Actor[]
  movieId: string
  searchedName: string = ''
  metadata: Metadata
  actorsLoading: boolean = true
  actorsInMovie: string[] = []
  
  constructor(private movieService: MovieService, private actorService: ActorService){
  }

  ngOnInit(){
    this.movieId = this.route.snapshot.params['id'];
    document.onscroll = (e) => {
      const scrollY = window.scrollY + document.body.clientHeight;
      const height = document.body.scrollHeight;
  
      if (this.metadata && !this.actorsLoading && this.metadata.CurrentPage < this.metadata.TotalPages  && scrollY >= height - 10) {
        this.getActors(this.metadata.CurrentPage + 1, true)
      }
    }

    this.getActors(1,false)
  }

  getActors(pageNumber: number, concat: boolean){
    this.actorsLoading = true
    this.actorService.getActors(`searchedName=${this.searchedName}&pageNumber=${pageNumber}`)
    .subscribe(allActors => {
      this.actorService.getActorsByMovie(this.movieId, `searchedName=${this.searchedName}&pageNumber=${pageNumber}`)
      .subscribe(actorsByMovie => {
        for(let actor of actorsByMovie.body){
          this.actorsInMovie.push(actor.id)
        }
        this.metadata = JSON.parse(allActors.headers.get('X-Pagination'))
        if(concat)
          this.actors = this.actors.concat(allActors.body)
        else
          this.actors = allActors.body
        console.log(this.actorsInMovie)
        this.actorsLoading = false
      })
      
    })
  }

  addActorRelation(actorId: string){
    this.movieService.addActorRelation(actorId, this.movieId)
    .subscribe(r => this.getActors(1,false))
  }

  searchActors(name: string, e){
    e.preventDefault()
    this.searchedName = name
    this.getActors(1, false)
  }

}
