import { TestBed } from '@angular/core/testing';

import { TokenActionsService } from './token-actions.service';

describe('TokenActionsService', () => {
  let service: TokenActionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TokenActionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
