import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Metadata } from 'src/app/models/metadata';
import { Movie } from 'src/app/models/movie-models/movie';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-movies-to-watch',
  templateUrl: './movies-to-watch.component.html',
  styleUrls: ['./movies-to-watch.component.css']
})
export class MoviesToWatchComponent {
  movies: Movie[]
  metadata: Metadata
  searchingTitle: string = ""
  searchingGenres: string = ""
  minDate: string = '1000-01-01'
  maxDate: string = '4000-01-01'
  selectedGenres: string[] = [];
  isLoading: boolean = true
  isDropdownOpen = false;

  constructor(private service: MovieService, public auth: AuthenticationService){}

  ngOnInit(){
    this.getMoviesByParameters(1)
    document.onscroll = (e) => {
      const scrollY = window.scrollY + document.body.clientHeight;
      const height = document.body.scrollHeight;
  
      if(this.metadata && this.metadata.CurrentPage < this.metadata.TotalPages  && scrollY >= height && !this.isLoading) {
        this.getMoviesByParameters(this.metadata.CurrentPage + 1, true)
      }
    }
  }

  getMoviesByParameters(pageNumber: number, concat: boolean = false){
    this.isLoading = true
    const params = `pageSize=6&pageNumber=${pageNumber}&searchedTitle=${this.searchingTitle}&searchedGenres=${this.searchingGenres}&`
    + `minDateRelease=${this.minDate}&maxDateRelease=${this.maxDate}`

    this.service.getMoviesToWatch(params)
    .subscribe(r => {
      if(concat)
        this.movies = this.movies.concat(r.body)
      else
        this.movies = r.body
      this.metadata = JSON.parse(r.headers.get('X-Pagination'))
      this.isLoading = false
    })
  }

  searchMovies(title: string, e){
    e.preventDefault()
    this.searchingTitle = title
    this.getMoviesByParameters(1)
  }

  filterByReleaseDate(){
    const maxDate = document.getElementById('maxDate') as HTMLInputElement
    const minDate = document.getElementById('minDate') as HTMLInputElement

    if(maxDate && minDate){
      this.minDate = minDate.value
      this.maxDate = maxDate.value
      this.getMoviesByParameters(1)
    }
  }

  toggleOption(option: string) {
    if (this.selectedGenres.includes(option)) {
      this.selectedGenres = this.selectedGenres.filter(item => item !== option);
    } else {
      this.selectedGenres.push(option);
    }
    this.searchingGenres = this.selectedGenres.join(' ')
    this.getMoviesByParameters(1)

  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  isSelected(option: string): boolean {
    return this.selectedGenres.includes(option);
  }

  deleteFromWatching(id: string){
    this.service.deleteMovieFromWatching(id)
    .subscribe(r => this.getMoviesByParameters(1, false))
  }

}
