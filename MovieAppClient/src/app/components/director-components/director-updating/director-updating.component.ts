import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DirectorForUpdate } from 'src/app/models/director-models/director-for-update';
import { DirectorService } from 'src/app/services/director.service';

@Component({
  selector: 'app-director-updating',
  templateUrl: './director-updating.component.html',
  styleUrls: ['./director-updating.component.css']
})
export class DirectorUpdatingComponent {
  route: ActivatedRoute = inject(ActivatedRoute);

  directorForm: FormGroup;
  directorId: string

  constructor(private directorService: DirectorService, private router: Router) { }

  ngOnInit(): void {
    this.directorId = this.route.snapshot.params['id']

    this.directorForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      bio: new FormControl('', [Validators.required]),
      age: new FormControl(0, [Validators.required]),
      photoUrl: new FormControl('', [Validators.required])
    });

    this.directorService.getDirector(this.directorId)
    .subscribe(r => this.directorForm.patchValue(r))
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

  updateDirector = (directorForm) => {
    if (this.directorForm.valid)
      this.executeDirectorUpdating(directorForm);
  }

  private executeDirectorUpdating = (directorForm) => {
      const director: DirectorForUpdate = {
        name: directorForm.name,
        bio: directorForm.bio,
        age: directorForm.age,
        photoUrl: directorForm.photoUrl
      }

      this.directorService.updateDirector(this.directorId, director)
      .subscribe((r : any) => {
        this.router.navigate(['directors', this.directorId]);
      });
  }

  redirectToDirectors = () => {
    this.router.navigate(['directors']);
  }
}
