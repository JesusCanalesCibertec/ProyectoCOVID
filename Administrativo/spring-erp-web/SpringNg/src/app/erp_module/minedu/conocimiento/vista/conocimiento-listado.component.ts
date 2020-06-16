import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent } from 'primeng/api';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ConocimientoService } from '../servicio/conocimiento.service';
import { DataTable, SelectItem, ConfirmationService } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { Conocimiento, ConocimientoPk } from '../dominio/conocimiento';
import { ConocimientoMantenimientoComponent } from './conocimiento-mantenimiento.component';
import { MessageService } from 'primeng/components/common/messageservice';



@Component({
  selector: 'app-conocimiento',
  templateUrl: './conocimiento-listado.component.html'
})
export class ConocimientoListadoComponent extends PrincipalBaseComponent implements OnInit {

  //Modales
  @ViewChild(ConocimientoMantenimientoComponent) ConocimientoMantenimientoComponent: ConocimientoMantenimientoComponent;

  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,

    //servicios
    private cdref: ChangeDetectorRef,
    private ConocimientoServicio: ConocimientoService,
    private miscelaneoServicio: MaMiscelaneosdetalleServicio,
    private confirmationService: ConfirmationService,
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  @ViewChild(DataTable) dt: DataTable;

  areabloquear: Boolean = false;
  cols: any[] = [];

  //Declaraciones
  filtro: DtoTabla = new DtoTabla();
  estados: SelectItem[] = [];
  estadoBol: Boolean;
  tipos: SelectItem[] = [];
  areas: SelectItem[] = [];
  Conocimiento: Conocimiento = new Conocimiento();
  Conocimientopk: ConocimientoPk = new ConocimientoPk();



  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  ngOnInit() {
    this.bloquearPagina();
    super.ngOnInit();
    this.cargarColumnas();
    this.cargarTipos();
    this.estadoBol = this.flagAEstado(this.filtro.estado);
  }

  cargarColumnas() {
    this.cols = [
      { field: 'idConocimiento', header: 'N°', width: 50 },
      { field: 'tipo', header: 'Tipo', width: 150 },
      { field: 'area', header: 'Área', width: 300 },
      { field: 'nombre', header: 'Nombre', width: 400 },
      { field: 'estado', header: 'Estado', width: 100 },
      { header: 'Acción', width: 150 }
    ];
  }


  cargarTipos() {
    this.tipos.push({ value: null, label: '--Todos--' });
    this.miscelaneoServicio.listarActivos('MP', 'TIPOCON')
      .then(res => {
        res.forEach(obj => this.tipos.push({ value: obj.codigoelemento.trim(), label: obj.descripcionlocal }))
      });

  }


  cargarAreas() {
    this.areabloquear = true;
    this.areas = [];
    this.areas.push({ value: null, label: '--Todos--' });

    if (this.filtro.valor1 == 'T') {
      this.miscelaneoServicio.listarActivos('MP', 'AREATCON')
        .then(res => {
          res.forEach(obj => this.areas.push({ value: obj.codigoelemento, label: obj.descripcionlocal }));
        });
    } else {
      this.areas = [];
      this.areabloquear = false;
      this.filtro.valor2 = null;
    }
  }

  cargarPaginacion(event: LazyLoadEvent) {
    this.bloquearPagina();
    this.filtro.paginacion.listaResultado = [];
    this.filtro.paginacion.registroInicio = event.first;
    this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;

    this.ConocimientoServicio.listarPaginacion(this.filtro)
      .then(res => {
        this.filtro.paginacion = res;
        this.desbloquearPagina();
      })

  }

  buscar(dt: any) {
    this.filtro.estado = this.estadoAFlag(this.estadoBol);
    dt.reset();
  }


  nuevo() {
    this.ConocimientoMantenimientoComponent.iniciarComponente(this.ACCIONES.NUEVO, null);
  }

  editar(bean: Conocimiento) {

    this.ConocimientoMantenimientoComponent.iniciarComponente(this.ACCIONES.EDITAR, bean.idConocimiento);
  }

  ver(bean: Conocimiento) {

    this.ConocimientoMantenimientoComponent.iniciarComponente(this.ACCIONES.VER, bean.idConocimiento);
  }


  cambiarestado(bean: Conocimiento, dt: any) {
    this.Conocimientopk.idConocimiento = bean.idConocimiento;
    if (bean.estado == 'A') {
      this.confirmationService.confirm({
        header: 'Confirmación',
        icon: 'fa fa-question-circle',
        message: this.getMensajePreguntaInactivar(),
        accept: () => {
          this.bloquearPagina();
          this.ConocimientoServicio.cambiarestado(this.Conocimientopk).then(res => {
            if (res != null) {
              this.buscar(dt);
              this.mostrarMensajeExito(this.getMensajeInactivado(res.idConocimiento));
              this.desbloquearPagina();
            }
          });
        }
      });
    } else {
      this.confirmationService.confirm({
        header: 'Confirmación',
        icon: 'fa fa-question-circle',
        message: this.getMensajePreguntaActivar(),
        accept: () => {
          this.bloquearPagina();
          this.ConocimientoServicio.cambiarestado(this.Conocimientopk).then(res => {
            if (res != null) {
              this.buscar(dt);
              this.mostrarMensajeExito(this.getMensajeActivado(res.idConocimiento));
              this.desbloquearPagina();
            }
          });
        }
      });
    }

  }

}
