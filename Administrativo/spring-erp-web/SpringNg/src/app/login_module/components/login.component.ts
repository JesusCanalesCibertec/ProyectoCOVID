import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioServicio } from '../servicio/UsuarioServicio';
import { LoginUser } from '../dominio/LoginUser';
import { BaseComponent } from '../../base_module/components/basecomponent';
import { NoAuthorizationInterceptor } from '../../base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from '../../base_module/components/PrincipalBaseComponent';
import { MessageService } from 'primeng/components/common/messageservice';
import { SelectItem } from 'primeng/primeng';
import { ViewEncapsulation } from '@angular/compiler/src/core';

@Component({
    selector: "login-base",
    templateUrl: './login.component.html',
    styles: ['.ui-helper-clearfix {height: 30px !important;}', ' .ng-star-inserted {height: 100% !important;}']
})
export class LoginComponent extends PrincipalBaseComponent implements OnInit {

    usuario: LoginUser = new LoginUser();
    verModalRecuperarClave: boolean = false;
    correoRecuperarClave: string = "";
    companias: SelectItem[] = [];

    constructor(private router: Router, private usuarioServicio: UsuarioServicio, noAuthorizationInterceptor: NoAuthorizationInterceptor
        , messageService: MessageService
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {
        super.ngOnInit();
        this.usuario.usuario = 'ALTORRES';
    }

    loginInstitucion() {
        this.router.navigate(['spring/institucion-login']);
    }
    loginFundacion() {
        this.router.navigate(['spring/fundacion-login']);
    }

}
