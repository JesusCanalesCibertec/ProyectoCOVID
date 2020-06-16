import { Component, OnInit, ViewChild, ChangeDetectorRef, Input } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent } from 'primeng/api';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { PersonaService } from '../servicio/persona.service';
import { DataTable, SelectItem, ConfirmationService } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PersonaMantenimientoComponent } from './persona-mantenimiento.component';
import { dtoPersona } from '../dominio/dtoPersona';
import { Persona, PersonaPk } from '../dominio/persona';
import { PersonaconocimientoListadoComponent } from '../../personaconocimiento/vista/personaconocimiento-listado.component';
import { PersonatituloListadoComponent } from '../../personatitulo/vista/personatitulo-listado.component';
import { MessageService } from 'primeng/components/common/messageservice';
import { ContratacionadendaListadoComponent } from '../../contratacionadendaentregable/vista/contratacionadenda-listado.component';
import { ContratacionentregableListadoComponent } from '../../contratacionadendaentregable/vista/contratacionentregable-listado.component';
import { ContratacionNuevoComponent } from './contratacion-nuevo.component';
import { Contratacion, ContratacionPk } from '../../contratacion/dominio/contratacion';


@Component({
  selector: 'app-persona',
  templateUrl: './persona-listado.component.html'
})
export class PersonaListadoComponent extends PrincipalBaseComponent implements OnInit {

  //Modales
  @ViewChild(PersonaMantenimientoComponent) personaMantenimientoComponent: PersonaMantenimientoComponent;
  @ViewChild(PersonaconocimientoListadoComponent) personaconocimientoListadoComponent: PersonaconocimientoListadoComponent;
  @ViewChild(PersonatituloListadoComponent) personatituloListadoComponent: PersonatituloListadoComponent;
  @ViewChild(ContratacionadendaListadoComponent) ContratacionadendaListadoComponent: ContratacionadendaListadoComponent;
  @ViewChild(ContratacionentregableListadoComponent) ContratacionentregableListadoComponent: ContratacionentregableListadoComponent;
  @ViewChild(ContratacionNuevoComponent) ContratacionNuevoComponent: ContratacionNuevoComponent;

  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,

    //servicios
    private cdref: ChangeDetectorRef,
    private personaServicio: PersonaService,
    private miscelaneoServicio: MaMiscelaneosdetalleServicio,
    private confirmationService: ConfirmationService,
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  @ViewChild(DataTable) dt: DataTable;

  //Declaraciones
  filtro: DtoTabla = new DtoTabla();
  estados: SelectItem[] = [];
  estadoBol: Boolean;
  persona: Persona = new Persona();
  personapk: PersonaPk = new PersonaPk();
  cols: any[] = [];
  modalidades: SelectItem[] = [];


  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  ngOnInit() {
    super.ngOnInit();
    this.cargarColumnas();
    this.cargarModalidades();
    this.estadoBol = this.flagAEstado(this.filtro.estado);
  }

  obtenerEstilosCesado(parametro: String) {
    if (parametro != null) {
      return { 'color': 'red' };
    }
  }

  obtenerEstilosEditar(parametro: String) {
    if (parametro != null) {
      return { 'pointer-events': 'none', opacity: 0.3 };
    } else {
      return { 'pointer-events': '', opacity: 10 };
    }
  }

  cargarColumnas() {
    this.cols = [
      { field: 'id', header: 'N°', width: 50 },
      { field: 'dni', header: 'DNI', width: 120 },
      { field: 'nombre', header: 'Nombre', width: 250 },
      { field: 'nomModalidad', header: 'Modalidad', width: 150 },
      { field: 'desde', header: 'Desde', width: 100 },
      { field: 'hasta', header: 'Hasta', width: 100 },
      { field: 'correo', header: 'Correo', width: 250 },
      { header: 'Entregables / Adendas', width: 100 },
      { field: 'estado', header: 'Estado', width: 100 },
      { header: 'Títulos', width: 80 },
      { header: 'Conocimientos', width: 120 },
      // { header: 'Histórico', width: 80 },
      { header: 'Acción', width: 100 }
    ];
  }

  cargarModalidades() {
    this.modalidades = [];
    this.modalidades.push({ label: '-- Todos --', value: null });
    this.modalidades.push({ label: 'ORDEN DE SERVICIO', value: 'OS' });
    this.modalidades.push({ label: 'CAS', value: 'CAS' });
    this.modalidades.push({ label: 'PRÁCTICAS', value: 'MDF' });
  }

  cargarPaginacion(event: LazyLoadEvent) {
    this.bloquearPagina();
    this.filtro.paginacion.listaResultado = [];
    this.filtro.paginacion.registroInicio = event.first;
    this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
    this.personaServicio.listarPaginacion(this.filtro)
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
    this.personaMantenimientoComponent.iniciarComponente(this.ACCIONES.NUEVO, null);
  }

