import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DirectorForCreate } from 'src/app/models/director-models/director-for-create';
import { DirectorService } from 'src/app/services/director.service';

@Component({
  selector: 'app-director-creation',
  templateUrl: './director-creation.component.html',
  styleUrls: ['./director-creation.component.css']
})
export class DirectorCreationComponent {
  directorForm: FormGroup;

  constructor(private directorService: DirectorService, private router: Router) { }

  ngOnInit(): void {
    this.directorForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      bio: new FormControl('', [Validators.required]),
      age: new FormControl(0, [Validators.required]),
      photoUrl: new FormControl('', [Validators.required])
    });
  }

  validateControl = (controlName: string) => {
    if (this.directorForm.get(controlName).invalid && this.directorForm.get(controlName).touched)
      return true;
    
    return false;
  } 

  hasError = (controlName: string, errorName: string) => {
    if (this.directorForm.get(controlName).hasError(errorName))
      return true;
    
    return false;
  }

  createDirector = (directorForm) => {
    if (this.directorForm.valid)
      this.executeDirectorCreation(directorForm);
  }

  private executeDirectorCreation = (directorForm) => {
      const director: DirectorForCreate = {
        name: directorForm.name,
        bio: directorForm.bio,
        age: directorForm.age,
        photoUrl: directorForm.photoUrl
      }

      this.directorService.createDirector(director)
      .subscribe((r : any) => {
        const location = r.headers.get('Location') as string;
        const createdId = location.substring(location.lastIndexOf('/') + 1);
        this.router.navigate(['directors', createdId]);
      });
  }

  redirectToDirectors = () => {
    this.router.navigate(['directors']);
  }
}
