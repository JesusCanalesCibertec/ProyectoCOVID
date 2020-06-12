import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Correo } from '../dominio/correo';
import { SyAprobacionNivelesDet } from '../dominio/syaprobacionnivelesdet';
import { ConfirmationService } from 'primeng/primeng';
import { SyAprobacionNiveles } from '../dominio/SyAprobacionNiveles';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { DtoTabla } from '../../../shared/dominio/dto/DtoTabla';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'correo-mantenimiento',
    templateUrl: './correo-mantenimiento.component.html'
})
export class CorreoMantenimientoComponent extends BaseComponent implements OnInit {

    verModal: boolean = false;
    tag: string = "";

    lstCorreos: DtoTabla[] = [];

    det: SyAprobacionNivelesDet = new SyAprobacionNivelesDet();
    nivel: SyAprobacionNiveles = new SyAprobacionNiveles();
    @Output() cargarDataEvent = new EventEmitter();
    @Output() cargarCabeceraEvent = new EventEmitter();
    @Output() cargarDataConfirmacionEvent = new EventEmitter();

    constructor(private confirmationService: ConfirmationService, messageService: MessageService) { super(messageService); }

    ngOnInit() {

    }
    iniciarComponente() {
        this.lstCorreos = [];

        switch (this.tag) {
            case 'D': this.llenarCorreos(this.det.correoelectronico); break;
            case 'C': this.llenarCorreos(this.nivel.correoelectronico); break;
            case 'AR': this.llenarCorreos(this.nivel.nivel0Correoelectronico); break;
            default: ;
        }

        this.verModal = true;
    }

    llenarCorreos(cadena: string) {

        if (cadena == null || cadena == undefined) {
            this.lstCorreos = [];
            return;
        }


        var c = cadena.split(';');

        var i = 1;

        let temp = [...this.lstCorreos];

        c.pop();

        c.forEach(r => {
            var reg = new DtoTabla();
            reg.id = i;
            reg.nombre = r + ';';
            temp.push(reg);
            i++;
        });
        this.lstCorreos = temp;
    }

    agregarCorreo() {

        let temp = [...this.lstCorreos];

        let c: DtoTabla = new DtoTabla();

        c.id = this.generarId();

        temp.push(c);

        this.lstCorreos = temp;

    }

    generarId(): number {

        if (this.lstCorreos.length == 0) return 1;

        let max: number = this.lstCorreos[0].id;

        this.lstCorreos.forEach(
            td => {
                if (max < +td.id) {
                    max = +td.id;
                }
            }
        );
        return max + 1;
    }

    salir() {
        this.verModal = false;
    }

    aceptar() {
        if (!this.validar()) {
            this.messageService.clear();
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El correo debe contener el "@" y terminar con ";"' });
            return;
        }

        var correo = '';
        this.lstCorreos.forEach(reg => {
            correo += reg.nombre;
        });

        this.det.correoelectronico = correo;
        this.nivel.correoelectronico = correo;

        switch (this.tag) {
            case 'D': this.cargarDataEvent.emit(this.det); break;
            case 'C': this.cargarCabeceraEvent.emit(this.nivel); break;
            case 'AR': this.cargarDataConfirmacionEvent.emit(this.nivel); break;
            default: ;
        }


    }

    validar(): boolean {
        var valida = true;
        var regexp = new RegExp("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*;$");
        var unit = true;

        this.lstCorreos.forEach(reg => {
            unit = regexp.test(reg.nombre);
            if (!unit) {
                valida = false;
            }
        });

        return valida;
    }

    quitar(tb: DtoTabla) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            message: '¿Desea remover el registro?',
            key: 'eliminarCorreo',
            accept: () => {
                let index = this.lstCorreos.indexOf(tb);
                this.lstCorreos = this.lstCorreos.filter((val, i) => i != index);
            }
        });
    }
}
