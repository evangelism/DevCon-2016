"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var common_1 = require('@angular/common');
var router_1 = require('@angular/router');
var customers_service_1 = require('../../services/customers/customers.service');
var CustomerComponent = (function () {
    function CustomerComponent(_router, _customerService, _formBuilder) {
        this._router = _router;
        this._customerService = _customerService;
        this._formBuilder = _formBuilder;
        this.customer = { Id: 0, Name: '', Age: 0 };
        this.customerForm = _formBuilder.group({
            name: ['', common_1.Validators.compose([common_1.Validators.required, common_1.Validators.minLength(4)])],
            age: ['', common_1.Validators.compose([common_1.Validators.required, this.ageValidator])]
        });
    }
    CustomerComponent.prototype.onSubmit = function (form) {
        console.log(this.customerForm.valid);
        console.log('Submitted value:', form);
    };
    CustomerComponent.prototype.ageValidator = function (control) {
        var val = +control.value;
        if (val % 2 != 0)
            return { invalidAge: true };
    };
    CustomerComponent.prototype.routerOnActivate = function (segment) {
        var _this = this;
        var id = +segment.getParam('id');
        this._customerService.getCustomers().subscribe(function (customers) {
            var filteredCustomers = customers.filter(function (c) { return c.Id == id; });
            if (filteredCustomers.length > 0) {
                _this.customer = filteredCustomers[0];
            }
        });
    };
    CustomerComponent = __decorate([
        core_1.Component({
            selector: 'app-customer-page',
            templateUrl: 'app/components/customer/customer.html',
            directives: [common_1.FORM_DIRECTIVES]
        }), 
        __metadata('design:paramtypes', [router_1.Router, customers_service_1.CustomersService, common_1.FormBuilder])
    ], CustomerComponent);
    return CustomerComponent;
}());
exports.CustomerComponent = CustomerComponent;
//# sourceMappingURL=customer.component.js.map