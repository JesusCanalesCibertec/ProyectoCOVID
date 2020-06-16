import { DtoCapacidad } from './../dominio/dtocapacidad';
import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import {  SelectItem, LazyLoadEvent } from 'primeng/primeng';
import { BaseComponent } from 'src/app/base_module/components/basecomponent';
import { FiltroCapacidades } from '../dominio/FiltroCapacidades';
import { Table } from 'primeng/table';
import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { PsCapacidadesService } from '../servicio/pscapacidades.service';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsConstante } from '../../psconstantes.ts/psconstantes';
import { BarthelComponent } from './pscapacidad-barthel.component';
import { KatzComponent } from './pscapacidad-katz.component';
import { PsCapacidadComentarioComponent } from './pscapacidad-comentario.component';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-capacidades',
    templateUrl: './pscapacidades-listado.component.html'
})
export class PsCapacidadesListadoComponent extends BaseComponent implements OnInit {
    constructor(
        messageService: MessageService,
        private cdref: ChangeDetectorRef,
        private psInstitucionAreaServicio: PsInstitucionAreaServicio,
        private empleadomastService: EmpleadomastServicio,
        private psInstitucionServicio: PsInstitucionServicio,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
        private psCapacidadesService: PsCapacidadesService,
    ) { super(messageService); }

    filtro: FiltroCapacidades = new FiltroCapacidades();
    sexos: SelectItem[] = [];
    listaInstitucion: SelectItem[] = [];
    listaResidencia: SelectItem[] = [];

    listaRiesgoCaida: SelectItem[] = [];
    listaRiesgoCaidaDetalle: SelectItem[] = [];

    listaHabilidadesOcupacionales: SelectItem[] = [];
    listaTipoInstitucion: SelectItem[] = [];
    listaNivel: SelectItem[] = [];
    listaGradoAcademico: SelectItem[] = [];
    listaCapacodadPromedio: SelectItem[] = [];
    listaTalleresPromedio: SelectItem[] = [];
    listaTipoComunicacion: SelectItem[] = [];
    listaPrograma: SelectItem[] = [];
    verGrado: Boolean = false;
    verNivel: Boolean = false;
    verListaProgramas: Boolean = false;


    @ViewChild(BarthelComponent)
    barthelComponent: BarthelComponent;

    @ViewChild(KatzComponent)
    katzComponent: KatzComponent;

    @ViewChild(PsCapacidadComentarioComponent)
    psCapacidadComentarioComponent: PsCapacidadComentarioComponent;

    cambiarInstitucion: Boolean = false;
    mostrarNinos: Boolean = false;
    mostrarAdultos: Boolean = false;
    tituloColumna: string;
    verBotonDependencia: Boolean = false;

    registroSeleccionado: DtoCapacidad = new DtoCapacidad();

