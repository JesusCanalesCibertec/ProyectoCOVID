import { BaseComponent, accionSolicitada } from './../../../../base_module/components/basecomponent';
import { SelectItem } from 'primeng/primeng';
import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { DtoBeneficiario } from '../dominio/dtoBeneficiario';
import { PsConstante } from '../../psconstantes.ts/psconstantes';
import { DtoCapacidad } from '../../capacidades/dominio/dtocapacidad';
import { BarthelComponent } from '../../capacidades/vista/pscapacidad-barthel.component';
import { KatzComponent } from '../../capacidades/vista/pscapacidad-katz.component';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsCapacidadesService } from '../../capacidades/servicio/pscapacidades.service';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-beneficiariocapacidades',
    templateUrl: './beneficiario-capacidades.component.html'
})
export class BeneficiarioCapacidadesComponent extends BaseComponent implements OnInit {
    constructor(
        messageService: MessageService,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
        private psCapacidadesService: PsCapacidadesService
    ) { super(messageService); }
    programa: string;
    dtoCapacidad: DtoCapacidad = new DtoCapacidad();
    verSelector: Boolean = false;
    verGrado: Boolean = false;
    verNivel: Boolean = false;
    nivel: string;
    gradoAcademico: string;

    listaTipoInstitucion: SelectItem[] = [];
    listaNivel: SelectItem[] = [];
    listaGradoAcademico: SelectItem[] = [];
    listaRiesgoCaida: SelectItem[] = [];
    listaRiesgoCaidaDetalle: SelectItem[] = [];
    @Output() volverAbuscar = new EventEmitter();

    @ViewChild(BarthelComponent)
    barthelComponent: BarthelComponent;

    @ViewChild(KatzComponent)
    katzComponent: KatzComponent;

