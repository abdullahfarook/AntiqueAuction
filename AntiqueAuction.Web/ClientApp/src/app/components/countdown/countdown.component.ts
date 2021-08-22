import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { interval, Subject } from 'rxjs';

@Component({
  selector: 'app-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.scss']
})
export class CountdownComponent implements OnInit {
  @Input()
  timeRange!: TimeSpan;
  public counter:TimeSpan = {days:0,hours:0,minutes:0,seconds:0}
  func =
  '' +
  '<span class="h1 font-weight-bold">1</span> Day' +
  '<span class="h1 font-weight-bold">5</span> Hr' +
  '<span class="h1 font-weight-bold">30</span> Min';
  constructor(private changeDetector: ChangeDetectorRef) {}

  private destroyed$ = new Subject();

ngOnInit() {
  var ms = new Date().getTime() + 87400000;
var tomorrow = new Date(ms);
  interval(1000).subscribe(() => {
    this.getElapsedTime({endDate:tomorrow});
  });
}

ngOnDestroy() {
  this.destroyed$.next();
  this.destroyed$.complete();
}
  getElapsedTime(entry: Entry) {        
    let totalSeconds = Math.floor((entry.endDate!.getTime() - new Date().getTime()) / 1000);
  
    let hours = 0;
    let minutes = 0;
    let seconds = 0;
    let days = 0;
    
    if (totalSeconds >= 86400) {
      days = Math.floor(totalSeconds / 86400);      
      totalSeconds -= 86400 * days;      
    }

    if (totalSeconds >= 3600) {
      hours = Math.floor(totalSeconds / 3600);      
      totalSeconds -= 3600 * hours;      
    }
  
    if (totalSeconds >= 60) {
      minutes = Math.floor(totalSeconds / 60);
      totalSeconds -= 60 * minutes;
    }
  
    seconds = totalSeconds;
  
    this.counter = {
      hours: hours,
      minutes: minutes,
      seconds: seconds,
      days:days
    };
  }
}
export interface TimeSpan {
  hours: number;
  minutes: number;
  seconds: number;
  days:number
  
}
export interface Entry {
  endDate: Date;
}
