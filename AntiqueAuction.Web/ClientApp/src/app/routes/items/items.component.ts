import { Component, OnInit } from '@angular/core';
import { ItemService } from 'src/app/services/item.service';
import { PaginatorState } from 'src/app/shared/components/paginator/model/paginator.model';
import { Item } from 'src/generated/services';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss']
})
export class ItemsComponent implements OnInit {

  search = '';
  priceSort: string | undefined;
  items:Item[] = [];
  paginator= <PaginatorState>{
    page:1,
    pageSize:10,
    pageSizes:[10],
    total:0

  };
  constructor(private itemService:ItemService) { }

  async ngOnInit() {
    await this.getItems();
    
  }
  async getItems(){
    var result =  await this.itemService.getItems(this.search,this.priceSort,
      {pageIndex:this.paginator.page,pageSize:this.paginator.pageSize});
    this.items = result.data;
    this.paginator.page = result.pageInfo.pageIndex;
    this.paginator.total = result.totalCount;
    console.log(result);
  }
  async submit(){

    await this.getItems();
    this.priceSort = undefined;
    console.log('res')
  }
  async onPaginationChaged(args:PaginatorState){
    this.paginator.page = args.page;
    await this.getItems();

  }

}
