import { HttpResponse } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Actor } from 'src/app/models/actor-models/actor';
import { Metadata } from 'src/app/models/metadata';
import { Movie } from 'src/app/models/movie-models/movie';
import { ActorService } from 'src/app/services/actor.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-actor-details',
  templateUrl: './actor-details.component.html',
  styleUrls: ['./actor-details.component.css']
})
export class ActorDetailsComponent {
  actorId: string
  route: ActivatedRoute = inject(ActivatedRoute);
  movies: Movie[]
  actor: Actor
  metadata: Metadata
  isLoading: boolean = true


  constructor(private service: ActorService, private movieService: MovieService){}

  ngOnInit(){
    this.actorId = this.route.snapshot.params['id']

    this.service.getActor(this.actorId)
    .subscribe(res => {
      this.actor = res
      this.isLoading = false
    })

    this.movieService.getMoviesByActor(this.actorId, 'pageSize=10')
    .subscribe(res => {
      this.metadata = JSON.parse(res.headers.get('X-Pagination'))
      this.movies = res.body
    })
  }
}
