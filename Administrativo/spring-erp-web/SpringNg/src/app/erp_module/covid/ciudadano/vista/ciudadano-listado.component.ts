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
import { DepartamentoServicio } from 'src/app/erp_module/shared/departamento/servicio/DepartamentoServicio';
import { ProvinciaServicio } from 'src/app/erp_module/shared/provincia/servicio/ProvinciaServicio';
import { ZonapostalServicio } from 'src/app/erp_module/shared/zonapostal/servicio/ZonapostalServicio';
import { dtoCiudadano } from '../dominio/dtoCiudadano';
import { Router } from '@angular/router';




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
    private departamentoServicio: DepartamentoServicio,
    private provinciaServicio: ProvinciaServicio,
    private distritoServicio: ZonapostalServicio,
    private confirmationService: ConfirmationService,
    private router: Router,
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
    super.ngOnInit();
    this.bloquearPagina();
    this.cargarColumnas();
    this.cargarPaises();
    this.cargarDepartamentos();
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
      { header: 'Triajes', width: 100 },
      //{ header: 'Acción', width: 100 }
    ];
  }


  cargarPaises() {
    this.paises = [];
    this.paises.push({ value: null, label: '--Todos--' });
    this.paisServicio.listarTodos().then(res => {
      res.forEach(obj => this.paises.push({ value: obj.idPais, label: obj.descripcion }))
    });
  }

  cargarDepartamentos() {
    this.departamentos = [];
    this.departamentos.push({ value: null, label: '--Todos--' });
    this.departamentoServicio.listarTodos().then(res => {
      res.forEach(obj => this.departamentos.push({ value: obj.departamento, label: obj.descripcion }))
    })
  }

  cargarProvincias() {
    this.provincias = [];
    this.distritos = [];
    this.filtro.provincia = null;
    this.filtro.distrito = null;
    this.provincias.push({ value: null, label: '--Todos--' });
    this.provinciaServicio.listarActivosPorDepartamento(this.filtro.departamento).then(res => {
      res.forEach(obj => this.provincias.push({ value: obj.provincia, label: obj.descripcion }));
    })
  }

  cargarDistritos() {
    this.distritos = [];
    this.filtro.distrito = null;
    this.distritos.push({ value: null, label: '--Todos--' });
    this.distritoServicio.listarActivosPorProvincia(this.filtro.departamento, this.filtro.provincia).then(res => {
      res.forEach(obj => this.distritos.push({ value: obj.codigopostal, label: obj.descripcion }));
    })
  }

  cargarPaginacion(event: LazyLoadEvent) {
    this.bloquearPagina();
    this.filtro.paginacion.listaResultado = [];
    this.filtro.paginacion.registroInicio = event.first;
    this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
    this.ciudadanoServicio.listarPaginacion(this.filtro)
      .then(res => {
        this.filtro.paginacion = res;
        this.desbloquearPagina();
      })
  }

  buscar(dt: any) {
    if (!this.estaVacio(this.filtro.documento)) {
      if (this.filtro.documento.length < 8) {
        return;
      }
    }
    if (!this.estaVacio(this.filtro.nombre)) {
      if (this.filtro.nombre.length < 5) {
        return;
      }
    }
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

  obtener(dto: dtoCiudadano) {
    this.router.navigate(['spring/triaje-listado', dto.codigo], { skipLocationChange: true });
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
