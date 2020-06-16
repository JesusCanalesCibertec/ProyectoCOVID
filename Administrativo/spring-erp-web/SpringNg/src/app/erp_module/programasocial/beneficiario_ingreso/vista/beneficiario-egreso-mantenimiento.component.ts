import { Component, OnInit, EventEmitter, Output, ViewChild, ChangeDetectorRef } from '@angular/core';

import { DataTable, LazyLoadEvent, ConfirmationService, SelectItem } from 'primeng/primeng';

import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from 'src/app/erp_module/shared/selectores/empleado/dominio/DtoEmpleadoBasico';

import { DtoBeneficiario } from '../../beneficiario/dominio/dtoBeneficiario';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
import { PsBeneficiarioIngreso, PsBeneficiarioIngresoPk } from '../../beneficiario/dominio/psbeneficiarioingreso';
import { PsBeneficiarioIngresoService } from '../service/PsBeneficiarioingresoService';
import { PsBeneficiarioIngresoDiagnostico } from '../../beneficiario/dominio/psbeneficiarioingresoDiagnostico';
import { MessageService } from 'primeng/components/common/messageservice';






@Component({
    selector: 'app-beneficiario-egreso-registro',
    templateUrl: './beneficiario-egreso-mantenimiento.component.html'
})

export class PsBeneficiarioegresoComponent extends PrincipalBaseComponent implements OnInit {



    @ViewChild(DataTable) dataTableComponent: DataTable;

    puedeEditar: Boolean = true;
    verSelector: Boolean = false;
    listado = [];
    estados = [];
    tipos: SelectItem[] = [];
    registrosPorPagina: number = 5;
    motivo: String;


    /*beans*/
    beanPsBeneficiarioIngresoDiagnostico: PsBeneficiarioIngresoDiagnostico = new PsBeneficiarioIngresoDiagnostico();
    beanPsBeneficiarioIngreso: PsBeneficiarioIngreso = new PsBeneficiarioIngreso();
    /*beans*/

    /*listados*/

    listaMotivos: SelectItem[] = [];
    listaDiagnosticos: SelectItem[] = [];
    /*listados*/

    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @Output() buscarpadre = new EventEmitter();

    PsBeneficiarioIngresoPk: PsBeneficiarioIngresoPk = new PsBeneficiarioIngresoPk();
    beneficiario: string = null;
    tipoNuevo: number = null;
    fechaactual: Date = null;

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private confirmationService: ConfirmationService,
        private cdref: ChangeDetectorRef,
        private PsBeneficiarioingresoService: PsBeneficiarioIngresoService,
        private empleadomastServicio: EmpleadomastServicio,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {

    }

    estado: string = "";

    iniciarComponente(bean: DtoBeneficiario) {

        this.block.emit();
        this.dataTableComponent.reset();
        this.beneficiario = bean.beneficiario;
        this.beanPsBeneficiarioIngreso.idInstitucion = bean.idInstitucion;
        this.beanPsBeneficiarioIngreso.idBeneficiario = bean.idBeneficiario;
        this.estado = bean.estado;

        this.tipoNuevo = bean.tipoNuevo;
        this.cargarMotivos();
        this.cargarDiagnosticos();
        this.obtenerUltimoIngreso();
        this.limpiar();

        

        this.verSelector = true;
        this.unBlock.emit();


    }

    limpiar() {
        this.beanPsBeneficiarioIngreso.listaDiagnostico = [];
        this.beanPsBeneficiarioIngreso.fechaEgreso = null;
        this.beanPsBeneficiarioIngreso.comentariosEgreso = null;
        this.beanPsBeneficiarioIngreso.idMotivoEgreso = null;
    }



    salir() {
        this.verSelector = false;
    }

    obtenerUltimoIngreso() {

        this.PsBeneficiarioIngresoPk.idInstitucion = this.beanPsBeneficiarioIngreso.idInstitucion;
        this.PsBeneficiarioIngresoPk.idBeneficiario = this.beanPsBeneficiarioIngreso.idBeneficiario;

        this.PsBeneficiarioingresoService.obtenerUltimoIngreso(this.PsBeneficiarioIngresoPk)
            .then(respuesta => {

                this.beanPsBeneficiarioIngreso.fechaIngreso = respuesta.fechaIngreso;
                this.beanPsBeneficiarioIngreso.fechaEgreso = respuesta.fechaEgreso;
                if(this.beanPsBeneficiarioIngreso.fechaEgreso == null){
                    this.beanPsBeneficiarioIngreso.fechaEgreso = new Date();
                }
               

            });
    }

    cargar() {
        if (this.beanPsBeneficiarioIngreso.idMotivoEgreso == "DECE") {
            this.beanPsBeneficiarioIngresoDiagnostico.idDiagnostico = null;
            this.beanPsBeneficiarioIngreso.listaDiagnostico = [];
            this.motivo = "DECE";

        }
        else {

            this.motivo = null;
            
        }
    }
    /*****************************del modal***************************************/
    /*
    nuevo() {
        this.bloquearPagina();
        this.accion = this.ACCIONES.NUEVO;
        this.puedeEditar = true;
        this.PsBeneficiarioIngreso = new PsBeneficiarioIngreso();
        // this.PsBeneficiarioIngreso.idProceso = this.idProceso;
      
        this.verModalFolio = true;
        this.desbloquearPagina();
    }
    */



