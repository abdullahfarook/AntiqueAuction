import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { BidHistory } from 'src/generated/services';

@Component({
  selector: 'app-bids-history',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './bids-history.component.html',
  styleUrls: ['./bids-history.component.scss']
})
export class BidsHistoryComponent implements OnInit {

  @Input()
  bidsHistory:BidHistory[]|null = []
  constructor() { }

  ngOnInit(): void {
  }

}
