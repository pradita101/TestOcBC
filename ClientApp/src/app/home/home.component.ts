import { Component, OnInit } from '@angular/core';
import { Subscription } from "rxjs";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { CallApiService } from '../call-api.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  baseUrl = location.host;

  constructor(
    private fb: FormBuilder,
    private cas: CallApiService
  ) { }

  customerGroup: FormGroup;
  customerData: Customer;
  submit = false;
  DataCustomer: Array<Customer>;

  ngOnInit() {
    this.customerGroup = this.fb.group({
      CustomerName: ['', [Validators.required]]
    });
    this.ShowTable();
  }

  get f() {
    return this.customerGroup.controls;
  }

  async submitData() {
    this.submit = true;
    console.log(this.customerGroup.value);
    if (this.customerGroup.valid) {
      this.cas.AddCustomer(this.customerGroup.value).subscribe(data => console.log(data), error => console.log(error));
      this.ShowTable();
    }
  }
  async ShowTable() {
    this.cas.ShowCustomer().subscribe(datacust => this.showData(datacust), error => console.log(error));
  }
  async showData(data) {
    this.DataCustomer = data;
    console.log(data);
  }

  addTransaction(customerId) {
  }
}

interface Customer {
  CustomersId: number,
  CustomerName: string
}
