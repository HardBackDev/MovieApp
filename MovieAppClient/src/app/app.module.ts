import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { MovieListComponent } from './components/movie-components/movie-list/movie-list.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
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
import { DirectorCreationComponent } from './components/director-components/director-creation/director-creation.component';
import { DirectorUpdatingComponent } from './components/director-components/director-updating/director-updating.component';
import { MoviesToWatchComponent } from './components/movie-components/movies-to-watch/movies-to-watch.component';
import { AddingActorComponent } from './components/movie-components/adding-actor/adding-actor.component';

export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
}

@NgModule({
  declarations: [
    AppComponent,
    MovieListComponent,
    MovieDetailsComponent,
    UserRegistrationComponent,
    UserLoginComponent,
    MovieCreationComponent,
    MovieUpdatingComponent,
    ActorListComponent,
    ActorDetailsComponent,
    ActorCreationComponent,
    ActorUpdatingComponent,
    DirectorListComponent,
    DirectorDetailsComponent,
    DirectorCreationComponent,
    DirectorUpdatingComponent,
    MoviesToWatchComponent,
    AddingActorComponent,
  ],
  imports: [
    MatSlideToggleModule,
    HttpClientModule,
    BrowserModule,
    ModalModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatRadioModule,
    MatCardModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
