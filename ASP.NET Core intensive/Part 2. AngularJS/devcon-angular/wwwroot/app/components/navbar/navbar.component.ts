import {Component,Input,Output,EventEmitter} from '@angular/core';
import {ROUTER_DIRECTIVES} from '@angular/router';

@Component({
    selector: 'app-navbar',
    directives: [ROUTER_DIRECTIVES],
    templateUrl: 'app/components/navbar/navbar.html'
})
export class NavbarComponent{
    @Input() HomeTitle: string;
    @Output() LinkMouseOver: EventEmitter<any> = new EventEmitter<any>();
    
    fireEvent(evt){
        this.LinkMouseOver.emit(evt);
    }
}