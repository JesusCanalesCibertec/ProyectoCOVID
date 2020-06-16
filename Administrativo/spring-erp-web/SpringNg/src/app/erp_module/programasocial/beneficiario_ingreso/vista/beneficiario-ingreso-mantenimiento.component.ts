import { Component, OnInit, EventEmitter, Output, ViewChild, ChangeDetectorRef } from '@angular/core';

import { DataTable, LazyLoadEvent,  ConfirmationService, SelectItem } from 'primeng/primeng';

import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from 'src/app/erp_module/shared/selectores/empleado/dominio/DtoEmpleadoBasico';

import { DtoBeneficiario } from '../../beneficiario/dominio/dtoBeneficiario';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
import { PsBeneficiarioIngreso, PsBeneficiarioIngresoPk } from '../../beneficiario/dominio/psbeneficiarioingreso';
import { PsBeneficiarioIngresoService } from '../service/PsBeneficiarioingresoService';
import { PsBeneficiarioIngresoCasual } from '../../beneficiario/dominio/psbeneficiarioingresocasual';
import { MaMiscelaneosdetalle } from 'src/app/erp_module/shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { PopUpMantenimientoMiscelaneoComponent } from 'src/app/erp_module/shared/miscelaneos/vista/miscelaneo-mantenimiento-popup.component';
import { MessageService } from 'primeng/components/common/messageservice';






@Component({
    selector: 'app-beneficiario-ingreso-registro',
    templateUrl: './beneficiario-ingreso-mantenimiento.component.html'
})

export class PsBeneficiarioingresoComponent extends PrincipalBaseComponent implements OnInit {


    @ViewChild(PopUpMantenimientoMiscelaneoComponent) popUpMantenimientoMiscelaneoComponent: PopUpMantenimientoMiscelaneoComponent;
    @ViewChild(DataTable) dataTableComponent: DataTable;

    puedeEditar: Boolean = true;
    verSelector: Boolean = false;
    listado = [];
    estados = [];
    tipos: SelectItem[] = [];
    registrosPorPagina: number = 5;

    /*beans*/
    beanPsBeneficiarioIngresoCasual: PsBeneficiarioIngresoCasual = new PsBeneficiarioIngresoCasual();
    beanPsBeneficiarioIngreso: PsBeneficiarioIngreso = new PsBeneficiarioIngreso();
    /*beans*/

    /*listados*/
    listaDeriva: SelectItem[] = [];
    listaLegal: SelectItem[] = [];
    listaPabellones: SelectItem[] = [];
    listaCausal: SelectItem[] = [];
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
        private psInstitucionAreaServicio: PsInstitucionAreaServicio,
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {

    }

    iniciarComponente(bean: DtoBeneficiario) {

        this.block.emit();
        this.dataTableComponent.reset();
        this.beneficiario = bean.beneficiario;
        this.beanPsBeneficiarioIngreso.idInstitucion = bean.idInstitucion;
        this.beanPsBeneficiarioIngreso.idBeneficiario = bean.idBeneficiario;
        this.tipoNuevo = bean.tipoNuevo;
        this.cargarDerivas();
        this.cargarLegales();
        this.cargarPabellones();
        this.cargarCausales();
        this.limpiar();
        this.beanPsBeneficiarioIngreso.fechaIngreso = new Date();
        this.unBlock.emit();
        this.verSelector = true;

    }

    limpiar() {
        this.beanPsBeneficiarioIngreso.listaCausal = [];
        this.beanPsBeneficiarioIngreso.fechaIngreso = null;
        this.beanPsBeneficiarioIngreso.diasPermanencia = null;
        this.beanPsBeneficiarioIngreso.comentarios = null;
    }



    salir() {
        this.verSelector = false;
    }









