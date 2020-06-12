import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { SelectItem, LazyLoadEvent, ConfirmationService, DataTable } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsConstante } from '../../psconstantes.ts/psconstantes';
import { BaseComponent, accionSolicitada } from 'src/app/base_module/components/basecomponent';
import { PsFurhService } from '../servicio/psfurh.service';
import { EmpleadomastSelectorComponent } from 'src/app/erp_module/shared/selectores/empleado/vista/empleadomastselector.component';
import { DtoEmpleadoBasico } from 'src/app/erp_module/shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { DtoComponenteFurh } from '../dominio/DtoComponenteFurh';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-furhlistado',
    templateUrl: './psfurh-listado.component.html'
})
export class PsFurhListadoComponent extends BaseComponent implements OnInit {
    constructor(
        private router: Router,
        messageService: MessageService,
        private psFurhService: PsFurhService,
        private cdref: ChangeDetectorRef,
        private confirmationService: ConfirmationService,
        private psInstitucionServicio: PsInstitucionServicio,
        private psInstitucionAreaServicio: PsInstitucionAreaServicio,

        private empleadomastService: EmpleadomastServicio,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
    ) { super(messageService); }

    listaInstitucion: SelectItem[] = [];
    listaArea: SelectItem[] = [];
    listaSentido: SelectItem[] = [];
    listaOrden: SelectItem[] = [];
    listaNivelAcademico: SelectItem[] = [];
    listaEspecialidadAcademica: SelectItem[] = [];
    listaEstados: SelectItem[] = [];
    cambiarInstitucion: Boolean = false;
    exportar: Boolean = false;
    empleado: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    filtro: DtoComponenteFurh = new DtoComponenteFurh();
    @ViewChild(DataTable) dt: DataTable;
    registrosDevolver: Number = 30;

    @ViewChild(EmpleadomastSelectorComponent)
    empleadomastSelectorComponent: EmpleadomastSelectorComponent;

