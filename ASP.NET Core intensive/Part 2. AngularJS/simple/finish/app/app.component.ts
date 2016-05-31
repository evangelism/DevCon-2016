
import {bootstrap} from '@angular/platform-browser-dynamic';
import {Component} from '@angular/core'

@Component({
    selector: 'my-app',
    template: 'Hello, {{fullName}}'
    //templateUrl: 'app/application.html'
})
export class AppComponent{
    fullName: string;
    color: string;
    showHello: boolean;
    names: string[];
    
    constructor(){
        this.fullName = "Sergey Pugachev";
        this.color = "red";
        this.showHello = true;
        this.names = ["Sergey", "Andrey", "Igor", "Mikhail"];
    }
    
    onSave(){
        alert("Saved");
    }
}

bootstrap(AppComponent);
