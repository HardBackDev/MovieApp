import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { MovieWithActors } from '../models/movie-models/movie-with-actors';
import { environment } from 'src/environments/environment';
import { Movie } from '../models/movie-models/movie';
import { Actor } from '../models/actor-models/actor';
import { Director } from '../models/director-models/director';
import { tokenGetter } from '../app.module';
import { DirectorForCreate } from '../models/director-models/director-for-create';
import { DirectorForUpdate } from '../models/director-models/director-for-update';

@Injectable({
  providedIn: 'root'
})
export class DirectorService {
  url = `${environment.apiUrl}/directors`

  constructor(private http: HttpClient) { }

  public getDirectors(params: string) : Observable<HttpResponse<Director[]>>{
    return this.http.get<Director[]>(`${this.url}?${params}`, {headers: this.generateHeaders(), observe: 'response'})
  }

  public getDirector(id: string) : Observable<Director>{
    return this.http.get<Director>(`${this.url}/${id}`, {headers: this.generateHeaders()})
  }

  public createDirector(director: DirectorForCreate){
    return this.http.post(this.url, director, {headers: this.generateHeaders(), observe: 'response'})
  }

  public updateDirector(id: string, director: DirectorForUpdate){
    return this.http.put<Director>(`${this.url}/${id}`, director, {headers: this.generateHeaders()})
  }

  public deleteDirector(id: string){
    return this.http.delete(`${this.url}/${id}`, {headers: this.generateHeaders()})
  }

  private generateHeaders = () => {
    return new HttpHeaders({ 
    'Content-Type': 'application/json',
    'Accept': 'application/json',
    'Authorization': `Bearer ${tokenGetter()}`
  })
  }
}