  editar(bean: dtoPersona) {
    this.personaMantenimientoComponent.iniciarComponente(this.ACCIONES.EDITAR, bean);
  }

  ver(bean: dtoPersona) {
    this.personaMantenimientoComponent.iniciarComponente(this.ACCIONES.VER, bean);
  }

  listarConocimientos(bean: dtoPersona) {
    this.personaconocimientoListadoComponent.iniciarComponente(bean, this.filtro);
  }

  listarTitulos(bean: dtoPersona) {
    this.personatituloListadoComponent.iniciarComponente(bean, this.filtro);
  }

  verModalFolio: Boolean;
  nota: string;
  auxfechacese: Date;
  auxmotivocese: string;
  nombrecese: string;
  auxdto: dtoPersona = new dtoPersona();
  cambiarestado(bean: dtoPersona, dt: any) {
    this.nombrecese = null;
    if (bean.estado == 'A') {
      this.nota = 'Nota: Al inactivar se finaliza el contrato antes de su fecha de cierre.';
      this.confirmationService.confirm({
        header: 'Confirmación',
        icon: 'fa fa-question-circle',
        message: this.getMensajePreguntaInactivar(),
        accept: () => {
          this.verModalFolio = true;
          this.auxdto = bean;
          this.nombrecese = 'CESE DE CONTRATO de ' + bean.nombre;
        },
        reject: () => {
          this.verModalFolio = false;
          this.nota = null;
        }
      });
    } else {
      if (bean.idModalidad != 'OS' && bean.fechacese == null) {
        this.nota = 'Nota: También puede activar extendiendo este contrato mediante una adenda.';
      }
      this.confirmationService.confirm({
        header: 'Confirmación',
        icon: 'fa fa-question-circle',
        message: 'Para activar al personal ' + bean.nombre + ' requiere registrar un nuevo contrato, continuar?',
        accept: () => {
          this.ContratacionNuevoComponent.iniciarComponente(bean);
          this.nota = null;
        },
        reject: () => {
          this.nota = null;
        }
      });
    }
  }

  inactivar(dt: any) {
    if (!this.validar()) {
      return;
    }
    this.bloquearPagina();
    let auxbean = new Contratacion();
    auxbean.idContratacion = this.auxdto.idContratacion;
    auxbean.idPersona = this.auxdto.id;
    auxbean.fechacese = this.auxfechacese;
    auxbean.motivocese = this.auxmotivocese;
    this.personaServicio.cambiarestado(auxbean).then(res => {
      if (res != null) {
        this.mostrarMensajeExito(this.getMensajeInactivado(this.auxdto.id));
        this.buscar(dt);
        this.verModalFolio = false;
        this.auxfechacese = null;
        this.auxmotivocese = null;
        this.desbloquearPagina();
      }
    });
  }


  validar(): boolean {
    let valida = true;
    this.messageService.clear();

    if (this.auxfechacese == null || this.auxfechacese == undefined) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione una fecha de salida' });
      valida = false;
    }
    if (this.estaVacio(this.auxmotivocese)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El motivo es requerido' });
      valida = false;
    }
    return valida;
  }

  consulta(bean: any) {
    // let dto = new dtoPersona();
    // dto.id = bean.idPersona;
    // dto.nombre = bean.nombre;
    // dto.apellido = bean.apellido;
    // this.confirmationService.confirm({
    //   header: 'Confirmación',
    //   icon: 'fa fa-question-circle',
    //   message: "Continuar con el registro de títulos de " + bean.nombre + '?',
    //   accept: () => {
    //     this.listarTitulos(dto);
    //   }
    // });
  }

  recargar(bean: any) {
    this.filtro.paginacion.listaResultado = [];
    this.filtro.paginacion.registroInicio = bean.paginacion.registroInicio - 1;
    this.filtro.paginacion.cantidadRegistrosDevolver = bean.paginacion.cantidadRegistrosDevolver;
    this.personaServicio.listarPaginacion(this.filtro)
      .then(res => {
        this.filtro.paginacion = res;
        this.desbloquearPagina();
      })
  }

  listar(bean: dtoPersona) {
    this.nota = null;
    if (bean.idModalidad == 'OS') {
      if(bean.hasta == null){
        this.mostrarMensajeError("Se requiere de una fecha fin en la contratación");
        return;
      }
      this.ContratacionentregableListadoComponent.iniciarComponente(bean);
    } else {
      if (bean.hasta >= new Date() && bean.fechacese == null) {
        this.mostrarMensajeError("Requiere que finalize el periodo de contrato");
        return;
      }
      this.ContratacionadendaListadoComponent.iniciarComponente(bean);
    }
  }

}
