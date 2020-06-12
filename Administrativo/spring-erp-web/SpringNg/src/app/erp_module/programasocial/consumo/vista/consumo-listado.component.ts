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
import { PsConsumo, PsConsumoPk } from '../dominio/psConsumo';
import { FiltroConsumo } from '../dominio/filtroConsumo';
import { UnidadesmastServicio } from 'src/app/erp_module/shared/unidades/servicio/UnidadesmastServicio';
import { DtoConsumo } from '../dominio/dtoConsumo';
import { PsConsumoService } from '../service/PsConsumoService';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';






@Component({
    selector: 'app-name',
    templateUrl: './consumo-listado.component.html'

})

export class PsConsumolistadoComponent extends PrincipalBaseComponent implements OnInit, AfterContentChecked {

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    psConsumo: PsConsumo = new PsConsumo();

    instituciones: SelectItem[] = [];
    estados: SelectItem[] = [];

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private router: Router,
        private confirmationService: ConfirmationService,
        private cdref: ChangeDetectorRef,
        private psConsumoService: PsConsumoService,
        private maMiscelaneosdetalleService: MaMiscelaneosdetalleServicio,
        private empleadomastServicio: EmpleadomastServicio,
        private unidadesmastServicio: UnidadesmastServicio,
        private psInstitucionServicio: PsInstitucionServicio,
    ) {
        super(noAuthorizationInterceptor, messageService);
        this.filtro.estado = 'A';
    }

    /*arreglos*/
    periodos: SelectItem[] = [];
    /*arreglos*/

    filtro: FiltroConsumo = new FiltroConsumo();

    filtroRango: Boolean = false;
    codInstitucion: string;

    paginacion: any = new Object();
    tecla: any;
    cambiarInstitucion: Boolean = false;
    @ViewChild(DataTable) dt: DataTable;

    verModalFolio: boolean;
    puedeEditar: Boolean = true;


    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }


    ngOnInit() {
        // this.bloquearPagina();
        super.ngOnInit();
        this.cargarEstados();
        this.cargarInstituciones();
        this.accion = null;
        this.puedeActivar = sessionStorage.getItem('modo') === 'F';
    }

    cargarPeriodos() {
        this.periodos.push({ label: '--- Todos ----', value: '' });
        this.periodos.push({ label: 'Primer semestre', value: 'S1' });
        this.periodos.push({ label: 'Segundo semestre', value: 'S2' });
    }

    cargarInstituciones() {
        this.instituciones.push({ label: '--Todos--', value: null });
        return this.psInstitucionServicio.listarInstituciones()
            .then(td => {
                td.forEach(obj => this.instituciones.push({ label: obj.nombre, value: obj.idInstitucion }));
                this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
                    r => {
                        this.puedeCambiarInstitucion();
                        this.filtro.codigoInstitucion = r.idInstitucion;
                        this.dt.reset();
                        return 1;
                    }
                );

            });
    }

    puedeCambiarInstitucion() {
        this.psInstitucionServicio.flgSeleccionarInstitucion().then(
            resp => {
                this.cambiarInstitucion = this.flagABoolean(resp.valorFlag);
            }
        );
    }

    cargarEstados() {
        this.estados.push({ label: 'Activo', value: 'A' });
        this.estados.push({ label: 'Inactivo', value: 'I' });
        this.estados.push({ label: 'Cerrado', value: 'C' });
    }


    /*cargando combos*/
    /*
     cargarClases() {
         this.clases.push({ label: '--- Seleccione ----', value: '' });
         this.maMiscelaneosdetalleService.listarActivos
             ('PS', 'ConsumoCLASE')
             .then(
                 td => {
                     td.forEach(obj => this.clases.push({ label: obj.descripcionlocal, value: obj.codigoelemento.trim() }));
                 });
     }
     */
    /*cargando combos*/

    /*listado*/
    cargarPaginacion(event: LazyLoadEvent) {
        this.bloquearPagina();


        if (!this.filtroRango) {
            this.filtro.fechainicial = null;
            this.filtro.fechafinal = null;
        }

        if (this.filtro.fechainicial != null && this.filtro.fechafinal != null) {
            if (this.filtro.fechainicial > this.filtro.fechafinal) {
                this.messageService.clear();
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha hasta debe se mayor o igual que la fecha de nacimiento desde' });
                this.desbloquearPagina();
                return;
            }
        }

        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;

        this.psConsumoService.listarPaginacion(this.filtro)
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



    fechaactual: Date = null;
    cargarUsuario() {
        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
            res => {
                this.solicitante = res;
                this.psConsumo.creacionUsuario = res.codigoUsuario;
                this.fechaactual = new Date();
                this.desbloquearPagina();
            }
        );
    }








    nuevo(codigo: string) {
        if (codigo == null) {
            this.mostrarMensajeAdvertencia('Seleccione una institución');
            return;
        }
        this.router.navigate(['spring/consumo-mantenimiento', codigo], {
            skipLocationChange: true
        });
    }



    editar(bean: DtoConsumo) {
        this.router.navigate(['spring/consumo-mantenimiento',
            bean.codInstitucion, bean.codConsumo, false], {
                skipLocationChange: true
            });
    }


    ver(bean: DtoConsumo) {

        this.router.navigate(['spring/consumo-mantenimiento',
            bean.codInstitucion, bean.codConsumo, true], { skipLocationChange: true });
    }


    // para eliminar con dto de nuestra lista
    anular(bean: DtoConsumo) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaAnular(),
            accept: () => {
                this.bloquearPagina();
                this.psConsumoService.solicitudAnular(bean.codInstitucion, bean.codConsumo).then(
                    respose => {
                        this.desbloquearPagina();

                        if (respose != null) {
                            this.messageService.clear();
                            this.messageService.add({
                                severity: 'info', summary: 'Información',
                                detail: this.getMensajeAnulado(bean.codConsumo)
                            });
                            this.dt.reset();
                        }

                    }
                );
            }
        });
    }

    activar(bean: DtoConsumo) {
        this.bloquearPagina();
        var beanPk = new PsConsumoPk();
        beanPk.idInstitucion = bean.codInstitucion;
        beanPk.idConsumo = bean.codConsumo;

        this.psConsumoService.obtenerPorid(beanPk).then(
            res => {
                this.psConsumo = res;
                this.bloquearPagina();
                this.psConsumo.estado = 'A';
                this.psConsumo.fechaCierre = null;
                this.psConsumoService.solicitudActualizar(this.psConsumo).then(resultado => {
                    this.desbloquearPagina();
                    if (resultado != null) {
                        this.dt.reset();
                        this.mostrarMensajeExito(this.getMensajeActualizado(resultado.idConsumo));
                    }
                });
            });
    }

    cerrar(bean: DtoConsumo) {

        var beanPk = new PsConsumoPk();
        beanPk.idInstitucion = bean.codInstitucion;
        beanPk.idConsumo = bean.codConsumo;

        this.psConsumoService.obtenerPorid(beanPk).then(
            res => {
                this.psConsumo = res;

                this.confirmationService.confirm({
                    header: 'Confirmación',
                    icon: 'fa fa-question-circle',
                    message: this.getMensajePreguntaCerrar(),
                    accept: () => {


                        //abrir popup con comentario y puntuacion
                        this.comentario = "";
                        this.valoracion = 0;

                        this.verModalComentario = true;


                    }

                }
                );
            });
    }

    comentario: string = "";
    valoracion: number = 0;
    verModalComentario: boolean = false;

    puedeActivar: boolean = false;

    cerrarAccionFinal() {
        this.bloquearPagina();
        this.psConsumo.estado = 'C';
        this.psConsumo.fechaCierre = new Date();
        this.psConsumo.comentario = this.comentario;
        this.psConsumo.valoracion = this.valoracion;
        this.verModalComentario = false;
        this.psConsumoService.solicitudActualizar(this.psConsumo).then(resultado => {
            this.desbloquearPagina();
            if (resultado != null) {
                this.verModalComentario = false;
                this.desbloquearPagina();
                this.router.navigate(['spring/consumo-listado']);
                this.dt.reset();
                this.mostrarMensajeExito(this.getMensajeActualizado(resultado.idConsumo));
            }
        });
    }

    exportar(bean: DtoConsumo) {
        this.psConsumoService.exportar(bean)
            .then(respuesta => {
                this.desbloquearPagina();
                if (respuesta) {
                    respuesta = respuesta.replace('../Archivos/', '..' + window.location.pathname + 'Archivos/');
                    window.open(respuesta);
                }
            });
    }
    exportarPDF(bean: DtoConsumo) {
        
    }
}



