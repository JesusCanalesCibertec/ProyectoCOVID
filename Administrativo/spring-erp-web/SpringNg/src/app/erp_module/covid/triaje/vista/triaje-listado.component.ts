import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent } from 'primeng/api';
import { TriajeService } from '../servicio/triaje.service';
import { DataTable, SelectItem, ConfirmationService } from 'primeng/primeng';
import { Triaje, TriajePk } from '../dominio/Triaje';
import { MessageService } from 'primeng/components/common/messageservice';
import { PaisServicio } from '../../pais/servicio/PaisServicio';
import { DepartamentoServicio } from 'src/app/erp_module/shared/departamento/servicio/DepartamentoServicio';
import { ProvinciaServicio } from 'src/app/erp_module/shared/provincia/servicio/ProvinciaServicio';
import { ZonapostalServicio } from 'src/app/erp_module/shared/zonapostal/servicio/ZonapostalServicio';
import { Router, ActivatedRoute } from '@angular/router';
import { CiudadanoService } from '../../ciudadano/servicio/Ciudadano.service';




@Component({
  selector: 'app-triaje',
  templateUrl: './triaje-listado.component.html'
})
export class TriajeListadoComponent extends PrincipalBaseComponent implements OnInit {

  //Modales
  //@ViewChild(TriajeMantenimientoComponent) TriajeMantenimientoComponent: TriajeMantenimientoComponent;

  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,

    //servicios
    private cdref: ChangeDetectorRef,
    private TriajeServicio: TriajeService,
    private ciudadanoServicio: CiudadanoService,
    private paisServicio: PaisServicio,
    private departamentoServicio: DepartamentoServicio,
    private provinciaServicio: ProvinciaServicio,
    private distritoServicio: ZonapostalServicio,
    private confirmationService: ConfirmationService,
    private route: ActivatedRoute,
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  @ViewChild(DataTable) dt: DataTable;

  areabloquear: Boolean = false;
  cols: any[] = [];
  idCiudadano: number;
  nombre;
 
  //Declaraciones
  //filtro: FiltroTriaje = new FiltroTriaje();
  estados: SelectItem[] = [];
  paises: SelectItem[] = [];
  departamentos: SelectItem[] = [];
  provincias: SelectItem[] = [];
  distritos: SelectItem[] = [];
  Triaje: Triaje = new Triaje();
  Triajepk: TriajePk = new TriajePk();

  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  ngOnInit() {
    super.ngOnInit();
    this.bloquearPagina();
    this.idCiudadano = this.route.snapshot.params['codigo'] as number;
    this.ciudadanoServicio.obtenerPorId(this.idCiudadano).then(res=>{
      this.nombre = res.nombre+' '+res.apellido;
      this.desbloquearPagina();
    })
    this.cargarColumnas();
    this.cargarPaises();
    this.cargarDepartamentos();
  }

  cargarColumnas() {
    this.cols = [
      { field: 'codigo', header: 'Nro.', width: 50 },
      { field: 'documento', header: 'Nro. de síntomas', width: 150 },
      { field: 'nombrecompleto', header: 'Nro. de situaciones', width: 150 },
      { field: 'nompais', header: 'Nro. de condiciones', width: 150 },
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
    // if (this.estaVacio(this.filtro.departamento)) {
    //   this.provincias = [];
    //   this.distritos = [];
    //   this.filtro.provincia = null;
    //   this.filtro.distrito = null;
    //   return;
    // }
    // this.provincias.push({ value: null, label: '--Todos--' });
    // this.provinciaServicio.listarActivosPorDepartamento(this.filtro.departamento).then(res => {
    //   res.forEach(obj => this.provincias.push({ value: obj.provincia, label: obj.descripcion }));
    // })
  }

  cargarDistritos() {
    this.distritos = [];
    // if (this.estaVacio(this.filtro.provincia)) {
    //   this.distritos = [];
    //   this.filtro.distrito = null;
    //   return;
    // }
    // this.distritos.push({ value: null, label: '--Todos--' });
    // this.distritoServicio.listarActivosPorProvincia(this.filtro.departamento, this.filtro.provincia).then(res => {
    //   res.forEach(obj => this.distritos.push({ value: obj.codigopostal, label: obj.descripcion }));
    // })
  }

  cargarPaginacion(event: LazyLoadEvent) {
    // this.bloquearPagina();
    // this.filtro.paginacion.listaResultado = [];
    // this.filtro.paginacion.registroInicio = event.first;
    // this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
    // this.TriajeServicio.listarPaginacion(this.filtro)
    //   .then(res => {
    //     this.filtro.paginacion = res;
    //     this.desbloquearPagina();
    //   })
  }

  buscar(dt: any) {
    // if (!this.estaVacio(this.filtro.documento)) {
    //   if (this.filtro.documento.length < 8) {
    //     return;
    //   }
    // }
    // if (!this.estaVacio(this.filtro.nombre)) {
    //   if (this.filtro.nombre.length < 5) {
    //     return;
    //   }
    // }
    dt.reset();
  }


  nuevo() {
    //this.TriajeMantenimientoComponent.iniciarComponente(this.ACCIONES.NUEVO, null);
  }

  editar(bean: Triaje) {

    // this.TriajeMantenimientoComponent.iniciarComponente(this.ACCIONES.EDITAR, bean.idTriaje);
  }

  ver(bean: Triaje) {

    // this.TriajeMantenimientoComponent.iniciarComponente(this.ACCIONES.VER, bean.idTriaje);
  }

  obtener(bean: Triaje) {
  }
}
