import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import { BaseComponent } from 'src/app/base_module/components/basecomponent';
import {  SelectItem, LazyLoadEvent } from 'primeng/api';
import { RasService } from '../servicio/ras.servicio';
import { Table } from 'primeng/table';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { TratamientoComponent } from './ras-tratamiento.component';
import { FiltroRas } from '../dominio/filtroras';
import { DtoAtencion } from '../dominio/dtoatencion';
import { obtenerPorDiaFechaHoraFin, obtenerPorDiaFechaHoraInicio } from 'src/app/base_module/util/funciones/dateutils';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-rasmantenimiento',
    templateUrl: './ras-mantenimiento.component.html'
})
export class RasMantenimientoComponent extends BaseComponent implements OnInit {
    constructor(
        messageService: MessageService,
        private cdref: ChangeDetectorRef,
        private empleadomastService: EmpleadomastServicio,
        private psInstitucionServicio: PsInstitucionServicio,
        private psInstitucionAreaServicio: PsInstitucionAreaServicio,
        private rasService: RasService,
    ) {
        super(messageService);
    }
    filtro: FiltroRas = new FiltroRas();
    listaInstitucion: SelectItem[] = [];
    listaResidencia: SelectItem[] = [];
    sexos: SelectItem[] = [];
    listaDiagnosticos: SelectItem[] = [];
    listaTipoRas: SelectItem[] = [];
    listaPrograma: SelectItem[] = [];
    psRasSeleccionado: DtoAtencion = new DtoAtencion();
    mostrarNinos: Boolean = false;
    mostrarAdultos: Boolean = false;
    secuencia: number;
    cantidadColumnas: number;
    cambiarInstitucion: Boolean = false;
    @ViewChild(TratamientoComponent)
    tratamientoComponent: TratamientoComponent;


    verListaProgramas: Boolean = false;
    puedeBuscar: Boolean = false;

