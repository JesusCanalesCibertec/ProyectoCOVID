import { Component, OnInit, EventEmitter, Output, ViewChild } from '@angular/core';
import { DataTable, ConfirmationService, SelectItem, Dropdown } from 'primeng/primeng';
import { Personatitulo, PersonatituloPk } from '../dominio/personatitulo';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { dtoPersona } from '../../persona/dominio/dtoPersona';
import { PersonatituloServicio } from '../servicio/personatitulo.service';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { dtoPersonatitulo } from '../dominio/dtoPersonatitulo';
import { MaMiscelaneosdetalle } from 'src/app/erp_module/shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { MessageService } from 'primeng/components/common/messageservice';





@Component({
    selector: 'app-personatitulo-listado',
    templateUrl: './personatitulo-listado.component.html'
})

export class PersonatituloListadoComponent extends PrincipalBaseComponent implements OnInit {



    @ViewChild(DataTable) dt: DataTable;
    @ViewChild(Dropdown) dd: Dropdown;

    deshabilitar: Boolean = false;
    desLimpiar: Boolean = false;
    auxEstadoPersona: Boolean = false;
    verModal: Boolean = false;
    verModalFolio: boolean = false;
    verMiscelaneo: boolean = false;
    listado = [];
    estados = [];
    centros: SelectItem[] = [];
    carreras: SelectItem[] = [];
    niveles: SelectItem[] = [];
    grados: SelectItem[] = [];
    registrosPorPagina: number = 10;

    tipoT: string;
    areaT: string;

    @Output() bloquear = new EventEmitter();
    @Output() desbloquear = new EventEmitter();
    @Output() actualizarGrilla = new EventEmitter();

    Personatitulo: Personatitulo = new Personatitulo();
    PersonatituloPk: PersonatituloPk = new PersonatituloPk();
    filtro: DtoTabla = new DtoTabla();
    titulo: string = null;
    idPersona: number = null;
    fechaactual: Date = null;

    miscelaneo: MaMiscelaneosdetalle = new MaMiscelaneosdetalle();

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private confirmationService: ConfirmationService,
        private personatituloService: PersonatituloServicio,
        private miscelaneoServicio: MaMiscelaneosdetalleServicio,
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {

    }

    iniciarComponente(bean: dtoPersona, filtro: DtoTabla) {
        this.bloquear.emit();
        this.deshabilitar = false;
        this.desLimpiar = true;
        this.idPersona = bean.id
        this.titulo = bean.nombre;
        this.auxEstadoPersona = bean.estado == 'A' ? false : true;
        this.filtro = filtro;
        this.cargarCarreras();
        this.cargarNiveles();
        this.cargarCentros();
        this.listarDefecto();

    }

    listarDefecto() {
        this.personatituloService.listado(this.idPersona).then(res => {
            this.listado = res;
            this.verModal = true;
            this.desbloquear.emit();
        });
    }

    salir() {
        this.verModal = false;
    }

    /*****************************del modal***************************************/

    nuevo() {
        this.deshabilitar = false;
        this.accion = this.ACCIONES.NUEVO;
        this.Personatitulo = new Personatitulo();
        this.cargarCarreras();
        this.cargarNiveles();
        this.cargarCentros();
        this.limpiar();
        this.verModalFolio = true;
    }

    cargarCentros() {
        this.centros = [];
        this.centros.push({ value: null, label: '--Seleccione--' })
        this.centros.push({ value: 'X', label: 'Otro' })
        return this.miscelaneoServicio.listarActivos('MP', 'CENTROES')
            .then(res => {
                res.forEach(obj => {
                    obj.codigoelemento == '1' ? this.centros.push({ value: obj.codigoelemento.trim(), label: obj.descripcionlocal }) :
                        this.centros.push({ value: obj.codigoelemento.trim(), label: "(" + obj.codigoelemento + ") " + obj.descripcionlocal })
                });
            });
    }
    cargarCarreras() {
        this.carreras = [];
        this.carreras.push({ value: null, label: '--Seleccione--' })
        this.carreras.push({ value: 'X', label: 'Otro' })
        return this.miscelaneoServicio.listarActivos('MP', 'CARRERAS')
            .then(res => {
                res.forEach(obj => {
                    this.carreras.push({ value: obj.codigoelemento, label: obj.descripcionlocal })
                });
                this.desbloquear.emit();
            });
    }


