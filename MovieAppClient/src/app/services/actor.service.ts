import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { MovieWithActors } from '../models/movie-models/movie-with-actors';
import { environment } from 'src/environments/environment';
import { Movie } from '../models/movie-models/movie';
import { Actor } from '../models/actor-models/actor';
import { tokenGetter } from '../app.module';
import { ActorForCreation } from '../models/actor-models/actor-for-creation';
import { ActorForUpdate } from '../models/actor-models/actor-for-update';

@Injectable({
  providedIn: 'root'
})
export class ActorService {
  url = `${environment.apiUrl}/actors`

  constructor(private http: HttpClient) { }

  public getActors(params: string) : Observable<HttpResponse<Actor[]>>{
    return this.http.get<Actor[]>(`${this.url}?${params}`, {headers: this.generateHeaders(), observe: 'response'})
  }

  public getActor(id: string) : Observable<Actor>{
    return this.http.get<Actor>(`${this.url}/${id}`, {headers: this.generateHeaders()})
  }

  public getActorsByMovie(movieId: string, params: string) : Observable<HttpResponse<Actor[]>>{
    return this.http.get<Actor[]>(`${this.url}/byMovie/${movieId}?${params}`, {headers: this.generateHeaders(), observe: 'response'})
  }

  public createActor(actor: ActorForCreation){
    return this.http.post(this.url, actor, {headers: this.generateHeaders(), observe: 'response'})
  }

  public updateActor(id: string, actor: ActorForUpdate){
    return this.http.put(`${this.url}/${id}`, actor, {headers: this.generateHeaders()})
  }

  public deleteActor(id: string){
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
