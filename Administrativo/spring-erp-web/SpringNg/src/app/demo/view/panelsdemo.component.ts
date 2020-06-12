import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/primeng';

@Component({
    templateUrl: './panelsdemo.component.html',
    styles: [`
        :host ::ng-deep button {
            margin-right: .25em;
            margin-left: .25em;
        }

        :host ::ng-deep .ui-splitbutton button {
            margin-right: 0;
        }`
    ]
})
export class PanelsDemoComponent implements OnInit {

    items: MenuItem[];

    constructor() {
    }

    ngOnInit() {
        this.items = [
            { label: 'Angular.io', icon: 'fa fa-link', url: 'http://angular.io' },
            { label: 'Theming', icon: 'fa fa-book', routerLink: ['/theming'] }
        ];
    }
}
