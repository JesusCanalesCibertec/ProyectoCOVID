import { PsInstitucionperiodoServicio } from './../../institucion_periodo/service/PsInstitucionperiodoServicio';
import { DtoCapacidad } from '../dominio/dtocapacidad';
import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import { SelectItem, LazyLoadEvent } from 'primeng/primeng';
import { BaseComponent } from 'src/app/base_module/components/basecomponent';
import { FiltroCapacidades } from '../dominio/FiltroCapacidades';
import { Table } from 'primeng/table';
import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { PsCapacidadesService } from '../servicio/pscapacidades.service';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsCapacidadComentarioComponent } from './pscapacidad-comentario.component';
import { PsConstante } from '../../psconstantes.ts/psconstantes';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-capacidades-consulta',
    templateUrl: './pscapacidades-consulta.component.html'
})
export class PsCapacidadesConsultaComponent extends BaseComponent implements OnInit {
    constructor(
        messageService: MessageService,
        private cdref: ChangeDetectorRef,
        private psInstitucionAreaServicio: PsInstitucionAreaServicio,
        private empleadomastService: EmpleadomastServicio,
        private psInstitucionServicio: PsInstitucionServicio,
        private psInstitucionperiodoServicio: PsInstitucionperiodoServicio,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
        private psCapacidadesService: PsCapacidadesService,
    ) { super(messageService); }


    listaSentido: SelectItem[] = [];
    listaOrden: SelectItem[] = [];
    filtro: FiltroCapacidades = new FiltroCapacidades();
    sexos: SelectItem[] = [];
    listaInstitucion: SelectItem[] = [];
    listaResidencia: SelectItem[] = [];
    listaPrograma: SelectItem[] = [];
    listaPeriodos: SelectItem[] = [];

    listaRiesgoCaida: SelectItem[] = [];
    listaRiesgoCaidaDetalle: SelectItem[] = [];

    listaHabilidadesOcupacionales: SelectItem[] = [];
    listaTipoInstitucion: SelectItem[] = [];
    listaNivel: SelectItem[] = [];
    listaGradoAcademico: SelectItem[] = [];
    listaCapacodadPromedio: SelectItem[] = [];
    listaTalleresPromedio: SelectItem[] = [];
    listaTipoComunicacion: SelectItem[] = [];
    verGrado: Boolean = false;
    verNivel: Boolean = false;
    verListaProgramas: Boolean = false;

    @ViewChild(PsCapacidadComentarioComponent)
    psCapacidadComentarioComponent: PsCapacidadComentarioComponent;

    cambiarInstitucion: Boolean = false;
    mostrarNinos: Boolean = false;
    mostrarAdultos: Boolean = false;
    tituloColumna: string;
    verBotonDependencia: Boolean = false;
    verBotonExcel: Boolean = false;

    registroSeleccionado: DtoCapacidad = new DtoCapacidad();

