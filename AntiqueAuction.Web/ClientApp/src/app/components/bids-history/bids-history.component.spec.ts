import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BidsHistoryComponent } from './bids-history.component';

describe('BidsHistoryComponent', () => {
  let component: BidsHistoryComponent;
  let fixture: ComponentFixture<BidsHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BidsHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BidsHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
