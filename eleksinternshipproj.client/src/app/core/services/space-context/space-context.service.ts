import { Injectable } from '@angular/core';
import { SpaceDto } from '../../../feature/spaces/models/spaces-models';
import { BehaviorSubject, Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpaceContextService {
  private readonly SPACE_KEY = "selectedSpace";

  private spaceSubject = new BehaviorSubject<SpaceDto | null>(this.getSpaceFromStorage());
  space$ = this.spaceSubject.asObservable();

  spaceId$: Observable<number | null> = this.space$.pipe(
    map((space: SpaceDto | null) => space ? space.id : null)
  );

  spaceName$: Observable<string | null> = this.space$.pipe(
    map((space: SpaceDto | null) => space ? space.name : null)
  )

  storeSpaceContext(space: SpaceDto): void {
    sessionStorage.setItem(this.SPACE_KEY, JSON.stringify(space));
    this.spaceSubject.next(space);
  }

  clearSpaceContext(): void {
    sessionStorage.removeItem(this.SPACE_KEY);
    this.spaceSubject.next(null);
  }

  getSpaceId(): number | null {
    return this.spaceSubject.getValue()?.id ?? null;
  }

  getSpaceName(): string | null {
    return this.spaceSubject.getValue()?.name ?? null;
  }

  private getSpaceFromStorage(): SpaceDto | null {
    const spaceStr = sessionStorage.getItem(this.SPACE_KEY);
    return spaceStr ? JSON.parse(spaceStr) : null;
  }
}
