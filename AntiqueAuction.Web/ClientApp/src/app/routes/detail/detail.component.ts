import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, interval, ReplaySubject } from 'rxjs';
import { map, take } from 'rxjs/operators';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
})
export class DetailComponent implements OnInit {
  public bidNowToggle$: ReplaySubject<boolean> = new ReplaySubject<boolean>(1);

  constructor() {
    this.bidNowToggle$.subscribe(x=>{
      console.log("toggle")
    })
  }


  ngOnInit(): void {
    window.scroll(0, 0);
  }
}