    cargarGrados() {
        this.bloquear.emit();
        this.desLimpiar = false;
        this.grados = [];
        this.Personatitulo.idGrado = null;
        if (this.accion === this.ACCIONES.NUEVO) {
            this.grados.push({ value: null, label: '--Seleccione--' })
            this.miscelaneoServicio.listarActivos('MP', 'GRADOINS  ')
                .then(res => {
                    res = res.sort((a, b) => Number(a.codigoelemento) - Number(b.codigoelemento));
                    res.forEach(obj => {
                        if (parseInt(this.Personatitulo.idNivel) <= 2 || this.estaVacio(this.Personatitulo.idNivel)) {
                            this.grados.push({ value: obj.codigoelemento.trim(), label: obj.descripcionlocal })
                        } else {
                            if (parseInt(obj.codigoelemento) != 3) {
                                this.grados.push({ value: obj.codigoelemento.trim(), label: obj.descripcionlocal })
                            }
                        }
                    });
                    this.desbloquear.emit();
                });
        } else {
            this.miscelaneoServicio.listarActivos('MP', 'GRADOINS  ')
                .then(res => {
                    res = res.sort((a, b) => Number(a.codigoelemento) - Number(b.codigoelemento));
                    res.forEach(obj => {
                        if (parseInt(this.Personatitulo.idNivel) <= 2) {
                            if (parseInt(obj.codigoelemento) >= parseInt(this.grado)) {
                                this.grados.push({ value: obj.codigoelemento.trim(), label: obj.descripcionlocal })
                                this.desbloquear.emit();
                            }
                        } else {
                            if (parseInt(obj.codigoelemento) != 3 && parseInt(obj.codigoelemento) >= parseInt(this.grado)) {
                                this.grados.push({ value: obj.codigoelemento.trim(), label: obj.descripcionlocal })
                                this.desbloquear.emit();
                            }
                        }
                    });
                    this.Personatitulo.idGrado = this.grado;
                    this.grado = null;
                    this.desbloquear.emit();
                });
        }
    }

    grado: string;
    editar(bean: dtoPersonatitulo) {
        this.accion = this.ACCIONES.EDITAR;
        this.deshabilitar = true;
        var pk = new PersonatituloPk();
        pk.idPersona = bean.idPersona;
        pk.idCarrera = bean.idCarrera;
        pk.idCentro = bean.idCentro;
        pk.idNivel = bean.idNivel;
        this.personatituloService.obtenerPorId(pk).then(
            res => {
                this.Personatitulo = res;
                this.grado = res.idGrado;
                this.cargarGrados();
            }
        );
        this.verModalFolio = true;
    }

    cargarNiveles() {
        this.niveles = [];
        this.niveles.push({ value: null, label: '--Seleccione--' })
        this.miscelaneoServicio.listarActivos('MP', 'NIVELEST')
            .then(res => {
                res = res.sort((a, b) => Number(a.codigoelemento) - Number(b.codigoelemento));
                res.forEach(obj => {
                    this.niveles.push({ value: obj.codigoelemento.trim(), label: obj.descripcionlocal })

                });
            });
    }

