import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PaginatorState, PageSizes } from './model/paginator.model';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.scss']
})
export class PaginatorComponent implements OnInit {
  @Input()
  paginator!: PaginatorState;
  @Input() isLoading: boolean | undefined;
  @Output() paginate: EventEmitter<PaginatorState> = new EventEmitter();
  pageSizes: number[] = PageSizes;
  constructor() {}

  ngOnInit(): void {
  }


  pageChange(num: number) {
    this.paginator.page = num;
    this.paginate.emit(this.paginator);
  }

  sizeChange() {
    this.paginator.pageSize = +this.paginator.pageSize;
    this.paginator.page = 1;
    this.paginate.emit(this.paginator);
  }
}
