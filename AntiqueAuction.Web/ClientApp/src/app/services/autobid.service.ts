import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { Generated, AutoBid, EnableBid } from "src/generated/services";
import { AuthService } from "./auth.service";
import { BaseService } from "./base.service";


@Injectable({
  providedIn: 'root'
})
export class AutobidService extends BaseService {


  url: string =environment.apiUrl+ '/api/items';

  constructor(private http: HttpClient, private generatedService: Generated,private authService:AuthService) {
    super();
  }
  isAutoBidEnabled(itemId: string): Observable<boolean> {
    var query = `${this.url}/${itemId}/auto-bids?userid=${this.authService.authUser?.id}&isactive=true&first=true`;
    return this.http.get<AutoBid>(query).pipe(
      map(
        (x)=>
        x?true:false
      )
    );
  }
  enableAutoBid(id: string): Promise<void> {
    return this.generatedService.autoBids(<EnableBid>{
      itemId: id,
    }).toPromise();
  }
}
