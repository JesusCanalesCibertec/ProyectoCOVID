import { Component, OnInit, EventEmitter, Output, ViewChild, ChangeDetectorRef } from '@angular/core';
import { DataTable, LazyLoadEvent, ConfirmationService, SelectItem } from 'primeng/primeng';
import { Contratacionadendaentregable, ContratacionadendaentregablePk } from '../dominio/contratacionadendaentregable';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { DtoEmpleadoBasico } from 'src/app/erp_module/shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { ContratacionadendaentregableServicio } from '../servicio/contratacionadendaentregable.service';
import { ContratacionService } from '../../contratacion/servicio/contratacion.service';
import { Contratacion } from '../../contratacion/dominio/contratacion';
import { DatePipe } from '@angular/common';
import { MessageService } from 'primeng/components/common/messageservice';
import { dtoPersona } from '../../persona/dominio/dtoPersona';


@Component({
    selector: 'app-contratacionentregable-listado',
    templateUrl: './contratacionentregable-listado.component.html'
})

export class ContratacionentregableListadoComponent extends PrincipalBaseComponent implements OnInit {



    @ViewChild(DataTable) dt: DataTable;
    registrosPorPagina: number = 7;

    idcontratacion: number;
    fechaInicioContrata: Date;
    fechaFinContrata: Date;
    puedeEditar: Boolean = true;
    verModal: Boolean = false;
    contratacion: Contratacion = new Contratacion();
    diasdiferencia: number;
    deshabilitar: Boolean = false;


    @Output() bloquear = new EventEmitter();
    @Output() desbloquear = new EventEmitter();
    @Output() buscarpadre = new EventEmitter();

    Contratacionadendaentregable: Contratacionadendaentregable = new Contratacionadendaentregable();
    ContratacionadendaentregablePk: ContratacionadendaentregablePk = new ContratacionadendaentregablePk();
    titulo: string = null;

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private confirmationService: ConfirmationService,
        private ContratacionadendaentregableService: ContratacionadendaentregableServicio,
        private ContratacionService: ContratacionService,
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {

    }

    iniciarComponente(bean: dtoPersona) {
        this.bloquear.emit();
        var fecha = new DatePipe('en-US');
        this.idcontratacion = bean.idContratacion;
        this.fechaInicioContrata = bean.desde;
        this.fechaFinContrata = bean.hasta;
        this.diasdiferencia = this.diferenciadias(this.fechaInicioContrata, this.fechaFinContrata);
        this.deshabilitar = bean.estado == 'A'? false: true;
        // this.titulo = 'Contrato ' + bean.idContratacion + ' desde ' +
        //     fecha.transform(this.fechaInicioContrata, 'dd/MM/yyyy') + ' hasta ' + fecha.transform(this.fechaFinContrata, 'dd/MM/yyyy') + ' : Entregables de ' + bean.nombre;

        this.titulo = 'Entregables de ' + bean.nombre +
                      ' desde ' + fecha.transform(this.fechaInicioContrata, 'dd/MM/yyyy') + 
                      ' hasta ' + fecha.transform(this.fechaFinContrata, 'dd/MM/yyyy')
        this.listarDefecto();
    }

    listarDefecto() {
        this.contratacion.idContratacion = this.idcontratacion;
        this.ContratacionadendaentregableService.listado(this.idcontratacion).then(res => {
            this.contratacion.listadetalle = res;
            if (res != null) {
                this.contratacion.listadetalle = this.bloquearUltimo(this.contratacion.listadetalle);
            }
            this.verModal = true;
            this.desbloquear.emit();
        });

    }

    obtenerEstilosEditar(valida: Boolean) {
        if (valida == true) {
            return { 'pointer-events': 'none', opacity: 0.3 };
        } else {
            return { 'pointer-events': '', opacity: 10 };
        }
    }

    bloquearUltimo(lista: Contratacionadendaentregable[]) {
        if (lista.length != 0) {
            lista.forEach(
                td => {
                    td.auxDeshabilitado = true;

                }
            )
            lista[lista.length - 1].auxDeshabilitado = false;
        }
        return lista;
    }



    salir() {
        this.verModal = false;
    }

