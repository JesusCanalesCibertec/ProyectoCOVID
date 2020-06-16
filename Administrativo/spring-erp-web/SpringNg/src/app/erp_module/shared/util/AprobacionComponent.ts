import { DtoSolicitudes } from '../dominio/dto/DtoSolicitudes';
import { SyAprobacionprocesoServicio } from '../../sistemas/aprobaciones/servicio/SyAprobacionprocesoServicio';
import { BaseComponent } from '../../../base_module/components/basecomponent';
import { AprobacionConstante } from '../../sistemas/aprobaciones/constante/AprobacionConstante';
import { DtoRestSolicitudLista } from '../../sistemas/aprobaciones/dominio/dto/DtoRestSolicitudLista';
import { ConfirmationService, DataTable, SelectItem } from 'primeng/primeng';
import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { NoAuthorizationInterceptor } from '../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { MaMiscelaneosdetalleServicio } from '../miscelaneos/servicio/MaMiscelaneosdetalleServicio';

@Component({
    selector: 'aprobacion-preguntas',
    templateUrl: './aprobaciones-preguntas.html'
})
export class AprobacionPreguntasComponent extends BaseComponent {

    dataTableComponent: DataTable;
    seleccionados: DtoSolicitudes[];
    accionActual: string;
    redirect: boolean;

    lstTipoPago: SelectItem[] = [];
    lstMoneda: SelectItem[] = [];

    @Output() reiniciarSeleccionados = new EventEmitter();
    @Output() mostrarMensajeEjecutar = new EventEmitter();

    constructor(
        messageService: MessageService,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
        private syAprobacionprocesoServicio: SyAprobacionprocesoServicio,
        private confirmationService: ConfirmationService,
        private router: Router,

    ) {
        super(messageService);

        this.lstMoneda.push({ label: " -- Seleccione -- ", value: null });
        this.lstTipoPago.push({ label: " -- Seleccione -- ", value: null });

      
    }

    aprobar(seleccionados: DtoSolicitudes[], redirect: boolean) {

        this.seleccionados = seleccionados;
        this.redirect = redirect;
        this.bloquearPagina();

        this.accionActual = AprobacionConstante.SOLICITUD_ACCION_APROBAR;

        this.seleccionados.forEach(
            r => {
                r.flgSeleccionado = 'S';
            }
        )
        var bean = new DtoRestSolicitudLista();
        bean.accion = this.accionActual;
        bean.listaSolicitudes = this.seleccionados;

        this.syAprobacionprocesoServicio.solicitudEventoPreparar(bean).then(res => {
            this.desbloquearPagina();
            this.seleccionados = res;

            this.aux = this.seleccionados[0];
            this.indiceActual = 0;
            this.ejecutarPrimeraPregunta();

        });
    }

    rechazar(seleccionados: DtoSolicitudes[], redirect: boolean) {

        this.seleccionados = seleccionados;
        this.redirect = redirect;

        this.accionActual = AprobacionConstante.SOLICITUD_ACCION_RECHAZAR;

        if (this.seleccionados.length == 0) {
            this.mostrarMensajeAdvertencia("Debe seleccionar un registro para ver su detalle.");
            return null;
        }

        this.bloquearPagina();

        this.seleccionados.forEach(
            r => {
                r.flgSeleccionado = 'S';
            }
        )

        var bean = new DtoRestSolicitudLista();
        bean.accion = this.accionActual;
        bean.listaSolicitudes = this.seleccionados;

        this.syAprobacionprocesoServicio.solicitudEventoPreparar(bean).then(res => {
            this.desbloquearPagina();
            this.seleccionados = res;

            this.aux = this.seleccionados[0];
            this.indiceActual = 0;
            this.ejecutarPrimeraPregunta();

        });
    }


    ejecutarPrimeraPregunta() {

        if (this.aux.flgEsAdministrador == 'S') {
            this.verModalPregunta1 = true;
        }
        else {
            this.ejecutarSegundaPregunta();
        }
    }

    onClickPrimeraPreguntaSi() {
        this.aux.flgAdministradorApruebaTodo = 'S';
        if (this.aux.procesoCodigo == 'PR') {
            this.aux.flgSolicitarInformacionPrestamo = 'S';
        }
        this.ejecutarSegundaPregunta();
    }

