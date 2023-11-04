import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ActorForCreation } from 'src/app/models/actor-models/actor-for-creation';
import { ActorService } from 'src/app/services/actor.service';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-actor-creation',
  templateUrl: './actor-creation.component.html',
  styleUrls: ['./actor-creation.component.css']
})
export class ActorCreationComponent {
  actorForm: FormGroup;

  constructor(private actorService: ActorService, private router: Router) { }

  ngOnInit(): void {
    this.actorForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      bio: new FormControl('', [Validators.required]),
      photoUrl: new FormControl('', [Validators.required])
    });
  }

  validateControl = (controlName: string) => {
    if (this.actorForm.get(controlName).invalid && this.actorForm.get(controlName).touched)
      return true;
    
    return false;
  } 

  hasError = (controlName: string, errorName: string) => {
    if (this.actorForm.get(controlName).hasError(errorName))
      return true;
    
    return false;
  }

  createActor = (actorForm) => {
    if (this.actorForm.valid)
      this.executeActorCreation(actorForm);
  }

  private executeActorCreation = (actorForm) => {
      const actor: ActorForCreation = {
        name: actorForm.name,
        bio: actorForm.bio,
        photoUrl: actorForm.photoUrl
      }

      this.actorService.createActor(actor)
      .subscribe((r : any) => {
        const location = r.headers.get('Location') as string;
        const createdId = location.substring(location.lastIndexOf('/') + 1);
        this.router.navigate(['actors', createdId]);
      });
  }

  redirectToActors = () => {
    this.router.navigate(['actors']);
  }
}
