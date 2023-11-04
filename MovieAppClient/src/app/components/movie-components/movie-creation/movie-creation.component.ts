import { HttpEventType, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Director } from 'src/app/models/director-models/director';
import { Metadata } from 'src/app/models/metadata';
import { MovieForCreation } from 'src/app/models/movie-models/movie-for-creation';
import { DirectorService } from 'src/app/services/director.service';
import { FileService } from 'src/app/services/file.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-movie-creation',
  templateUrl: './movie-creation.component.html',
  styleUrls: ['./movie-creation.component.css']
})
export class MovieCreationComponent {
  movieForm: FormGroup;
  selectedFile: File;
  fileData: FormData;
  fileChoosed: boolean = false;
  filePath: string
  directors: Director[]
  metadata: Metadata
  isChoosingDirector: boolean = false;
  directorChoosed: boolean = false;

  constructor(private movieService: MovieService, private router: Router, private fileService: FileService, private directorService: DirectorService) { }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
    this.selectedFile.arrayBuffer();
  }

  ngOnInit(): void {
    this.movieForm = new FormGroup({
      title: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      genres: new FormControl('', [Validators.required]),
      photoUrl: new FormControl('', [Validators.required]),
      directorId: new FormControl('', [Validators.required]),
      releaseDate: new FormControl('', [Validators.required])
    });
  }

  validateControl = (controlName: string) => {
    if (this.movieForm.get(controlName).invalid && this.movieForm.get(controlName).touched){
      return true;
    }
    return false;
  } 

  hasError = (controlName: string, errorName: string) => {
    if (this.movieForm.get(controlName).hasError(errorName))
      return true;
    
    return false;
  }

  createMovie = (movieForm) => {
    if (this.movieForm.valid)
      this.executeMovieCreation(movieForm);
  }

  private executeMovieCreation = (movieForm) => {
    const genres = movieForm.genres as string
      const movie: MovieForCreation = {
        title: movieForm.title,
        directorId: movieForm.directorId,
        description: movieForm.description,
        photoUrl: movieForm.photoUrl,
        genres: genres.split(' '),
        releaseDate: movieForm.releaseDate,
        videoPath: this.filePath
      }

      this.movieService.createMovie(movie)
      .subscribe((r : any) => {
        const location = r.headers.get('Location') as string;
        const createdId = location.substring(location.lastIndexOf('/') + 1);
        this.router.navigate(['movies', createdId]);
      });
  }

  redirectToMovies = () => {
    this.router.navigate(['']);
  }

  progress: number;
  message: string;
  isUploading: boolean = false;

  uploadFinished = (event) => { 
    this.filePath = event.body.path;
    console.log(this.filePath) 
  }

  uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const fileData = new FormData();
    fileData.append('file', fileToUpload, fileToUpload.name);
    console.log(fileData)
    
    this.fileService.uploadVideo(fileData)
          .subscribe({
            next: (event: any) => {
            if (event.type === HttpEventType.UploadProgress){
              this.progress = Math.round(100 * event.loaded / event.total);
              this.isUploading = false;
            }
            else if (event.type === HttpEventType.Response) {
              this.message = 'Upload success.';
              this.isUploading = true;
              this.filePath = event.body.path;
            }
          },
          error: (err: HttpErrorResponse) => console.log(err)
        });
  }

  chooseDirector(searchName: string = ''){
    this.directorService.getDirectors(`pageNumber=1&searchedName=${searchName}`)
    .subscribe(r => {
      this.directors = r.body
      this.metadata = JSON.parse(r.headers.get('X-Pagination'))
      this.isChoosingDirector = true
    })
  }

  passDirectorId(id: string){
    let directorInput = document.getElementById('directorId') as HTMLInputElement
    directorInput.value = id
    this.isChoosingDirector = false
    let controll = this.movieForm.get('directorId')
    controll.setValue(id)
  }

  unloadNotification($event: any): void {
    console.log('ads')
  }
}
