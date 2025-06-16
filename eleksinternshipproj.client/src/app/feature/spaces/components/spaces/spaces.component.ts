import { Component, OnInit } from '@angular/core';
import { SpacesService } from '../../services/spaces.service';
import { SpaceDto } from '../../models/spaces-models';

@Component({
  selector: 'app-spaces',
  templateUrl: './spaces.component.html',
  styleUrls: ['./spaces.component.scss']
})
export class SpacesComponent implements OnInit {
  spaces: SpaceDto[] = [];
  showAddMenu: boolean = false;

  constructor(private spacesService: SpacesService) {}

  ngOnInit(): void {
    this.getSpaces();
  }

  getSpaces(): void {
    this.spacesService.getSpaces().subscribe({
      next: (response) => {
        this.spaces = response;
      },
      error: (err) => {
        console.error('Помилка завантаження просторів', err);
      }
    });
  }

  toggleAddMenu(): void {
    this.showAddMenu = !this.showAddMenu;
  }

  createSpace(): void {
    const name = prompt('Введіть назву простору:');
    if (name && name.trim()) {
      this.spacesService.createSpace(name.trim()).subscribe({
        next: (newSpace) => {
          this.spaces.push(newSpace);
        },
        error: (err) => {
          console.error('Не вдалося створити простір', err);
        }
      });
    }
    this.showAddMenu = false;
  }

  renameSpace(space: SpaceDto): void {
    if (space.name && space.name.trim()) {
      this.spacesService.renameSpace(space).subscribe({
        next: (updatedSpace) => {
          space.name = updatedSpace.name;
        },
        error: (err) => {
          console.error('Не вдалося перейменувати простір', err);
        }
      });
    }
  }

  deleteSpace(id: number): void {
    const confirmed = confirm('Ви впевнені, що хочете видалити цей простір?');
    if (confirmed) {
      this.spacesService.deleteSpace(id).subscribe({
        next: () => {
          this.spaces = this.spaces.filter(space => space.id !== id);
        },
        error: (err) => {
          console.error('Не вдалося видалити простір', err);
        }
      });
    }
  }
}
