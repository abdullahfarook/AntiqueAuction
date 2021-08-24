import { TestBed } from '@angular/core/testing';

import { AutobidService } from './autobid.service';

describe('AutobidService', () => {
  let service: AutobidService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AutobidService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