    // tslint:disable-next-line:use-life-cycle-interface
    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }
    cargarOrden() {
        this.listaOrden.push({ label: " -- Seleccione --", value: null });

        this.listaOrden.push({ label: "Nombre Completo						   ", value: 1 });
        this.listaOrden.push({ label: "Fecha Ingreso						   ", value: 2 });
        this.listaOrden.push({ label: "Nivel Académico						   ", value: 3 });
        this.listaOrden.push({ label: "Especialidad Académica				   ", value: 4 });

        this.listaSentido.push({ label: " -- Seleccione --", value: 1 });
        this.listaSentido.push({ label: "Ascendente", value: 1 });
        this.listaSentido.push({ label: "Descendente", value: -1 });
    }
    cargarProgramaArea() {
        this.bloquearPagina();
        this.filtro.idAreaTrabajo = null;
        this.listaArea = [];
        this.listaArea.push({ label: " -- Todos --", value: null });
        this.psInstitucionAreaServicio.listado(this.filtro.idInstitucion).then(
            r => {
                r.forEach(
                    rr => {
                        this.listaArea.push({ label: rr.nombre, value: rr.idArea });
                    }
                );
                this.desbloquearPagina();
            }
        );
    }
    ngOnInit(): void {
        this.listaArea.push({ label: " -- Todos --", value: null });
        this.cargarOrden();
        this.cargarNivelAcademico();
        this.cargarEstados();
        const p1 = this.cargarInstitucion();

        Promise.all([p1]).then(f => {
            this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
                empleado => {
                    this.puedeCambiarInstitucion();
                    this.filtro.idInstitucion = empleado.idInstitucion;
                    this.cargarProgramaArea();
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

    cargarInstitucion(): Promise<number> {
        this.listaInstitucion.push({ label: '-- Seleccione --', value: null });
        return this.psInstitucionServicio.listarInstituciones()
            .then(respuesta => {
                respuesta.forEach(obj => this.listaInstitucion.push({ label: obj.nombre, value: obj.idInstitucion }));
                return 1;
            });
    }

    nuevo() {
        this.router.navigate(['spring/psfurh-mantenimiento'], { skipLocationChange: true });
    }

    cargarNivelAcademico() {
        this.listaNivelAcademico.push({ label: '--Todos--', value: '' });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_NIVEL_ACADEMICO)
            .then(td => {

                for (let i = 0; i < td.length; i++) {
                    this.listaNivelAcademico.push({ label: td[i].descripcionlocal, value: td[i].codigoelemento.trim() });
                }

            });
    }

    obtenerEspecialidadAcademica() {
        if (this.filtro.idNivelAcademico === 'PRO') {
            this.cargarEspecialidadAcedemicaProfesional();
        } if (this.filtro.idNivelAcademico === 'TEC' || this.filtro.idNivelAcademico === 'SEC') {
            this.cargarEspecialidadAcedemicaTecnica();
        } else {
            this.listaEspecialidadAcademica = [];
        }
    }

    cargarEstados() {
        this.listaEstados.push({ label: '--Todos--', value: '' });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_ESTADO)
            .then(td => {
                td.forEach(obj => this.listaEstados.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                this.filtro.estado = 'A';
            });
    }


    cargarEspecialidadAcedemicaProfesional() {
        this.listaEspecialidadAcademica = [];
        this.listaEspecialidadAcademica.push({ label: '--Seleccione--', value: '' });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_ESPECIALIDA_ACADEMICA_PROFESIONAL)
            .then(td => {

                for (let i = 0; i < td.length; i++) {
                    this.listaEspecialidadAcademica.push({ label: td[i].descripcionlocal, value: td[i].codigoelemento.trim() });
                }
            });
    }

    cargarEspecialidadAcedemicaTecnica() {
        this.listaEspecialidadAcademica = [];
        this.listaEspecialidadAcademica.push({ label: '--Seleccione--', value: '' });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_ESPECIALIDA_ACADEMICA_TECNICO)
            .then(td => {

                for (let i = 0; i < td.length; i++) {
                    this.listaEspecialidadAcademica.push({ label: td[i].descripcionlocal, value: td[i].codigoelemento.trim() });
                }
            });
    }

    editar(data: DtoComponenteFurh) {
        this.router.navigate(['spring/psfurh-mantenimiento', data.idEntidad, data.idInstitucion, accionSolicitada.EDITAR], { skipLocationChange: true });
    }

    ver(data: DtoComponenteFurh) {
        this.router.navigate(['spring/psfurh-mantenimiento', data.idEntidad, data.idInstitucion, accionSolicitada.VER], { skipLocationChange: true });
    }


    anular(bean: DtoComponenteFurh) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaAnular(),
            accept: () => {
                this.bloquearPagina();
                this.psFurhService.anular(bean).then(
                    respose => {
                        this.desbloquearPagina();

                        if (respose != null) {
                            this.messageService.clear();
                            this.messageService.add({
                                severity: 'info', summary: 'Información',
                                detail: this.getMensajeAnulado(respose.idEmpleado)
                            });
                            this.dt.reset();
                        }
                    }
                );
            }
        });
    }

    buscar(dt: any) {
        dt.reset();
    }

    cargarFurh(event: LazyLoadEvent) {
        this.bloquearPagina();

        this.filtro.paginacion.listaResultado = [];

        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.psFurhService.listarFurh(this.filtro)
            .then(pg => {
                this.desbloquearPagina();
                this.filtro.paginacion = pg;
                // tslint:disable-next-line:triple-equals
                if (this.filtro.paginacion.registroEncontrados != 0) {
                    this.exportar = true;
                }
            });
    }

    export() {
        this.bloquearPagina();
        this.psFurhService.exportar()
            .then(respuesta => {
                this.desbloquearPagina();
                if (respuesta) {
                    respuesta = respuesta.replace('../Archivos/', '..' + window.location.pathname + 'Archivos/');
                    window.open(respuesta);
                }
            });
    }

    mostrarSelectorEmpleadomast() {
        this.empleadomastSelectorComponent.estado = '';
        this.empleadomastSelectorComponent.iniciarComponente();
    }

    cargarEmpleado(dto: any) {
        this.empleado = dto.data;
        this.filtro.idEmpleado = this.empleado.personaId;
    }

    limpiarEmpleado() {
        this.empleado.personaNombre = '';
        this.empleado.personaId = null;
        this.filtro.idEmpleado = null;
    }
}
