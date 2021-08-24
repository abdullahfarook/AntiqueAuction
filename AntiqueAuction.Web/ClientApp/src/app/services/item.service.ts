import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Generated, Item, PlaceBid } from 'src/generated/services';
import { PageInfo, PageResult } from '../core/models/page-result';
import { AuthService } from './auth.service';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ItemService extends BaseService {


  url: string =environment.apiUrl+ '/api/items';

  constructor(private http: HttpClient, private generatedService: Generated,private authService:AuthService) {
    super();
  }

  getItems(search?:string,priceSort?:string,page?:PageInfo): Observable<PageResult<Item>> {
    var query = `${this.url}?`
    if(search){
      query = `${query}&namecontains=${search}&descriptioncontains=${search}`;
    }
    if(priceSort){
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
    );
  }

  getItem(id:string):Observable<Item>{
    var query = `${this.url}?id=${id}&first=true`
    return this.http.get<Item>(query);
      
  }
  placeBid(id: string, amount: number): Promise<void> {
    return this.generatedService.items(<PlaceBid>{
      itemId: id,
      amount: amount
    }).toPromise();
  }



}
