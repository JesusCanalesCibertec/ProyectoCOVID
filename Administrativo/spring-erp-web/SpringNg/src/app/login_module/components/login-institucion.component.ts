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
import { PsInstitucionServicio } from 'src/app/erp_module/programasocial/institucion/service/PsInstitucionServicio';

@Component({
    selector: "login-base",
    templateUrl: './login-institucion.component.html',
    styles: ['.ui-helper-clearfix {height: 30px !important;}', ' .ng-star-inserted {height: 100% !important;}']
})
export class LoginInstitucionComponent extends PrincipalBaseComponent implements OnInit {

    usuario: LoginUser = new LoginUser();
    verModalRecuperarClave: boolean = false;
    correoRecuperarClave: string = "";
    companias: SelectItem[] = [];

    constructor(
        private psInstitucionServicio: PsInstitucionServicio,
        private router: Router, private usuarioServicio: UsuarioServicio, noAuthorizationInterceptor: NoAuthorizationInterceptor
        , messageService: MessageService
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {
        super.ngOnInit();
        this.usuario.usuario = null;//'ALTORRES';
        /*
        this.bloquearPagina();
        this.usuarioServicio.listarInstituciones().then(
            res => {
                this.companias = [];
                this.companias.push({ label: " -- Seleccionar --", value: null })
                res.forEach(obj => this.companias.push({ label: obj.nombre, value: obj.idInstitucion }));
                this.desbloquearPagina();
            }
        );*/
    }

    loginPress(event?: any) {
        if (event.keyCode === 13) {
            this.login();
        }
    }

    login() {
        if (this.usuario.compania == null || this.usuario.usuario == null) {
            this.mostrarMensajeAdvertencia("Ingresar todos los campos");
            return;
        }
        this.bloquearPagina();
        this.usuario.origen = 'institucion';

        this.usuarioServicio.login(this.usuario).then(
            obj => {
                if (obj != null) {
                    sessionStorage.setItem('modo', 'I');
                    sessionStorage.removeItem('filtroBeneficiario');
                    sessionStorage.setItem('access_token', obj);
                    this.router.navigate(['spring']);
                } else {
                    this.desbloquearPagina();
                }
            }
        );
    }

    mostrarDialogRecuperarClave() {
        this.correoRecuperarClave = "";
        this.verModalRecuperarClave = true;
    }

    cargarCompaniasPorUsuario() {
        this.bloquearPagina();
        this.companias = [];

        if (this.trim(this.usuario.usuario) == '' || this.trim(this.usuario.usuario) == null) { this.desbloquearPagina(); return; }

        this.usuarioServicio.listarCompanias(this.usuario.usuario).then(
            res => {
                this.companias = [];
                this.companias.push({ label: '-- Seleccione --', value: '' })
                res.forEach(obj => this.companias.push({ label: obj.nombre, value: obj.codigo }));
                this.desbloquearPagina();
            }
        );
    }

    enviarRecuperarClave() {

        if (this.correoRecuperarClave == '' || this.correoRecuperarClave.trim() == '') {
            this.mostrarMensajeAdvertencia("Ingresar la cuenta de correo");
            return;
        }

        var regexp = new RegExp("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
        var unit = true;

        unit = regexp.test(this.correoRecuperarClave);
        if (!unit) {
            this.mostrarMensajeAdvertencia("Ingresar una cuenta de correo válida");
            return;
        }

        this.bloquearPagina();

        this.usuarioServicio.enviarRecuperarClave(this.correoRecuperarClave, 'I').then(
            res => {
                this.desbloquearPagina();
                if (res != null) {
                    this.mostrarMensajeExito(res);
                    this.verModalRecuperarClave = false;
                    this.correoRecuperarClave = "";
                }
            }
        );
    }
}
