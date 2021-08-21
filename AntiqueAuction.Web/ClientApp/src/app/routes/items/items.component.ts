import { Component, OnInit } from '@angular/core';
import { ItemService } from 'src/app/services/item.service';
import { PaginatorState } from 'src/app/shared/components/paginator/model/paginator.model';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss']
})
export class ItemsComponent implements OnInit {
  items = [1,2,3,4,5,6,7,8,9,10]
  paginator= <PaginatorState>{
    page:1,
    pageSize:10,
    pageSizes:[10],
    total:100

  };
  constructor(private itemService:ItemService) { }

  async ngOnInit() {
   var result =  await this.itemService.getItems();
   console.log(result);
  }

}
