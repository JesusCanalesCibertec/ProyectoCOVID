import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent, MessageService, ConfirmationService, SelectItem } from 'primeng/api';
import { CiudadanoService } from '../servicio/Ciudadano.service';
import { Ciudadano, CiudadanoPk } from '../dominio/Ciudadano';
import { FiltroCiudadano } from '../dominio/filtroCiudadano';
import { PaisServicio } from '../../pais/servicio/PaisServicio';
import { DepartamentoServicio } from 'src/app/erp_module/shared/departamento/servicio/DepartamentoServicio';
import { ProvinciaServicio } from 'src/app/erp_module/shared/provincia/servicio/ProvinciaServicio';
import { ZonapostalServicio } from 'src/app/erp_module/shared/zonapostal/servicio/ZonapostalServicio';
import { dtoCiudadano } from '../dominio/dtoCiudadano';
import { Router } from '@angular/router';
import { Resultado } from '../../resultado/dominio/resultado';




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

  areabloquear: Boolean = false;
  cols: any[] = [];

  //Declaraciones
  filtro: FiltroCiudadano = new FiltroCiudadano();
  estados: SelectItem[] = [];
  paises: SelectItem[] = [];
  departamentos: SelectItem[] = [];
  provincias: SelectItem[] = [];
  distritos: SelectItem[] = [];
  tipos: SelectItem[] = [];
  Ciudadano: Ciudadano = new Ciudadano();
  Ciudadanopk: CiudadanoPk = new CiudadanoPk();
  resultados: Resultado[] = [];

  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  cars: SelectItem[];
  selectedCar2: string = 'BMW';

  ngOnInit() {
    super.ngOnInit();
    this.bloquearPagina();
    this.cargarPaises();
    this.cargarDepartamentos();
    this.cargarResultados();
    this.cargarEstados();
    this.cargarColumnas();
    this.cargarTipos();

    this.cars = [
      { label: 'Audi', value: 'Audi' },
      { label: 'BMW', value: 'BMW' },
      { label: 'Fiat', value: 'Fiat' },
      { label: 'Ford', value: 'Ford' },
      { label: 'Honda', value: 'Honda' },
      { label: 'Jaguar', value: 'Jaguar' },
      { label: 'Mercedes', value: 'Mercedes' },
      { label: 'Renault', value: 'Renault' },
      { label: 'VW', value: 'VW' },
      { label: 'Volvo', value: 'Volvo' }
    ];
  }

  cargarColumnas() {
    this.cols = [
      { field: 'codigo', header: 'ID', width: 50 },
      { field: 'tipodocumento', header: 'Tipo documento', width: 100 },
      { field: 'documento', header: 'Nro. Documento', width: 120 },
      { field: 'nombrecompleto', header: 'Nombres y Apellidos', width: 250 },
      { field: 'nompais', header: 'País', width: 120 },
      { field: 'direccion', header: 'Dirección actual', width: 250 },
      { field: 'nomdepartamento', header: 'Departamento', width: 150 },
      { field: 'nomprovincia', header: 'Provincia', width: 150 },
      { field: 'nomdistrito', header: 'Distrito', width: 200 },
      { field: 'nomestado', header: 'Estado', width: 150 },
      { header: 'Triajes', width: 70 },
      //{ header: 'Acción', width: 100 }
    ];
  }

  cargarTipos() {
    this.tipos = [];
    this.tipos.push({ value: null, label: '--Todos--' });
    this.tipos.push({ value: 1, label: 'D.N.I' });
    this.tipos.push({ value: 2, label: 'Carnet Ext.' });
  }


  cargarResultados() {
    this.resultados.push({ idResultado: 1, nombre: 'Sano', descripcion: '', recomendacion: '', color: 'green' });
    this.resultados.push({ idResultado: 2, nombre: 'Sospechoso', descripcion: '', recomendacion: '', color: '#8f9a9f' });
    this.resultados.push({ idResultado: 3, nombre: 'Positivo leve', descripcion: '', recomendacion: '', color: 'yellow' });
    this.resultados.push({ idResultado: 4, nombre: 'Positivo moderado', descripcion: '', recomendacion: '', color: 'orange' });
    this.resultados.push({ idResultado: 5, nombre: 'Positivo crítico', descripcion: '', recomendacion: '', color: 'red' });
    this.resultados.push({ idResultado: 6, nombre: 'Sin evaluación', descripcion: '', recomendacion: '', color: '#f5f5dc' });
  }

  cargarEstados() {
    this.estados = [];
    this.estados.push({ value: null, label: '--Todos--' });
    this.resultados.forEach(res => {
      this.estados.push({ value: res.idResultado, label: res.nombre });
    });
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
    if (this.estaVacio(this.filtro.departamento)) { return; }
    this.provincias.push({ value: null, label: '--Todos--' });
    this.provinciaServicio.listarActivosPorDepartamento(this.filtro.departamento).then(res => {
      res.forEach(obj => this.provincias.push({ value: obj.provincia, label: obj.descripcion }));
    })
  }

  cargarDistritos() {
    this.distritos = [];
    this.filtro.distrito = null;
    if (this.estaVacio(this.filtro.provincia)) { return; }
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

  deshabilitar: boolean = true;
  maximo: number;
  buscar(dt: any) {
    this.deshabilitar = !this.estaVacioNumber(this.filtro.tipodocumento) ? false : true;
    this.maximo = 5;
    if (!this.estaVacio(this.filtro.documento)) {
      if (this.filtro.documento.length < 8 && this.filtro.tipodocumento == 1) {
        return;
      }

      if (this.filtro.documento.length < 12 && this.filtro.tipodocumento == 2) {
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
    if (dto.estado == 6) {
      this.mostrarMensajeInfo('El ciudadano no cuenta con triajes realizados');
      return;
    }
    this.router.navigate(['spring/triaje-listado', JSON.stringify(dto)], { skipLocationChange: true });
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

  obtenerEstilos(estado: number, tipo: number) {
    if (!this.estaVacioNumber(estado)) {
      if (tipo == 1) {
        return {
          'background-color': this.resultados[estado - 1].color,
          'border-radius': '50%',
          'width': '20px',
          'height': '20px',
          'border': '1px solid black',
          'margin': 'auto'
        }
      } else {
        return {
          'background-color': this.resultados[estado - 1].color,
          'border-radius': '50%',
          'width': '16px',
          'height': '16px',
          'border': '1px solid black'
        }
      }

    }
  }
}
