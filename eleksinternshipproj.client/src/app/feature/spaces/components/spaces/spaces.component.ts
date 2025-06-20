import { Component, OnInit } from '@angular/core';
import { SpaceDto, UserSpaceDto } from '../../models/spaces-models';
import { SpacesService } from '../../services/spaces.service';

@Component({
  selector: 'app-spaces',
  templateUrl: './spaces.component.html',
  styleUrls: ['./spaces.component.css']
})
export class SpacesComponent implements OnInit {
  spaces: SpaceDto[] = [];
  createFormVisible = false;
  renameFormId: number | null = null;
  addUserFormId: number | null = null;
  newSpaceName: string = '';
  newUserEmail: string = '';

  deleteConfirmDialog = false;
  spaceToDelete: SpaceDto | null = null;

  constructor(private spacesService: SpacesService) {}

  ngOnInit(): void {
    this.getSpaces();
  }

  getSpaces(): void {
    this.spacesService.getSpaces().subscribe({
      next: (res) => this.spaces = Array.isArray(res) ? res : [],
      error: (err) => console.error('Помилка завантаження просторів', err)
    });
  }

  onCreateSpace(): void {
    if (this.newSpaceName.trim()) {
      this.spacesService.createSpace(this.newSpaceName.trim()).subscribe({
        next: (newSpace) => {
          this.spaces.push(newSpace);
          this.newSpaceName = '';
          this.createFormVisible = false;
        },
        error: (err) => console.error('Не вдалося створити простір', err)
      });
    }
  }

  onRenameSpace(space: SpaceDto): void {
    if (space.name.trim()) {
      this.spacesService.renameSpace(space.id, space.name).subscribe({
        next: (updated) => {
          space.name = updated.name;
          this.renameFormId = null;
        },
        error: (err) => console.error('Не вдалося перейменувати', err)
      });
    }
  }

  addUserToSpace(spaceId: number): void {
    const username = this.newUserEmail.trim();
    if (username && username.trim()) {
      this.spacesService.addUserToSpace(spaceId, username.trim()).subscribe({
        next: (updatedUserSpace: UserSpaceDto) => {
          const space = this.spaces.find(s => s.id === spaceId);
          if (space) {
            if (!space.userSpaces) space.userSpaces = [];
            space.userSpaces.push(updatedUserSpace);
          }
          this.newUserEmail = '';
          this.addUserFormId = null;
        },
        error: (err) => {
          console.error('Не вдалося додати користувача до простору', err);
        }
      });
    }
  }

  confirmDelete(space: SpaceDto): void {
    this.spaceToDelete = space;
    this.deleteConfirmDialog = true;
  }

  deleteSpaceConfirmed(): void {
    if (!this.spaceToDelete) return;
    this.spacesService.deleteSpace(this.spaceToDelete.id).subscribe({
      next: () => {
        this.spaces = this.spaces.filter(s => s.id !== this.spaceToDelete?.id);
        this.deleteConfirmDialog = false;
        this.spaceToDelete = null;
      },
      error: (err) => console.error('Не вдалося видалити', err)
    });
  }

  selectSpace(space: SpaceDto): void {
    sessionStorage.setItem('selectedSpace', JSON.stringify(space));
  }

  trackBySpaceId(index: number, space: SpaceDto): number {
    return space.id;
  }
}
