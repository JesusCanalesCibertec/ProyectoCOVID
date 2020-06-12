import { Component, OnInit, EventEmitter, Output, ViewChild, ChangeDetectorRef } from '@angular/core';
import { PsInstitucionAreaServicio } from '../servicio/PsInstitucionAreaServicio';
import { DataTable, LazyLoadEvent, ConfirmationService, SelectItem } from 'primeng/primeng';
import { PsInstitucionArea, PsInstitucionAreaPk } from '../dominio/PsInstitucionArea';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from 'src/app/erp_module/shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { DtoInstitucion } from '../../institucion/dominio/dtoinstitucion';
import { MessageService } from 'primeng/components/common/messageservice';





@Component({
    selector: 'app-institucionArea-listado',
    templateUrl: './institucionArea-listado.component.html'
})

export class PsInstitucionAreaListadoComponent extends PrincipalBaseComponent implements OnInit {



    @ViewChild(DataTable) dataTableComponent: DataTable;

    puedeEditar: Boolean = true;
    verSelector: Boolean = false;
    verModalFolio: boolean;
    listado = [];
    estados = [];
    programas: SelectItem[] = [];
    componentes: SelectItem[] = [];
    tipos: SelectItem[] = [];
    registrosPorPagina: number = 5;

    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();

    PsInstitucionArea: PsInstitucionArea = new PsInstitucionArea();
    PsInstitucionAreaPk: PsInstitucionAreaPk = new PsInstitucionAreaPk();
    titulo: string = null;
    idInstitucion: string = null;
    fechaactual: Date = null;

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private confirmationService: ConfirmationService,
        private cdref: ChangeDetectorRef,
        private PsInstitucionAreaService: PsInstitucionAreaServicio,
        private empleadomastServicio: EmpleadomastServicio,
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {
        this.cargarEstados();
        this.cargarProgramas();
        this.cargarComponentes();
    }

    iniciarComponente(bean: DtoInstitucion) {
        this.PsInstitucionArea.idInstitucion = bean.codigo;
        this.idInstitucion = bean.codigo
        this.titulo = bean.nombre;
        this.dataTableComponent.reset();
        this.block.emit();
        this.listarDefecto();
    }

    listarDefecto() {
        this.PsInstitucionAreaService.listado(this.PsInstitucionArea.idInstitucion).then(pg => {

            this.listado = pg;
            this.verSelector = true;
            this.unBlock.emit();

        });
    }

    salir() {
        this.verSelector = false;
    }

    /*****************************del modal***************************************/

    nuevo() {
        this.bloquearPagina();
        this.accion = this.ACCIONES.NUEVO;
        this.puedeEditar = true;
        this.PsInstitucionArea = new PsInstitucionArea();
        // this.PsInstitucionArea.idProceso = this.idProceso;
        this.cargarUsuario();
        this.verModalFolio = true;
        this.desbloquearPagina();
    }

    cargarEstados() {
        this.estados.push({ label: 'Activo', value: 'A' });
        this.estados.push({ label: 'Inactivo', value: 'I' });
    }

    cargarUsuario() {
        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
            res => {
                this.solicitante = res;
                this.PsInstitucionArea.creacionUsuario = res.codigoUsuario;
                this.fechaactual = new Date();
                this.desbloquearPagina();
            }
        );
    }
    cargarProgramas() {
        this.programas = [];
        this.programas.push({ label: '--Seleccione--', value: '' });
       
    }

    cargarComponentes() {
        this.componentes.push({ label: '--Seleccione--', value: '' });
     
    }


    guardar() {

        if (!this.validar()) {
            return;
        }
        this.PsInstitucionArea.idInstitucion = this.idInstitucion;

        if (this.accion === this.ACCIONES.NUEVO) {
            this.PsInstitucionArea.creacionFecha = this.fechaactual;
            this.PsInstitucionAreaService.registrar(this.PsInstitucionArea).then(

                resultado => {
                    if (resultado != null) {
                        this.mostrarMensajeExito(this.getMensajeGrabado2(resultado.idArea));
                        this.verModalFolio = false;
                        this.listarDefecto();
                    }
                });
        } else {
            this.PsInstitucionAreaService.solicitudActualizar(this.PsInstitucionArea).then(resultado => {
                this.desbloquearPagina();
                if (resultado != null) {
                    this.mostrarMensajeExito(this.getMensajeActualizado2(resultado.idArea));
                    this.verModalFolio = false;
                    this.listarDefecto();
                }
            });
        }
    }

    editar(bean: PsInstitucionArea) {

        this.accion = this.ACCIONES.EDITAR;
        this.puedeEditar = true;

        this.PsInstitucionAreaPk = new PsInstitucionAreaPk();
        this.PsInstitucionAreaPk.idInstitucion = this.idInstitucion;
        this.PsInstitucionAreaPk.idArea = bean.idArea;

        this.PsInstitucionAreaService.obtenerPorId(this.PsInstitucionAreaPk).then(
            res => {
                this.PsInstitucionArea = res;
                this.verModalFolio = true;
            }
        );

    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        if (this.estaVacio(this.PsInstitucionArea.idArea)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El código del rol es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.PsInstitucionArea.nombre)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El nombre es requerido' });
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


    ver(bean: PsInstitucionArea) {

        this.editar(bean);
        this.puedeEditar = false;
        this.accion = this.ACCIONES.VER;

    }


    // para eliminar con dto de nuestra lista
    anular(bean: PsInstitucionArea) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaAnular(),
            accept: () => {
                this.bloquearPagina();
                this.PsInstitucionAreaService.solicitudAnular(bean.idInstitucion,bean.idArea).then(
                    respose => {
                        this.desbloquearPagina();

                        if (respose != null) {
                            this.messageService.clear();
                            this.messageService.add({
                                severity: 'info', summary: 'Información',
                                detail: this.getMensajeAnulado2(bean.idArea)
                            });
                            this.listarDefecto();
                        }

                    }
                );
            }
        });
    }





}



