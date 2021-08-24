import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { ParseUtil } from 'src/app/core/utils';
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
  bidAmount: number = 0;
  errorMsg:string|undefined;
  item$!:Observable<Item>;
  bidsHistory$!:Observable<BidHistory[]>;
  isAutoBidEnabled$:BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor(
    private itemService:ItemService,
    private bidHistoryService:BidsHistoryService,
    route:ActivatedRoute) {
    const itemId =  route.snapshot.params.id!;
    this.getDetailAndHistory(itemId)
    this.getAutoBidEnable(itemId);
    this.itemId = itemId;
  }
  ngOnInit(): void {
    window.scroll(0, 0);
  }
  getAutoBidEnable(itemId:string){
    this.itemService.isAutoBidEnabled(itemId).subscribe(this.isAutoBidEnabled$);
  }
  getDetailAndHistory(itemId:string){
    this.item$ = this.itemService.getItem(itemId);
    this.bidsHistory$ = this.bidHistoryService.getBidsHistory(itemId);
  }
  async enableAutoBid(){
    try {
      this.errorMsg = undefined;
      await this.itemService.createOrUpdateAutoBidding(this.itemId);
      this.getAutoBidEnable(this.itemId);
    } catch (error) {
      this.errorMsg = ParseUtil.error(error)?.description;
      
    }
  }
  async bidNow(){
    try {
      this.errorMsg = undefined;
      await this.itemService.placeBid(this.itemId,this.bidAmount);
      this.bidAmount = 0;
      this.getDetailAndHistory(this.itemId);
    } catch (error) {
      this.errorMsg = ParseUtil.error(error)?.description;
    }
  }
}
