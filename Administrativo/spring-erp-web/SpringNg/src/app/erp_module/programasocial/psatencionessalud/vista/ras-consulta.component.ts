import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { SelectItem, LazyLoadEvent } from 'primeng/primeng';
import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import { RasService } from '../servicio/ras.servicio';
import { FiltroRas } from '../dominio/filtroras';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
import { Table } from 'primeng/table';
import { obtenerPorDiaFechaHoraInicio, obtenerPorDiaFechaHoraFin } from 'src/app/base_module/util/funciones/dateutils';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-rasconsulta',
    templateUrl: './ras-consulta.component.html'
})
export class RasConsultaComponent extends BaseComponent implements OnInit {
    constructor(
        messageService: MessageService,
        private cdref: ChangeDetectorRef,
        private rasService: RasService,
        private psInstitucionAreaServicio: PsInstitucionAreaServicio,
        private psInstitucionServicio: PsInstitucionServicio,
        private empleadomastService: EmpleadomastServicio,

    ) {
        super(messageService);
    }
    filtro: FiltroRas = new FiltroRas();
    listaInstitucion: SelectItem[] = [];
    listaResidencia: SelectItem[] = [];
    listaTipoRas: SelectItem[] = [];
    listaPrograma: SelectItem[] = [];
    listaDiagnosticos: SelectItem[] = [];
    listaPeriodos: SelectItem[] = [];
    verBotonExcel: Boolean = false;
    verListaProgramas: Boolean = false;
    listaSentido: SelectItem[] = [];
    listaOrden: SelectItem[] = [];
    sexos: SelectItem[] = [];
    mostrarNinos: Boolean = false;
    mostrarAdultos: Boolean = false;
    cambiarInstitucion: Boolean = false;
    puedeBuscar: Boolean = false;



