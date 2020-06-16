import { Component, OnInit, EventEmitter, Output, ViewChild, ElementRef } from '@angular/core';
import { CompaniaRecursosService } from '../servicio/companiarecursos.service';
import { Companyownerrecurso } from '../dominio/companyownerrecurso';
import { BaseComponent, accionSolicitada } from '../../../../base_module/components/basecomponent';
import { MaMiscelaneosdetalle } from '../../../shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { EmpleadomastServicio, CONVERTIR_FOTO } from '../../../shared/selectores/empleado/servicio/EmpleadomastServicio';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-companiadetallemantenimiento',
    templateUrl: './companiarecursos_detalle_mantenimiento.component.html'
})
export class CompaniaDetalleMantenimientoComponent extends BaseComponent implements OnInit {

    verSelector: boolean = false;
    companyownerrecurso: Companyownerrecurso;
    nombreCompania: string;
    @Output() volveraBuscar = new EventEmitter();
    api: string;
    imageSrc: string;

    constructor(
        private companiaRecursosService: CompaniaRecursosService,
        private empleadomastService: EmpleadomastServicio,
        messageService: MessageService
    ) { super(messageService); }

    ngOnInit(): void {
        this.companyownerrecurso = new Companyownerrecurso();
    }

    eliminarImagen() {
        this.imageSrc = '';
    }

    cargarRecurso(event: any) {

        this.bloquearPagina();

        const files = event.files;

        if (files.length !== 1) {
            this.desbloquearPagina();
            return;
        }

        var reader = new FileReader();
        reader.readAsDataURL(files[0]);

        reader.onloadend = (evt) => {
            var result = reader.result;
            this.imageSrc = result.toString();
            this.companyownerrecurso.nombrerecurso = event.files[0].name;
            this.companyownerrecurso.auxString = result.toString();

            this.desbloquearPagina();
        };
    }

    verVentana(maMiscelaneoDetalle: MaMiscelaneosdetalle) {
        this.verSelector = true;
        this.imageSrc = '';
        this.accion = accionSolicitada.NUEVO;
        this.companyownerrecurso = new Companyownerrecurso();
        this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
            empleado => {
                this.companyownerrecurso.companyowner = empleado.companiaId;
                this.companyownerrecurso.ultimousuario = empleado.codigoUsuario;
                this.nombreCompania = empleado.companiaNombre;
            });

        this.companyownerrecurso.tiporecurso = maMiscelaneoDetalle.codigoelemento;

    }

    editar(data: Companyownerrecurso) {
        this.verSelector = true;
        this.imageSrc = '';
        this.imageSrc = CONVERTIR_FOTO(data.auxString);
        this.accion = accionSolicitada.EDITAR;
        this.companyownerrecurso = data;
        this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
            empleado => {
                this.companyownerrecurso.companyowner = empleado.companiaId;
                this.companyownerrecurso.ultimousuario = empleado.codigoUsuario;
                this.nombreCompania = empleado.companiaNombre;
            });
    }

    guardar() {
        if (this.companyownerrecurso.periodo === null || this.companyownerrecurso.periodo === undefined) {
            this.messageService.clear();
            this.mostrarMensaje('Es obligatorio ingresar el periodo', 'error');
            return;
        }

        if (this.accion === accionSolicitada.NUEVO) {
            this.companiaRecursosService.registrar(this.companyownerrecurso)
                .then(tx => {
                    this.messageService.clear();
                    this.mostrarMensaje('Se Registró el Recurso : ' + tx.tiporecurso, 'info');
                    this.volveraBuscar.emit(this.companyownerrecurso);
                    this.verSelector = false;
                })
                .catch(error => {
                    this.desbloquearPagina();
                });
        } else {
            this.companiaRecursosService.actualizar(this.companyownerrecurso).then(
                resp => {
                    this.messageService.clear();
                    this.mostrarMensaje('Se Actualizó la Compañia: ' + resp.companyowner, 'info');
                    this.volveraBuscar.emit();
                    this.verSelector = false;
                }
            ).catch(error => {
                this.messageService.clear();
                this.desbloquearPagina();
                this.verSelector = false;
            });
        }

    }

}
