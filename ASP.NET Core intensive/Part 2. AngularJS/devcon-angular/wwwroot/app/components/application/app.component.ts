import {Component} from '@angular/core'
import {Routes, ROUTER_DIRECTIVES} from '@angular/router'
import {Http, HTTP_PROVIDERS} from '@angular/http'
import {CustomersService} from '../../services/customers/customers.service'
import {NavbarComponent} from '../navbar/navbar.component'
import {HomeComponent} from '../home/home.component'
import {CustomersComponent} from '../customers/customers.component'
import {CustomerComponent} from '../customer/customer.component'

@Component({
    selector: 'my-app',
    templateUrl: 'app/components/application/application.html',
    directives: [
        ROUTER_DIRECTIVES,
        NavbarComponent
    ],
    providers: [
        HTTP_PROVIDERS,
        CustomersService
    ]
})
@Routes([
    {path: '/', component: HomeComponent},
    {path: '/customers', component: CustomersComponent},
    {path: '/customer/:id', component: CustomerComponent}
])
export class AppComponent{
    onEvent(evt){
        console.log('onEvent');
        console.dir(evt);
    }
}