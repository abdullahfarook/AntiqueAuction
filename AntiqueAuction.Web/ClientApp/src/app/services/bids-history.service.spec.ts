import { TestBed } from '@angular/core/testing';

import { BidsHistoryService } from './bids-history.service';

describe('BidsHistoryService', () => {
  let service: BidsHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BidsHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
