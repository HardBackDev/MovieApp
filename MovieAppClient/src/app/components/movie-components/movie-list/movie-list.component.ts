import { HttpResponse } from '@angular/common/http';
import { Component, ElementRef, Renderer2 } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Metadata } from 'src/app/models/metadata';
import { Movie } from 'src/app/models/movie-models/movie';
import { MovieWithActors } from 'src/app/models/movie-models/movie-with-actors';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent {
  movies: MovieWithActors[] = []
  metadata: Metadata
  isLoading: boolean = true
  searchingTitle: string = ""
  searchingGenres: string = ""
  isConfirmDelete: boolean
  movieToDelete: MovieWithActors
  minDate: string = '0001-01-01'
  maxDate: string = '9999-12-31'
  orderBy: string = 'ReleaseDate'

  isDropdownOpen = false;
  selectedGenres: string[] = [];

  constructor(public service: MovieService, public authService: AuthenticationService){
  }

  ngOnInit(){
    const currentPage = localStorage.getItem('currentPage')
    if(currentPage){
      this.getMoviesByParameters(Number(currentPage))
    }else{
      this.getMoviesByParameters(1)
    }
  }

  getMoviesByParameters(pageNumber: number, onLoaded: (res: HttpResponse<MovieWithActors[]>) => void = null, defaultPageChange = true){
    const params = `pageSize=6&pageNumber=${pageNumber}&searchedTitle=${this.searchingTitle}&searchedGenres=${this.searchingGenres}&`
    + `minDateRelease=${this.minDate}&maxDateRelease=${this.maxDate}&orderBy=${this.orderBy}`
    this.isLoading = true
    const element = document.getElementById('movies-section')
    if(defaultPageChange){
      element.style.transition = 'transform 1s ease, opacity 1s ease'
      element.style.opacity = '0';
    }

    this.service.getMoviesWithActors(params, 2)
    .subscribe((res: HttpResponse<MovieWithActors[]>) => {
      this.metadata = JSON.parse(res.headers.get('X-Pagination'));
      localStorage.setItem('currentPage', this.metadata.CurrentPage.toString())
      this.movies = res.body.map((m) => {
        m.genres = m.genres.map(g => ' ' + g )
        return m
      })
      var pageInput = document.getElementById('page-input') as HTMLInputElement
      pageInput.value = pageNumber.toString()
      this.isLoading = false
      if(defaultPageChange){
        element.style.opacity = '1';
      }
      if(onLoaded)
        onLoaded(res)
    })
  }

  changePage(dir: string) {
    if(this.isLoading)
      return
    this.isLoading = true
    const element = document.getElementById('movies-section')
    element.style.transition = 'transform 1s ease, opacity 0.6s ease'
    element.style.opacity = '0';
    element.style.transform = dir == 'left' ? 'translateX(100%)' : 'translateX(-100%)'

    setTimeout(async () => {
      element.style.transition = ''
      element.style.transform = dir == 'left' ?  'translateX(-100%)' : 'translateX(100%)'
      const pageNumber = dir == 'left' ? this.metadata.CurrentPage - 1 : this.metadata.CurrentPage + 1
      await new Promise(f => setTimeout(f, 100));

      this.getMoviesByParameters(pageNumber, (r) => {
        element.style.transition = 'transform 0.6s ease, opacity 1s ease'
        element.style.opacity = '1'
        element.style.transform = 'translateX(0)'; 
      }, false)
    }, 600);
  }

  setPage(number: string, e){
    e.preventDefault()
    if(number == '')
      return
    const pageNumber = Number.parseInt(number)
    
    if(!(pageNumber <= 0) && !(pageNumber > this.metadata.TotalPages)){
      this.getMoviesByParameters(pageNumber)
    }
  }

  onInputPageChange(e){
    const inputElement = e.target;
    if(inputElement == null)
      return
    if(inputElement.value == '')
      return
    const settingPage: number = Number.parseInt(inputElement.value);
    if (settingPage > this.metadata.TotalPages) {
      inputElement.value = this.metadata.TotalPages
    }
    else if(settingPage <= 0){
      inputElement.value = 1
    }
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
  
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
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
  

  getMostLiked(e: Event) {
    e.preventDefault()
    const button = e.target as HTMLButtonElement

    if(this.orderBy == 'ReleaseDate'){
      button.style.background = 'white'
      button.style.color = 'black'
      this.orderBy = 'GoodRating'
    }else{
      button.style.background = ''
      button.style.color = ''
      this.orderBy = 'ReleaseDate'
    }
    this.getMoviesByParameters(1)
  }

  deleteMovie(movieId: string){
    this.movieToDelete = null
    this.isConfirmDelete = false
    this.service.deleteMovie(movieId)
    .subscribe(r => {
      this.getMoviesByParameters(this.metadata.CurrentPage);
    })
  }
  
  isSelected(option: string): boolean {
    return this.selectedGenres.includes(option);
  }
}
