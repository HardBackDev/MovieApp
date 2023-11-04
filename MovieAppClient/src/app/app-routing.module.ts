import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieListComponent } from './components/movie-components/movie-list/movie-list.component';
import { MovieDetailsComponent } from './components/movie-components/movie-details/movie-details.component';
import { UserRegistrationComponent } from './components/user-components/user-registration/user-registration.component';
import { UserLoginComponent } from './components/user-components/user-login/user-login.component';
import { MovieCreationComponent } from './components/movie-components/movie-creation/movie-creation.component';
import { MovieUpdatingComponent } from './components/movie-components/movie-updating/movie-updating.component';
import { ActorListComponent } from './components/actor-components/actor-list/actor-list.component';
import { ActorDetailsComponent } from './components/actor-components/actor-details/actor-details.component';
import { ActorCreationComponent } from './components/actor-components/actor-creation/actor-creation.component';
import { ActorUpdatingComponent } from './components/actor-components/actor-updating/actor-updating.component';
import { DirectorListComponent } from './components/director-components/director-list/director-list.component';
import { DirectorDetailsComponent } from './components/director-components/director-details/director-details.component';
import { MoviesToWatchComponent } from './components/movie-components/movies-to-watch/movies-to-watch.component';
import { DirectorCreationComponent } from './components/director-components/director-creation/director-creation.component';
import { DirectorUpdatingComponent } from './components/director-components/director-updating/director-updating.component';
import { AddingActorComponent } from './components/movie-components/adding-actor/adding-actor.component';
import { AdminGuard } from './guards/admin.guard';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {path:'', component: MovieListComponent},
  {path:'movies/:id', component: MovieDetailsComponent},
  {path:'auth', component: UserRegistrationComponent},
  {path:'login', component: UserLoginComponent},
  {path:'create/movie', component: MovieCreationComponent, canActivate: [AdminGuard]},
  {path:'update/movie/:id', component: MovieUpdatingComponent, canActivate: [AdminGuard]},
  {path:'actors', component: ActorListComponent},
  {path:'actors/:id', component: ActorDetailsComponent},
  {path:'create/actor', component: ActorCreationComponent, canActivate: [AdminGuard]},
  {path:'update/actor/:id', component: ActorUpdatingComponent, canActivate: [AdminGuard]},
  {path:'directors', component: DirectorListComponent},
  {path:'directors/:id', component: DirectorDetailsComponent},
  {path:'moviesToWatch', component: MoviesToWatchComponent, canActivate: [AuthGuard]},
  {path:'create/director', component: DirectorCreationComponent, canActivate: [AdminGuard]},
  {path:'update/director/:id', component: DirectorUpdatingComponent, canActivate: [AdminGuard]},
  {path:'movies/:id/addActor', component: AddingActorComponent, canActivate: [AdminGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
