import { Component, OnInit, EventEmitter, Output, ViewChild, ChangeDetectorRef } from '@angular/core';
import { DataTable, LazyLoadEvent, ConfirmationService, SelectItem } from 'primeng/primeng';
import { Personaconocimiento, PersonaconocimientoPk } from '../dominio/personaconocimiento';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from 'src/app/erp_module/shared/selectores/empleado/dominio/DtoEmpleadoBasico';

import { dtoPersona } from '../../persona/dominio/dtoPersona';
import { PersonaconocimientoServicio } from '../servicio/personaconocimiento.service';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { ConocimientoService } from '../../conocimiento/servicio/conocimiento.service';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { IfStmt } from '@angular/compiler';
import { MessageService } from 'primeng/components/common/messageservice';





@Component({
    selector: 'app-personaconocimiento-listado',
    templateUrl: './personaconocimiento-listado.component.html'
})

export class PersonaconocimientoListadoComponent extends PrincipalBaseComponent implements OnInit {



    @ViewChild(DataTable) dt: DataTable;

    puedeEditar: Boolean = true;
    areabloquear: Boolean = false;
    verModal: Boolean = false;
    verModalFolio: boolean;
    listado = [];
    estados = [];
    programas: SelectItem[] = [];
    componentes: SelectItem[] = [];
    tipos: SelectItem[] = [];
    areas: SelectItem[] = [];
    niveles: SelectItem[] = [];
    conocimientos: SelectItem[] = [];
    registrosPorPagina: number = 10;
    deshabilitar: Boolean = false;

    areaT: string;

    @Output() bloquear = new EventEmitter();
    @Output() desbloquear = new EventEmitter();
    @Output() actualizarGrilla = new EventEmitter();

    Personaconocimiento: Personaconocimiento = new Personaconocimiento();
    PersonaconocimientoPk: PersonaconocimientoPk = new PersonaconocimientoPk();
    titulo: string = null;
    idPersona: number = null;
    fechaactual: Date = null;
    filtro: DtoTabla = new DtoTabla();

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private confirmationService: ConfirmationService,
        private personaconocimientoService: PersonaconocimientoServicio,
        private miscelaneoServicio: MaMiscelaneosdetalleServicio,
        private conocimientoServicio: ConocimientoService
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {
    }

    iniciarComponente(bean: dtoPersona, filtro: DtoTabla) {
        this.bloquear.emit();
        this.idPersona = bean.id
        this.titulo = bean.nombre;
        this.deshabilitar = bean.estado == 'A' ? false : true;
        this.filtro = filtro;
        this.listarDefecto();

    }

    listarDefecto() {
        this.personaconocimientoService.listado(this.idPersona).then(res => {
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
        this.bloquear.emit();
        this.accion = this.ACCIONES.NUEVO;
        this.areas = [];
        this.conocimientos = [];
        this.Personaconocimiento = new Personaconocimiento();
        this.cargarTipos();
        this.cargarNiveles();
        this.verModalFolio = true;
        this.desbloquearPagina();
    }

    cargarTipos() {
        this.tipos = [];
        this.tipos.push({ value: null, label: '--Seleccione--' })
        this.miscelaneoServicio.listarActivos('MP', 'TIPOCON')
            .then(res => {
                res.forEach(obj => this.tipos.push({ value: obj.codigoelemento, label: obj.descripcionlocal }));
                this.desbloquear.emit();
            });

    }

    cargarNiveles() {
        this.niveles = [];
        this.niveles.push({ value: null, label: '--Seleccione--' })
        this.miscelaneoServicio.listarActivos('MP', 'NIVELCON')
            .then(res => {
                res = res.sort((a, b) => Number(a.codigoelemento) - Number(b.codigoelemento));
                res.forEach(obj => this.niveles.push({ value: obj.codigoelemento, label: obj.descripcionlocal }))
            });
    }

    cargarAreas() {
        this.bloquear.emit();
        this.areabloquear = false;
        this.areas = [];
        this.conocimientos = [];
        this.areas.push({ value: null, label: '--Seleccione--' });

        if (this.estaVacio(this.Personaconocimiento.tipo)) {
            this.areas = [];
            this.conocimientos = [];
            this.desbloquear.emit();
        }
        else if (this.Personaconocimiento.tipo == 'T') {
            this.miscelaneoServicio.listarActivos('MP', 'AREATCON')
                .then(res => {
                    res.forEach(obj => this.areas.push({ value: obj.codigoelemento, label: obj.descripcionlocal }))
                    this.desbloquear.emit();
                });
        } else {
            this.areas = [];
            this.areabloquear = true;
            this.Personaconocimiento.area = null;
            this.cargarConocimientos();
        }

    }

    cargarConocimientos() {
        this.conocimientos = [];
        this.conocimientos.push({ value: null, label: '--Seleccione--' })
        if (this.estaVacio(this.Personaconocimiento.area) && this.Personaconocimiento.tipo == 'T') {
            this.conocimientos = [];
        } else {
            var filtro = new DtoTabla();
            filtro.estado = 'A';
            filtro.valor1 = this.Personaconocimiento.tipo;
            filtro.valor2 = this.Personaconocimiento.area;
            this.conocimientoServicio.listado(filtro).then(res => {
                res.forEach(obj => {
                    this.conocimientos.push({ value: obj.idConocimiento, label: obj.nombre });
                    this.desbloquear.emit();
                });
            });
        }
    }

    guardar() {
        if (!this.validar()) {
            return;
        }
        this.Personaconocimiento.idPersona = this.idPersona;

        if (this.accion === this.ACCIONES.NUEVO) {
            this.personaconocimientoService.registrar(this.Personaconocimiento).then(

                resultado => {
                    if (resultado != null) {
                        this.mostrarMensajeExito(this.getMensajeGrabadoEmpty());
                        this.verModalFolio = false;
                        this.actualizarGrilla.emit(this.filtro);
                        this.listarDefecto();
                    }
                });
        } else {
            this.personaconocimientoService.solicitudActualizar(this.Personaconocimiento).then(resultado => {
                this.desbloquearPagina();
                if (resultado != null) {
                    this.mostrarMensajeExito(this.getMensajeGrabadoEmpty());
                    this.verModalFolio = false;
                    this.listarDefecto();
                }
            });
        }
    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        if (this.estaVacio(this.Personaconocimiento.tipo)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un tipo' });
            valida = false;
        }
        if (this.estaVacio(this.Personaconocimiento.area) && this.Personaconocimiento.tipo == 'T') {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un área' });
            valida = false;
        }
        if (this.estaVacioNumber(this.Personaconocimiento.idConocimiento)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un conocimiento' });
            valida = false;
        }
        if (this.estaVacio(this.Personaconocimiento.nivel)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un nivel' });
            valida = false;
        }
        if (this.listado.filter(obj => obj.id == this.Personaconocimiento.idConocimiento).length > 0) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El conocimiento se encuentra registrado' });
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

    eliminar(dto: DtoTabla) {
        var bean = new Personaconocimiento();
        bean.idPersona = this.idPersona;
        bean.idConocimiento = dto.id
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaEliminar(),
            accept: () => {
                this.bloquearPagina();
                this.personaconocimientoService.eliminar(bean).then(
                    respose => {
                        this.desbloquearPagina();

                        if (respose != null) {
                            this.listarDefecto();
                        }
                    }
                );
            }
        });
    }
}



