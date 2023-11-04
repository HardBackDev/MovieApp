import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { MovieWithActors } from '../models/movie-models/movie-with-actors';
import { environment } from 'src/environments/environment';
import { Movie } from '../models/movie-models/movie';
import { Actor } from '../models/actor-models/actor';
import { tokenGetter } from '../app.module';
import { Comment } from 'src/app/models/comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  url = `${environment.apiUrl}/comments`

  constructor(private http: HttpClient) { }

  public getComments(movieId: string, params: string) : Observable<HttpResponse<Comment[]>>{
    return this.http.get<Comment[]>(`${this.url}/${movieId}?${params}`, {headers: this.generateHeaders(), observe: 'response'})
  }

  public addComment(movieId: string, commentText: string){
    return this.http.post(`${this.url}/${movieId}`, commentText, {headers: this.generateHeaders(), observe: 'response'})
  }

  public editComment(commentId: string, editText: string){
    return this.http.put(`${this.url}/${commentId}`, editText, {headers: this.generateHeaders(), observe: 'response'})
  }

  public deleteComment(commentId: string){
    return this.http.delete(`${this.url}/${commentId}`, {headers: this.generateHeaders(), observe: 'response'})

  }

  private generateHeaders = () => {
    return new HttpHeaders({ 
    'Content-Type': 'application/json',
    'Accept': 'application/json',
    'Authorization': `Bearer ${tokenGetter()}`
    })
  }
}
