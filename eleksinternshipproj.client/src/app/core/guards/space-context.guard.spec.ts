import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { spaceContextGuard } from './space-context.guard';

describe('spaceContextGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => spaceContextGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
