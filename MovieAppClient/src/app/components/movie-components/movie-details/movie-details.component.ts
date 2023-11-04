import { Component, inject } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Actor } from 'src/app/models/actor-models/actor';
import { Director } from 'src/app/models/director-models/director';
import { Metadata } from 'src/app/models/metadata';
import { Movie } from 'src/app/models/movie-models/movie';
import { ActorService } from 'src/app/services/actor.service';
import { CommentService } from 'src/app/services/comment.service';
import { DirectorService } from 'src/app/services/director.service';
import { MovieService } from 'src/app/services/movie.service';
import { environment } from 'src/environments/environment';
import { Comment } from 'src/app/models/comment';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { MovieRate } from 'src/app/models/movie-rate';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent {
  movieId: string
  route: ActivatedRoute = inject(ActivatedRoute);
  movie: Movie
  actors: Actor[]
  comments: Comment[]
  director: Director = null
  actorsMetadata: Metadata
  commentsMetadata: Metadata
  videoURL: string;
  movieLoading: boolean = true
  commentsLoading: boolean = true
  actorsLoading: boolean = true
  editingCommentId: string = null;
  movieRate: MovieRate
  movieInWatching: boolean = false

  constructor(private movieService: MovieService, private actorService: ActorService, private commentService: CommentService,
    private directorService: DirectorService, public auth: AuthenticationService){
  }

  ngOnInit(){
    this.movieId = this.route.snapshot.params['id'];
    document.onscroll = (e) => {
      const scrollY = window.scrollY + document.body.clientHeight;
      const height = document.body.scrollHeight;
  
      if (this.commentsMetadata && !this.commentsLoading && this.commentsMetadata.CurrentPage < this.commentsMetadata.TotalPages  && scrollY >= height - 10) {
        this.getCommentsByParameters(this.commentsMetadata.CurrentPage + 1, true)
      }
    }

    this.getMovie()
    this.getActorsByParameters(1,false)
    this.getCommentsByParameters(1)
  }

  getMovie(onlyMovie: boolean = false){
    this.movieLoading = true
    this.movieService.getMovie(this.movieId)
    .subscribe(res => {
      this.movie = res
      this.movie.genres = this.movie.genres.map(genre => ' ' + genre)
      
      if(this.movie.videoPath != null)
        this.videoURL = `${environment.apiUrl}/video/play?filePath=${this.movie.videoPath}`;
      else
        this.videoURL = null

      if(this.auth.isUserAuthenticated()){
        this.checkMovieInWatching()
        this.movieService.getMovieRate(this.movieId)
        .subscribe(r => this.movieRate = r)
      }
      if(onlyMovie)
        return

      this.directorService.getDirector(this.movie.directorId)
        .subscribe(res => this.director = res)
  
      this.movieLoading = false
    })

  }

  getActorsByParameters(pageNumber: number, concat: boolean = false){
    this.actorsLoading = true
    this.actorService.getActorsByMovie(this.movieId, `pageSize=6&pageNumber=${pageNumber}`)
    .subscribe(res => {
      this.actorsMetadata = JSON.parse(res.headers.get('X-Pagination'));
      if(concat)
        this.actors = this.actors.concat(res.body)
      else
        this.actors = res.body
      this.actorsLoading = false
    })

  }

  getCommentsByParameters(pageNumber: number, concat: boolean = false){
    this.commentsLoading = true
    this.commentService.getComments(this.movieId, `pageSize=6&pageNumber=${pageNumber}&orderBy=DateAdded`)
    .subscribe(res => {
      this.commentsMetadata = JSON.parse(res.headers.get('X-Pagination'))
      if(concat){
        this.comments = this.comments.concat(res.body)
      }else{
        this.comments = res.body
      }
      this.commentsLoading = false
    })
  }
  
  actorsScroll(e: Event) {
    const element = e.target as HTMLElement
    const scrollWidth = element.scrollWidth
    const scrollX = element.scrollLeft + element.clientWidth

    if (this.actorsMetadata.CurrentPage < this.actorsMetadata.TotalPages && !this.actorsLoading && scrollX === scrollWidth) {
      this.getActorsByParameters(this.actorsMetadata.CurrentPage + 1, true)
    }

  }

  addComment(e) {
    e.preventDefault()
    const commentInput = document.getElementById('comment-input') as HTMLInputElement
    if(commentInput.value == null)
      return

    this.commentService.addComment(this.movieId, commentInput.value)
    .subscribe({
      next: (r) => {
        this.getCommentsByParameters(1)
        commentInput.value = ''
      },
      error: (e) => console.log(e)
    })
  }

  editComment(e, commentId: string, editText: string){
    e.preventDefault()
    
    this.editingCommentId = commentId
    this.commentService.editComment(commentId, editText)
    .subscribe({
      next: (r) => {
        this.getCommentsByParameters(1)
        this.editingCommentId = null
      },
      error: (e) => {
        console.log(e)
        this.editingCommentId = null
      }
    })
  }

  deleteComment(e, commentId: string){
    e.preventDefault()

    this.commentService.deleteComment(commentId)
    .subscribe({
      next: (r) => {
        (document.getElementById('comment-input') as HTMLInputElement).value = ''
        this.getCommentsByParameters(1)
      },
      error: (e) => console.log(e)
    })
  }

  likeMovie(){
    if(this.movieRate != null && this.movieRate.rate.toLowerCase() == 'dislike'){
      this.movieService.unRateMovie(this.movieId)
        .subscribe(r => {
          this.movieService.rateMovie(this.movieId, 'like')
          .subscribe(r => this.getMovie(true))
        })
    }else{
      this.movieService.rateMovie(this.movieId, 'like')
        .subscribe(r => this.getMovie(true))
    }
  }

  disLikeMovie(){
    if(this.movieRate != null && this.movieRate.rate.toLowerCase() == 'like'){
      this.movieService.unRateMovie(this.movieId)
      .subscribe(r => {
        this.movieService.rateMovie(this.movieId, 'dislike')
        .subscribe(r => this.getMovie(true))
      })
    }else{
      this.movieService.rateMovie(this.movieId, 'dislike')
      .subscribe(r => this.getMovie(true))

    }
  }

  unRateMovie(){
    this.movieService.unRateMovie(this.movieId)
    .subscribe(r => this.getMovie(true))
  }

  addToWatching(){
    this.movieService.addMovieToWatching(this.movieId)
    .subscribe(r => this.checkMovieInWatching())
  }

  deleteFromWatching(){
    this.movieService.deleteMovieFromWatching(this.movieId)
    .subscribe(r => this.checkMovieInWatching())
  }

  checkMovieInWatching(){
    this.movieService.checkInWatching(this.movieId)
    .subscribe(r => this.movieInWatching = r)
  }

  checkCommentLessThan500(e){
    const input = e.target as HTMLInputElement
    if(input.value && input.value.length > 500)
      input.value = input.value.substring(0, 500)
  }
}

