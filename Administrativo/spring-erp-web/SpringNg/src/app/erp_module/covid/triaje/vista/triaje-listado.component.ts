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
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { dtoTriaje } from '../dominio/dtoTriaje';
import { Resultado } from '../../resultado/dominio/resultado';
import { Ciudadano, CiudadanoPk } from '../../ciudadano/dominio/ciudadano';
import { dtoCiudadano } from '../../ciudadano/dominio/dtoCiudadano';




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
    private triajeServicio: TriajeService,
    private router: Router,
    private route: ActivatedRoute,
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  @ViewChild(DataTable) dt: DataTable;

  areabloquear: Boolean = false;
  cols: any[] = [];
  ciudadano: dtoCiudadano = new dtoCiudadano();

  //Declaraciones
  //filtro: FiltroTriaje = new FiltroTriaje();
  estados: SelectItem[] = [];
  paises: SelectItem[] = [];
  listadoTriajes: DtoTabla[] = [];
  Triaje: Triaje = new Triaje();
  Triajepk: TriajePk = new TriajePk();
  dtoTriaje: dtoTriaje = new dtoTriaje();
  resultados: Resultado[] = [];
  ciudadanos: SelectItem[] = [];

  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  ngOnInit() {
    super.ngOnInit();
    this.bloquearPagina();
    this.ciudadano = JSON.parse(this.route.snapshot.params['dto']);
    this.cargarColumnas();
    this.cargarResultados();
    this.listarTriajes();
  }

  cargarColumnas() {
    this.cols = [
      { field: 'secuencia', header: 'Nro.', width: 50 },
      { field: 'num1', header: 'Nro. de síntomas', width: 130 },
      { field: 'num2', header: 'Nro. de situaciones', width: 130 },
      { field: 'num3', header: 'Nro. de condiciones', width: 130 },
      { header: 'Resultado', width: 100 },
      { field: 'fechainicio1', header: 'Fecha de evaluación', width: 150 },
      { header: 'Acción', width: 50 }
    ];
  }

  listarTriajes() {
    this.listadoTriajes = [];
    this.triajeServicio.listarporciudadano(this.ciudadano.codigo).then(res => {
      if (res != null) {
        this.listadoTriajes = res;
        var dto = new DtoTabla();
        dto.codigoNumerico = this.listadoTriajes[0].codigoNumerico;
        dto.secuencia = this.listadoTriajes[0].secuencia;
        this.obtener(dto);
      }
      this.desbloquearPagina();
    });
  }

  cargarResultados() {
    this.resultados.push({ idResultado: null, nombre: 'Sano', descripcion: '', recomendacion: '', color: 'green' });
    this.resultados.push({ idResultado: null, nombre: 'Sospechoso', descripcion: '', recomendacion: '', color: '#8f9a9f' });
    this.resultados.push({ idResultado: null, nombre: 'Positivo leve', descripcion: '', recomendacion: '', color: 'yellow' });
    this.resultados.push({ idResultado: null, nombre: 'Positivo moderado', descripcion: '', recomendacion: '', color: 'orange' });
    this.resultados.push({ idResultado: null, nombre: 'Positivo crítico', descripcion: '', recomendacion: '', color: 'red' });
    this.resultados.push({ idResultado: 6, nombre: 'Sin evaluación', descripcion: '', recomendacion: '', color: '#f5f5dc' });
  }



  volver() {
    this.router.navigate(['spring/ciudadano-listado']);
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

  obtener(dto: DtoTabla) {
    this.bloquearPagina();
    this.dtoTriaje.secuencia = dto.secuencia;
    this.triajeServicio.obtenerPorId(dto.codigoNumerico).then(res => {
      if (res != null) {
        this.Triaje = res;
        this.suministraDatos(this.Triaje);
      }
      this.desbloquearPagina();
    });
  }

  suministraDatos(bean: Triaje) {

    this.dtoTriaje.disgus = this.dato_a_boleano(bean.disgus);
    this.dtoTriaje.tos = this.dato_a_boleano(bean.tos);
    this.dtoTriaje.dolor = this.dato_a_boleano(bean.dolor);
    this.dtoTriaje.difi = this.dato_a_boleano(bean.difi);
    this.dtoTriaje.nasal = this.dato_a_boleano(bean.nasal);
    this.dtoTriaje.fiebre = this.dato_a_boleano(bean.fiebre);

    this.dtoTriaje.situacion1 = bean.situacion1;
    this.dtoTriaje.situacion2 = bean.situacion2;
    this.dtoTriaje.situacion3 = bean.situacion3;

    this.dtoTriaje.obesidad = this.dato_a_boleano(bean.obesidad);
    this.dtoTriaje.pulmonar = this.dato_a_boleano(bean.pulmonar);
    this.dtoTriaje.asma = this.dato_a_boleano(bean.asma);
    this.dtoTriaje.diabetes = this.dato_a_boleano(bean.diabetes);
    this.dtoTriaje.hipertension = this.dato_a_boleano(bean.hipertension);
    this.dtoTriaje.cardio = this.dato_a_boleano(bean.cardio);
    this.dtoTriaje.renal = this.dato_a_boleano(bean.renal);
    this.dtoTriaje.cancer = this.dato_a_boleano(bean.cancer);

    this.dtoTriaje.fechainicio = bean.fechainicio;
  }

  dato_a_boleano(dato: string): Boolean {
    if (dato == "SI") {
      return true;
    } else { return false; }
  }

  obtenerEstilos(color: string) {
    return {
      'background-color': color,
      'border-radius': '50%',
      'width': '20px',
      'height': '20px',
      'display': 'inline',
      'margin-left': '40%',
      'border': '1px solid black'
    };
  }
}
