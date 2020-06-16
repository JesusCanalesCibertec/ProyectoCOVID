import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent } from 'primeng/api';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ContratacionService } from '../servicio/contratacion.service';
import { DataTable, SelectItem, SelectButtonModule } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { ContratacionMantenimientoComponent } from './contratacion-mantenimiento.component';
import { dtoContratacion } from '../dominio/dtoContratacion';
import { Contratacion, ContratacionPk } from '../dominio/contratacion';
import { ContratacionentregableListadoComponent } from '../../contratacionadendaentregable/vista/contratacionentregable-listado.component';
import { ContratacionadendaListadoComponent } from '../../contratacionadendaentregable/vista/contratacionadenda-listado.component';
import { MessageService } from 'primeng/components/common/messageservice';
import { dtoPersona } from '../../persona/dominio/dtoPersona';






@Component({
  selector: 'app-contratacion',
  templateUrl: './contratacion-listado.component.html'
})
export class ContratacionListadoComponent extends PrincipalBaseComponent implements OnInit {

  //Modales
  @ViewChild(ContratacionMantenimientoComponent) ContratacionMantenimientoComponent: ContratacionMantenimientoComponent;
  @ViewChild(ContratacionentregableListadoComponent) ContratacionentregableListadoComponent: ContratacionentregableListadoComponent;
  @ViewChild(ContratacionadendaListadoComponent) ContratacionadendaListadoComponent: ContratacionadendaListadoComponent;


  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,

    //servicios
    private cdref: ChangeDetectorRef,
    private ContratacionServicio: ContratacionService,
    private miscelaneoServicio: MaMiscelaneosdetalleServicio
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  @ViewChild(DataTable) dt: DataTable;

  //Declaraciones
  modalidad: string;
  filtro: DtoTabla = new DtoTabla();
  estados: SelectItem[] = [];
  modalidades: SelectItem[] = [];
  cargos: SelectItem[] = [];
  Contratacion: Contratacion = new Contratacion();
  Contratacionpk: ContratacionPk = new ContratacionPk();


  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  ngOnInit() {
    super.ngOnInit();

    this.cargos.push({ label: '-- Todos --', value: null });


    this.modalidades.push({ label: '-- Todos --', value: null });
    this.modalidades.push({ label: 'ORDEN DE SERVICIO', value: 'OS' });
    this.modalidades.push({ label: 'CAS', value: 'CAS' });
    this.modalidades.push({ label: 'PRÁCTICAS', value: 'MDF' });


    var p1 = this.miscelaneoServicio.listarActivos('PS', 'ESTADOBASI').then(r => {
      r.forEach(
        rr => {
          this.estados.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
        });
    });


    var p2 = this.miscelaneoServicio.listarActivos('MP', 'CARGOMIN').then(r => {
      r.forEach(
        rr => {
          this.cargos.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
        });
    });

    Promise.all([p1, p2]).then(f => {

      this.filtro.estado = "A";

    });


  }



  cargarPaginacion(event: LazyLoadEvent) {
    this.bloquearPagina();
    this.filtro.paginacion.listaResultado = [];
    this.filtro.paginacion.registroInicio = event.first;
    this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
    this.ContratacionServicio.listarPaginacion(this.filtro)
      .then(res => {
        this.filtro.paginacion = res;
        this.desbloquearPagina();
      })
  }

  buscar(dt: any) {
    dt.reset();
  }


  nuevo() {
    if(this.estaVacio(this.filtro.valor1)){
      this.mostrarMensajeInfo("Seleccione una modalidad");
      return;
    }
    this.ContratacionMantenimientoComponent.iniciarComponente(this.ACCIONES.NUEVO, null, this.filtro.valor1);
  }

  editar(bean: dtoContratacion) {

    this.ContratacionMantenimientoComponent.iniciarComponente(this.ACCIONES.EDITAR, bean.idContratacion, bean.idModalidad);
  }

  ver(bean: dtoContratacion) {

    this.ContratacionMantenimientoComponent.iniciarComponente(this.ACCIONES.VER, bean.idContratacion, bean.idModalidad);
  }

  listar(bean: dtoPersona) {
    if (bean.idModalidad == 'OS') {
      this.ContratacionentregableListadoComponent.iniciarComponente(bean);
    } else {
      if (bean.hasta >= new Date()) {
        this.mostrarMensajeError("Requiere que finalize el periodo de contrato");
        return;
      }
      this.ContratacionadendaListadoComponent.iniciarComponente(bean);
    }
  }


  cambiarestado(bean: dtoContratacion) {
    this.Contratacionpk.idContratacion = bean.idContratacion;
    this.ContratacionServicio.cambiarestado(this.Contratacionpk)
      .then(res => {
        if (res != null) {
          if (bean.estado == 'A') {
            this.mostrarMensajeExito(this.getMensajeInactivado(res.idContratacion));
          } else {
            this.mostrarMensajeExito(this.getMensajeActivado(res.idContratacion));
          }

          this.dt.reset();
        }
      });

  }

}
