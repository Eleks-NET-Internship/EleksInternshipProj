import { Injectable } from '@angular/core';
import { SpaceDto } from '../../../feature/spaces/models/spaces-models';

@Injectable({
  providedIn: 'root'
})
export class SpaceContextService {

  constructor() { }

  storeSpaceContext(space: SpaceDto): void {
    sessionStorage.setItem('selectedSpace', JSON.stringify(space));
  }

  clearSpaceContext(): void {
    sessionStorage.removeItem('selectedSpace');
  }

  getSpaceId(): number | null {
    const space = this.getSpace()
    if (!space)
      return null;
    return space.id;
  }

  getSpaceName(): string| null {
    const space = this.getSpace()
    if (!space)
      return null;
    return space.name;
  }

  private getSpace(): SpaceDto | null {
    const spaceStr = sessionStorage.getItem('selectedSpace');
    if (!spaceStr)
      return null;
    return JSON.parse(spaceStr);
  }
}
