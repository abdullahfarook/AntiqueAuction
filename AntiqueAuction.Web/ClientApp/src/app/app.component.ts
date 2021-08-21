import { Component } from '@angular/core';
import { PaginatorState } from './shared/components/paginator/model/paginator.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'antique-auction';
  paginator:PaginatorState = <PaginatorState>{
    page:1,
    pageSize:2,
    pageSizes:[1,2,3],
    total:100
  };
}
