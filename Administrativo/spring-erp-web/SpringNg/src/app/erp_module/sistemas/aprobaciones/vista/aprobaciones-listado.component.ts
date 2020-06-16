import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { SelectItem, DataTable, LazyLoadEvent, ConfirmationService, Message } from 'primeng/primeng';
import { Router, ActivatedRoute } from '@angular/router';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { DtoEmpleadoBasico } from '../../../shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { DtoSolicitudes } from '../../../shared/dominio/dto/DtoSolicitudes';
import { FiltroSolicitudes } from '../dominio/filtro/FiltroSolicitudes';
import { MaMiscelaneosdetalleServicio } from '../../../shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { CompanyownerServicio } from '../../../shared/companyowner/servicio/CompanyownerServicio';
import { SyAprobacionprocesoServicio } from '../servicio/SyAprobacionprocesoServicio';
import { AprobacionConstante } from '../constante/AprobacionConstante';
import { DtoRestSolicitudLista } from '../dominio/dto/DtoRestSolicitudLista';
import { AprobacionPreguntasComponent } from '../../../shared/util/AprobacionComponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { SyAprobacionnivelesServicio } from '../../aprobacionniveles/servicio/SyAprobacionnivelesServicio';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { MessageService } from 'primeng/components/common/messageservice';
import { EmpleadomastSelectorComponent } from '../../../shared/selectores/empleado/vista/empleadomastselector.component';

@Component({
    selector: 'aprobacion-listado',
    templateUrl: './aprobaciones-listado.component.html'
})
export class AprobacionesListadoComponent extends PrincipalBaseComponent implements OnInit {

    @ViewChild(DataTable) dataTableComponent: DataTable;
    @ViewChild(AprobacionPreguntasComponent) aprobacionPreguntasComponent: AprobacionPreguntasComponent;
    @ViewChild(EmpleadomastSelectorComponent) empleadomastSelectorComponent: EmpleadomastSelectorComponent;

    paginacion: ParametroPaginacionGenerico = new ParametroPaginacionGenerico();
    empleado: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    filtro: FiltroSolicitudes = new FiltroSolicitudes();
    companias: SelectItem[] = [];
    lstAplicacion: SelectItem[] = [];
    lstProceso: SelectItem[] = [];
    lstEstado: SelectItem[] = [];
    lstUnidad: SelectItem[] = [];
    estado: string = '';
    unidad: string = '';
    listar: boolean = false;
    seleccionados: DtoSolicitudes[] = [];
    filtroConRango: boolean = false;

