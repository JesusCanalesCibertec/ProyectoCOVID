import { Component, OnInit, ViewChild, ChangeDetectorRef, ViewChildren, AfterContentChecked, Testability } from '@angular/core';
import { SelectItem, LazyLoadEvent, ConfirmationService } from 'primeng/api';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router } from '@angular/router';
import { DataTable } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from '../../../shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { EmpleadomastServicio } from '../../../shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from '../../../shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { FiltroInstitucion } from '../dominio/filtroinstituto';
import { DtoInstitucion } from '../dominio/dtoinstitucion';
import { PsInstitucion, PsInstitucionPk } from '../dominio/PsInstitucion';
import { PsInstitucionServicio } from '../service/PsInstitucionServicio';
import { PsInstitucionAreaListadoComponent } from '../../institucion_area/vista/InstitucionArea-listado..component';
import { UbicacionGeograficaSelectorComponent } from 'src/app/erp_module/shared/selectores/ubicaciongeografica/ubicaciongeografica-selector.component';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { DtoUbigeo } from 'src/app/erp_module/shared/selectores/ubicaciongeografica/dominio/dto/DtoUbigeo';




@Component({
    selector: 'app-name',
    templateUrl: './institucion-listado.component.html'

})

export class PsInstitucionlistadoComponent extends PrincipalBaseComponent implements OnInit, AfterContentChecked {

    @ViewChild(PsInstitucionAreaListadoComponent) PsInstitucionAreaListadoComponent: PsInstitucionAreaListadoComponent;
    @ViewChild(UbicacionGeograficaSelectorComponent) UbicacionGeograficaSelectorComponent: UbicacionGeograficaSelectorComponent;
    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    psInstitucion: PsInstitucion = new PsInstitucion();
    dtoInstitucion: DtoInstitucion;
    psInstitucionPk: PsInstitucionPk;
    ubigeo: DtoTabla = new DtoTabla();

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private router: Router,
        private confirmationService: ConfirmationService,
        private cdref: ChangeDetectorRef,
        private PsInstitucionServicio: PsInstitucionServicio,

        private maMiscelaneosdetalleService: MaMiscelaneosdetalleServicio,
        private empleadomastServicio: EmpleadomastServicio
    ) {
        super(noAuthorizationInterceptor, messageService);
        this.filtro.estado = "A";
    }


    estados: SelectItem[] = [];
  
    filtro: FiltroInstitucion = new FiltroInstitucion();

    paginacion: any = new Object();
    @ViewChild(DataTable) dt: DataTable;

    verModalFolio: boolean;
    puedeEditar: Boolean = true;

    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }



    ngOnInit() {
        this.bloquearPagina();
        super.ngOnInit();
        this.cargarEstados();
       
        this.accion = null;

    }

    cargarEstados() {
        this.estados.push({ label: 'Activo', value: 'A' });
        this.estados.push({ label: 'Inactivo', value: 'I' });
    }

   


    cargarPaginacion(event: LazyLoadEvent) {
        this.bloquearPagina();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;

        this.PsInstitucionServicio.listarPaginacion(this.filtro)
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

    // para abrir a nuestro mantenimiento y poder registrar uno nuevo.


    nuevo() {
        this.accion = this.ACCIONES.NUEVO;
        this.puedeEditar = true;
        this.psInstitucion = new PsInstitucion();
        this.cargarUsuario();
        this.verModalFolio = true;
    }

    fechaactual: Date = null;
    cargarUsuario() {
        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
            res => {
                this.solicitante = res;
                this.psInstitucion.creacionUsuario = res.codigoUsuario;
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
            this.PsInstitucionServicio.registrar(this.psInstitucion).then(

                resultado => {
                    if (resultado != null) {
                        this.mostrarMensajeExito(this.getMensajeGrabado2(resultado.idInstitucion));
                        this.verModalFolio = false;
                        this.dt.reset();
                    }
                });
        } else {
            this.PsInstitucionServicio.solicitudActualizar(this.psInstitucion).then(resultado => {
                this.desbloquearPagina();
                if (resultado != null) {
                    this.mostrarMensajeExito(this.getMensajeActualizado2(resultado.idInstitucion));
                    this.verModalFolio = false;
                    this.dt.reset();
                }
            });
        }
    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        if (this.estaVacio(this.psInstitucion.idInstitucion)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El código de la institución es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.psInstitucion.nombre)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El nombre de la institución es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.psInstitucion.direccion)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La dirección de la institución es requerido' });
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
    editar(bean: DtoInstitucion) {
        this.accion = this.ACCIONES.EDITAR;
        this.puedeEditar = true;
        this.PsInstitucionServicio.obtenerPorId(bean.codigo).then(
            res => {
                this.psInstitucion = res;
            }
        );
        this.verModalFolio = true;
    }

    ver(bean: DtoInstitucion) {
        this.editar(bean);
        this.puedeEditar = false;
        this.accion = this.ACCIONES.VER;
        this.verModalFolio = true;
    }
    // para eliminar con dto de nuestra lista
    anular(bean: DtoInstitucion) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaAnular(),
            accept: () => {
                this.bloquearPagina();
                this.PsInstitucionServicio.solicitudAnular(bean.codigo).then(
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
    }

    mantener(bean: DtoInstitucion) {
        if(bean.estado === 'Inactivo'){
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La institución se encuentra inactiva' });
            return;
        }
        this.PsInstitucionAreaListadoComponent.iniciarComponente(bean);
    }

    mostrarSelectorUbigeo() {
        this.UbicacionGeograficaSelectorComponent.iniciarComponente();

    }

    cargarUbigeo(data: DtoUbigeo) {
        this.UbicacionGeograficaSelectorComponent.salir();

        this.ubigeo.codigo = data.codigoElemento;
        this.ubigeo.descripcion = data.descripcion;

        
    }

    limpiarUbigeo() {
        this.ubigeo = new DtoTabla();
        
    }


}



