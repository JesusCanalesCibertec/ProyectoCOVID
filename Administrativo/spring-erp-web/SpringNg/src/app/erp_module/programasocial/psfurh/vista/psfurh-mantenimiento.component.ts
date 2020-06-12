import { PsFurhService } from './../servicio/psfurh.service';
import { BaseComponent, accionSolicitada } from './../../../../base_module/components/basecomponent';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DtoComponenteFurh } from '../dominio/DtoComponenteFurh';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectItem } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsConstante } from '../../psconstantes.ts/psconstantes';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { PopUpMantenimientoMiscelaneoComponent } from 'src/app/erp_module/shared/miscelaneos/vista/miscelaneo-mantenimiento-popup.component';
import { MaMiscelaneosdetalle } from 'src/app/erp_module/shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { validarEmail } from 'src/app/base_module/util/funciones/validation';
import { EdadCompletaPipe } from 'src/app/base_module/util/pipes/edad.pipe';
import { PsInstitucionAreaTrabajoServicio } from '../../psintitucionareatrabajo/servicio/psinstitucionareatrabajo.service';
import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-furhmantenimiento',
    templateUrl: './psfurh-mantenimiento.component.html'
})
export class PsFurhMantenimientoComponent extends BaseComponent implements OnInit {
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private empleadomastService: EmpleadomastServicio,
        private psInstitucionServicio: PsInstitucionServicio,
        private psInstitucionAreaServicio: PsInstitucionAreaServicio,
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
        messageService: MessageService,
        private psFurhService: PsFurhService,
    ) { super(messageService); }

    dtoComponenteFurh: DtoComponenteFurh;
    tiempoPermanencia: string;
    edad: string;
    estados: SelectItem[] = [];
    listaDiscapacidad: SelectItem[] = [];
    listaNivelAcademico: SelectItem[] = [];
    listaEspecialidadAcademica: SelectItem[] = [];
    listaSexo: SelectItem[] = [];
    listaGrupoSanguineo: SelectItem[] = [];
    listaInstitucionAreaTrabajo: SelectItem[] = [];
    listaFactorRh: SelectItem[] = [];
    tipodocumento: SelectItem[] = [];
    listaInstitucion: SelectItem[] = [];
    puedeModificar: Boolean = false;
    puedeModificarEstado: Boolean = false;
    especialidadT: string;
    cambiarInstitucion: Boolean = false;

    @ViewChild(PopUpMantenimientoMiscelaneoComponent)
    popUpMantenimientoMiscelaneoComponent: PopUpMantenimientoMiscelaneoComponent;


    ngOnInit(): void {
        this.dtoComponenteFurh = new DtoComponenteFurh();
        const idEntidad = this.route.snapshot.params['idEntidad'];
        const idInstitucion = this.route.snapshot.params['idInstitucion'];
        const accion = this.route.snapshot.params['accion'];
        this.cargarEstados();

        const p1 = this.cargarSexo();
        const p2 = this.cargarNivelAcademico();
        const p3 = null;
        const p4 = this.cargarDiscapacidad();
        const p5 = this.cargarGrupoSanguineo();
        const p6 = this.cargarFactorRH();
        const p7 = null;
        const p8 = this.cargarInstitucion();

        if (accion !== undefined) {
            // tslint:disable-next-line:triple-equals
            if (accion == this.ACCIONES.VER) {
                this.puedeModificar = true;
            }
        }

        Promise.all([p1, p2, p3, p4, p5, p6, p7, p8]).then(f => {
            if (idEntidad !== undefined) {
                this.editar(idEntidad, idInstitucion);
            } else {
                this.nuevo();
            }
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
        this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
            empleado => {
                this.dtoComponenteFurh.idInstitucion = empleado.idInstitucion;
                this.dtoComponenteFurh.idInstitucionNombre = empleado.idInstitucionNombre;
            }
        );
        this.puedeModificarEstado = false;
        this.accion = accionSolicitada.NUEVO;
    }

    obtenerTiempoPermanencia() {
        this.tiempoPermanencia = '';
        this.tiempoPermanencia = new EdadCompletaPipe().transform(this.dtoComponenteFurh.fechaIngreso);
    }

    obtenerEdad() {
        this.edad = '';
        this.edad = new EdadCompletaPipe().transform(this.dtoComponenteFurh.fechanacimiento);
    }

    editar(idEntidad: number, idInstitucion: string) {
        this.puedeModificarEstado = true;
        this.accion = accionSolicitada.EDITAR;
        this.psFurhService.obtenerPorId(idInstitucion, idEntidad).then(
            resp => {
                this.dtoComponenteFurh = resp;
                this.obtenerTiempoPermanencia();
                this.obtenerEdad();
                this.especialidadT = resp.idEspecialidadAcademica;
                this.dtoComponenteFurh.idEspecialidadAcademica = null;
                this.listaEspecialidadAcademica = [];
                this.listaEspecialidadAcademica.push({ label: '--Seleccione--', value: '' });

                let codigoElemento;
                if (this.dtoComponenteFurh.idNivelAcademico === 'PRO') {
                    codigoElemento = PsConstante.MISCELANEO_ESPECIALIDA_ACADEMICA_PROFESIONAL;
                } else if (this.dtoComponenteFurh.idNivelAcademico === 'TEC') {
                    codigoElemento = PsConstante.MISCELANEO_ESPECIALIDA_ACADEMICA_TECNICO;
                }

                this.listaEspecialidadAcademica = [];
                return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, codigoElemento)
                    .then(td => {
                        td.forEach(obj => this.listaEspecialidadAcademica.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                        this.dtoComponenteFurh.idEspecialidadAcademica = this.especialidadT;
                    });
            }
        );
    }

    guardar() {

        if (!this.validar()) {
            this.desbloquearPagina();
            return;
        }

        // tslint:disable-next-line:triple-equals
        if (this.accion == accionSolicitada.NUEVO) {
            this.psFurhService.registrar(this.dtoComponenteFurh).then(
                resp => {
                    if (resp != null) {
                        this.router.navigate(['spring/psfurh-listado']);
                        this.mostrarMensajeExito(this.getMensajeGrabado(resp.idEntidad));
                    }
                }
            );
        } else {
            this.psFurhService.actualizar(this.dtoComponenteFurh).then(
                resp => {
                    if (resp != null) {
                        this.router.navigate(['spring/psfurh-listado']);
                        this.mostrarMensajeExito(this.getMensajeActualizado(resp.idEntidad));
                    }
                }
            );
        }

    }


    validar() {

        this.messageService.clear();

        let valida = true;

        if (this.dtoComponenteFurh.apellidoMaterno == null || this.dtoComponenteFurh.apellidoMaterno === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Apellido Materno  es requerido' });
            valida = false;
        }
        if (this.dtoComponenteFurh.apellidoPaterno == null || this.dtoComponenteFurh.apellidoPaterno === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El  Apellido Materno es requerido' });
            valida = false;
        }

        if (this.dtoComponenteFurh.nombres == null || this.dtoComponenteFurh.nombres === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Nombre es requerido' });
            valida = false;
        }



        if (this.dtoComponenteFurh.idDiscapacidad == null || this.dtoComponenteFurh.idDiscapacidad === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe selecciona si es discapacitado' });
            valida = false;
        }


        if (this.dtoComponenteFurh.idNivelAcademico == null || this.dtoComponenteFurh.idNivelAcademico === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Nivel Académico es requerido' });
            valida = false;
        }

        if (this.dtoComponenteFurh.idNivelAcademico != null && this.dtoComponenteFurh.idNivelAcademico !== undefined) {
            if (this.dtoComponenteFurh.idNivelAcademico === 'PRO' || this.dtoComponenteFurh.idNivelAcademico === 'TEC') {
                if (this.dtoComponenteFurh.idEspecialidadAcademica === null || this.dtoComponenteFurh.idEspecialidadAcademica === undefined) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe de seleccionar una Especialidad Académica' });
                    valida = false;
                }
            }
        }

        if (this.dtoComponenteFurh.fechanacimiento == null || this.dtoComponenteFurh.fechanacimiento === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'la Fecha de Nacimiento es requerida' });
            valida = false;
        } else {
            if (this.dtoComponenteFurh.fechanacimiento >= new Date()) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de nacimiento debe ser menor a la fecha actual' });
                valida = false;
            }
        }

        if (this.dtoComponenteFurh.idSexo == null || this.dtoComponenteFurh.idSexo === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Debe Seleccionar el Sexo' });
            valida = false;
        }
        if (this.dtoComponenteFurh.fechaIngreso == null || this.dtoComponenteFurh.fechaIngreso === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La Fecha de Ingreso es requerida' });
            valida = false;
        } else {
            if (this.dtoComponenteFurh.fechaIngreso >= new Date()) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de Ingreso debe ser menor a la fecha actual' });
                valida = false;
            }
        }

        if (this.dtoComponenteFurh.documento == null || this.dtoComponenteFurh.documento === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Nro de Documento es requerido' });
            valida = false;
        }

        if (this.dtoComponenteFurh.correo1 !== null && this.dtoComponenteFurh.correo1 !== undefined && this.dtoComponenteFurh.correo1 !== '') {
            const correo1 = validarEmail(this.dtoComponenteFurh.correo1);
            if (!correo1) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Correo Electrónico ingresado no es correcto, considerar el @ y el "." ' });
                valida = false;
            }
        }

        if (this.dtoComponenteFurh.idTipoDocumento !== null && this.dtoComponenteFurh.idTipoDocumento !== undefined) {
            if (this.dtoComponenteFurh.idTipoDocumento === 'D') {
                if (this.dtoComponenteFurh.documento.length !== 8) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El D.N.I debe tener 8 caracteres' });
                    valida = false;
                }

                const dniNumerico = Number(this.dtoComponenteFurh.documento);

                if (isNaN(dniNumerico)) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El D.N.I debe ingresar números' });
                    valida = false;
                }

            }

            if (this.dtoComponenteFurh.idTipoDocumento === 'P') {
                if (this.dtoComponenteFurh.documento.length > 12) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Pasaporte solo permite 12 caracteres' });
                    valida = false;
                }
            }

            if (this.dtoComponenteFurh.idTipoDocumento === 'N') {
                if (this.dtoComponenteFurh.documento.length > 15) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Carnet de Extranjeria solo permite 15 caracteres' });
                    valida = false;
                }
            }


            if (this.dtoComponenteFurh.idTipoDocumento === 'X') {
                if (this.dtoComponenteFurh.documento.length > 12) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Carnet de Extranjeria solo permite 12 caracteres' });
                    valida = false;
                }
            }

            if (this.dtoComponenteFurh.idTipoDocumento === 'R') {
                if (this.dtoComponenteFurh.documento.length !== 11) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El R.U.C. debe tener 11 caracteres' });
                    valida = false;
                }

                const rucNumerico = Number(this.dtoComponenteFurh.documento);

                if (isNaN(rucNumerico)) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El R.U.C. debe ingresar números' });
                    valida = false;
                }

            }
        }


        return valida;

    }

    cargarEstados() {
        this.estados.push({ label: '--Seleccione--', value: '' });
        this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_ESTADO)
            .then(td => {

                for (let i = 0; i < td.length; i++) {
                    this.estados.push({ label: td[i].descripcionlocal, value: td[i].codigoelemento.trim() });
                }
                this.dtoComponenteFurh.estado = 'A';
            });
    }



    cargarDiscapacidad(): Promise<number> {
        this.listaDiscapacidad.push({ label: '--Seleccione--', value: '' });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_DISCAPACIDAD)
            .then(td => {
                for (let i = 0; i < td.length; i++) {
                    this.listaDiscapacidad.push({ label: td[i].descripcionlocal, value: td[i].codigoelemento.trim() });
                }
                return 1;
            });
    }

    buscarNivelAcademico() {
        this.cargarNivelAcademico();
    }

    buscarNivelEspecialidad() {
        this.obtenerEspecialidadAcademica();
    }

    cargarNivelAcademico() {
        this.listaNivelAcademico = [];
        this.listaNivelAcademico.push({ label: '--Seleccione--', value: '' });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_NIVEL_ACADEMICO)
            .then(td => {
                td.forEach(obj => this.listaNivelAcademico.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
            });
    }

    cargarGrupoSanguineo(): Promise<number> {
        this.listaGrupoSanguineo = [];
        this.listaGrupoSanguineo.push({ label: '--Seleccione--', value: '' });
        return this.maMiscelaneosdetalleServicio.listarActivos("PS", PsConstante.MISCELANEO_GRUPO_SANGUINEO)
            .then(td => {
                td.forEach(obj => this.listaGrupoSanguineo.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });
    }

    cargarFactorRH(): Promise<number> {
        this.listaFactorRh = [];
        this.listaFactorRh.push({ label: '--Seleccione--', value: '' });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_FACTOR_RH)
            .then(td => {
                td.forEach(obj => this.listaFactorRh.push({ label: obj.descripcionlocal, value: obj.codigoelemento }));
                return 1;
            });
    }

    obtenerEspecialidadAcademica() {
        if (this.dtoComponenteFurh.idNivelAcademico === 'PRO') {
            this.cargarEspecialidadAcedemicaProfesional();
        } if (this.dtoComponenteFurh.idNivelAcademico === 'TEC' || this.dtoComponenteFurh.idNivelAcademico === 'SEC') {
            this.cargarEspecialidadAcedemicaTecnica();
        } else {
            this.listaEspecialidadAcademica = [];
        }
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

    cargarSexo(): Promise<number> {
        this.listaSexo.push({ label: '--Seleccione--', value: '' });
        return this.maMiscelaneosdetalleServicio.listarActivos(PsConstante.APLICACION_CODIGO, PsConstante.MISCELANEO_SEXO)
            .then(td => {

                for (let i = 0; i < td.length; i++) {
                    this.listaSexo.push({ label: td[i].descripcionlocal, value: td[i].codigoelemento.trim() });
                }
                return 1;
            });
    }

    agregarMiscelaneosAreaTrabajo() {
        const maMiscelaneosdetalle: MaMiscelaneosdetalle = new MaMiscelaneosdetalle();
        maMiscelaneosdetalle.aplicacioncodigo = PsConstante.APLICACION_CODIGO;
        maMiscelaneosdetalle.codigotabla = PsConstante.MISCELANEO_AREA_TRABAJO;
        maMiscelaneosdetalle.compania = '999999';
        maMiscelaneosdetalle.estado = 'A';

        this.popUpMantenimientoMiscelaneoComponent.mostrarVentana(maMiscelaneosdetalle, 'ACA');
    }

    agregarMiscelaneosNivelAcademico() {
        const maMiscelaneosdetalle: MaMiscelaneosdetalle = new MaMiscelaneosdetalle();
        maMiscelaneosdetalle.aplicacioncodigo = PsConstante.APLICACION_CODIGO;
        maMiscelaneosdetalle.codigotabla = PsConstante.MISCELANEO_NIVEL_ACADEMICO;
        maMiscelaneosdetalle.compania = '999999';
        maMiscelaneosdetalle.estado = 'A';

        this.popUpMantenimientoMiscelaneoComponent.mostrarVentana(maMiscelaneosdetalle, 'ACA');
    }

    agregarMiscelaneosEspecialidad() {

        if (this.dtoComponenteFurh.idNivelAcademico === 'PRO') {
            const maMiscelaneosdetalle: MaMiscelaneosdetalle = new MaMiscelaneosdetalle();
            maMiscelaneosdetalle.aplicacioncodigo = PsConstante.APLICACION_CODIGO;
            maMiscelaneosdetalle.codigotabla = PsConstante.MISCELANEO_ESPECIALIDA_ACADEMICA_PROFESIONAL;
            maMiscelaneosdetalle.compania = '999999';
            maMiscelaneosdetalle.estado = 'A';
            this.popUpMantenimientoMiscelaneoComponent.mostrarVentana(maMiscelaneosdetalle, 'PRO');
        } if (this.dtoComponenteFurh.idNivelAcademico === 'TEC') {
            const maMiscelaneosdetalle: MaMiscelaneosdetalle = new MaMiscelaneosdetalle();
            maMiscelaneosdetalle.aplicacioncodigo = PsConstante.APLICACION_CODIGO;
            maMiscelaneosdetalle.codigotabla = PsConstante.MISCELANEO_ESPECIALIDA_ACADEMICA_TECNICO;
            maMiscelaneosdetalle.compania = '999999';
            maMiscelaneosdetalle.estado = 'A';
            this.popUpMantenimientoMiscelaneoComponent.mostrarVentana(maMiscelaneosdetalle, 'TEC');
        } else {

        }
    }

    cancelar() {
        this.router.navigate(['spring/psfurh-listado']);
    }

}