    lstMoneda: SelectItem[] = [];
    lstTipoPago: SelectItem[] = [];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
        private companyownerServicio: CompanyownerServicio,
        private syAprobacionnivelesServicio: SyAprobacionnivelesServicio,
        private syAprobacionprocesoServicio: SyAprobacionprocesoServicio,
        private cdref: ChangeDetectorRef,
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService
    ) { super(noAuthorizationInterceptor, messageService); }


    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }
    ngOnInit() {

        super.ngOnInit();

        this.inicializarDatos();
        this.listar = true;
        this.seleccionados = [];

    }

    inicializarDatos(): void {
        this.bloquearPagina();
        this.cargarCompanias();
        this.cargarAplicaciones();
        this.cargarEstados();
        this.cargarUnidadReplicacion();
        this.cargarCombosPrestamo();

    }



    cargarCombosPrestamo() {
    
    }


    cargarUnidadReplicacion() {
        this.lstUnidad.push({ label: ' -- Todos -- ', value: null });
        /*this.syUnidadReplicacionService.listarTodos().then(
            res => {
                res.forEach(r => this.lstUnidad.push({ label: r.descripcionlocal, value: r.pk.unidadreplicacion.trim() }));
                this.unidad = '';
            }
        );*/

    }

    private cargarCompanias(): void {
        this.companias.push({ label: ' -- Todos -- ', value: null });
        this.companyownerServicio.listarActivos()
            .then(comps => {
                comps.forEach(comp => this.companias.push({ label: comp.descripcion, value: comp.codigo }));
                this.desbloquearPagina();
            });
    }

    cargarEstados() {
        this.lstEstado.push({ label: ' -- Todos -- ', value: null });
        /*this.syAprobacionNivelesService.listarEstados().then(
            res => {
                res.forEach(r => this.lstEstado.push({ label: r.descripcionlocal, value: r.pk.codigoelemento.trim() }));
                this.estado = null;
            }
        );*/
    }

    cargarAplicaciones() {
        this.lstAplicacion.push({ label: ' -- Todos -- ', value: null });
        this.syAprobacionnivelesServicio.listarAplicacionPorUsuario().then(
            res => {
                res.forEach(r => this.lstAplicacion.push({ label: r.nombre, value: r.codigo }));
                this.filtro.aplicacion = null;
                this.cargarProcesos();
            }
        );
    }

    cargarProcesos() {
        this.bloquearPagina();
        this.lstProceso = [];
        this.lstProceso.push({ label: ' -- Todos -- ', value: null });
        if (this.filtro.aplicacion == null) {
            this.filtro.proceso = null;
            this.desbloquearPagina();
            return;
        }
        this.syAprobacionprocesoServicio.listarPorAplicacion(this.filtro.aplicacion).then(
            res => {
                res.forEach(r => this.lstProceso.push({ label: r.proceso, value: r.codigoproceso }));
                this.desbloquearPagina();
            }
        );
    }

    buscar(dt: any) {
        let valida: boolean = false;
        if (this.filtro.fechaDesde !== null && this.filtro.fechaDesde !== undefined) {
            valida = true;
        }

        if (this.filtro.fechaHasta !== null && this.filtro.fechaHasta !== undefined) {
            valida = true;
        }

        if (valida) {
            if (this.filtro.fechaDesde > this.filtro.fechaHasta) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha hasta debe se mayor o igual que la fecha desde' });
                return null;
            }
        }

        dt.reset();
    }

    cargarSolicitudes(event: LazyLoadEvent) {
        if (!this.listar) {
            return;
        }
        if (!this.filtroConRango) {
            this.filtro.fechaDesde = null;
            this.filtro.fechaHasta = null;
        }

        this.bloquearPagina();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.filtro.paginacion.atributoOrdenar = 'fechaSolicitud';
        this.syAprobacionprocesoServicio.listarSolicitudes(this.filtro)
            .then(pg => {
                pg.listaResultado = pg.listaResultado.filter(reg => reg.procesoNro != null);
                pg.listaResultado.forEach(r => {
                    r.dataKey = r.documentoReferencia + r.procesoNro;
                });
                this.paginacion = pg;
                this.desbloquearPagina();
            });
    }

    limpiarEmpleado() {
        // this.empleado.personaNombre = '';
        // this.empleado.personaId = null;
        // this.filtro.idPersonaSolicitante = null;

        this.filtro.nombreSolicitante = null;
        this.filtro.idPersonaSolicitante = null;
    }

    // cargarEmpleado(dto: any) {
    //     this.empleado = dto.data;
    //     this.filtro.idPersonaSolicitante = this.empleado.personaId;
    // }

    mostrarSelectorEmpleadomast() {
        this.empleadomastSelectorComponent.tag = 'j';
        this.empleadomastSelectorComponent.estado = '';
        this.empleadomastSelectorComponent.iniciarComponente();
    }

    cargarEmpleado(d: any) {
        const data = d.data;

        if (d.tag === 'j') {
            this.filtro.nombreSolicitante = d.data.personaNombre;
            this.filtro.idPersonaSolicitante = d.data.personaId;
        }
    }

    ver() {
        this.messageService.clear();

        if (this.seleccionados.length !== 1) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccionar un registro' });
            return null;
        }

        const dto = this.seleccionados[0];

        switch (this.seleccionados[0].procesoCodigo) {
            case AprobacionConstante.PROCESO_PLANILLA_PRESTAMO:
                this.router.navigate(['/spring/prestamos-mantenimiento', JSON.stringify(dto)], { skipLocationChange: true });
                break;
            case AprobacionConstante.PROCESO_RRHH_FICHA_PERSONAL:
                this.router.navigate(['/spring/ficha-empleado', JSON.stringify(dto)], { skipLocationChange: true });
                break;
            case AprobacionConstante.PROCESO_ASISTENCIA_SOLICITUDES:
                this.router.navigate(['/spring/permisos-mantenimiento', JSON.stringify(dto)], { skipLocationChange: true });
                break;
            case AprobacionConstante.PROCESO_PLANILLA_VACACION_WEB:
                this.router.navigate(['/spring/solicitudvacaciones-mantenimiento',
                    JSON.stringify(dto)], { skipLocationChange: true });
                break;
            case AprobacionConstante.PROCESO_RRHH_RECLUTAMIENTO:
                this.router.navigate(['/spring/requerimiento-mantenimiento',
                    JSON.stringify(dto)], { skipLocationChange: true });
                break;
            case AprobacionConstante.PROCESO_RRHH_CAPACITACION:
                this.router.navigate(['/spring/capacitacion-mantenimiento',
                    JSON.stringify(dto)], { skipLocationChange: true });
                break;
            default:

                break;
        }
    }

    seleccionarTodos() {
        this.seleccionados = this.paginacion.listaResultado;
        this.aprobacionPreguntasComponent.seleccionados = this.seleccionados;
    }

    aprobar() {

        if (this.seleccionados.length === 0) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccionar un registro' });
            return null;
        }

        this.validarRegistrosAAprobar(' Aprobada ',
            () => {
                if (this.seleccionados.length > 0) {
                    this.aprobacionPreguntasComponent.dataTableComponent = this.dataTableComponent;
                    this.aprobacionPreguntasComponent.aprobar(this.seleccionados, false);
                    this.seleccionados = [];
                } else {
                    this.seleccionados = [];
                }
            }
        );
    }

    validarRegistrosAAprobar(mensaje: string, callBack?: () => void) {
        this.seleccionados.forEach(
            resp => {
                if (resp.procesoAprobar === 51) {
                    const index = this.seleccionados.indexOf(resp);
                    this.messageService.add({
                        severity: 'error', summary: 'Error',
                        detail: 'La ficha de empleado no puede ser  ' + mensaje + ' automáticamente. Presionar el botón VER'
                    });
                    this.seleccionados.splice(index, 1);
                }
            }
        );
        if (callBack) { callBack(); }
    }

    rechazar() {

        if (this.seleccionados.length === 0) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccionar un registro' });
            return null;
        }

        this.validarRegistrosAAprobar(' Rechazada ',
            () => {
                if (this.seleccionados.length > 0) {
                    this.aprobacionPreguntasComponent.dataTableComponent = this.dataTableComponent;
                    this.aprobacionPreguntasComponent.rechazar(this.seleccionados, false);
                    this.seleccionados = [];
                } else {
                    this.seleccionados = [];
                }
            }
        );
    }

    reiniciarSeleccionados() {
        this.seleccionados = [];
    }

    mostrarMensajeEjecutar(bean: DtoRestSolicitudLista) {
        this.seleccionados = [];

        this.messageService.clear();

        bean.listaSolicitudes.forEach(
            solicitud => {
                this.messageService.add({
                    severity: 'info', summary: bean.accion,
                    detail: 'La solicitud ' + solicitud.procesoNro + ' ha sido '
                        + (bean.accion === AprobacionConstante.SOLICITUD_ACCION_APROBAR ? 'aprobada' : 'rechazada')
                });
            }
        );
    }

}
