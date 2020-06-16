import { Component, OnInit, ViewChild, ChangeDetectorRef, ViewChildren, AfterContentChecked } from '@angular/core';
import { SelectItem, LazyLoadEvent, ConfirmationService } from 'primeng/api';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router } from '@angular/router';
import { DataTable } from 'primeng/primeng';

import { MaMiscelaneosdetalleServicio } from '../../../shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';

import { EmpleadomastServicio } from '../../../shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from '../../../shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { PsBeneficiario, PsBeneficiarioPk } from '../dominio/psBeneficiario';

import { FiltroBeneficiario } from '../dominio/filtroBeneficiario';
import { PsBeneficiarioServicio } from '../service/psBeneficiarioServicio';
import { PsInstitucionServicio } from 'src/app/erp_module/programasocial/institucion/service/PsInstitucionServicio';
import { PsInstitucion } from '../../institucion/dominio/PsInstitucion';
import { BeneficiarioCapacidadesComponent } from './beneficiario-capacidades.component';
import { PsBeneficiarioegresoComponent } from '../../beneficiario_ingreso/vista/beneficiario-egreso-mantenimiento.component';
import { PsBeneficiarioingresoComponent } from '../../beneficiario_ingreso/vista/beneficiario-ingreso-mantenimiento.component';
import { DtoBeneficiario } from '../dominio/dtoBeneficiario';
import { PsBeneficiarioIngresoListadoComponent } from '../../beneficiario_ingreso/vista/beneficiario-ingreso-listado.component';
import { PsBeneficiarioIngreso, PsBeneficiarioIngresoPk } from '../dominio/psbeneficiarioingreso';
import { PsBeneficiarioIngresoService } from '../../beneficiario_ingreso/service/PsBeneficiarioingresoService';

@Component({
    selector: 'app-name',
    templateUrl: './beneficiario-listado.component.html'

})

export class PsBeneficiariolistadoComponent extends PrincipalBaseComponent implements OnInit, AfterContentChecked {

    @ViewChild(PsBeneficiarioingresoComponent) PsBeneficiarioingresoComponent: PsBeneficiarioingresoComponent;
    @ViewChild(PsBeneficiarioegresoComponent) PsBeneficiarioegresoComponent: PsBeneficiarioegresoComponent;
    @ViewChild(PsBeneficiarioIngresoListadoComponent) PsBeneficiarioIngresoListadoComponent: PsBeneficiarioIngresoListadoComponent;

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    PsBeneficiario: PsBeneficiario = new PsBeneficiario();
    PsBeneficiarioIngreso: PsBeneficiarioIngreso = new PsBeneficiarioIngreso();
    PsBeneficiarioPk: PsBeneficiarioPk;
    cambiarInstitucion: Boolean = false;

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private router: Router,
        private confirmationService: ConfirmationService,
        private cdref: ChangeDetectorRef,
        private psBeneficiarioServicio: PsBeneficiarioServicio,
        private psBeneficiarioIngresoServicio: PsBeneficiarioIngresoService,
        private psInstitucionServicio: PsInstitucionServicio,
        private maMiscelaneosdetalleService: MaMiscelaneosdetalleServicio,
        private PsBeneficiarioingresoService: PsBeneficiarioIngresoService,

