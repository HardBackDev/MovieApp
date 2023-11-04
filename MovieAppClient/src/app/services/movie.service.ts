import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { MovieWithActors } from '../models/movie-models/movie-with-actors';
import { environment } from 'src/environments/environment';
import { Movie } from '../models/movie-models/movie';
import { tokenGetter } from '../app.module';
import { MovieForCreation } from '../models/movie-models/movie-for-creation';
import { MovieForUpdate } from '../models/movie-models/movie-for-update';
import { MovieRate } from '../models/movie-rate';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  url = `${environment.apiUrl}/movies`

  constructor(private http: HttpClient) { }

  public getMoviesWithActors(params: string, actorsCount: number) : Observable<HttpResponse<MovieWithActors[]>>{
    return this.http.get<MovieWithActors[]>(`${this.url}/withActors?${params}&actorsCount=${actorsCount}`,
     {headers: this.generateHeaders(), observe: 'response'})
  }

  public getMoviesByActor(actorId: string, params: string) : Observable<HttpResponse<Movie[]>>{
    return this.http.get<Movie[]>(`${this.url}/byActor/${actorId}?${params}`,
     {headers: this.generateHeaders(), observe: 'response'})
  }

  public getMoviesByDirector(directorId: string, params: string) : Observable<HttpResponse<Movie[]>>{
    return this.http.get<Movie[]>(`${this.url}/byDirector/${directorId}?${params}`,
     {headers: this.generateHeaders(), observe: 'response'})
  }

  public getMoviesToWatch(params: string) : Observable<HttpResponse<Movie[]>>{
    return this.http.get<Movie[]>(`${this.url}/toWatch?${params}`,
     {headers: this.generateHeaders(), observe: 'response'})
  }

  public getMovie(id: string) : Observable<Movie>{
    return this.http.get<Movie>(`${this.url}/${id}`, {headers: this.generateHeaders()})
  }

  public checkInWatching(id: string) : Observable<boolean>{
    return this.http.get<boolean>(`${this.url}/checkInWatching/${id}`, {headers: this.generateHeaders()})
  }

  public createMovie(movie: MovieForCreation){
    return this.http.post(`${this.url}`, movie, {headers: this.generateHeaders(), observe: 'response' })
  }

  public addMovieToWatching(id: string){
    return this.http.post(`${this.url}/addToWatching/${id}`, null, {headers: this.generateHeaders()})
  }

  public addActorRelation(actorId: string, movieId: string){
    return this.http.post(`${this.url}/addActor/${movieId}/${actorId}`, null, {headers: this.generateHeaders()})
  }

  public updateMovie(id: string, movie: MovieForUpdate){
    return this.http.put(`${this.url}/${id}`, movie, {headers: this.generateHeaders()})
  }

  public deleteMovie(id: string){
    return this.http.delete(`${this.url}/${id}`, {headers: this.generateHeaders()})
  }

  public deleteMovieFromWatching(id: string){
    return this.http.delete(`${this.url}/deleteFromWatching/${id}`, {headers: this.generateHeaders()})
  }

  public rateMovie(id: string, rate: string){
    return this.http.post(`${this.url}/rate/${id}/${rate}`, null, {headers: this.generateHeaders()})
  }

  public unRateMovie(id: string){
    return this.http.delete(`${this.url}/unRate?id=${id}`, {headers: this.generateHeaders()})
  }

  public getMovieRate(id: string) : Observable<MovieRate>{
    return this.http.get<MovieRate>(`${this.url}/getRate/${id}`, {headers: this.generateHeaders()})
  }

  private generateHeaders = () => {
    return new HttpHeaders({ 
    'Content-Type': 'application/json',
    'Accept': 'application/json',
    'Authorization': `Bearer ${tokenGetter()}`
  })
  }
}
