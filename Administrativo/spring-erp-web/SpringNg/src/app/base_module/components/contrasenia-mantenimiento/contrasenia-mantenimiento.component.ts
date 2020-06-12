import { Component } from '@angular/core';
import { BaseComponent } from '../basecomponent';
import { UsuarioServicio } from '../../../login_module/servicio/UsuarioServicio';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-contrasenia-mantenimiento',
    templateUrl: 'contrasenia-mantenimiento.component.html'
})
export class ContraseniaMantenientoComponent extends BaseComponent {

    verModal: boolean = false;
    usuario: string = '';
    clave: string = '';
    clave1: string = '';
    clave2: string = '';

    constructor(
        private seguridadService: UsuarioServicio,
        messageService: MessageService
    ) {
        super(messageService);
    }

    iniciarComponente(usuario: string) {
        this.usuario = usuario;
        this.clave = '';
        this.clave1 = '';
        this.clave2 = '';
        this.verModal = true;
    }

    guardar() {

        if (this.validar()) {
            return;
        }

        this.seguridadService.cambiarClave(this.usuario, this.clave, this.clave1, this.clave2).then(
            res => {
                if (res !== null) {
                    this.messageService.add({ severity: 'info', summary: 'Información', detail: 'Se han guardado los cambios' });
                    this.salir();
                }
            }
        ).catch(
            error => {
            }
        );

    }
    salir() {
        this.verModal = false;
    }


    validar(): boolean {
        let result: boolean = false;
        this.messageService.clear();
        if (this.clave === '') {
            this.mostrarMensaje('Es obligatorio ingresar la Clave Anterior', 'error');
            result = true;
        }

        if (this.clave1 === '') {
            this.mostrarMensaje('Es obligatorio ingresar la Clave Nueva', 'error');
            result = true;
        }
        if (this.clave2 === '') {
            this.mostrarMensaje('Es obligatorio ingresar Repetir Clave', 'error');
            result = true;
        }

        return result;
    }
}
