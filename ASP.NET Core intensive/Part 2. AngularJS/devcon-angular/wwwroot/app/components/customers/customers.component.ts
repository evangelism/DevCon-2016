import {Component, OnInit} from '@angular/core';
import {ROUTER_DIRECTIVES} from '@angular/router'
import {CustomersService, Customer} from '../../services/customers/customers.service'
import {Observable} from 'rxjs/Rx'

@Component({
    selector: 'app-customers-page',
    directives: [ROUTER_DIRECTIVES],
    templateUrl: 'app/components/customers/customers.html'
})
export class CustomersComponent implements OnInit{
    customers: Observable<Customer>;
    
    constructor(private _customerService: CustomersService){
        
    }
    
    ngOnInit(){
        this.customers = this._customerService.getCustomers();
    }
}