    ngOnInit(): void {
        this.bloquearPagina();
        this.filtro.cantidadRegistros = 20;
        this.filtro.idComponente = 'CAPA';
        this.cargarSexos();

        const p1 = this.cargarInstitucion();
        const p2 = this.cargarRiesgoCaida();
        const p9 = this.cargarRiesgoCaidaDetalle();

        const p3 = this.cargarHabilidadesOcupacionales();
        const p4 = this.cargarTipoInstitucion();
        const p5 = this.cargarNivel();
        const p6 = this.cargarCapacidadPromedio();
        const p7 = this.cargarTalleresPromedio();
        const p8 = this.cargarTipoComunicacion();

        Promise.all([p1, p2, p3, p4, p5, p6, p7, p8, p9]).then(f => {
            this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
                empleado => {
                    this.filtro.idInstitucion = empleado.idInstitucion;
                    this.cargarPeriodo();
                    this.puedeCambiarInstitucion();
                    this.cargarPrograma();
                }
            );
        });
    }

    onChangeInstitucion(value: any, tb?: any) {
        this.listaPrograma = [];
        this.filtro.idInstitucion = value;
        this.filtro.idPrograma = null;
        
    }


    cargarPeriodo() {
        this.psInstitucionServicio.listarPeriodo(
            this.filtro.idInstitucion,
            this.filtro.idPrograma,
            this.filtro.idComponente,
            this.filtro.idUsuario,
            this.filtro.idBeneficiario
        ).then(respuesta => {
            if (respuesta.length > 0) {
                this.filtro.periodo = respuesta[0].codigo;
            } else {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No existe periodo habilitado para este componente' });
            }

        });
    }

    cargarInstitucion(): Promise<number> {
        this.listaInstitucion.push({ label: '-- Seleccione --', value: null });
        return this.psInstitucionServicio.listarInstituciones()
            .then(respuesta => {
                respuesta.forEach(obj => this.listaInstitucion.push({ label: obj.nombre, value: obj.idInstitucion }));
                return 1;
            });
    }

    cargarResidencia() {
        this.listaResidencia = [];
        this.listaResidencia.push({ label: '-- Seleccione --', value: null });
        this.psInstitucionAreaServicio.listadoPorPrograma(this.filtro.idInstitucion,
            this.filtro.idPrograma)
            .then(respuesta => {
                respuesta = respuesta.filter(x => x.idComponente == '' || x.idComponente == null);

                respuesta.forEach(obj => this.listaResidencia.push({ label: obj.nombre, value: obj.idArea }));
            });
    }

    cargarRiesgoCaida() {
        this.listaRiesgoCaida.push({ label: '-- Seleccione --', value: null });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_TABLA_SINO)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaRiesgoCaida.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });
    }
    cargarRiesgoCaidaDetalle() {
        this.listaRiesgoCaidaDetalle.push({ label: '-- Seleccione --', value: null });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, "RIESGOCA")
            .then(respuesta => {
                respuesta.forEach(obj => this.listaRiesgoCaidaDetalle.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });
    }

    cargarTipoComunicacion(): Promise<number> {
        this.listaTipoComunicacion.push({ label: '-- Seleccione --', value: null });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_TIPO_COMUNICACION)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaTipoComunicacion.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });
    }

    cargarHabilidadesOcupacionales() {
        this.listaHabilidadesOcupacionales.push({ label: '-- Seleccione --', value: null });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_HABILIDADES_OCUPACIONALES)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaHabilidadesOcupacionales.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });
    }


    cargarTipoInstitucion() {
        this.listaTipoInstitucion.push({ label: '-- Seleccione --', value: null });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_TIPO_INSTITUCION)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaTipoInstitucion.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });

    }

    cargarCapacidadPromedio() {
        this.listaCapacodadPromedio.push({ label: '-- Seleccione --', value: null });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_CAPACIDAD_PROMEDIO)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaCapacodadPromedio.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });
    }


    cargarTalleresPromedio() {
        this.listaTalleresPromedio.push({ label: '-- Seleccione --', value: null });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_TALLERES_PROMEDIO)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaTalleresPromedio.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });
    }

    cargarNivel() {
        this.listaNivel.push({ label: '-- Seleccione --', value: null });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_NIVEL)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaNivel.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });

    }

    mostrarNombres() {
        if (this.filtro.idPrograma === 'AAM') {
            this.mostrarAdultos = true;
            this.mostrarNinos = false;
            this.tituloColumna = 'Dependencia Katz';
            this.verBotonDependencia = false;
        } else {
            this.mostrarNinos = true;
            this.mostrarAdultos = false;
            this.tituloColumna = 'Dependencia Barthel';
            this.verBotonDependencia = true;
        }
    }

    onchangePrograma(tb?: any) {
        this.mostrarNombres();
        this.buscar(tb);
        this.cargarResidencia();
    }

    cargarPrograma() {
        
    }

    onChangeGrado(rowData) {
        this.psCapacidadesService.calcularAniosRetraso(rowData).then(
            resp => {
                rowData.anioRetraso = resp.anioRetraso;
            }
        );
    }

    onChangeNivel(value: any) {
        if (value === 'INIC') {
            this.cargarGradoAcademicoInicial();
        } else if (value === 'PRIM') {
            this.cargarGradoAcademicoPrimaria();
        } else if (value === 'SECU') {
            this.cargarGradoAcademicoSecundaria();
        } else {
            this.verGrado = true;
            this.listaGradoAcademico = [];
        }
    }

    onChangeTipoInstitucion(value: any) {
        if (value === 'NIN') {
            this.verGrado = true;
            this.verNivel = true;
            this.listaGradoAcademico = [];
        } else {
            this.verGrado = false;
            this.verNivel = false;
        }
        this.cargarResidencia();
    }

    cargarGradoAcademicoInicial() {
        this.verGrado = false;
        this.listaGradoAcademico = [];
        this.listaGradoAcademico.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_GRADO_ACADEMICO_INICIAL)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaGradoAcademico.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
            });
    }

    cargarGradoAcademicoPrimaria() {
        this.verGrado = false;
        this.listaGradoAcademico = [];
        this.listaGradoAcademico.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_GRADO_ACADEMICO_PRIMARIA)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaGradoAcademico.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
            });
    }

    cargarGradoAcademicoSecundaria() {
        this.verGrado = false;
        this.listaGradoAcademico = [];
        this.listaGradoAcademico.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_GRADO_ACADEMICO_SECUNDARIA)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaGradoAcademico.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
            });
    }

    // tslint:disable-next-line:use-life-cycle-interface
    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }

    mostrarComentario(rowData, tipo: string) {
        this.registroSeleccionado = rowData;
        this.psCapacidadComentarioComponent.verVentana(rowData, tipo, 'nuevo');
    }

    onchangeBuscar(tb?: any) {
        this.buscar(tb);
    }


    preBuscar(event?: any, tb?: any) {
        if (event.keyCode === 13) {
            this.buscar(tb);
        }
    }

    buscar(data: Table) {
        if (this.filtro.idPrograma === null || this.filtro.idPrograma === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Seleccionar un Programa' });
            return;
        }

        if (this.filtro.periodo === null || this.filtro.periodo === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No existe periodo habilitado para este componente' });
            return;
        }

        this.mostrarNombres();
        data.reset();
    }

    puedeCambiarInstitucion() {
        this.psInstitucionServicio.flgSeleccionarInstitucion().then(
            resp => {
                this.cambiarInstitucion = this.flagABoolean(resp.valorFlag);
            }
        );
    }

    onMouseEnter(rowData): void {
        rowData.hover = true;
    }

    onMouseLeave(rowData): void {
        rowData.hover = false;
    }

    cargarListaEvent(event: LazyLoadEvent) {
        this.bloquearPagina();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = this.filtro.cantidadRegistros; // event.rows;
        this.psCapacidadesService.listarPaginacion(this.filtro)
            .then(res => {
                this.filtro.paginacion = res;
                this.desbloquearPagina();
            });
    }

    mostrarDependenciaBerthel(rowData) {
        this.registroSeleccionado = rowData;
        this.barthelComponent.verVentana(rowData);
    }

    obtenerBarthel(rowData) {
        this.registroSeleccionado.gradoDependenciaBarthel = rowData.gradoDependenciaBarthel;
        this.registroSeleccionado.porcentajeGradoBarthel = rowData.porcentajeGradoBarthel;
        const lista = [...this.filtro.paginacion.listaResultado];
        lista[this.findSelectedCapacidadIndex()] = this.registroSeleccionado;
        this.filtro.paginacion.listaResultado = lista;

        this.onMouseEnter(rowData);
    }

    obtenerKatz(rowData) {
        this.registroSeleccionado.gradoDependenciaKatz = rowData.gradoDependenciaKatz;
        this.registroSeleccionado.porcentajeGradoKatz = rowData.porcentajeGradoKatz;
        const lista = [...this.filtro.paginacion.listaResultado];
        lista[this.findSelectedCapacidadIndex()] = this.registroSeleccionado;
        this.filtro.paginacion.listaResultado = lista;

        this.onMouseEnter(rowData);
    }

    findSelectedCapacidadIndex(): number {
        return this.filtro.paginacion.listaResultado.indexOf(this.registroSeleccionado);
    }


    mostrarDependenciaKatz(rowData) {
        this.registroSeleccionado = rowData;
        this.katzComponent.verVentana(rowData);
    }

    guardar(rowData, tb?: any) {
        this.registroSeleccionado = rowData;
        if (this.mostrarNinos) {
            rowData.ValidarNinos = true;
        } else {
            rowData.ValidarNinos = false;
        }

        // Es nuevo
        if (rowData.idCapacidad === undefined || rowData.idCapacidad === null) {
            rowData.estado = 'A';
            rowData.idInstitucion = this.filtro.idInstitucion;
            rowData.idPeriodo = this.filtro.periodo;
            rowData.idOrigen = PsConstante.MISCELANEO_TIPO_EVALUACION_PERIODICA;
            this.psCapacidadesService.registrar(rowData).then(
                resp => {
                    if (resp != null) {
                        this.messageService.clear();
                        this.messageService.add({
                            severity: 'success', summary: 'Mensaje',
                            detail: 'Se Registró Información de Capacidades para el Beneficiario : ' + rowData.nombreCompleto
                        });
                        rowData.hover = false;
                        this.registroSeleccionado.idCapacidad = resp.idCapacidad;
                        this.registroSeleccionado.evaluado = resp.evaluado;
                        const lista = [...this.filtro.paginacion.listaResultado];
                        lista[this.findSelectedCapacidadIndex()] = this.registroSeleccionado;
                        this.filtro.paginacion.listaResultado = lista;
                    }
                }
            );
        } else {

            this.psCapacidadesService.actualizar(rowData).then(
                resp => {
                    if (resp != null) {
                        this.messageService.clear();
                        this.messageService.add({
                            severity: 'success', summary: 'Mensaje',
                            detail: 'Se Actualizó Información de Capacidades para el Beneficiario : ' + rowData.nombreCompleto
                        });
                        rowData.hover = false;
                        this.registroSeleccionado.idCapacidad = resp.idCapacidad;
                        this.registroSeleccionado.evaluado = resp.evaluado;
                        const lista = [...this.filtro.paginacion.listaResultado];
                        lista[this.findSelectedCapacidadIndex()] = this.registroSeleccionado;
                        this.filtro.paginacion.listaResultado = lista;
                    }
                }
            );
        }

    }

    cargarSexos() {
        this.sexos.push({ label: '--Todos--', value: '' });
        this.sexos.push({ label: 'Masculino', value: 'M' });
        this.sexos.push({ label: 'Femenino', value: 'F' });
    }



}