    cargarDerivas() {
        this.listaDeriva = [];
        this.listaDeriva.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos('PS', this.tipoNuevo === 1 ? 'INSDAAM' : 'INSDERI')
            .then(r => { r.forEach(rr => { this.listaDeriva.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
    }

    cargarLegales() {
        this.listaLegal = [];
        this.listaLegal.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos('PS', this.tipoNuevo === 1 ? 'SITLEGNIN' : 'SITLEGADU')
            .then(r => { r.forEach(rr => { this.listaLegal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
    }


    cargarPabellones() {
        this.listaPabellones = [];
        this.listaPabellones.push({ label: '-- Seleccione --', value: null });
        this.psInstitucionAreaServicio.listado(this.beanPsBeneficiarioIngreso.idInstitucion)
            .then(r => { r.forEach(rr => { this.listaPabellones.push({ label: rr.nombre, value: rr.idArea }); }); });

    }

    cargarCausales() {
        this.listaCausal = []
        this.listaCausal.push({ label: '-- Seleccione --', value: null });

        if (this.tipoNuevo === 1) {
            this.maMiscelaneosdetalleServicio.listarActivos('PS', 'CINGNNA')
                .then(r => { r.forEach(rr => { this.listaCausal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
        } else if (this.tipoNuevo === 2) {
            this.maMiscelaneosdetalleServicio.listarActivos('PS', 'CINGAAM')
                .then(r => { r.forEach(rr => { this.listaCausal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

        } else if (this.tipoNuevo === 3) {
            this.maMiscelaneosdetalleServicio.listarActivos('PS', 'CINGAAM')
                .then(r => { r.forEach(rr => { this.listaCausal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
            this.maMiscelaneosdetalleServicio.listarActivos('PS', 'CINGNNA')
                .then(r => { r.forEach(rr => { this.listaCausal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
        }
    }





    guardar() {

        if (!this.validar()) {
            return;
        }


        this.PsBeneficiarioingresoService.registrar(this.beanPsBeneficiarioIngreso).then(

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

        if (this.beanPsBeneficiarioIngreso.fechaIngreso == null) {
            this.mostrarMensajeAdvertencia('La fecha de ingreso es requerida');
            valida = false;
        }
        if (this.beanPsBeneficiarioIngreso.idInstitucionDeriva == null) {
            this.mostrarMensajeAdvertencia('La institución que deriva es requerida');
            valida = false;
        }
        if (this.beanPsBeneficiarioIngreso.idSituacionLegal == null) {
            this.mostrarMensajeAdvertencia('La situación legal es requerida');
            valida = false;
        }
        if (this.beanPsBeneficiarioIngreso.listaCausal.length === 0) {
            this.mostrarMensajeAdvertencia('El causal de ingreso es requerido');
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




    eliminarCausal(causal) {

        let lst = [...this.beanPsBeneficiarioIngreso.listaCausal];
        lst = lst.filter(x => x.idCausal != causal);
        this.beanPsBeneficiarioIngreso.listaCausal = lst;
        this.mostrarMensajeInfo(this.getMensajeElilminadoEmpty());

    }

    validarCausal() {
        var valido = true;
        if (this.estaVacio(this.beanPsBeneficiarioIngresoCasual.idCausal)) {
            this.mostrarMensajeAdvertencia('El causal es requerido');
            valido = false;
        }
        if (this.beanPsBeneficiarioIngreso.listaCausal.find(x => x.idCausal == this.beanPsBeneficiarioIngresoCasual.idCausal) != null) {
            this.mostrarMensajeAdvertencia('El causal ya ha sido ingresado');
            valido = false;
        }
        return valido;
    }

    agregarCausal() {

        if (!this.validarCausal()) {
            return;
        }
        const lst = [...this.beanPsBeneficiarioIngreso.listaCausal];

        var bb = new PsBeneficiarioIngresoCasual();
        bb.idCausal = this.beanPsBeneficiarioIngresoCasual.idCausal;
        bb.auxCausal = this.obtenerLabelPorCombo(this.listaCausal, bb.idCausal);
        lst.push(bb);
        this.beanPsBeneficiarioIngreso.listaCausal = lst;
        this.beanPsBeneficiarioIngresoCasual = new PsBeneficiarioIngresoCasual();
        this.mostrarMensajeExito(this.getMensajeAgregadoEmpty());

    }


    listaActualizar: SelectItem[] = [];
    tag: string = "";
    buscarLista(data) {
        var d = data as MaMiscelaneosdetalle;
        this.bloquearPagina();
        this.listaActualizar = [];
        this.listaActualizar.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(d.aplicacioncodigo, d.codigotabla)
            .then(r => {
                r.forEach(rr => { this.listaActualizar.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); });


                switch (this.tag) {
                    case "CAUSAL": {
                        this.beanPsBeneficiarioIngresoCasual.idCausal = null;
                        this.listaCausal = this.listaActualizar;
                        this.beanPsBeneficiarioIngresoCasual.idCausal = d.codigoelemento;
                        this.agregarCausal();
                        break;
                    }

                    case "DERIVA": {
                        this.beanPsBeneficiarioIngreso.idInstitucionDeriva = null;
                        this.listaDeriva = this.listaActualizar;
                        this.beanPsBeneficiarioIngreso.idInstitucionDeriva = d.codigoelemento;
                        break;
                    }
                    default:
                        break;
                }



                this.desbloquearPagina();
            });
    }


    agregarMiscelaneos(codigo, app, tag) {
        const maMiscelaneosdetalle: MaMiscelaneosdetalle = new MaMiscelaneosdetalle();
        maMiscelaneosdetalle.aplicacioncodigo = app;
        maMiscelaneosdetalle.codigotabla = codigo;
        maMiscelaneosdetalle.compania = '999999';
        maMiscelaneosdetalle.estado = 'A';
        this.tag = tag;
        this.popUpMantenimientoMiscelaneoComponent.mostrarVentana(maMiscelaneosdetalle, null);
    }




}



