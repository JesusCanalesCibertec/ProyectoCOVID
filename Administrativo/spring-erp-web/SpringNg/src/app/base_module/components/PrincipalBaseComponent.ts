import { BaseComponent } from './basecomponent';
import { NoAuthorizationInterceptor } from '../interceptor/NoAuthorizationInterceptor';
import { OnInit } from '@angular/core';
import { MessageService } from 'primeng/components/common/messageservice';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';

export class PrincipalBaseComponent extends BaseComponent implements OnInit {

    erroresList: DtoTabla[] = [];
    verTablaErrores: Boolean = false;

    constructor(
        private noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
    ) {
        super(messageService);
    }

    salirTablaErrores() {
        this.verTablaErrores = false;
        this.erroresList = [];
    }

    ngOnInit() {
        this.noAuthorizationInterceptor.dataStream$().subscribe(errores => {
            this.messageService.clear();
            if (errores != undefined) {
                this.erroresList = [];

                if (errores.error.flagTabla) {
                    this.verTablaErrores = true;
                    var c = 1;
                    errores.error.errores.forEach(element => {
                        var er = new DtoTabla();
                        er.id = c;
                        er.descripcion = element.Mensaje;
                        this.erroresList.push(er);
                        c++;
                    });
                }
                else {
                    this.messageService.addAll(this.getMensajesUsuario(errores.error.errores));
                }
            }
        });
    }
}