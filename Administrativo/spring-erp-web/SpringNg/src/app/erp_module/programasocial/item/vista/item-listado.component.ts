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

import { PsItem } from '../dominio/psItem';
import { PsItemServicio } from '../service/psItemServicio';
import { FiltroItem } from '../dominio/filtroitem';
import { UnidadesmastServicio } from 'src/app/erp_module/shared/unidades/servicio/UnidadesmastServicio';
import { DtoItem } from '../dominio/dtoItem';

@Component({
    selector: 'app-name',
    templateUrl: './item-listado.component.html'
})

export class PsItemlistadoComponent extends PrincipalBaseComponent implements OnInit, AfterContentChecked {

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    psItem: PsItem = new PsItem();

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private router: Router,
        private confirmationService: ConfirmationService,
        private cdref: ChangeDetectorRef,
        private psItemServicio: PsItemServicio,
        private maMiscelaneosdetalleService: MaMiscelaneosdetalleServicio,
        private empleadomastServicio: EmpleadomastServicio,
        private unidadesmastServicio: UnidadesmastServicio,
    ) {
        super(noAuthorizationInterceptor, messageService);
        this.filtro.estado = "A";
    }

    /*arreglos*/
    estados: SelectItem[] = [];
    tipos: SelectItem[] = [];
    unidades: SelectItem[] = [];
    clases: SelectItem[] = [];
    grupos: SelectItem[] = [];
    /*arreglos*/

    filtro: FiltroItem = new FiltroItem();

    paginacion: any = new Object();
    tecla: any;
    @ViewChild(DataTable) dt: DataTable;

    verModalFolio: boolean;
    puedeEditar: Boolean = true;


    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }


    ngOnInit() {
        //this.bloquearPagina();
        super.ngOnInit();
        this.cargarEstados();
        this.cargarTipos();
        this.cargarClases();
        this.cargarGrupos();
        this.cargarUnidades();
        this.accion = null;

    }

    cargarEstados() {
        this.estados.push({ label: 'Activo', value: 'A' });
        this.estados.push({ label: 'Inactivo', value: 'I' });
    }

    /*cargando combos*/
    cargarTipos() {
        this.tipos.push({ label: '--- Seleccione ----', value: '' });
        this.maMiscelaneosdetalleService.listarActivos
            ('PS', 'ITEMTIPO')
            .then(
                td => {
                    td.forEach(obj => this.tipos.push({ label: obj.descripcionlocal, value: obj.codigoelemento.trim() }));
                });
    }
    cargarClases() {
        this.clases.push({ label: '--- Seleccione ----', value: '' });
        this.maMiscelaneosdetalleService.listarActivos
            ('PS', 'ITEMCLASE')
            .then(
                td => {
                    td.forEach(obj => this.clases.push({ label: obj.descripcionlocal, value: obj.codigoelemento.trim() }));
                });
    }
    cargarGrupos() {
        this.grupos.push({ label: '--- Seleccione ----', value: '' });
        this.maMiscelaneosdetalleService.listarActivos
            ('PS', 'ITEMGRUPO')
            .then(
                td => {
                    td.forEach(obj => this.grupos.push({ label: obj.descripcionlocal, value: obj.codigoelemento.trim() }));
                });
    }
    cargarUnidades() {
        this.unidades.push({ label: '--- Seleccione ----', value: '' });
        this.unidadesmastServicio.listarTodos().then(
            td => {
                td.forEach(obj => this.unidades.push({ label: obj.descripcioncorta, value: obj.unidadcodigo.trim() }));
            });
    }
    /*cargando combos*/

    /*listado*/
    cargarPaginacion(event: LazyLoadEvent) {
        this.bloquearPagina();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;

        this.psItemServicio.listarPaginacion(this.filtro)
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
            this.buscar(this.dt)
        }
    }
    /*listado*/

    nuevo() {
        this.accion = this.ACCIONES.NUEVO;
        this.puedeEditar = true;
        this.psItem = new PsItem();
        this.cargarUsuario();
        this.verModalFolio = true;
    }

    fechaactual: Date = null;
    cargarUsuario() {
        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
            res => {
                this.solicitante = res;
                this.psItem.creacionUsuario = res.codigoUsuario;
                this.fechaactual = new Date();
                this.desbloquearPagina();
            }
        );
    }



    guardar() {
        if (!this.validar()) {
            return;
        }
        if (this.accion === this.ACCIONES.NUEVO) {
            this.psItemServicio.registrar(this.psItem).then(

                resultado => {
                    if (resultado != null) {
                        this.mostrarMensajeExito(this.getMensajeGrabado2(resultado.idItem));
                        this.verModalFolio = false;
                        this.dt.reset();
                    }
                });
        } else {
            this.psItemServicio.solicitudActualizar(this.psItem).then(resultado => {
                this.desbloquearPagina();
                if (resultado != null) {
                    this.mostrarMensajeExito(this.getMensajeActualizado2(resultado.idItem));
                    this.verModalFolio = false;
                    this.dt.reset();
                }
            });
        }
    }

    defaultGuardar(event?: any) {
        if (event.keyCode === 13) {
            this.guardar();
        }
    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        if (this.estaVacio(this.psItem.idItem)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El código del item es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.psItem.nombre)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El nombre del item es requerido' });
            valida = false;
        }
        if (this.psItem.idTipoItem === undefined || this.psItem.idTipoItem == '') {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un tipo de item' });
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
    editar(bean: PsItem) {
        this.accion = this.ACCIONES.EDITAR;
        this.puedeEditar = true;
        this.psItemServicio.obtenerPorId(bean.idItem).then(
            res => {
                this.psItem = res;
            }
        );

        this.verModalFolio = true;
    }





    ver(bean: PsItem) {

        this.editar(bean);
        this.puedeEditar = false;
        this.accion = this.ACCIONES.VER;

        this.verModalFolio = true;
    }


    // para eliminar con dto de nuestra lista
    anular(bean: DtoItem) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaAnular(),
            accept: () => {
                this.bloquearPagina();
                this.psItemServicio.solicitudAnular(bean.idItem).then(
                    respose => {
                        this.desbloquearPagina();

                        if (respose != null) {
                            this.messageService.clear();
                            this.messageService.add({
                                severity: 'info', summary: 'Información',
                                detail: this.getMensajeAnulado2(bean.idItem)
                            });
                            this.dt.reset();
                        }

                    }
                );
            }
        });
    }


}



