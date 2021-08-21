import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-gallery-item',
  templateUrl: './gallery-item.component.html',
  styleUrls: ['./gallery-item.component.scss']
})
export class GalleryItemComponent implements OnInit {
  win = window;
  constructor() { }

  ngOnInit(): void {
  }

}