    guardar() {
        if (!this.validar()) {
            return;
        }
        this.Personatitulo.idPersona = this.idPersona;

        if (this.accion === this.ACCIONES.NUEVO) {
            if (!this.validarlista()) {
                return;
            }
            this.bloquear.emit();
            this.personatituloService.registrar(this.Personatitulo).then(
                resultado => {
                    if (resultado != null) {
                        this.mostrarMensajeExito(this.getMensajeGrabadoEmpty());
                        this.verModalFolio = false;
                        this.listarDefecto();
                        this.actualizarGrilla.emit(this.filtro);
                    }
                });
        }
        else {
            this.bloquear.emit();
            this.personatituloService.solicitudActualizar(this.Personatitulo).then(resultado => {
                if (resultado != null) {
                    this.mostrarMensajeExito(this.getMensajeActualizadoEmpty());
                    this.verModalFolio = false;
                    this.listarDefecto();
                }
            });
        }
    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();
        if (this.estaVacio(this.Personatitulo.idCarrera)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione una carrera' });
            valida = false;
        }
        if (this.estaVacio(this.Personatitulo.idCentro)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un centro de estudios' });
            valida = false;
        }
        if (this.estaVacio(this.Personatitulo.idNivel)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un nivel de estudio' });
            valida = false;
        }
        if (this.estaVacio(this.Personatitulo.idGrado)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un grado de instrucción' });
            valida = false;
        }
        if (this.listado.length == 0) {
            if (this.Personatitulo.idNivel == '3') {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Se requiere contar con un bachiller o titulado sea técnico o universitario' });
                valida = false;
            }
            if (this.Personatitulo.idNivel == '4') {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Se requiere contar con una maestría titulada' });
                valida = false;
            }
        } else {
            if (this.Personatitulo.idNivel == '3' && this.listado.filter(obj => parseInt(obj.idGrado) > 2).length == 0) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Se requiere contar con un bachiller o titulado sea técnico o universitario' });
                valida = false;
            }
            if (this.Personatitulo.idNivel == '4' && this.listado.filter(obj => obj.idNivel == "3" && obj.idGrado == '4').length == 0) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Se requiere contar con una maestría titulada' });
                valida = false;
            }
        }
        return valida;
    }

    validarlista(): boolean {
        let valida = true;
        this.messageService.clear();

        this.listado.forEach(
            td => {
                if (td.idCarrera == this.Personatitulo.idCarrera &&
                    td.idCentro == this.Personatitulo.idCentro &&
                    td.idNivel == this.Personatitulo.idNivel) {
                    this.messageService.add({
                        severity: 'error', summary: 'Error', detail: 'El registro se encuentra en la lista'
                    });
                    valida = false;
                }
            }
        );
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

    salirRegistro() {
        this.verModalFolio = false;
    }

    eliminar(dto: dtoPersonatitulo) {
        var bean = new Personatitulo;
        bean.idPersona = dto.idPersona;
        bean.idCarrera = dto.idCarrera;
        bean.idCentro = dto.idCentro;
        bean.idNivel = dto.idNivel;

        let listaNivelesActual;
        let auxNivel = parseInt(dto.idNivel) <= 2 ? 2 : parseInt(dto.idNivel);

        if (auxNivel <= 2) {
            listaNivelesActual = this.listado.filter(obj => obj.idNivel <= auxNivel && obj.idGrado > 2);
        } else {
            listaNivelesActual = this.listado.filter(obj => obj.idNivel == auxNivel && obj.idGrado > 2);
        }

        if (listaNivelesActual.length <= 1) {
            let nivelsiguiente = auxNivel + 1;
            let listaNivelsiguiente = this.listado.filter(obj => obj.idNivel == nivelsiguiente);
            if (listaNivelsiguiente.length > 0 && parseInt(dto.idGrado) > 2) {
                this.mostrarMensajeError('No se puede eliminar habiendo una carrera de nivel ' + listaNivelsiguiente[0].nomNivel + ' en la lista');
                return;
            }
        }

        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaEliminar(),
            accept: () => {
                this.bloquear.emit();
                this.personatituloService.eliminar(bean).then(
                    respose => {
                        if (respose != null) {
                            this.mostrarMensajeExito(this.getMensajeElilminadoEmpty());
                            this.listarDefecto();
                            this.actualizarGrilla.emit(this.filtro);
                            this.desbloquear.emit();
                        }
                    }
                );
            }
        });
    }

    tag: string;
    cargarMiscelaneo() {
        this.miscelaneo = new MaMiscelaneosdetalle();
        if (this.Personatitulo.idCarrera == 'X') {
            this.tag = 'CARRERAS';
            this.verMiscelaneo = true;
        } else if (this.Personatitulo.idCentro == 'X') {
            this.tag = 'CENTROES';
            this.verMiscelaneo = true;
        }
    }
    salirMiscelaneo() {
        if (this.Personatitulo.idCarrera == 'X') {
            this.Personatitulo.idCarrera = null;
            this.verMiscelaneo = false;
        } else if (this.Personatitulo.idCentro == 'X') {
            this.Personatitulo.idCentro = null;
            this.verMiscelaneo = false;
        }
    }

    agregar() {
        if (!this.validarMiscelaneo(this.tag)) {
            return;
        }
        this.miscelaneo.aplicacioncodigo = 'MP';
        this.miscelaneo.codigotabla = this.tag;
        this.miscelaneo.compania = '000200';
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: '¿Seguro que desea agregar ' + (this.tag == 'CARRERAS' ? ' la carrera ' : 'el centro de estudios ') +
                (this.miscelaneo.descripcionlocal).toUpperCase() + '?',
            accept: () => {
                this.bloquear.emit();
                switch (this.tag) {
                    case "CARRERAS": {
                        this.miscelaneoServicio.registrar(this.miscelaneo).then(res => {
                            if (res != null) {
                                this.mostrarMensajeExito(this.getMensajeAgregadoEmpty());
                                return this.cargarCarreras().then(a => {
                                    this.Personatitulo.idCarrera = res.codigoelemento;
                                    this.verMiscelaneo = false;
                                    this.desbloquear.emit();
                                })
                            }
                        });
                        break;
                    }
                    case "CENTROES": {
                        this.miscelaneoServicio.registrar(this.miscelaneo).then(res => {
                            if (res != null) {
                                this.mostrarMensajeExito(this.getMensajeAgregadoEmpty());
                                return this.cargarCentros().then(a => {
                                    this.Personatitulo.idCentro = res.codigoelemento;
                                    this.verMiscelaneo = false;
                                    this.desbloquear.emit();
                                })
                            }
                        });
                        break;
                    }
                }
            }
        });
    }

    validarMiscelaneo(tag: string): boolean {
        let valida = true;
        this.messageService.clear();
        if (this.estaVacio(this.miscelaneo.codigoelemento) && tag == 'CENTROES') {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Las siglas son requeridas' });
            valida = false;
        }
        if (this.estaVacio(this.miscelaneo.descripcionlocal)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El nombre es requerido' });
            valida = false;
        }
        return valida;
    }

    limpiar() {
        this.desLimpiar = true;
        this.Personatitulo.idNivel = null;
        this.Personatitulo.idGrado = null;
        this.grados = [];
    }
}



