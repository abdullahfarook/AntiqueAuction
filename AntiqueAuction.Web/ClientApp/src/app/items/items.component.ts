import { Component, OnInit } from '@angular/core';
import { ItemService } from '../services/item.service';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss']
})
export class ItemsComponent implements OnInit {

  constructor(private itemService:ItemService) { }

  async ngOnInit() {
   var result =  await this.itemService.getItems();
   console.log(result);
  }

}
