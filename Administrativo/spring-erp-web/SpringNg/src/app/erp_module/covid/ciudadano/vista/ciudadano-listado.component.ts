import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent } from 'primeng/api';
import { CiudadanoService } from '../servicio/Ciudadano.service';
import { DataTable, SelectItem, ConfirmationService } from 'primeng/primeng';
import { Ciudadano, CiudadanoPk } from '../dominio/Ciudadano';
import { MessageService } from 'primeng/components/common/messageservice';
import { FiltroCiudadano } from '../dominio/filtroCiudadano';
import { PaisServicio } from '../../pais/servicio/PaisServicio';




@Component({
  selector: 'app-ciudadano',
  templateUrl: './ciudadano-listado.component.html'
})
export class CiudadanoListadoComponent extends PrincipalBaseComponent implements OnInit {

  //Modales
  //@ViewChild(CiudadanoMantenimientoComponent) CiudadanoMantenimientoComponent: CiudadanoMantenimientoComponent;

  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,

    //servicios
    private cdref: ChangeDetectorRef,
    private ciudadanoServicio: CiudadanoService,
    private paisServicio: PaisServicio,
    private confirmationService: ConfirmationService,

  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  @ViewChild(DataTable) dt: DataTable;

  areabloquear: Boolean = false;
  cols: any[] = [];

  //Declaraciones
  filtro: FiltroCiudadano = new FiltroCiudadano();
  estados: SelectItem[] = [];
  paises: SelectItem[] = [];
  departamentos: SelectItem[] = [];
  provincias: SelectItem[] = [];
  distritos: SelectItem[] = [];
  Ciudadano: Ciudadano = new Ciudadano();
  Ciudadanopk: CiudadanoPk = new CiudadanoPk();

  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  ngOnInit() {
    this.bloquearPagina();
    super.ngOnInit();
    this.cargarColumnas();
    this.cargarPaises();
  }

  cargarColumnas() {
    this.cols = [
      { field: 'codigo', header: 'ID', width: 50 },
      { field: 'documento', header: 'Nro. Documento', width: 100 },
      { field: 'nombrecompleto', header: 'Nombres y Apellidos', width: 250 },
      { field: 'nompais', header: 'País', width: 120 },
      { field: 'direccion', header: 'Dirección actual', width: 250 },
      { field: 'nomdepartamento', header: 'Departamento', width: 150 },
      { field: 'nomprovincia', header: 'Provincia', width: 150 },
      { field: 'nomdistrito', header: 'Distrito', width: 200 },
      { field: 'nomestado', header: 'Estado', width: 100 },
      { header: 'Acción', width: 100 }
    ];
  }


  cargarPaises() {
    this.paises.push({ value: null, label: '--Todos--' });
    this.paisServicio.listarTodos().then(res=>{
        res.forEach(obj=> this.paises.push({value:obj.idPais,label:obj.descripcion}))
    });
  }

  cargarAreas() {
    // this.areabloquear = true;
    // this.areas = [];
    // this.areas.push({ value: null, label: '--Todos--' });

    // if (this.filtro.valor1 == 'T') {
    //   this.miscelaneoServicio.listarActivos('MP', 'AREATCON')
    //     .then(res => {
    //       res.forEach(obj => this.areas.push({ value: obj.codigoelemento, label: obj.descripcionlocal }));
    //     });
    // } else {
    //   this.areas = [];
    //   this.areabloquear = false;
    //   this.filtro.valor2 = null;
    // }
  }

  cargarPaginacion(event: LazyLoadEvent) {
    this.bloquearPagina();
    this.filtro.paginacion.listaResultado = [];
    this.filtro.paginacion.registroInicio = event.first;
    this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;

    this.ciudadanoServicio.listarPaginacion(this.filtro)
      .then(res => {
        this.filtro.paginacion = res;
      })
    this.desbloquearPagina();
  }

  buscar(dt: any) {
    dt.reset();
  }


  nuevo() {
    //this.CiudadanoMantenimientoComponent.iniciarComponente(this.ACCIONES.NUEVO, null);
  }

  editar(bean: Ciudadano) {

   // this.CiudadanoMantenimientoComponent.iniciarComponente(this.ACCIONES.EDITAR, bean.idCiudadano);
  }

  ver(bean: Ciudadano) {

   // this.CiudadanoMantenimientoComponent.iniciarComponente(this.ACCIONES.VER, bean.idCiudadano);
  }


  cambiarestado(bean: Ciudadano, dt: any) {
    this.Ciudadanopk.idCiudadano = bean.idCiudadano;
    if (bean.estado == 'A') {
      this.confirmationService.confirm({
        header: 'Confirmación',
        icon: 'fa fa-question-circle',
        message: this.getMensajePreguntaInactivar(),
        accept: () => {
          this.bloquearPagina();
          this.ciudadanoServicio.cambiarestado(this.Ciudadanopk).then(res => {
            if (res != null) {
              this.buscar(dt);
              this.mostrarMensajeExito(this.getMensajeInactivado(res.idCiudadano));
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
          this.ciudadanoServicio.cambiarestado(this.Ciudadanopk).then(res => {
            if (res != null) {
              this.buscar(dt);
              this.mostrarMensajeExito(this.getMensajeActivado(res.idCiudadano));
              this.desbloquearPagina();
            }
          });
        }
      });
    }

  }

}
