import { Component, OnInit } from '@angular/core';
import { interval } from 'rxjs';
import { map, take } from 'rxjs/operators';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
})
export class DetailComponent implements OnInit {
  constructor() {}
  get15dayFromNow() {
    return new Date(new Date().valueOf() + 15 * 24 * 60 * 60 * 1000);
  }

  func =
    '' +
    '<span class="h1 font-weight-bold">%D</span> Day%!d' +
    '<span class="h1 font-weight-bold">%H</span> Hr' +
    '<span class="h1 font-weight-bold">%M</span> Min' +
    '<span class="h1 font-weight-bold">%S</span> Sec';

  ngOnInit(): void {
    window.scroll(0, 0);
    const time = 5; // 5 seconds
    var timer$ = interval(1000); // 1000 = 1 second
    timer$
      .pipe(
        take(time),
        map((v: number) => time - 1 - v)) // to reach zero)
      .subscribe((v: number) => console.log('Countdown', v));
  }
}
