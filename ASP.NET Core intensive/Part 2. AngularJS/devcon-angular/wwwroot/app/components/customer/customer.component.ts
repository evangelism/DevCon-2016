import {Component} from '@angular/core'
import {FORM_DIRECTIVES, FormBuilder, NgForm, Control, ControlGroup, Validators} from '@angular/common'
import {Router, RouteSegment, OnActivate} from '@angular/router'
import {CustomersService, Customer} from '../../services/customers/customers.service'

@Component({
    selector: 'app-customer-page',
    templateUrl: 'app/components/customer/customer.html',
    directives: [FORM_DIRECTIVES]
})
export class CustomerComponent implements OnActivate{
    customer: Customer = {Id: 0, Name: '', Age: 0};
    customerForm: ControlGroup;
    
    constructor(
        private _router: Router,
        private _customerService: CustomersService,
        private _formBuilder: FormBuilder
    ){
            
       this.customerForm = _formBuilder.group({  
        name: ['', Validators.compose([Validators.required, Validators.minLength(4)])],
        age: ['', Validators.compose([Validators.required, this.ageValidator])]
       });  
    }
    
    onSubmit(form: any): void {  
        console.log(this.customerForm.valid); 
        console.log('Submitted value:', form); 
    }
    
    ageValidator(control: Control): { [s: string]: boolean } {  
        let val = +control.value;
        if(val % 2 != 0) return {invalidAge: true};
    }
    
    routerOnActivate(segment: RouteSegment): void{
        let id = +segment.getParam('id');
        
        this._customerService.getCustomers().subscribe((customers:Customer[]) =>{
            let filteredCustomers = customers.filter(c => c.Id == id);
            if(filteredCustomers.length > 0){
                this.customer = filteredCustomers[0];
            }
        });
    }
}