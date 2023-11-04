import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserRegister } from 'src/app/models/user_models/user-register';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent {
  userRegisterForm: FormGroup;
  errorMessage: any;

  get jsonItems(): { key: string; value: any }[] {
    return Object.entries(this.errorMessage || {}).map(([key, value]) => ({ key, value }));
  }


  constructor(private repository: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
    this.userRegisterForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      phoneNumber: new FormControl('0', [Validators.required]),
      password: new FormControl('', [Validators.required])
    });
    
  }

  validateControl = (controlName: string) => {
    if (this.userRegisterForm.get(controlName).invalid && this.userRegisterForm.get(controlName).touched)
      return true;
    
    return false;
  } 

  hasError = (controlName: string, errorName: string) => {
    if (this.userRegisterForm.get(controlName).hasError(errorName))
      return true;
    
    return false;
  }

  registerUser = (userForm) => {
    if (this.userRegisterForm.valid)
      this.executeUserRegistration(userForm);
  }

  private executeUserRegistration = (userForm) => {
    const user: UserRegister = {
      userName: userForm.userName,
      password: userForm.password,
      email: userForm.email,
      phoneNumber: userForm.phoneNumber
    }
    this.repository.registerUser(user)
    .subscribe(
      {
        next: () => {
          this.router.navigate(['/login'])
        },
        error: (err: HttpErrorResponse) => {
            this.errorMessage = err.error;
        }
      })
  }

  redirectToMovies = () => {
    this.router.navigate(['']);
  }
}
