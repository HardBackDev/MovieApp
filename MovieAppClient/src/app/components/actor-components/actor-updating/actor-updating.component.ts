import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ActorForUpdate } from 'src/app/models/actor-models/actor-for-update';
import { ActorService } from 'src/app/services/actor.service';

@Component({
  selector: 'app-actor-updating',
  templateUrl: './actor-updating.component.html',
  styleUrls: ['./actor-updating.component.css']
})
export class ActorUpdatingComponent {
  route: ActivatedRoute = inject(ActivatedRoute);
  actorForm: FormGroup;
  actorId: string

  constructor(private actorService: ActorService, private router: Router) { }

  ngOnInit(): void {
    this.actorId = this.route.snapshot.params['id']

    this.actorForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      bio: new FormControl('', [Validators.required]),
      photoUrl: new FormControl('', [Validators.required])
    });

    this.actorService.getActor(this.actorId)
    .subscribe(res => {
      this.actorForm.patchValue(res)
    })
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

  updateActor = (actorForm) => {
    if (this.actorForm.valid)
      this.executeActorUpdate(actorForm);
  }

  private executeActorUpdate = (actorForm) => {
      const actor: ActorForUpdate = {
        name: actorForm.name,
        bio: actorForm.bio,
        photoUrl: actorForm.photoUrl
      }

      this.actorService.updateActor(this.actorId, actor)
      .subscribe((r : any) => {
        this.router.navigate(['/actors', this.actorId]);
      });
  }

  redirectToActors = () => {
    this.router.navigate(['/actors']);
  }
}
