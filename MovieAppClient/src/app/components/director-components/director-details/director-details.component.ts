import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Director } from 'src/app/models/director-models/director';
import { Metadata } from 'src/app/models/metadata';
import { Movie } from 'src/app/models/movie-models/movie';
import { DirectorService } from 'src/app/services/director.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-director-details',
  templateUrl: './director-details.component.html',
  styleUrls: ['./director-details.component.css']
})
export class DirectorDetailsComponent {
  directorId: string
  route: ActivatedRoute = inject(ActivatedRoute);
  movies: Movie[]
  director: Director
  metadata: Metadata
  isLoading: boolean = true


  constructor(private service: DirectorService, private movieService: MovieService){}

  ngOnInit(){
    this.directorId = this.route.snapshot.params['id']

    this.service.getDirector(this.directorId)
    .subscribe(res => {
      this.director = res
      this.isLoading = false
    })

    this.movieService.getMoviesByDirector(this.directorId, 'pageSize=10')
    .subscribe(res => {
      this.metadata = JSON.parse(res.headers.get('X-Pagination'))
      this.movies = res.body
    })
  }
}
