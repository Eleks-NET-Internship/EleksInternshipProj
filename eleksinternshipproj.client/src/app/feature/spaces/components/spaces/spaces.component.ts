import { Component } from '@angular/core';
import { SpacesService } from '../../services/spaces.service';

@Component({
  selector: 'app-spaces',
  templateUrl: './spaces.component.html',
  styleUrl: './spaces.component.css'
})
export class SpacesComponent {
  constructor(private spacesService: SpacesService) { }
}
