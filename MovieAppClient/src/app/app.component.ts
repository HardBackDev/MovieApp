import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, Renderer2, inject } from '@angular/core';
import { Observable, map, shareReplay } from 'rxjs';
import { AuthenticationService } from './services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MovieAppClient';
  private breakpointObserver = inject(BreakpointObserver);

  constructor(private renderer: Renderer2, public authService: AuthenticationService){
  }

  ngOnInit(){
    const bodyElement = document.body;
    this.renderer.setStyle(bodyElement, 'overflow-x', 'hidden');
  }

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
  .pipe(
    map(result => result.matches),
    shareReplay()
  );


  logOut(){
    localStorage.removeItem('jwt')
    localStorage.removeItem('refresh')

  }
}