    ngOnInit(): void {
        this.bloquearPagina();
        this.filtro.cantidadRegistros = 20;
        this.filtro.idComponente = 'CAPA';
        this.cargarSexos();

        this.cargarOrden();

        const p1 = this.cargarInstitucion();
        const p2 = this.cargarRiesgoCaida();
        const p10 = this.cargarRiesgoCaidaDetalle();

        const p3 = this.cargarHabilidadesOcupacionales();
        const p4 = this.cargarTipoInstitucion();
        const p5 = this.cargarNivel();
        const p6 = this.cargarCapacidadPromedio();
        const p7 = this.cargarTalleresPromedio();
        const p8 = this.cargarTipoComunicacion();
        const p9 = null;

        Promise.all([p1, p2, p3, p4, p5, p6, p7, p8, p10]).then(f => {
            this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
                empleado => {
                    this.filtro.idInstitucion = empleado.idInstitucion;
                    this.puedeCambiarInstitucion();
                    this.cargarResidencia();
                    this.cargarPrograma();
                    this.cargarPeriodo();
                }
            );
        });
    }

    cargarOrden() {
        this.listaOrden.push({ label: " -- Seleccione --", value: null });

        this.listaOrden.push({ label: "Nombre Completo						   ", value: 1 });
        this.listaOrden.push({ label: "Edad									   ", value: 2 });
        this.listaOrden.push({ label: "Sexo									   ", value: 3 });
        this.listaOrden.push({ label: "Fecha Informe						   ", value: 4 });
        this.listaOrden.push({ label: "Dependencia Katz						   ", value: 5 });
        this.listaOrden.push({ label: "Dependencia Barthel					   ", value: 6 });
        this.listaOrden.push({ label: "Riesgo caída							   ", value: 7 });
        this.listaOrden.push({ label: "Riesgo caída detalle					   ", value: 8 });
        this.listaOrden.push({ label: "Tipo Institución						   ", value: 9 });
        this.listaOrden.push({ label: "No Evaluado							   ", value: 10 });
        this.listaOrden.push({ label: "Habilidades Ocupacionales?			   ", value: 11 });
        this.listaOrden.push({ label: "Evaluado en Hab. Ocupacional?		   ", value: 12 });
        this.listaOrden.push({ label: "Nivel								   ", value: 13 });
        this.listaOrden.push({ label: "Grado Académico Actual				   ", value: 14 });
        this.listaOrden.push({ label: "Años de Retraso Educativo			   ", value: 15 });

        /*
        this.listaOrden.push({ label: "Lógico Matemático					   ", value: 16 });
        this.listaOrden.push({ label: "Comunicación							   ", value: 17 });
        this.listaOrden.push({ label: "Comprensión Lectora					   ", value: 18 });
        this.listaOrden.push({ label: "Personal Social						   ", value: 19 });
        this.listaOrden.push({ label: "Ciencia y Ambiente					   ", value: 20 });
        this.listaOrden.push({ label: "Tipo de Comunicación					   ", value: 21 });*/

        this.listaSentido.push({ label: " -- Seleccione --", value: 1 });
        this.listaSentido.push({ label: "Ascendente", value: 1 });
        this.listaSentido.push({ label: "Descendente", value: -1 });
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
        this.psInstitucionAreaServicio.listado(this.filtro.idInstitucion)
            .then(respuesta => {
                respuesta = respuesta.filter(x => x.idComponente == '' || x.idComponente == null);

                respuesta.forEach(obj => this.listaResidencia.push({ label: obj.nombre, value: obj.idArea }));
            });
    }

    cargarPrograma() {
        this.listaPrograma = [];
       
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


    // tslint:disable-next-line:use-life-cycle-interface
    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }

    mostrarComentario(rowData, tipo: string) {
        this.registroSeleccionado = rowData;
        this.psCapacidadComentarioComponent.verVentana(rowData, tipo, 'consultar');
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
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Seleccionar un Periodo' });
            return;
        }

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
        data.reset();
    }

    puedeCambiarInstitucion() {
        this.psInstitucionServicio.flgSeleccionarInstitucion().then(
            resp => {
                this.cambiarInstitucion = this.flagABoolean(resp.valorFlag);
            }
        );
    }

    onChangeInstitucion(value: any, tb?: any) {
        this.listaPrograma = [];
        this.filtro.idInstitucion = value;
        this.filtro.idPrograma = null;

    }

    export() {
        this.bloquearPagina();
        this.psCapacidadesService.exportar(this.mostrarNinos)
            .then(respuesta => {
                this.desbloquearPagina();
                if (respuesta) {
                    respuesta = respuesta.replace('../Archivos/', '..' + window.location.pathname + 'Archivos/');
                    window.open(respuesta);
                }
            });
    }

    cargarListaEvent(event: LazyLoadEvent) {
        this.bloquearPagina();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = this.filtro.cantidadRegistros; // event.rows;
        this.psCapacidadesService.listarPaginacionConsulta(this.filtro)
            .then(res => {
                this.filtro.paginacion = res;
                if (this.filtro.paginacion.listaResultado.length > 0) {
                    this.verBotonExcel = true;
                } else {
                    this.verBotonExcel = false;
                }

                this.desbloquearPagina();
            });
    }

    cargarSexos() {
        this.sexos.push({ label: '--Todos--', value: '' });
        this.sexos.push({ label: 'Masculino', value: 'M' });
        this.sexos.push({ label: 'Femenino', value: 'F' });
    }

    cargarPeriodo() {
        this.listaPeriodos = [];
        this.listaPeriodos.push({ label: '--Seleccione--', value: '' });
        this.psInstitucionperiodoServicio.listarPeriodoPorComponente(
            this.filtro.idInstitucion,
            this.filtro.idPrograma,
            this.filtro.idComponente,
        ).then(respuesta => {
            respuesta.forEach(obj => this.listaPeriodos.push({ label: obj.nombre, value: obj.codigo }));
        });
    }


}
