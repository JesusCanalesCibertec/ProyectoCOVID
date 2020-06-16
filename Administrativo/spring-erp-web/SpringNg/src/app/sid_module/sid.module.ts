import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { InputTextModule } from 'primeng/inputtext';

import { PasswordModule } from 'primeng/password';

import { BlockUIModule, GrowlModule } from 'primeng/primeng';
import { SidServicio } from './servicio/SidServicio';
import { SidComponent } from './components/sid.component';


@NgModule({

    imports: [
        BrowserModule,
        FormsModule,
        PasswordModule,
        InputTextModule,
        BlockUIModule,
        GrowlModule
    ],

    declarations: [
        SidComponent
    ],

    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        SidServicio
    ],

})
export class SidModule { }