    ngOnInit(): void {

        const p1 = this.cargarRiesgoCaida();
        const p2 = this.cargarTipoInstitucion();
        const p3 = this.cargarRiesgoCaidaDetalle();

        Promise.all([p1, p2, p3]).then(f => {
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
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, 'RIESGOCA')
            .then(respuesta => {
                respuesta.forEach(obj => this.listaRiesgoCaidaDetalle.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });
    }

    cargarNivel() {
        this.listaNivel = [];
        this.listaNivel.push({ label: '-- Seleccione --', value: null });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_NIVEL)
            .then(respuesta => {
                respuesta.forEach(obj => this.listaNivel.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
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


    aceptar() {
        // tslint:disable-next-line:triple-equals
        if (this.accion == accionSolicitada.NUEVO) {
            this.dtoCapacidad.idOrigen = PsConstante.MISCELANEO_TIPO_EVALUACION_INICIAL;
            this.dtoCapacidad.fechaInforme = new Date();
            this.psCapacidadesService.registrar(this.dtoCapacidad).then(
                resp => {
                    if (resp != null) {
                        this.messageService.clear();
                        this.messageService.add({
                            severity: 'success', summary: 'Mensaje',
                            detail: 'Se Registró Información de Capacidades para el Beneficiario : ' + this.dtoCapacidad.nombreCompleto
                        });
                        this.volverAbuscar.emit();
                        this.verSelector = false;
                    }
                }
            );
        } else {

            this.psCapacidadesService.registrar(this.dtoCapacidad).then(
                resp => {
                    if (resp != null) {
                        this.messageService.clear();
                        this.messageService.add({
                            severity: 'success', summary: 'Mensaje',
                            detail: 'Se Actualizó Información de Capacidades para el Beneficiario : ' + this.dtoCapacidad.nombreCompleto
                        });
                        this.volverAbuscar.emit();
                        this.verSelector = false;
                    }
                }
            );
        }

    }

    onChangeTipoInstitucion(value: any) {
        if (value === 'NIN') {
            this.verGrado = true;
            this.verNivel = true;
            this.listaGradoAcademico = [];
            this.listaNivel = [];

        } else {
            this.verGrado = false;
            this.verNivel = false;
            this.cargarNivel();
        }

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

    obtenerNivell(callBack?: () => void): void {
        const p1 = this.onChangeNivel(this.nivel);
        Promise.all([p1]).then(f => {
            if (callBack) { callBack(); }
        });
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


    mostrarDependenciaBerthel() {
        this.barthelComponent.verVentana(this.dtoCapacidad);

    }
    obtenerBarthel(rowData) {
        this.dtoCapacidad.gradoDependenciaBarthel = rowData.gradoDependenciaBarthel;
        this.dtoCapacidad.porcentajeGradoBarthel = rowData.porcentajeGradoBarthel;
        this.dtoCapacidad.barthel1 = rowData.barthel1;
        this.dtoCapacidad.barthel2 = rowData.barthel2;
        this.dtoCapacidad.barthel3 = rowData.barthel3;
        this.dtoCapacidad.barthel4 = rowData.barthel4;
        this.dtoCapacidad.barthel5 = rowData.barthel5;
        this.dtoCapacidad.barthel6 = rowData.barthel6;
        this.dtoCapacidad.barthel7 = rowData.barthel7;
        this.dtoCapacidad.barthel8 = rowData.barthel8;
        this.dtoCapacidad.barthel9 = rowData.barthel9;
        this.dtoCapacidad.barthel10 = rowData.barthel10;
    }

    mostrarDependenciaKatz() {
        this.katzComponent.verVentana(this.dtoCapacidad);
    }

    obtenerKatz(rowData) {
        this.dtoCapacidad.gradoDependenciaKatz = rowData.gradoDependenciaKatz;
        this.dtoCapacidad.porcentajeGradoKatz = rowData.porcentajeGradoKatz;
        this.dtoCapacidad.katz1 = rowData.katz1;
        this.dtoCapacidad.katz2 = rowData.katz2;
        this.dtoCapacidad.katz3 = rowData.katz3;
        this.dtoCapacidad.katz4 = rowData.katz4;
        this.dtoCapacidad.katz5 = rowData.katz5;
        this.dtoCapacidad.katz6 = rowData.katz6;
    }
    editarMantenimiento: boolean = false;

    verVentana(dto: DtoBeneficiario) {
        this.dtoCapacidad = new DtoCapacidad();
        this.editarMantenimiento = dto.estado == "EGRESADO" || dto.estado == "ANULADO";

        this.programa = dto.idPrograma;
        if (this.programa === PsConstante.MISCELANEO_TIPO_PROGRAMA_ADULTOS) {
            this.dtoCapacidad.validarNinos = false;
        } else {
            this.dtoCapacidad.validarNinos = true;
        }
        const verNinos = this.dtoCapacidad.validarNinos;

        if (dto.idComponenteCapacidad === null || dto.idComponenteCapacidad === undefined) {
            this.accion = accionSolicitada.NUEVO;
            this.dtoCapacidad.idBeneficiario = dto.idBeneficiario;
            this.dtoCapacidad.idInstitucion = dto.idInstitucion;
            this.dtoCapacidad.nombreCompleto = dto.beneficiario;
            this.dtoCapacidad.institucionNombre = dto.institucion;
            this.dtoCapacidad.idPeriodo = dto.idPeriodo;
            this.verSelector = true;
        } else {
            this.accion = accionSolicitada.EDITAR;
            const nombreCompleto = dto.beneficiario;
            const institucion = dto.institucion;
            this.psCapacidadesService.obtenerPorId(
                dto.idInstitucion,
                dto.idBeneficiario,
                dto.idComponenteCapacidad
            ).then(
                resp => {
                    this.dtoCapacidad = resp;
                    this.nivel = this.dtoCapacidad.idNivel;
                    this.gradoAcademico = this.dtoCapacidad.idGradoEducativo;
                    this.dtoCapacidad.idNivel = null;
                    this.dtoCapacidad.idGradoEducativo = null;
                    this.listaNivel = [];
                    this.listaNivel.push({ label: '-- Seleccione --', value: null });
                    this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_NIVEL)
                        .then(respuesta => {
                            respuesta.forEach(obj => this.listaNivel.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                            this.dtoCapacidad.idNivel = this.nivel;
                        });

                    let CodigoElemento;

                    if (this.nivel === 'INIC') {
                        this.verGrado = false;
                        CodigoElemento = PsConstante.MISCELANEO_GRADO_ACADEMICO_INICIAL;
                    } else if (this.nivel === 'PRIM') {
                        this.verGrado = false;
                        CodigoElemento = PsConstante.MISCELANEO_GRADO_ACADEMICO_PRIMARIA;
                    } else if (this.nivel === 'SECU') {
                        this.verGrado = false;
                        CodigoElemento = PsConstante.MISCELANEO_GRADO_ACADEMICO_SECUNDARIA;
                    } else {
                        this.verGrado = true;
                        this.listaGradoAcademico = [];
                    }

                    this.listaGradoAcademico = [];
                    this.listaGradoAcademico.push({ label: '-- Seleccione --', value: null });
                    this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, CodigoElemento)
                        .then(respuesta => {
                            respuesta.forEach(obj => this.listaGradoAcademico.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                            this.dtoCapacidad.idGradoEducativo = this.gradoAcademico;
                        });

                    this.dtoCapacidad.validarNinos = verNinos;
                    this.dtoCapacidad.nombreCompleto = nombreCompleto;
                    this.dtoCapacidad.institucionNombre = institucion;
                    this.verSelector = true;
                }
            );
        }
    }
}
