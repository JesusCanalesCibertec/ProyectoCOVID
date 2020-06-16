import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent } from 'primeng/api';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ContratacionService } from '../servicio/contratacion.service';
import { DataTable, SelectItem } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { accionSolicitada } from 'src/app/base_module/components/basecomponent';
import { Contratacion, ContratacionPk } from '../dominio/contratacion';
import { PersonaService } from '../../persona/servicio/persona.service';
import { MessageService } from 'primeng/components/common/messageservice';


@Component({
  selector: 'app-contratacion-mantenimiento',
  templateUrl: './contratacion-mantenimiento.component.html'
})
export class ContratacionMantenimientoComponent extends PrincipalBaseComponent implements OnInit {

  @ViewChild(DataTable) dataTableComponent: DataTable;

  //Declaraciones
  filtro: DtoTabla = new DtoTabla();
  modalidades: SelectItem[] = [];
  cargos: SelectItem[] = [];
  personas: SelectItem[] = [];
  verModal: Boolean = false;
  verCampo: Boolean = false;
  deshabilitar: Boolean = false;
  contratacion: Contratacion = new Contratacion();
  ContratacionPk: ContratacionPk = new ContratacionPk();
  action: number;
  titulomoda: string;
  minDate: Date;
  arreglo: string[] = [];

  //Emitter
  @Output() bloquear = new EventEmitter();
  @Output() desbloquear = new EventEmitter();
  @Output() buscarpadre = new EventEmitter();

  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,
  //Servicios
    private ContratacionServicio: ContratacionService,
    private miscelaneoServicio: MaMiscelaneosdetalleServicio,
    private personaServicio: PersonaService
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  ngOnInit() {
    super.ngOnInit();
  
    this.cargos.push({ label: '-- Seleccione --', value: null });
    this.miscelaneoServicio.listarActivos('MP', 'CARGOMIN').then(r => {
      r.forEach(
        rr => {
          this.cargos.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
        });
    });

    this.personas.push({ label: '-- Seleccione --', value: null });
    this.personaServicio.listarNombres().then(r => {
      r.forEach(
        rr => {
          this.personas.push({ label: rr.apellido + ', ' + rr.nombre, value: rr.idPersona });
        });
    });
  }

  iniciarComponente(accion: accionSolicitada, id: number, modalidad: string) {
    this.bloquear.emit();
    this.verCampo = false;
    this.accion = accion;
    this.titulomoda = modalidad == 'OS' ? 'ORDEN DE SERVICIO' : modalidad == 'CAS' ? 'CONTRATO ADMINISTRATIVO DE SERVICIOS' : 'PRÁCTICAS';
    this.verCampo = modalidad == 'OS' ? true : 'CAS' ? false : false;
    this.deshabilitar = false;


    if (this.accion != this.ACCIONES.NUEVO) {
      this.ContratacionPk.idContratacion = id;
      this.obtener();
      if (this.accion == this.ACCIONES.VER) {
        this.deshabilitar = true;
      }
    } else {
      accion = this.ACCIONES.NUEVO;
      this.contratacion = new Contratacion();
      this.contratacion.numeroplazo = 1;
      this.contratacion.fechainicio = new Date();
      this.contratacion.idModalidad = modalidad;
    }
    this.verModal = true;
    this.desbloquear.emit();
  }

  guardar() {
    if (!this.validar()) {
      return;
    }
    if (this.accion === this.ACCIONES.NUEVO) {
      this.ContratacionServicio.registrar(this.contratacion).then(
        resultado => {
          if (resultado != null) {
            this.mostrarMensajeExito(this.getMensajeGrabado2(resultado.idContratacion));
            this.verModal = false;
            this.buscarpadre.emit();
          }
        });
    } else {
      this.ContratacionServicio.actualizar(this.contratacion).then(resultado => {
        if (resultado != null) {
          this.mostrarMensajeExito(this.getMensajeActualizado2(resultado.idContratacion));
          this.verModal = false;
          this.buscarpadre.emit();
        }
      });
    }
  }

  validar(): boolean {
    let valida = true;
    this.messageService.clear();
    if (this.estaVacio(this.contratacion.numeroorden) && this.contratacion.idModalidad == 'OS') {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El número de orden es requerido' });
      valida = false;
    }
    if (this.estaVacio(this.contratacion.cargo)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El cargo es requerido' });
      valida = false;
    }
    if (this.contratacion.idPersona === undefined || this.contratacion.idPersona == null) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El personal es requerido' });
      valida = false;
    }
    if (this.contratacion.fechainicio == null) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de inicio es requerida' });
      valida = false;
    }
    // if (this.contratacion.fechafin == null && this.contratacion.idModalidad == 'CAS') {
    //   this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de fin es requerida' });
    //   valida = false;
    // }
    return valida;
  }

  obtener() {
    this.ContratacionServicio.obtenerPorId(this.ContratacionPk.idContratacion)
      .then(res => {
        this.contratacion = res;
      });
  }

  salir() {
    this.verModal = false;
  }
  // calcularfecha() {
  //   if (this.contratacion.fechainicio != null) {
  //     this.contratacion.fechafin = this.addDate("d", this.contratacion.numeroplazo * 30, this.contratacion.fechainicio);
  //   }
  // }
}
