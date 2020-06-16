import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LocationStrategy, HashLocationStrategy, registerLocaleData } from '@angular/common';
import { AppRoutes } from './app.routes';
import { GrowlModule } from 'primeng/growl';
import { AppComponent } from './app.component';
import { SidModule } from './sid_module/sid.module';
import { SistemasModule } from './erp_module/sistemas/sistemas.module';
import { BaseModule } from './base_module/base.module';
import { LoginModule } from './login_module/login.module';
import { NoAuthorizationInterceptor } from './base_module/interceptor/NoAuthorizationInterceptor';
import { ConfirmationService } from 'primeng/primeng';
import { AuthGuard } from './base_module/guard/AuthGuard';
import { MessageService } from 'primeng/components/common/messageservice';
import { ProgramaSocialModule } from './erp_module/programasocial/programasocial.module';
import { MineduModule } from './erp_module/minedu/minedu.module';
import localeES from '@angular/common/locales/es';
import { CovidModule } from './erp_module/covid/covid.module';

registerLocaleData(localeES,'es')

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        AppRoutes,
        HttpClientModule,
        BrowserAnimationsModule,
        GrowlModule,
        // NgxChartsModule,

        /* MODULOS*/
        SidModule,
        LoginModule,
        SistemasModule,
        BaseModule,
        ProgramaSocialModule,
        MineduModule,
        CovidModule
        /* MODULOS */
    ],
    declarations: [
        AppComponent
    ],
    providers: [
        NoAuthorizationInterceptor,
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        { provide: HTTP_INTERCEPTORS, useExisting: NoAuthorizationInterceptor, multi: true },
        ConfirmationService,
        AuthGuard,
        MessageService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