    ngOnInit(): void {
        this.bloquearPagina();
        this.puedeBuscar = false;
        this.filtro.cantidadRegistros = 20;
        this.cargarSexos();
        this.cargarOrden();

        this.filtro.fechaAtencionInicio = new Date();
        this.filtro.fechaAtencionFin = new Date();
        this.filtro.idTipoAtencion = 'I';
        this.listaTipoRas.push({ label: 'Interno', value: 'I' });
        this.listaTipoRas.push({ label: 'Externo', value: 'E' });

        const p2 = this.cargarInstitucion();
        const p3 = null;

        Promise.all([p2, p3]).then(f => {
            this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
                empleado => {
                    this.filtro.idInstitucion = empleado.idInstitucion;
                    this.puedeCambiarInstitucion();
                    this.cargarResidencia();
                    this.cargarPrograma();
                    this.desbloquearPagina();
                }
            );
        });
    }

    cargarOrden() {
        this.listaOrden.push({ label: " -- Seleccione --", value: null });

        this.listaOrden.push({ label: "Nombre Completo						   ", value: 1 });
        this.listaOrden.push({ label: "Fecha    							   ", value: 2 });
        this.listaOrden.push({ label: "Residencia							   ", value: 3 });

        this.listaOrden.push({ label: "Diagnóstico 1						   ", value: 4 });
        this.listaOrden.push({ label: "Diagnóstico 1 Tratamientos 1			   ", value: 5 });
        this.listaOrden.push({ label: "Diagnóstico 1 Tratamientos 2			   ", value: 6 });
        this.listaOrden.push({ label: "Diagnóstico 1 Tratamientos 3			   ", value: 7 });

        this.listaOrden.push({ label: "Diagnóstico 2						   ", value: 8 });
        this.listaOrden.push({ label: "Diagnóstico 2 Tratamientos 1			   ", value: 9 });
        this.listaOrden.push({ label: "Diagnóstico 2 Tratamientos 2			   ", value: 10 });
        this.listaOrden.push({ label: "Diagnóstico 2 Tratamientos 3			   ", value: 11 });

        this.listaOrden.push({ label: "Diagnóstico 3						   ", value: 12 });
        this.listaOrden.push({ label: "Diagnóstico 3 Tratamientos 1 		   ", value: 13 });
        this.listaOrden.push({ label: "Diagnóstico 3 Tratamientos 2 		   ", value: 14 });
        this.listaOrden.push({ label: "Diagnóstico 3 Tratamientos 3 		   ", value: 15 });

        this.listaSentido.push({ label: " -- Seleccione --", value: 1 });
        this.listaSentido.push({ label: "Ascendente", value: 1 });
        this.listaSentido.push({ label: "Descendente", value: -1 });
    }


    verDiagnosticos() {

    }

    cargarDiagnostico(dtoDiagnostico: any) {
        this.filtro.nombreDiagnostico = dtoDiagnostico.data.nombre;
        this.filtro.idDiagnostico = dtoDiagnostico.data.idDiagnostico;
      
    }

    limpiarDiagnosticos() {
        this.filtro.nombreDiagnostico = null;
        this.filtro.idDiagnostico = null;
    }

   

    cargarPrograma() {
        
    }

    onchangeBuscar(tb?: any) {
        this.buscar(tb);
    }


    onChangeInstitucion(value: any, tb?: any) {
        this.listaPrograma = [];
        this.filtro.idInstitucion = value;
        this.filtro.idPrograma = null;
        
    }

    onChangePrograma(tb?: any) {
        if (this.filtro.idPrograma === 'AAM') {
            this.mostrarAdultos = true;
            this.mostrarNinos = false;
        } else {
            this.mostrarNinos = true;
            this.mostrarAdultos = false;
        }
        this.buscar(tb);
    }

    cargarResidencia() {
        this.listaResidencia = [];
        this.listaResidencia.push({ label: '-- Seleccione --', value: null });
        this.psInstitucionAreaServicio.listado(this.filtro.idInstitucion)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaResidencia.push({ label: obj.nombre, value: obj.idArea }));
            });
    }

    puedeCambiarInstitucion() {
        this.psInstitucionServicio.flgSeleccionarInstitucion().then(
            resp => {
                this.cambiarInstitucion = this.flagABoolean(resp.valorFlag);
            }
        );
    }

    cargarSexos() {
        this.sexos.push({ label: '--Todos--', value: '' });
        this.sexos.push({ label: 'Masculino', value: 'M' });
        this.sexos.push({ label: 'Femenino', value: 'F' });
    }


    cargarInstitucion(): Promise<number> {
        this.listaInstitucion.push({ label: '-- Seleccione --', value: null });
        return this.psInstitucionServicio.listarInstituciones()
            .then(respuesta => {
                respuesta.forEach(obj => this.listaInstitucion.push({ label: obj.nombre, value: obj.idInstitucion }));
                return 1;
            });
    }

    // tslint:disable-next-line:use-life-cycle-interface
    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }

    preBuscar(event?: any, tb?: any) {
        if (event.keyCode === 13) {
            this.buscar(tb);
        }
    }

    buscar(data: Table) {
        if (this.filtro.fechaAtencionInicio != null && this.filtro.fechaAtencionFin != null) {
            if (this.filtro.fechaAtencionInicio > this.filtro.fechaAtencionFin) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'la fecha de Inicio debe ser menor que la fecha fin' });
                return;
            }
        }


        if (this.puedeBuscar) {
            data.reset();
        }

        this.puedeBuscar = true;
    }

    export() {
        this.bloquearPagina();
        this.rasService.exportar()
            .then(respuesta => {
                this.desbloquearPagina();
                if (respuesta) {
                    respuesta = respuesta.replace('../Archivos/', '..' + window.location.pathname + 'Archivos/');
                    window.open(respuesta);
                }
            });
    }

    cargarListaEvent(event: LazyLoadEvent) {
        if (!this.puedeBuscar) {
            return;
        }
        this.bloquearPagina();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = this.filtro.cantidadRegistros; // event.rows;
        this.filtro.fechaAtencionInicio = obtenerPorDiaFechaHoraInicio(this.filtro.fechaAtencionInicio);
        this.filtro.fechaAtencionFin = obtenerPorDiaFechaHoraFin(this.filtro.fechaAtencionFin);

        this.rasService.listarPaginacionConsulta(this.filtro)
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


}
