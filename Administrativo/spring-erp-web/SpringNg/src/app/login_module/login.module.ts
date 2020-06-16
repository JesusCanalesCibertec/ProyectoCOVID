import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { InputTextModule } from 'primeng/inputtext';

import { PasswordModule } from 'primeng/password';

import { UsuarioServicio } from './servicio/UsuarioServicio';
import { BlockUIModule, GrowlModule, Dropdown, DropdownModule } from 'primeng/primeng';
import { DialogModule } from 'primeng/dialog';
import { PanelModule } from 'primeng/panel';
import { LoginFundacionComponent } from './components/login-fundacion.component';
import { LoginInstitucionComponent } from './components/login-institucion.component';
import { LoginComponent } from './components/login.component';
import { PsInstitucionServicio } from '../erp_module/programasocial/institucion/service/PsInstitucionServicio';


@NgModule({

    imports: [
        BrowserModule,
        FormsModule,
        PasswordModule,
        InputTextModule,
        BlockUIModule,
        GrowlModule,
        DialogModule,
        PanelModule,
        DropdownModule
    ],

    declarations: [
        LoginComponent,
        LoginFundacionComponent,
        LoginInstitucionComponent
    ],

    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        UsuarioServicio,
        PsInstitucionServicio,
    ],

})
export class LoginModule { }
