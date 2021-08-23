import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, interval, Observable, ReplaySubject } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { BidsHistoryService } from 'src/app/services/bids-history.service';
import { ItemService } from 'src/app/services/item.service';
import { environment } from 'src/environments/environment';
import { BidHistory, Item } from 'src/generated/services';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
})
export class DetailComponent implements OnInit {
  api= environment.apiUrl;
  itemId:string;
  public bidNowToggle$: ReplaySubject<boolean> = new ReplaySubject<boolean>(1);
  public item$:Observable<Item>;
  public bidsHistory:Observable<BidHistory[]>;
  constructor(
    itemService:ItemService,
    bidHistoryService:BidsHistoryService,
    route:ActivatedRoute) {
    const itemId =  route.snapshot.params.id!;
    this.item$ = itemService.getItem(itemId);
    this.bidsHistory = bidHistoryService.getBidsHistory(itemId)
    this.itemId = itemId;
    this.bidNowToggle$.subscribe(x=>{
      console.log("toggle")
    })
  }


  ngOnInit(): void {
    window.scroll(0, 0);
  }
}
