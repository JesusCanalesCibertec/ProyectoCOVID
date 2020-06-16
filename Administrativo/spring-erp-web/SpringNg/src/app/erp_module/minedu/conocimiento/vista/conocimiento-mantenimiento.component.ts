import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent } from 'primeng/api';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';

import { DataTable, SelectItem } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { accionSolicitada } from 'src/app/base_module/components/basecomponent';
import { Conocimiento, ConocimientoPk } from '../dominio/conocimiento';
import { ConocimientoService } from '../servicio/conocimiento.service';
import { MaMiscelaneosdetalle, MaMiscelaneosdetallePk } from 'src/app/erp_module/shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { MessageService } from 'primeng/components/common/messageservice';



@Component({
  selector: 'app-conocimiento-mantenimiento',
  templateUrl: './conocimiento-mantenimiento.component.html'
})
export class ConocimientoMantenimientoComponent extends PrincipalBaseComponent implements OnInit {

  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,

    //servicios
    private conocimientoServicio: ConocimientoService,
    private miscelaneoServicio: MaMiscelaneosdetalleServicio
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  @ViewChild(DataTable) dt: DataTable;

  //Emitter
  @Output() buscarpadre = new EventEmitter;
  @Output() bloquear = new EventEmitter();
  @Output() desbloquear = new EventEmitter();

  //Declaraciones
  filtro: DtoTabla = new DtoTabla();
  tipos: SelectItem[] = [];
  areas: SelectItem[] = [];
  verModal: Boolean = false;
  deshabilitar: Boolean = false;
  conocimiento: Conocimiento = new Conocimiento();
  conocimientoPk: ConocimientoPk = new ConocimientoPk();
  miscelaneoPK: MaMiscelaneosdetallePk = new MaMiscelaneosdetallePk();
  action: number;
  arreglo: string[] = [];
  areabloquear: Boolean = false;

  ngOnInit() {
    super.ngOnInit();
    this.cargarTipos();

  }

  iniciarComponente(accion: accionSolicitada, id: number) {

    this.accion = accion;
    this.deshabilitar = false;
    this.areabloquear = false;
    this.bloquear.emit();

    if (this.accion != this.ACCIONES.NUEVO) {

      this.conocimientoPk.idConocimiento = id;
      this.obtener();

      if (this.accion == this.ACCIONES.VER) {
        this.deshabilitar = true;
      }

    } else {

      accion = this.ACCIONES.NUEVO;
      this.conocimiento = new Conocimiento();
      this.areas = [];

    }

    this.desbloquear.emit();
    this.verModal = true;

  }

  cargarTipos() {
    this.tipos.push({ value: null, label: '--Seleccione--' });
    this.miscelaneoServicio.listarActivos('MP', 'TIPOCON')
      .then(res => {
        res.forEach(obj => this.tipos.push({ value: obj.codigoelemento, label: obj.descripcionlocal }))
      });

  }

  areaT: string;
  cargarAreas() {
    this.areabloquear = true;
    this.conocimiento.area = null;
    this.areas = [];
    this.areas.push({ value: null, label: '--Seleccione--' });

    if (this.estaVacio(this.conocimiento.tipo)) {
      this.areas = [];
      this.areabloquear = false;
    }
    else if (this.conocimiento.tipo == 'T') {
      this.miscelaneoServicio.listarActivos('MP', 'AREATCON')
        .then(res => {
          res.forEach(obj => this.areas.push({ value: obj.codigoelemento, label: obj.descripcionlocal }));
          this.conocimiento.area = this.areaT;
          this.areaT = null;
        });
    } else {
      this.areas = [];
      this.areabloquear = false;
      this.conocimiento.area = null;
    }

  }


  guardar() {
    if (!this.validar()) {
      return;
    }
    if (this.accion === this.ACCIONES.NUEVO) {
      this.conocimientoServicio.registrar(this.conocimiento).then(
        resultado => {
          this.desbloquear.emit();
          if (resultado != null) {
            this.mostrarMensajeExito(this.getMensajeGrabado(resultado.idConocimiento));
            this.verModal = false;
            this.buscarpadre.emit();
          }
        });
    } else {
      this.conocimientoServicio.actualizar(this.conocimiento).then(resultado => {
        this.desbloquear.emit();
        if (resultado != null) {
          this.mostrarMensajeExito(this.getMensajeActualizado(resultado.idConocimiento));
          this.verModal = false;
          this.buscarpadre.emit();
        }
      });
    }
  }


  validar(): boolean {
    let valida = true;
    this.messageService.clear();

    if (this.estaVacio(this.conocimiento.tipo)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El tipo de conocimiento es requerido' });
      valida = false;
    }
    if (this.estaVacio(this.conocimiento.area) && this.conocimiento.tipo == 'T') {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El área del conocimiento es requerido' });
      valida = false;
    }
    if (this.estaVacio(this.conocimiento.nombre)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El nombre del conocimiento es requerido' });
      valida = false;
    }

    return valida;
  }

  obtener() {
    this.conocimientoServicio.obtenerPorId(this.conocimientoPk.idConocimiento)
      .then(res => {
        this.conocimiento = res;
        this.areaT = res.area;
        this.cargarAreas();
      });
  }

  salir() {
    this.verModal = false;
  }



}