    ngOnInit(): void {
        this.bloquearPagina();
        this.cantidadColumnas = 5;
        this.filtro.cantidadRegistros = 20;
        this.cargarSexos();
        this.puedeBuscar = false;
        this.filtro.fechaAtencion = new Date();
        this.filtro.idTipoAtencion = 'I';
        this.listaTipoRas.push({ label: 'Interno', value: 'I' });
        this.listaTipoRas.push({ label: 'Externo', value: 'E' });

        const p1 = this.cargarInstitucion();

        Promise.all([p1]).then(f => {
            this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
                empleado => {
                    this.filtro.idInstitucion = empleado.idInstitucion;
                    this.puedeCambiarInstitucion();
                    this.cargarPrograma();
                    this.desbloquearPagina();
                }
            );
        });
    }

    onChangeInstitucion(value: any, tb?: any) {
        this.listaPrograma = [];
        this.filtro.idInstitucion = value;
        this.filtro.idPrograma = null;
    }

    cargarPrograma() {
    }

    onChangePrograma(tb?: any) {
        if (this.filtro.idPrograma === 'AAM') {
            this.mostrarAdultos = true;
            this.mostrarNinos = false;
        } else {
            this.mostrarNinos = true;
            this.mostrarAdultos = false;
        }
        this.cargarResidencia();
        this.buscar(tb);
    }

    onMouseEnter(rowData): void {
        rowData.hover = true;
    }

    onMouseLeave(rowData): void {
        rowData.hover = false;
    }

    preBuscar(event?: any, tb?: any) {
        if (event.keyCode === 13) {
            this.buscar(tb);
        }
    }

    onchangeBuscar(tb?: any) {
        this.buscar(tb);
    }

    buscar(data: Table) {
        if (this.filtro.idPrograma === null || this.filtro.idPrograma === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Seleccionar un Programa' });
            return;
        }

        if (this.filtro.fechaAtencion === null || this.filtro.fechaAtencion === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Ingresar la fecha de Atención' });
            return;
        }
        if (this.puedeBuscar) {
            //data.reset();
            this.cargarListaEvent();
        }

        this.puedeBuscar = true;
    }

    puedeCambiarInstitucion() {
        this.psInstitucionServicio.flgSeleccionarInstitucion().then(
            resp => {
                this.cambiarInstitucion = this.flagABoolean(resp.valorFlag);
            }
        );
    }

    // tslint:disable-next-line:use-life-cycle-interface
    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }

    verDiagnosticos(rowData, secuencia: number) {
        this.psRasSeleccionado = rowData;

    }

    recortarTexto(texto: string): string {
        return texto.substring(0, 40);
    }

    cargarDiagnostico(dtoDiagnostico: any) {

        this.secuencia = dtoDiagnostico.secuencia;
        if (this.secuencia === 1) {
            this.psRasSeleccionado.nombreDiagnostico01 = this.recortarTexto(dtoDiagnostico.data.nombre);
            this.psRasSeleccionado.idDiagnostico01 = dtoDiagnostico.data.idDiagnostico;
        }

        if (this.secuencia === 2) {
            this.psRasSeleccionado.nombreDiagnostico02 = this.recortarTexto(dtoDiagnostico.data.nombre);
            this.psRasSeleccionado.idDiagnostico02 = dtoDiagnostico.data.idDiagnostico;
        }

        if (this.secuencia === 3) {
            this.psRasSeleccionado.nombreDiagnostico03 = this.recortarTexto(dtoDiagnostico.data.nombre);
            this.psRasSeleccionado.idDiagnostico03 = dtoDiagnostico.data.idDiagnostico;
        }

        if (this.secuencia === 4) {
            this.psRasSeleccionado.nombreDiagnostico04 = this.recortarTexto(dtoDiagnostico.data.nombre);
            this.psRasSeleccionado.idDiagnostico04 = dtoDiagnostico.data.idDiagnostico;
        }

        if (this.secuencia === 5) {
            this.psRasSeleccionado.nombreDiagnostico05 = this.recortarTexto(dtoDiagnostico.data.nombre);
            this.psRasSeleccionado.idDiagnostico05 = dtoDiagnostico.data.idDiagnostico;
        }

        const lista = [...this.filtro.paginacion.listaResultado];
        lista[this.findSelectedRasIndex()] = this.psRasSeleccionado;
        this.filtro.paginacion.listaResultado = lista;
        this.onMouseEnter(this.psRasSeleccionado);
    }

    mostrarVentanaTratamiento(rowData, secuencia: number) {

        if (secuencia === 1) {
            if (rowData.idDiagnostico01 === null || rowData.idDiagnostico01 === undefined) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Seleccionar el Diagnostico 1' });
                return;
            }
        }
        if (secuencia === 2) {
            if (rowData.idDiagnostico02 === null || rowData.idDiagnostico02 === undefined) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Seleccionar el Diagnostico 2' });
                return;
            }
        }

        if (secuencia === 3) {
            if (rowData.idDiagnostico03 === null || rowData.idDiagnostico03 === undefined) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Seleccionar el Diagnostico 3' });
                return;
            }
        }
        if (secuencia === 4) {
            if (rowData.idDiagnostico04 === null || rowData.idDiagnostico04 === undefined) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Seleccionar el Diagnostico 4' });
                return;
            }
        }

        if (secuencia === 5) {
            if (rowData.idDiagnostico05 === null || rowData.idDiagnostico05 === undefined) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Seleccionar el Diagnostico 5' });
                return;
            }
        }

        this.psRasSeleccionado = rowData;
        this.tratamientoComponent.verVentana(rowData, secuencia);
    }

    cargarListaEvent() {
        if (!this.puedeBuscar) {
            this.puedeBuscar = true;
            return;
        }
        this.bloquearPagina();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = 1;
        this.filtro.fechaAtencionInicio = obtenerPorDiaFechaHoraInicio(this.filtro.fechaAtencion);
        this.filtro.fechaAtencionFin = obtenerPorDiaFechaHoraFin(this.filtro.fechaAtencion);

        this.filtro.paginacion.cantidadRegistrosDevolver = this.filtro.cantidadRegistros; // event.rows;
        this.rasService.listarPaginacion(this.filtro)
            .then(res => {
                this.filtro.paginacion = res;
                this.desbloquearPagina();
            });
    }

    verModalComentario: boolean = false;
    comentario: string = "";

    guardarComentario() {
        this.psRasSeleccionado.comentario = this.comentario;
        this.verModalComentario = false;
    }

    mostrarVentanaComentario(rowData) {
        this.verModalComentario = true;
        this.comentario = rowData.comentario;
        this.psRasSeleccionado = rowData;
    }

    mostrarIconos(dtoTratamientos): DtoAtencion {
        if (dtoTratamientos.secuencia === 1) {
            if (dtoTratamientos.tratamiento1 === undefined) {
                this.psRasSeleccionado.idTratamiento01_1 = '';

            } else {
                this.psRasSeleccionado.idTratamiento01_1 = dtoTratamientos.tratamiento1;
            }

            if (dtoTratamientos.tratamiento2 === undefined) {
                this.psRasSeleccionado.idTratamiento01_2 = '';
            } else {
                this.psRasSeleccionado.idTratamiento01_2 = dtoTratamientos.tratamiento2;
            }

            if (dtoTratamientos.tratamiento3 === undefined) {
                this.psRasSeleccionado.idTratamiento01_3 = '';
            } else {
                this.psRasSeleccionado.idTratamiento01_3 = dtoTratamientos.tratamiento3;
            }
        }



        if (dtoTratamientos.secuencia === 2) {
            if (dtoTratamientos.tratamiento1 === undefined) {
                this.psRasSeleccionado.idTratamiento02_1 = '';
            } else {
                this.psRasSeleccionado.idTratamiento02_1 = dtoTratamientos.tratamiento1;
            }

            if (dtoTratamientos.tratamiento2 === undefined) {
                this.psRasSeleccionado.idTratamiento02_2 = '';
            } else {
                this.psRasSeleccionado.idTratamiento02_2 = dtoTratamientos.tratamiento2;
            }

            if (dtoTratamientos.tratamiento3 === undefined) {
                this.psRasSeleccionado.idTratamiento02_3 = '';
            } else {
                this.psRasSeleccionado.idTratamiento02_3 = dtoTratamientos.tratamiento3;
            }
        }

        if (dtoTratamientos.secuencia === 3) {
            if (dtoTratamientos.tratamiento1 === undefined) {
                this.psRasSeleccionado.idTratamiento03_1 = '';
            } else {
                this.psRasSeleccionado.idTratamiento03_1 = dtoTratamientos.tratamiento1;
            }

            if (dtoTratamientos.tratamiento2 === undefined) {
                this.psRasSeleccionado.idTratamiento03_2 = '';
            } else {
                this.psRasSeleccionado.idTratamiento03_2 = dtoTratamientos.tratamiento2;
            }

            if (dtoTratamientos.tratamiento3 === undefined) {
                this.psRasSeleccionado.idTratamiento03_3 = '';
            } else {
                this.psRasSeleccionado.idTratamiento03_3 = dtoTratamientos.tratamiento3;
            }
        }


        if (dtoTratamientos.secuencia === 4) {
            if (dtoTratamientos.tratamiento1 === undefined) {
                this.psRasSeleccionado.idTratamiento04_1 = '';
            } else {
                this.psRasSeleccionado.idTratamiento04_1 = dtoTratamientos.tratamiento1;
            }

            if (dtoTratamientos.tratamiento2 === undefined) {
                this.psRasSeleccionado.idTratamiento04_2 = '';
            } else {
                this.psRasSeleccionado.idTratamiento04_2 = dtoTratamientos.tratamiento2;
            }

            if (dtoTratamientos.tratamiento3 === undefined) {
                this.psRasSeleccionado.idTratamiento04_3 = '';
            } else {
                this.psRasSeleccionado.idTratamiento04_3 = dtoTratamientos.tratamiento3;
            }
        }


        if (dtoTratamientos.secuencia === 5) {
            if (dtoTratamientos.tratamiento1 === undefined) {
                this.psRasSeleccionado.idTratamiento05_1 = '';
            } else {
                this.psRasSeleccionado.idTratamiento05_1 = dtoTratamientos.tratamiento1;
            }

            if (dtoTratamientos.tratamiento2 === undefined) {
                this.psRasSeleccionado.idTratamiento05_2 = '';
            } else {
                this.psRasSeleccionado.idTratamiento05_2 = dtoTratamientos.tratamiento2;
            }

            if (dtoTratamientos.tratamiento3 === undefined) {
                this.psRasSeleccionado.idTratamiento05_3 = '';
            } else {
                this.psRasSeleccionado.idTratamiento05_3 = dtoTratamientos.tratamiento3;
            }
        }

        return this.psRasSeleccionado;
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
        this.psInstitucionAreaServicio.listadoPorPrograma(this.filtro.idInstitucion, this.filtro.idPrograma)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaResidencia.push({ label: obj.nombre, value: obj.idArea }));
            });
    }

    cargarSexos() {
        this.sexos.push({ label: '--Todos--', value: '' });
        this.sexos.push({ label: 'Masculino', value: 'M' });
        this.sexos.push({ label: 'Femenino', value: 'F' });
    }

    insertarTratamientos(dtoTratamientos) {
        this.psRasSeleccionado = this.mostrarIconos(dtoTratamientos);

        const lista = [...this.filtro.paginacion.listaResultado];
        lista[this.findSelectedRasIndex()] = this.psRasSeleccionado;
        this.filtro.paginacion.listaResultado = lista;

        this.onMouseEnter(this.psRasSeleccionado);
    }

    findSelectedRasIndex(): number {
        return this.filtro.paginacion.listaResultado.indexOf(this.psRasSeleccionado);
    }

    validar(data: DtoAtencion): Boolean {

        this.messageService.clear();

        let valida = true;

        if (data.fechaAtencion == null || data.fechaAtencion === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Campo Fecha Atención es requerida' });
            valida = false;
        }
        return valida;

    }

    agregarColumna(tb?: any) {
        if (this.cantidadColumnas === 5) {
            this.messageService.clear();
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El máximo de columnas permitidas es 5.' });
            this.cantidadColumnas = 5;
        } else {
            this.cantidadColumnas = this.cantidadColumnas + 1;
        }
    }

    quitarColumna(tb?: any) {
        if (this.cantidadColumnas === 3) {
            this.messageService.clear();
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se puede ocultar menos de 3 columnas' });
            this.cantidadColumnas = 3;
        } else {
            this.cantidadColumnas = this.cantidadColumnas - 1;
        }
    }

    mostrarColumna(columna: number): boolean {
        if (this.cantidadColumnas >= columna) {
            return true;
        } else {
            return false;
        }
    }

    guardar(rowData, tb?: any) {
        this.psRasSeleccionado = rowData;
        if (!this.validar(rowData)) {
            return;
        }

        // Es nuevo
        if (rowData.idAtencion === undefined || rowData.idAtencion === null) {
            rowData.estado = 'A';
            rowData.idInstitucion = this.filtro.idInstitucion;
            rowData.idTipoAtencion = this.filtro.idTipoAtencion;
            rowData.fechaAtencion = this.filtro.fechaAtencion;
            this.rasService.registrar(rowData).then(
                resp => {
                    if (resp != null) {
                        this.mostrarMensajeExito(this.getMensajeGrabado(rowData.nombreCompleto));
                        rowData.hover = false;
                        this.psRasSeleccionado.idAtencion = resp.idAtencion;
                        const lista = [...this.filtro.paginacion.listaResultado];
                        lista[this.findSelectedRasIndex()] = this.psRasSeleccionado;
                        this.filtro.paginacion.listaResultado = lista;
                    }
                }
            );
        } else {

            this.rasService.actualizar(rowData).then(
                resp => {
                    if (resp != null) {
                        this.mostrarMensajeExito(this.getMensajeActualizado(rowData.nombreCompleto));
                        rowData.hover = false;
                    }
                }
            );
        }

    }

}