        private empleadomastService: EmpleadomastServicio
    ) { super(noAuthorizationInterceptor, messageService); }
    listaEdades: SelectItem[] = [];
    sexos: SelectItem[] = [];
    instituciones: SelectItem[] = [];
    filtro: FiltroBeneficiario = new FiltroBeneficiario();
    institucionesBean: PsInstitucion[] = [];
    idPeriodo: string;

    paginacion: any = new Object();
    @ViewChild(DataTable) dt: DataTable;

    @ViewChild(BeneficiarioCapacidadesComponent) beneCapacidades: BeneficiarioCapacidadesComponent;
    listaSentido: SelectItem[] = [];
    listaOrden: SelectItem[] = [];
    verModalFolio: boolean;
    puedeEditar: Boolean = true;
    filtrofechaNac: Boolean = false;
    filtrofechaReg: Boolean = false;
    listaEstado: SelectItem[] = [];
    listar: boolean = false;

    solonumeros: RegExp = /^[0-9]+$/
    sololetras: RegExp = /^[a-zA-Z\s]+$/

    ngOnInit() {
        this.bloquearPagina();
        super.ngOnInit();
        this.programas = [];
        this.programas.push({ label: '  -- Seleccione --', value: null });
        this.listaEstado.push({ label: '  -- Seleccione --', value: null });
        this.cargarOrden();
        this.cargarSexos();
        this.instituciones.push({ label: '--Todos--', value: '' });
        this.psInstitucionServicio.listarInstituciones()
            .then(td => {
                this.institucionesBean = td;
                td.forEach(obj => {
                    if (obj.estado == 'A') {
                        this.instituciones.push({ label: obj.nombre, value: obj.idInstitucion.trim() })
                    }
                });
                this.maMiscelaneosdetalleService.listarActivos("PS", "BENEESTA").then(
                    r => {
                        this.filtro.estado = null;
                        r.forEach(
                            rr => {
                                this.listaEstado.push({ label: rr.descripcionlocal, value: rr.codigoelemento })
                            }
                        );
                        this.filtro.estado = "ACT";

                        this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
                            empleado => {
                                this.puedeCambiarInstitucion();
                                this.filtro.institucion = empleado.idInstitucion;
                                this.cargarProgramaArea();
                                this.idPeriodo = '201901'; // empleado.periodoActual;
                            }
                        );
                    }
                );
            });
    }

    cargarEdades() {

        this.listaEdades = [];
        this.listaEdades.push({ label: '--Todos--', value: null });

        if (this.filtro.programa == 'NNA') {
            this.listaEdades.push({ label: '0 a 5', value: 1 });
            for (let i = 2; i < 4; i++) {
                this.listaEdades.push({ label: ((i * 5) - 4) + ' a ' + i * 5, value: i });
            }
            this.listaEdades.push({ label: '16 a más', value: 4 });
        }
        if (this.filtro.programa == 'AAM') {
            this.listaEdades.push({ label: '50 a 55', value: 11 });
            for (let i = 12; i < 19; i++) {
                this.listaEdades.push({ label: ((i * 5) - 4) + ' a ' + i * 5, value: i });
            }
            this.listaEdades.push({ label: '91 a más', value: 19 });
        }

    }

    cargarOrden() {
        this.listaOrden.push({ label: " -- Seleccione --", value: null });

        this.listaOrden.push({ label: "Nombre Completo						   ", value: 1 });
        this.listaOrden.push({ label: "Edad									   ", value: 2 });
        this.listaOrden.push({ label: "Sexo									   ", value: 3 });

        this.listaSentido.push({ label: " -- Seleccione --", value: 1 });
        this.listaSentido.push({ label: "Ascendente", value: 1 });
        this.listaSentido.push({ label: "Descendente", value: -1 });
    }


    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }

    puedeCambiarInstitucion() {
        this.psInstitucionServicio.flgSeleccionarInstitucion().then(
            resp => {
                this.cambiarInstitucion = this.flagABoolean(resp.valorFlag);
            }
        );
    }

    irASalud(dto: DtoBeneficiario) {
     
    }

    mostrarSocioAmbiental(dto: DtoBeneficiario) {
        dto.idPeriodo = this.idPeriodo;
    }



    mostrarCapaciadades(dto: DtoBeneficiario) {
        dto.idPeriodo = this.idPeriodo;
        this.beneCapacidades.verVentana(dto);
    }

    mostrarNutricion(dto: DtoBeneficiario) {
        dto.idPeriodo = this.idPeriodo;
    }


    cargarSexos() {
        this.sexos.push({ label: '--Todos--', value: '' });
        this.sexos.push({ label: 'Masculino', value: 'M' });
        this.sexos.push({ label: 'Femenino', value: 'F' });
    }

    cargarInstituciones() {
        this.instituciones.push({ label: '--Todos--', value: '' });
        this.psInstitucionServicio.listarInstituciones()
            .then(td => {
                this.institucionesBean = td;
                td.forEach(obj => {
                    if (obj.estado == 'A') {
                        this.instituciones.push({ label: obj.nombre, value: obj.idInstitucion.trim() })
                    }
                });
            });
    }



    cargarPaginacion(event: LazyLoadEvent) {

        if (!this.listar) {
            return;
        }

        this.bloquearPagina();

        if (!this.filtrofechaNac) {
            this.filtro.desdeNac = null;
            this.filtro.hastaNac = null;
        }

        if (this.filtro.hastaNac != null && this.filtro.desdeNac != null) {
            if (this.filtro.desdeNac > this.filtro.hastaNac) {
                this.messageService.clear();
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de nacimiento hasta debe se mayor o igual que la fecha de nacimiento desde' });
                this.desbloquearPagina();
                return;
            }
        }

        if (!this.filtrofechaReg) {
            this.filtro.desdeReg = null;
            this.filtro.hastaReg = null;
        }

        if (this.filtro.hastaReg != null && this.filtro.desdeReg != null) {
            if (this.filtro.desdeReg > this.filtro.hastaReg) {
                this.messageService.clear();
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de registro hasta debe se mayor o igual que la fecha de registro desde' });
                this.desbloquearPagina();
                return;
            }
        }

        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        sessionStorage.setItem("filtroBeneficiario", JSON.stringify(this.filtro));
        this.psBeneficiarioServicio.listarPaginacion(this.filtro)
            .then(res => {
                this.filtro.paginacion = res;
                this.desbloquearPagina();
            });
    }

    buscar(dt: any) {
        dt.reset();
    }

    defaultBuscar(event?: any) {
        if (event.keyCode === 13) {
            this.buscar(this.dt);
        }
    }


    nuevo() {

        if (this.estaVacio(this.filtro.institucion)) {
            this.mostrarMensajeAdvertencia("Seleccionar una institución");
            return;
        }

        this.tipos = [];
        this.tipoNuevo = null;

        var beanInstitucion = this.institucionesBean.find(x => x.idInstitucion == this.filtro.institucion);

        
    }

    verModalSeleccion: boolean = false;
    tipoNuevo: number = null;
    tipos: SelectItem[] = [];

    seleccionar() {
        if (this.tipoNuevo == null) {
            this.mostrarMensajeAdvertencia("Seleccione el tipo de beneficiario");
            return;
        }
        this.router.navigate(['spring/beneficiario-mantenimiento', this.tipoNuevo, this.filtro.institucion, 0, 1], { skipLocationChange: true });
    }

    editar(dto) {
        this.router.navigate(['spring/beneficiario-mantenimiento', dto.tipoNuevo, dto.idInstitucion, dto.idBeneficiario, 1], { skipLocationChange: true });
    }
    ver(dto) {
        this.router.navigate(['spring/beneficiario-mantenimiento', dto.tipoNuevo, dto.idInstitucion, dto.idBeneficiario, 0], { skipLocationChange: true });
    }

    reingresar(bean: DtoBeneficiario) {


        if (bean.estado == "ANULADO") {
            this.mostrarMensajeAdvertencia("El beneficiario se encuentra anulado");
            return;
        }
        if (bean.estado == "ACTIVO") {
            this.mostrarMensajeAdvertencia("El beneficiario se encuentra activo");
            return;
        }
        if (bean.estado == "TEMPORAL") {
            this.mostrarMensajeAdvertencia("El beneficiario se encuentra temporal");
            return;
        }

        this.PsBeneficiarioingresoComponent.iniciarComponente(bean);




    }

    egresar(bean: DtoBeneficiario) {

        if (bean.estado == "ANULADO") {
            this.mostrarMensajeAdvertencia("El beneficiario se encuentra anulado");
            return;
        }
        if (bean.estado == "EGRESADO") {
            this.mostrarMensajeAdvertencia("El beneficiario se encuentra egresado");
            return;
        }

        this.PsBeneficiarioegresoComponent.iniciarComponente(bean);
    }

    historial(bean: DtoBeneficiario) {
        this.PsBeneficiarioIngresoListadoComponent.iniciarComponente(bean);
    }

    anular(bean: DtoBeneficiario) {
        this.bloquearPagina();
        this.psBeneficiarioServicio.anularBeneficiario(bean).then(
            r => {
                this.mostrarMensajeInfo(this.getMensajeAnuladoNombre(bean.beneficiario));
                this.dt.reset();
            }
        );
    }

    export() {
        this.bloquearPagina();
        this.psBeneficiarioServicio.exportar()
            .then(respuesta => {
                this.desbloquearPagina();
                if (respuesta) {
                    respuesta = respuesta.replace("../Archivos/", ".." + window.location.pathname + "Archivos/");
                    window.open(respuesta);
                }
            });
    }


    cargarProgramaArea() {
        this.bloquearPagina();
        this.filtro.programa = null;
        this.programas = [];
        this.programas.push({ label: '  -- Seleccione --', value: null });
       
    }
    programas: SelectItem[] = [];

    // para abrir a nuestro mantenimiento y poder registrar uno nuevo.

    /*
    cargarUsuario() {
        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
            res => {
                this.solicitante = res;
                this.desbloquearPagina();
            }
        );
    }

    nuevo() {
        this.accion = this.ACCIONES.NUEVO;
        this.puedeEditar = true;
        this.PsBeneficiario = new PsBeneficiario();
        this.limpiar();
        this.verModalFolio = true;
    }

    limpiar() {
        this.filtro.codigo = null;
        this.filtro.nombre = null;
    }

    guardar() {
        if (!this.validar()) {
            return;
        }
        if (this.accion === this.ACCIONES.NUEVO) {
            this.psBeneficiarioServicio.registrar(this.PsBeneficiario).then(

                resultado => {
                    if (resultado != null) {
                        this.mostrarMensajeExito(this.getMensajeGrabado2(resultado.idBeneficiario));
                        this.verModalFolio = false;
                        this.dt.reset();
                    }
                });
        } else {
            this.psBeneficiarioServicio.solicitudActualizar(this.PsBeneficiario).then(resultado => {
                this.desbloquearPagina();
                if (resultado != null) {
                    this.mostrarMensajeExito(this.getMensajeActualizado2(resultado.idBeneficiario));
                    this.verModalFolio = false;
                    this.dt.reset();
                }
            });
        }
    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        if (this.estaVacio(this.PsBeneficiario.idBeneficiario)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El código es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.PsBeneficiario.nombre)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El nombre es requerido' });
            valida = false;
        }
        if (this.PsBeneficiario.idPrograma === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un programa' });
            valida = false;
        }
        if (this.PsBeneficiario.idComponente === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un componente' });
            valida = false;
        }
        if (this.estaVacio(this.PsBeneficiario.descripcion)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La descripción es requerida' });
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


    // para obtener y setear datos del empleado
    editar(bean: DtoBeneficiario) {
        this.accion = this.ACCIONES.EDITAR;
        this.puedeEditar = true;
        this.psBeneficiarioServicio.obtenerPorId(bean.codigo).then(
            res => {
                this.PsBeneficiario = res;
            }
        );
        this.limpiar();
        this.verModalFolio = true;
    }





    ver(bean: DtoBeneficiario) {

        this.editar(bean);
        if (this.PsBeneficiario.modificacionUsuario == null) {
            this.PsBeneficiario.modificacionUsuario = 'Sin cambios';

        }
        this.puedeEditar = false;
        this.accion = this.ACCIONES.VER;
        this.limpiar();
        this.verModalFolio = true;
    }


    // para eliminar con dto de nuestra lista
    anular(bean: DtoBeneficiario) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaAnular(),
            accept: () => {
                this.bloquearPagina();
                this.psBeneficiarioServicio.solicitudAnular(bean).then(
                    respose => {
                        this.desbloquearPagina();

                        if (respose != null) {
                            this.messageService.clear();
                            this.messageService.add({
                                severity: 'info', summary: 'Información',
                                detail: this.getMensajeEliminado2(bean.codigo)
                            });
                            this.dt.reset();
                        }

                    }
                );
            }
        });
    }*/


}



