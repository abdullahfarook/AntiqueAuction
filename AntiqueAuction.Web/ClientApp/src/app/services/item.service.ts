import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Generated, AutoBid, AutomateBid, Item, PlaceBid } from 'src/generated/services';
import { PageResult } from '../core/models/page-result';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ItemService extends BaseService {

  url: string = '/api/items';

  constructor(private http: HttpClient, private generatedService: Generated) {
    super();
  }
  // get items
  getItems(): Promise<PageResult<Item>> {
    return this.http.get(this.toPaginatedUrl(this.url)).pipe(
      map(
        (x)=>
          new PageResult<Item>(x)
      )
    ).toPromise();
  }
  placeBid(id: string, amount: number): Promise<void> {
    return this.generatedService.itemsPOST(<PlaceBid>{
      itemId: id,
      amount: amount
    }).toPromise();
  }
  createOrUpdateAutoBidding(id: string, maxAmount: number): Promise<void> {
    return this.generatedService.itemsPUT(<AutomateBid>{
      itemId: id,
      maxBidAmount:maxAmount
    }).toPromise();
  }


}
