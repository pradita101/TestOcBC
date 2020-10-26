import { Component, OnInit, Input } from '@angular/core';
import { CallApiService } from '../call-api.service'

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent implements OnInit {
  public currentCount = 0;
  constructor(
    private cas: CallApiService
  ) { }
  @Input() custId: number;
  DataTransaction: Array<Transactions>
  ngOnInit() {
  }

  public incrementCounter() {
    this.currentCount++;
  }
}

interface Transactions {
  TransactionId: number,
  CustomersId: number,
  Description: string,
  Amount: number,
}