    onClickPrimeraPreguntaNo() {
        this.aux.flgAdministradorApruebaTodo = 'N';
        this.ejecutarSegundaPregunta();
    }



    verModalPregunta1: boolean = false;
    verModalPregunta2: boolean = false;
    verModalOtorgaminento: boolean = false;

    ejecutarSegundaPregunta() {

        this.verModalPregunta1 = false;

        if (this.aux.flgTieneCorreos == 'N') {
            this.verModalPregunta2 = true;
        }
        else {
            return this.ejecutarOtorgamiento();
        }

    }

    onClickSegundaPreguntaSi() {
        this.aux.flgEnviarSinCorreos = 'S';
        this.verModalPregunta2 = false;
        this.ejecutarOtorgamiento();
    }

    onClickSegundaPreguntaNo() {
        this.aux.flgEnviarSinCorreos = 'N';
        this.verModalPregunta2 = false;
        this.aux.flgSeleccionado = 'N'
        this.pasarSiguienteDto();
        //this.ejecutarOtorgamiento();
    }

    onClickOtorgamiento() {
        this.verModalOtorgaminento = false;
        this.ejecutarObsevaciones();
    }

    aux: DtoSolicitudes = new DtoSolicitudes();
    indiceActual: number = 0;

    ejecutarOtorgamiento() {

        if (this.aux.flgSolicitarInformacionPrestamo == 'S') {
            //this.aux.prestamoTipoPago = 'C';
            this.aux.prestamoOtorgadoFlag = 'N';
            this.verModalOtorgaminento = true;
        }
        else {
            this.ejecutarObsevaciones();
        }
    }

    ejecutarObsevaciones() {

        this.verModalOtorgaminento = false;
        if (this.aux.flgSolicitarObservaciones == 'S') {
            this.verModalComentario = true;
        }
        else {
            this.pasarSiguienteDto();
        }

    }

    verModalComentario: boolean = false;

    pasarSiguienteDto() {

        this.verModalPregunta1 = false;
        this.verModalPregunta2 = false;
        this.verModalComentario = false;

        this.indiceActual++;
        if (this.indiceActual == this.seleccionados.length) {

            this.aux = new DtoSolicitudes();


            this.bloquearPagina();

            var bean = new DtoRestSolicitudLista();
            bean.accion = this.accionActual;
            bean.listaSolicitudes = this.seleccionados;

            this.syAprobacionprocesoServicio.solicitudEventoEjecutar(bean).then(res => {
                this.mostrarMensajeEjecutarA(res);

                if (this.redirect) {
                    if (res.length === 1) {
                        if (res[0].procesoCodigo === 'SO') {
                            setTimeout(() => { this.router.navigate(['/spring/permisos-listado']); }, 2000);
                        } else {
                            setTimeout(() => { this.router.navigate(['/spring/aprobacion-listado']); }, 2000);
                        }
                    } else {
                        setTimeout(() => { this.router.navigate(['/spring/aprobacion-listado']); }, 2000);
                    }

                } else {
                    this.seleccionados = [];
                    this.reiniciarSeleccionados.emit();
                    this.dataTableComponent.reset();
                }

            });

            return null;
        }

        this.aux = this.seleccionados[this.indiceActual];
        this.ejecutarPrimeraPregunta();
    }

    mostrarMensajeEjecutarA(solicitudes: DtoSolicitudes[]) {

        var bean = new DtoRestSolicitudLista();
        bean.accion = this.accionActual;
        bean.listaSolicitudes = solicitudes;

        this.mostrarMensajeEjecutar.emit(bean);
    }

    evaluarOtorgado() {
        this.aux.prestamoNumeroRecibo = null;
        this.aux.prestamoCuentaBancaria = null;
        this.aux.prestamoNumeroCheque = null;
    }

    evaluarTipoPago() {
        if (this.aux.prestamoTipoPago != 'C') {
            this.aux.prestamoCuentaBancaria = null;
            this.aux.prestamoNumeroCheque = null;
        }
    }



}