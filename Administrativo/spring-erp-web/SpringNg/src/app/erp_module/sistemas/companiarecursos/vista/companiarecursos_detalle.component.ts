import { Component, OnInit, ViewChild } from '@angular/core';
import { CompaniaDetalleMantenimientoComponent } from './companiarecursos_detalle_mantenimiento.component';
import { ActivatedRoute } from '@angular/router';
import { CompaniaRecursosService } from '../servicio/companiarecursos.service';
import { Companyownerrecurso, CompanyownerrecursoPk } from '../dominio/companyownerrecurso';
import { DtoCompanyownerrecurso } from '../dominio/dtocompanyownerrecurso';
import { ConfirmationService } from 'primeng/primeng';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { MaMiscelaneosdetalle } from '../../../shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { EmpleadomastServicio } from '../../../shared/selectores/empleado/servicio/EmpleadomastServicio';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-companiarecursosdetalle',
    templateUrl: './companiarecursos_detalle.component.html'
})
export class CompaniaDetalleComponent extends BaseComponent implements OnInit {
    constructor(
        private rutaActiva: ActivatedRoute,
        private confirmationService: ConfirmationService,
        private empleadomastService: EmpleadomastServicio,
        private companiaRecursosService: CompaniaRecursosService,
        messageService: MessageService
    ) {
        super(messageService);
    }
    listaRecurso: Companyownerrecurso[] = [];
    maMiscelaneoDetalle: MaMiscelaneosdetalle;

    @ViewChild(CompaniaDetalleMantenimientoComponent)
    companiaDetalleMantenimientoComponent: CompaniaDetalleMantenimientoComponent;


    ngOnInit(): void {
        this.maMiscelaneoDetalle = JSON.parse(this.obtenerParametroRutaPorClave(this.rutaActiva, 'dtoMaMiscelaneo'));
        this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
            empleado => {
                this.buscarRecursos(this.maMiscelaneoDetalle.codigoelemento);
            });

    }

    volveraBuscar(companyownerrecurso: Companyownerrecurso) {
        this.buscarRecursos(companyownerrecurso.tiporecurso);
    }

    buscarRecursos(codigo: string) {
        this.companiaRecursosService.obtenerListaRecursosDetalle(codigo).then(
            resp => {
                this.listaRecurso = resp;
            }
        );
    }

    agregarRecurso() {
        this.companiaDetalleMantenimientoComponent.verVentana(this.maMiscelaneoDetalle);
    }

    editar(data: Companyownerrecurso) {
        this.companiaRecursosService.obtenerPorId(data.companyowner, data.tiporecurso, data.periodo).then(
            resp =>
                this.companiaDetalleMantenimientoComponent.editar(resp)
        );
    }

    eliminar(data: Companyownerrecurso) {
        const pk: CompanyownerrecursoPk = new CompanyownerrecursoPk();
        pk.companyowner = data.companyowner;
        pk.periodo = data.periodo;
        pk.tiporecurso = data.tiporecurso;

        this.confirmationService.confirm({
            message: '¿Desea Eliminar el registro?',
            accept: () => {
                this.bloquearPagina();
                this.companiaRecursosService.eliminar(pk).then(res => {
                    this.messageService.clear();
                    this.mostrarMensaje(`Se Eliminó El Recurso ${data.tiporecurso}`, 'info');
                    this.buscarRecursos(data.tiporecurso);
                    this.desbloquearPagina();
                })
                    .catch(errors => {
                        this.messageService.clear();
                        this.desbloquearPagina();
                    });
            }
        });
    }

}


