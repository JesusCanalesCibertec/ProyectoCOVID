import { Component, OnInit, EventEmitter, Output, ViewChild, ChangeDetectorRef } from '@angular/core';
import { DataTable, LazyLoadEvent, ConfirmationService, SelectItem } from 'primeng/primeng';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { DtoEmpleadoBasico } from 'src/app/erp_module/shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { dtoContratacion } from '../../contratacion/dominio/dtoContratacion';
import { ContratacionService } from '../../contratacion/servicio/contratacion.service';
import { Contratacion } from '../../contratacion/dominio/contratacion';
import { ContratacionadendaentregableServicio } from '../servicio/contratacionadendaentregable.service';
import { Contratacionadendaentregable, ContratacionadendaentregablePk } from '../dominio/contratacionadendaentregable';
import { MessageService } from 'primeng/components/common/messageservice';
import { DatePipe } from '@angular/common';
import { dtoPersona } from '../../persona/dominio/dtoPersona';





@Component({
    selector: 'app-contratacionadenda-listado',
    templateUrl: './contratacionadenda-listado.component.html'
})

export class ContratacionadendaListadoComponent extends PrincipalBaseComponent implements OnInit {



    @ViewChild(DataTable) dt: DataTable;
    registrosPorPagina: number = 7;

    idcontratacion: number;
    fechaInicioContrata: Date;
    fechaFinContrata: Date;
    deshabilitar: Boolean = false;


    puedeEditar: Boolean = true;
    areabloquear: Boolean = false;
    verModal: Boolean = false;
    verModalFolio: boolean;
    contratacion: Contratacion = new Contratacion();
    maxDate: Date;
    minDate2: Date;


    @Output() bloquear = new EventEmitter();
    @Output() desbloquear = new EventEmitter();
    @Output() buscarpadre = new EventEmitter();

    Contratacionadendaadenda: Contratacionadendaentregable = new Contratacionadendaentregable();
    ContratacionadendaadendaPk: ContratacionadendaentregablePk = new ContratacionadendaentregablePk();
    titulo: string = null;

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private confirmationService: ConfirmationService,
        private ContratacionadendaentregableServicio: ContratacionadendaentregableServicio,
        private ContratacionService: ContratacionService,
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {


    }

    iniciarComponente(bean: dtoPersona) {
        this.bloquear.emit();
        this.idcontratacion = bean.idContratacion;
        this.fechaInicioContrata = bean.desde;
        this.fechaFinContrata = bean.hasta;
        this.deshabilitar = bean.estado == 'A' || (bean.fechacese == null && bean.idModalidad != 'OS') ? false : true;
        this.titulo = 'Adendas' + ' de ' + bean.nombre;
        this.listarDefecto();

    }

    listarDefecto() {
        this.contratacion.idContratacion = this.idcontratacion;
        this.ContratacionadendaentregableServicio.listado(this.idcontratacion).then(res => {
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
                    
                    if(td.idCodigo == 1){
                        td.fechaminima = this.addDate("d", 1, this.fechaFinContrata);
                    }else{
                        let fechafin = lista.find(obj=> obj.idCodigo == td.idCodigo - 1).fechafin;
                        td.fechaminima = this.addDate("d", 1, fechafin);
                    }
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

        if (this.contratacion.listadetalle.length > 0) {
            if (lst[lst.length - 1].fechainicio == null) {
                this.mostrarMensajeError("La adenda N° " + lst[lst.length - 1].idCodigo + ' requiere una fecha de inicio');
                return;
            }
            if (lst[lst.length - 1].fechafin == null) {
                this.mostrarMensajeError("La adenda N° " + lst[lst.length - 1].idCodigo + ' requiere una fecha de fin');
                return;
            }
            if (lst[lst.length - 1].fechafin >= new Date()) {
                this.mostrarMensajeError("La adenda N° " + lst[lst.length - 1].idCodigo + ' requiere completar su periodo');
                return;
            }
            registro.idContratacion = this.idcontratacion;
            registro.idCodigo = lst[lst.length - 1].idCodigo + 1;
        } else {
            registro.idContratacion = this.idcontratacion;
            registro.idCodigo = 1;
        }

        lst.push(registro);
        lst = this.bloquearUltimo(lst);
        this.contratacion.listadetalle = lst;

    }

    bloquearfecha(data) {
        this.minDate2 = this.toDate(data.fechainicio);
        //this.maxDate = this.toDate(data.fechafin);
    }


    guardar() {
        if (!this.validar()) {
            return;
        }
        this.bloquear.emit();
        this.ContratacionService.registrarlista(this.contratacion).then(resultado => {
            if (resultado != null) {
                this.mostrarMensajeExito(this.getMensajeActualizadoEmpty());
                this.verModal = false;
                this.buscarpadre.emit();
                this.desbloquear.emit();
            }
        });


    }



    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        this.contratacion.listadetalle.forEach(res => {

            if (res.fechainicio == null) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La adenda nro. ' + res.idCodigo + ' requiere de un inicio.' });
                valida = false;
            }
            if (res.fechafin == null) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La adenda nro. ' + res.idCodigo + ' requiere de un fin.' });
                valida = false;
            }
            if (res.fechafin != null && res.fechainicio != null) {
                if (res.fechafin < res.fechainicio) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La adenda nro. ' + res.idCodigo + ' su fecha fin debe ser mayor que la de inicio' });
                    valida = false;
                }
            }
        });

        return valida;
    }



}