    borrarFila(parametro: number) {
        if (parametro == null) {
            let lst = [...this.contratacion.listadetalle];
            lst = lst.filter(p => p.idCodigo !== parametro);
            lst = this.bloquearUltimo(lst);
            this.contratacion.listadetalle = lst;
        } else {
            this.confirmationService.confirm({
                header: 'Confirmación',
                icon: 'fa fa-question-circle',
                message: this.getMensajePreguntaEliminar(),
                accept: () => {
                    let lst = [...this.contratacion.listadetalle];
                    lst = lst.filter(p => p.idCodigo !== parametro);
                    lst = this.bloquearUltimo(lst);
                    this.contratacion.listadetalle = lst;
                }
            });
        }
    }

    adicionarFila() {
        let registro = new Contratacionadendaentregable();
        this.contratacion.listadetalle = this.contratacion.listadetalle == null ? [] : this.contratacion.listadetalle;
        let lst = [...this.contratacion.listadetalle];

        let totaldias = 0;
        this.contratacion.listadetalle.forEach(element => {
            totaldias += Number(element.dias);
        });

        if (this.contratacion.listadetalle.length > 0) {
            if (this.contratacion.listadetalle.length > 6) {
                this.mostrarMensajeError("Máximo 6 entregables");
                return;
            }
            registro.idContratacion = this.idcontratacion;
            registro.idCodigo = lst[lst.length - 1].idCodigo + 1;
            registro.dias = this.diasdiferencia > totaldias + 30 ? registro.dias = 30 : this.diasdiferencia - totaldias + 1;
            registro.fechainicio = this.addDate("d", 1, lst[lst.length - 1].fechafin);
            registro.fechafin = this.addDate("d", registro.dias - 1, registro.fechainicio);
            //registro.maximo = this.diasdiferencia > totaldias + 30 ? registro.dias = 30 : this.diasdiferencia - totaldias + 1;

            if (registro.dias <= 0) {
                this.mostrarMensajeError("Cantidad de días de trabajo completados");
                return;
            }
        } else {
            registro.idContratacion = this.idcontratacion;
            registro.idCodigo = 1;
            registro.dias = this.diasdiferencia < 30 ? this.diasdiferencia + 1 : 30;
            //registro.maximo = this.diasdiferencia < 30 ? this.diasdiferencia + 1 : 30;
            registro.fechainicio = this.addDate("d", 1, this.fechaInicioContrata);
            registro.fechafin = this.addDate("d", registro.dias - 1, registro.fechainicio);
        }

        lst.push(registro);
        lst = this.bloquearUltimo(lst);
        this.contratacion.listadetalle = lst;
    }



    calcularango(event) {

        let totaldias = 0;
        this.contratacion.listadetalle.forEach(element => {
            totaldias += Number(element.dias);
        });
 
        if (totaldias > this.diasdiferencia + 1) {
            this.mostrarMensajeError("Cantidad de días de trabajo completados");
            this.contratacion.listadetalle.find(obj=> obj.idCodigo == event.idCodigo).dias = null;
            return;
        }
        event.fechafin = this.addDate("d", event.dias - 1, event.fechainicio);
        this.contratacion.listadetalle.forEach(
            td => {
                if (td.idCodigo > event.idCodigo) {
                    td.fechainicio = this.addDate("d", 1, event.fechafin);
                    td.fechafin = this.addDate("d", td.dias - 1, td.fechainicio);
                }
            }
        )
    }

    guardar() {
        this.bloquearPagina();
        if (!this.validar()) {
            return;
        }
        this.ContratacionService.registrarlista(this.contratacion).then(resultado => {
            if (resultado != null) {
                this.mostrarMensajeExito(this.getMensajeActualizadoEmpty());
                this.buscarpadre.emit();
                this.verModal = false;
            }
            this.desbloquearPagina();
        });
    }



    validar(): boolean {
        let valida = true;
        this.messageService.clear()
        this.contratacion.listadetalle.forEach(res => {
            if (res.dias == null) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El entregable N° ' + res.idCodigo + ' requiere de un número de días.' });
                valida = false;
            }
        });
        return valida;
    }

}



