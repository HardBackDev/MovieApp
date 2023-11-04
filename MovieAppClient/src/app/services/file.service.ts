import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { tokenGetter } from '../app.module';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  url = `${environment.apiUrl}/video`

  constructor(private http: HttpClient) { }

  public uploadVideo = (formData) => {
    return this.http.post(`${this.url}/upload`, formData, {reportProgress: true, observe: 'events', 
    headers: new HttpHeaders({'Authorization': `Bearer ${tokenGetter()}`})})
  }


}
