import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ItemService } from 'src/app/services/item.service';
import { PaginatorState } from 'src/app/shared/components/paginator/model/paginator.model';
import { Item } from 'src/generated/services';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss'],
})
export class ItemsComponent implements OnInit {
  search = '';
  priceSort: string | undefined;
  items: Item[] = [];
  items$!: Observable<Item[]>;
  paginator = <PaginatorState>{
    page: 1,
    pageSize: 10,
    pageSizes: [10],
    total: 0,
  };
  constructor(private itemService: ItemService) {
    this.getItems();
  }

  ngOnInit() {}
  getItems() {
    this.items$ = this.itemService
      .getItems(this.search, this.priceSort, {
        pageIndex: this.paginator.page,
        pageSize: this.paginator.pageSize,
      })
      .pipe(
        map((result) => {
          this.paginator.page = result.pageInfo.pageIndex;
          this.paginator.total = result.totalCount;
          return result.data;
        })
      );
  }
  submit() {
    this.getItems();
    this.priceSort = undefined;
  }
  onPaginationChaged(args: PaginatorState) {
    this.paginator.page = args.page;
    this.getItems();
  }
}
