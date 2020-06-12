import { Component, OnInit, ViewChild, ChangeDetectorRef, ViewChildren, AfterContentChecked, Testability } from '@angular/core';
import { SelectItem, LazyLoadEvent, ConfirmationService } from 'primeng/api';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router } from '@angular/router';
import { DataTable, TRISTATECHECKBOX_VALUE_ACCESSOR } from 'primeng/primeng';
import { EmpleadomastServicio } from '../../../shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from '../../../shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { PsInstitucionperiodo, PsInstitucionperiodoPk } from '../dominio/PsInstitucionperiodo';
import { PsInstitucionperiodoServicio } from '../service/PsInstitucionperiodoServicio';
import { DtoPsInstitucionperiodo } from '../dominio/dtoPsInstitucionperiodo';
import { FiltroPsInstitucionperiodo } from '../dominio/filtroPsInstitucionperiodo';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { InstitucionPeriodoCopiarComponent } from './institucionperiodo-copiar.component';





@Component({
    selector: 'app-name',
    templateUrl: './institucionperiodo-listado.component.html'

})

export class PsInstitucionperiodolistadoComponent extends PrincipalBaseComponent implements OnInit, AfterContentChecked {


    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    PsInstitucionperiodo: PsInstitucionperiodo = new PsInstitucionperiodo();
    dtoPsInstitucionperiodo: DtoPsInstitucionperiodo;
    PsInstitucionperiodoPk: PsInstitucionperiodoPk;

    listado: DtoPsInstitucionperiodo[];


    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private router: Router,
        private confirmationService: ConfirmationService,
        private cdref: ChangeDetectorRef,
        private psInstitucionperiodoServicio: PsInstitucionperiodoServicio,
        private psInstitucionServicio: PsInstitucionServicio,
        private empleadomastServicio: EmpleadomastServicio
    ) {
        super(noAuthorizationInterceptor, messageService);
    }


    instituciones: SelectItem[] = [];
    listaPeriodo: SelectItem[] = [];
    filtro: FiltroPsInstitucionperiodo = new FiltroPsInstitucionperiodo();
    fechaactual: Date = null;

    paginacion: any = new Object();
    @ViewChild(DataTable) dt: DataTable;
    @ViewChild(InstitucionPeriodoCopiarComponent)
    institucionPeriodoCopiarComponent: InstitucionPeriodoCopiarComponent;


    verModalFolio: boolean;
    puedeEditar: Boolean = false;
    admin: Boolean = false;
    validarentrada: boolean = false;

    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }

    ngOnInit() {
        this.bloquearPagina();
        super.ngOnInit();
        const p1 = this.cargarInstituciones();

        Promise.all([p1]).then(f => {
            this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
                empleado => {

                    if(sessionStorage.getItem('modo') == 'I'){
                    this.PsInstitucionperiodo.idInstitucion = empleado.idInstitucion;
                    this.admin = true;
                    }
                   
                }
            );
        });
        
        
       
    }

   

    

    cargarInstituciones() {
        this.instituciones.push({ label: '--Seleccione--', value: null });
        this.psInstitucionServicio.listarInstituciones()
            .then(td => {
                td.forEach(obj => this.instituciones.push({ label: obj.nombre, value: obj.idInstitucion }));
                this.desbloquearPagina();
            });
    }

    


    listarDefecto() {
        this.bloquearPagina();
        if (this.PsInstitucionperiodo.idInstitucion == null || this.PsInstitucionperiodo.idInstitucion === undefined) {
            this.puedeEditar = false;
            this.psInstitucionperiodoServicio.listado(this.PsInstitucionperiodo.idInstitucion, this.PsInstitucionperiodo.idPeriodo)
                .then(pg => {

                    this.listado = pg;
                    /*
                    this.listado.forEach(
                       res=>{
                        this.psInstitucionperiodoServicio.listarPeriodoPorComponente(
                            res.codInstitucion,
                            null,
                            res.codConcepto,
                        ).then(respuesta => {
                            respuesta.forEach(obj => this.listaPeriodo.push({ label: obj.nombre, value: obj.codigo }));
                        });
                       }
                    )*/
                    this.desbloquearPagina();
                });
            return;
        }
        this.puedeEditar = true;

        this.psInstitucionperiodoServicio.listado(this.PsInstitucionperiodo.idInstitucion, this.PsInstitucionperiodo.idPeriodo)
            .then(pg => {

                this.listado = pg;
                this.desbloquearPagina();
            });
    }

    copiarPeriodo() {
        if(this.PsInstitucionperiodo.idInstitucion == null || this.PsInstitucionperiodo.idInstitucion === undefined){
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione una institución' });
            return;
        }
        this.institucionPeriodoCopiarComponent.verVentana(this.PsInstitucionperiodo);
    }

    cargarPeriodo() {
        // this.listaPeriodos.push({ label: '--Seleccione--', value: '' });
        this.psInstitucionperiodoServicio.listarPeriodoPorComponente(
            this.PsInstitucionperiodo.idInstitucion,
            null,
            this.PsInstitucionperiodo.idConcepto,
        ).then(respuesta => {
            respuesta.forEach(obj => this.listaPeriodo.push({ label: obj.nombre, value: obj.codigo }));
        });
    }

    cargarListado() {
        this.listarDefecto();
    }


    buscar() {
        if (!this.validar()) {
            return;
        }
        this.listarDefecto();
    }
   

    // para abrir a nuestro mantenimiento y poder registrar uno nuevo.
    cargarUsuario() {
        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
            res => {
                this.solicitante = res;
                this.PsInstitucionperiodo.creacionUsuario = res.codigoUsuario;
                this.fechaactual = new Date();
                this.desbloquearPagina();
            }
        );
    }

    guardar() {
        this.bloquearPagina();
        this.psInstitucionperiodoServicio.solicitudActualizar(this.listado).then(resultado => {
            if (resultado != null) {
                this.mostrarMensajeExito(this.getMensajeActualizadoEmpty());
                this.listarDefecto();
            }
            this.desbloquearPagina();
        });
    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        if (this.estaVacio(this.PsInstitucionperiodo.idInstitucion)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione una institución' });
            valida = false;
        }
        if (this.estaVacio(this.PsInstitucionperiodo.idPeriodo)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Indique un periodo' });
            valida = false;
        }
       
        return valida;
    }
   
    // para obtener y setear datos del empleado
    editar(bean: DtoPsInstitucionperiodo) {
        this.accion = this.ACCIONES.EDITAR;
        this.puedeEditar = true;
        this.psInstitucionperiodoServicio.obtenerPorId(bean.codConcepto).then(
            res => {
                this.PsInstitucionperiodo = res;
            }
        );
        this.verModalFolio = true;
    }

    cargar(bean: any) {
        const data = bean.data;
        this.PsInstitucionperiodo.idInstitucion = data.codInstitucion;
        this.PsInstitucionperiodo.idPeriodo = data.codPeriodo;
        this.listarDefecto();
     
        this.institucionPeriodoCopiarComponent.salir();
    }


}



