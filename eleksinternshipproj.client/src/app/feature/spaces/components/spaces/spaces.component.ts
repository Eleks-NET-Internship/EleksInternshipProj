import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { SpacesService } from '../../services/spaces.service';
import { SpaceDto } from '../../models/spaces-models';

@Component({
  selector: 'app-spaces',
  templateUrl: './spaces.component.html',
  styleUrls: ['./spaces.component.css'] // або .scss
})
export class SpacesComponent implements OnInit {
  spaces: SpaceDto[] = [];
  showAddMenu: boolean = false;

  @ViewChild('addMenuWrapper') addMenuWrapper!: ElementRef;

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

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (this.showAddMenu && this.addMenuWrapper && !this.addMenuWrapper.nativeElement.contains(target)) {
      this.showAddMenu = false;
    }
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
    const newName = prompt('Нова назва:', space.name);
    if (newName && newName.trim()) {
      space.name = newName.trim();
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