    cargarMotivos() {
        this.listaMotivos = []
        this.listaMotivos.push({ label: '-- Seleccione --', value: null });

        if (this.tipoNuevo == 1) {
            this.maMiscelaneosdetalleServicio.listarActivos('PS', 'NNAEGRES')
                .then(r => {
                    r.forEach(rr => { this.listaMotivos.push({ label: rr.descripcionlocal, value: rr.codigoelemento.trim() }); });
                    if (this.estado == "TEMPORAL") {
                        this.beanPsBeneficiarioIngreso.idMotivoEgreso = "DECE";
                    }
                });
        }
        else if (this.tipoNuevo == 2) {
            this.maMiscelaneosdetalleServicio.listarActivos('PS', 'AAMEGRES')
                .then(r => {
                    r.forEach(rr => { this.listaMotivos.push({ label: rr.descripcionlocal, value: rr.codigoelemento.trim() }); }); if (this.estado == "TEMPORAL") {
                        this.beanPsBeneficiarioIngreso.idMotivoEgreso = "DECE";
                    }
                });

        } else if (this.tipoNuevo == 3) {
            this.maMiscelaneosdetalleServicio.listarActivos('PS', 'NNAEGRES')
                .then(r => {
                    r.forEach(rr => { this.listaMotivos.push({ label: rr.descripcionlocal, value: rr.codigoelemento.trim() }); }); if (this.estado == "TEMPORAL") {
                        this.beanPsBeneficiarioIngreso.idMotivoEgreso = "DECE";
                    }
                });
            this.maMiscelaneosdetalleServicio.listarActivos('PS', 'AAMEGRES')
                .then(r => {
                    r.forEach(rr => { this.listaMotivos.push({ label: rr.descripcionlocal, value: rr.codigoelemento.trim() }); }); if (this.estado == "TEMPORAL") {
                        this.beanPsBeneficiarioIngreso.idMotivoEgreso = "DECE";
                    }
                });
        }
    }

    cargarDiagnosticos() {
        this.listaDiagnosticos = [];
        this.listaDiagnosticos.push({ label: '-- Seleccione --', value: null });
    

    }





    guardar(parametro) {

        if (!this.validar()) {
            return;
        }

        if (this.beanPsBeneficiarioIngreso.listaDiagnostico.length == 0 && parametro == 'FIN') {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Ingrese uno o más diagnósticos' });
            return;
            
        }

        this.beanPsBeneficiarioIngreso.estado = parametro;


        this.PsBeneficiarioingresoService.solicitudActualizar(this.beanPsBeneficiarioIngreso).then(

            resultado => {
                if (resultado != null) {
                    this.mostrarMensajeExito(this.getMensajeGrabadoEmpty());
                    this.buscarpadre.emit();
                    this.verSelector = false;

                }
            });

    }


    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        if (this.beanPsBeneficiarioIngreso.fechaEgreso == null) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de egreso es requerida' });
            valida = false;
        }

        if (this.beanPsBeneficiarioIngreso.idMotivoEgreso == null|| this.beanPsBeneficiarioIngreso.idMotivoEgreso == undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El motivo de egreso es requerido' });
            valida = false;
        }

        

        return valida;
    }


    estaVacio(cadena: string): boolean {
        if (cadena == null) {
            return true;
        }
        if (cadena.trim() === '') {
            return true;
        }
        return false;
    }




    /*
        // para eliminar con dto de nuestra lista
        anular(bean: PsBeneficiarioIngreso) {
            this.confirmationService.confirm({
                header: 'Confirmación',
                icon: 'fa fa-question-circle',
                message: this.getMensajePreguntaAnular(),
                accept: () => {
                    this.bloquearPagina();
                    this.PsBeneficiarioIngresoService.solicitudAnular(bean).then(
                        respose => {
                            this.desbloquearPagina();
    
                            if (respose != null) {
                                this.messageService.clear();
                                this.messageService.add({
                                    severity: 'info', summary: 'Información',
                                    detail: this.getMensajeEliminado(bean.idBeneficiario)
                                });
                                this.listarDefecto();
                            }
    
                        }
                    );
                }
            });
        }*/


    eliminarMotivo(idDiagnostico) {

        let lst = [...this.beanPsBeneficiarioIngreso.listaDiagnostico];
        lst = lst.filter(x => x.idDiagnostico != idDiagnostico);
        this.beanPsBeneficiarioIngreso.listaDiagnostico = lst;
        this.mostrarMensajeInfo(this.getMensajeElilminadoEmpty());

    }

    validarMotivo() {
        var valido = true;
        if (this.beanPsBeneficiarioIngresoDiagnostico.idDiagnostico == null) {
            this.mostrarMensajeAdvertencia('El diagnóstico es requerido');
            valido = false;
        }
        if (this.beanPsBeneficiarioIngreso.listaDiagnostico.find(x => x.idDiagnostico == this.beanPsBeneficiarioIngresoDiagnostico.idDiagnostico) != null) {
            this.mostrarMensajeAdvertencia('El diagnóstico ya ha sido ingresado');
            valido = false;
        }
        return valido;
    }

    agregarDiagnostico() {

        if (!this.validarMotivo()) {
            return;
        }
        const lst = [...this.beanPsBeneficiarioIngreso.listaDiagnostico];

        var bb = new PsBeneficiarioIngresoDiagnostico();
        bb.idDiagnostico = this.beanPsBeneficiarioIngresoDiagnostico.idDiagnostico;
        bb.auxDiagnostico = this.obtenerLabelPorCombo(this.listaDiagnosticos, bb.idDiagnostico);
        lst.push(bb);
        this.beanPsBeneficiarioIngreso.listaDiagnostico = lst;
        this.beanPsBeneficiarioIngresoDiagnostico = new PsBeneficiarioIngresoDiagnostico();
        this.mostrarMensajeExito(this.getMensajeAgregadoEmpty());

    }




}



