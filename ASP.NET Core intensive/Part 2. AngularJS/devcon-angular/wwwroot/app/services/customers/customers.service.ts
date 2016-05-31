import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http'
import {Observable} from 'rxjs/Rx';
import 'rxjs/add/operator/map';

export class Customer{
    Id: number;
    Name: string;
    Age: number;
}

@Injectable()
export class CustomersService{
    constructor(private _http: Http){
        
    }
    
    getCustomers(){
        return this._http.get('/api/customers').map((resp: Response)=> 
        <Customer[]>resp.json())
        .do(data => console.log(data))
        .catch(this.handleError);
    }
    
    private handleError(error: Response){
        console.error(error);
        
        return Observable.throw(error.json().error || 'server error');
    }
}