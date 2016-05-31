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
var router_1 = require('@angular/router');
var http_1 = require('@angular/http');
var customers_service_1 = require('../../services/customers/customers.service');
var navbar_component_1 = require('../navbar/navbar.component');
var home_component_1 = require('../home/home.component');
var customers_component_1 = require('../customers/customers.component');
var customer_component_1 = require('../customer/customer.component');
var AppComponent = (function () {
    function AppComponent() {
    }
    AppComponent.prototype.onEvent = function (evt) {
        console.log('onEvent');
        console.dir(evt);
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            templateUrl: 'app/components/application/application.html',
            directives: [
                router_1.ROUTER_DIRECTIVES,
                navbar_component_1.NavbarComponent
            ],
            providers: [
                http_1.HTTP_PROVIDERS,
                customers_service_1.CustomersService
            ]
        }),
        router_1.Routes([
            { path: '/', component: home_component_1.HomeComponent },
            { path: '/customers', component: customers_component_1.CustomersComponent },
            { path: '/customer/:id', component: customer_component_1.CustomerComponent }
        ]), 
        __metadata('design:paramtypes', [])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map