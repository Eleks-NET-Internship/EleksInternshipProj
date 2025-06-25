import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { SpaceContextService } from '../services/space-context/space-context.service';

export const spaceContextGuard: CanActivateFn = (route, state) => {
  const spaceContextService = inject(SpaceContextService);
  const router = inject(Router);

  const id = spaceContextService.getSpaceId();
  const name = spaceContextService.getSpaceName();

  if (id && name) {
    return true;
  }

  return router.createUrlTree(['/spaces']);
};
