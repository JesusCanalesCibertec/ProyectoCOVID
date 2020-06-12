import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { ScrollPanel } from 'primeng/primeng';
import { LayoutComponent } from './layout.component';

@Component({
    selector: 'app-rightpanel',
    templateUrl: './app.rightpanel.component.html'
})
export class AppRightPanelComponent implements AfterViewInit {

    @ViewChild('scrollRightPanel') rightPanelMenuScrollerViewChild: ScrollPanel;

    constructor(public app: LayoutComponent) { }

    ngAfterViewInit() {
        setTimeout(() => { this.rightPanelMenuScrollerViewChild.moveBar(); }, 100);
    }

    onTabChange(event) {
        setTimeout(() => { this.rightPanelMenuScrollerViewChild.moveBar(); }, 450);
    }
}
