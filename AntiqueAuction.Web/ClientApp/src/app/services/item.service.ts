import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Generated, AutoBid, AutomateBid, Item, PlaceBid } from 'src/generated/services';
import { PageInfo, PageResult } from '../core/models/page-result';
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
  getItems(search?:string,priceSort?:string,page?:PageInfo): Promise<PageResult<Item>> {
    var query = `${this.url}?`
    if(search){
      query = `${query}&namecontains=${search}&descriptioncontains=${search}`;
    }
    if(priceSort){
      console.log(priceSort)
      query = `${query}&$orderby=${priceSort ==='asc'?'price':'-price'}`;
    }
    if(page){
      query = `${query}&page=${page.pageIndex}&pagesize=${page.pageSize}`;
    }else{
      query = `${query}&page=1&pagesize=10`;
    }

    return this.http.get(this.toPaginatedUrl(query)).pipe(
      map(
        (x)=>
          new PageResult<Item>(x,page)
      )
    ).toPromise();
  }
  addQuery = (q:string)=>{ 
    if(!q.includes(this.url)){
      q = this.url+'?'+q;
    }
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
