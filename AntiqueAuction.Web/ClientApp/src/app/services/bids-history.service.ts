import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BidHistory, Generated } from 'src/generated/services';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class BidsHistoryService extends BaseService {

  url: string = '/api/items';

  constructor(private http: HttpClient, private generatedService: Generated) {
    super();
  }
  getBidsHistory(itemId:string):Observable<BidHistory[]>{
    var query = `${this.url}?itemId=${itemId}`
    return this.http.get<BidHistory[]>(query);

  }
}
