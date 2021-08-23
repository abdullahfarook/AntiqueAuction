import { Component, Input, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from 'src/generated/services';

@Component({
  selector: 'app-gallery-item',
  templateUrl: './gallery-item.component.html',
  styleUrls: ['./gallery-item.component.scss']
})
export class GalleryItemComponent implements OnInit {
  @Input() item!:Item
  win = window;
  api = environment.apiUrl;
  constructor() { }

  ngOnInit(): void {
  }

}
