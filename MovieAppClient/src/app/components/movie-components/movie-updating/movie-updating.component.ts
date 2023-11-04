import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Director } from 'src/app/models/director-models/director';
import { Metadata } from 'src/app/models/metadata';
import { Movie } from 'src/app/models/movie-models/movie';
import { MovieForCreation } from 'src/app/models/movie-models/movie-for-creation';
import { MovieForUpdate } from 'src/app/models/movie-models/movie-for-update';
import { DirectorService } from 'src/app/services/director.service';
import { FileService } from 'src/app/services/file.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-movie-updating',
  templateUrl: './movie-updating.component.html',
  styleUrls: ['./movie-updating.component.css']
})
export class MovieUpdatingComponent {
  route: ActivatedRoute = inject(ActivatedRoute);
  movieForm: FormGroup;
  selectedFile: File;
  fileData: FormData;
  fileChoosed: boolean = false;
  filePath: string
  movieId: string
  updatingMovie: Movie
  metadata: Metadata
  directors: Director[]
  isChoosingDirector: boolean = false

  constructor(private movieService: MovieService, private router: Router, private fileService: FileService,
    private directorService: DirectorService) { }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
    this.selectedFile.arrayBuffer();
  }

  ngOnInit(): void {
    this.movieId = this.route.snapshot.params['id'];
    this.movieService.getMovie(this.movieId)
    .subscribe((res: Movie) => {
      this.updatingMovie = res
      this.movieForm = new FormGroup({
        title: new FormControl(this.updatingMovie.title, [Validators.required]),
        description: new FormControl(this.updatingMovie.description, [Validators.required]),
        genres: new FormControl(this.updatingMovie.genres.join(' '), [Validators.required]),
        photoUrl: new FormControl(this.updatingMovie.photoUrl, [Validators.required]),
        directorId: new FormControl(this.updatingMovie.directorId, [Validators.required]),
        releaseDate: new FormControl(this.updatingMovie.releaseDate.toString().substring(0,10), [Validators.required])
      });
    })
  }

  validateControl = (controlName: string) => {
    if (this.movieForm.get(controlName).invalid && this.movieForm.get(controlName).touched)
      return true;
    
    return false;
  } 

  hasError = (controlName: string, errorName: string) => {
    if (this.movieForm.get(controlName).hasError(errorName))
      return true;
    
    return false;
  }

  updateMovie = (movieForm) => {
    if (this.movieForm.valid)
      this.executeMovieUpdating(movieForm);
  }

  private executeMovieUpdating = (movieForm) => {
    const genres = movieForm.genres as string
      const movie: MovieForUpdate = {
        title: movieForm.title,
        directorId: movieForm.directorId,
        description: movieForm.description,
        photoUrl: movieForm.photoUrl,
        genres: genres.split(' '),
        releaseDate: movieForm.releaseDate,
        videoPath: this.filePath ?? this.updatingMovie.videoPath ?? ''
      }

      this.movieService.updateMovie(this.movieId, movie)
      .subscribe((r : any) => {
        this.router.navigate(['movies', this.movieId]);
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
  }

  uploadFile = (files) => {
    this.isUploading = true
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const fileData = new FormData();
    fileData.append('file', fileToUpload, fileToUpload.name);
    
    this.fileService.uploadVideo(fileData)
          .subscribe({
            next: (event: any) => {
            if (event.type === HttpEventType.UploadProgress){
              this.progress = Math.round(100 * event.loaded / event.total);
            }
            else if (event.type === HttpEventType.Response) {
              this.message = 'Upload success.';
              this.isUploading = false;
              this.filePath = event.body.path;
            }
          },
          error: (err: HttpErrorResponse) => console.log(err)
        });
  }
  
  chooseDirector(searchName: string = '', e){
    e.preventDefault()
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
    console.log(this.movieForm)
  }
}
