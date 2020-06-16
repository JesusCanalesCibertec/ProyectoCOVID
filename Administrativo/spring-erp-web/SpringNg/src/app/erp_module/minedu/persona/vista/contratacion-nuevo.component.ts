import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { DataTable, SelectItem } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { MessageService } from 'primeng/components/common/messageservice';
import { Contratacion, ContratacionPk } from '../../contratacion/dominio/contratacion';
import { ContratacionService } from '../../contratacion/servicio/contratacion.service';
import { dtoPersona } from '../dominio/dtoPersona';


@Component({
  selector: 'app-contratacion-nuevo',
  templateUrl: './contratacion-nuevo.component.html'
})
export class ContratacionNuevoComponent extends PrincipalBaseComponent implements OnInit {

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
  auxmodalidad: Boolean = true;
  nombre: string;
  fechaminima: Date;
  auxverificacion: Boolean;

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
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  ngOnInit() {
    super.ngOnInit();
    this.cargarModalidades();
    this.cargarCargos();
  }

  iniciarComponente(bean: dtoPersona) {
    this.contratacion = new Contratacion();
    this.contratacion.idPersona = bean.id;
    this.nombre = bean.nombre;
    this.fechaminima = this.addDate("d", 1, bean.ultimafecha);
    this.verModal = true;
    this.desbloquear.emit();
  }

  guardar() {
    if (!this.validar()) {
      return;
    }
    this.ContratacionServicio.registrar(this.contratacion).then(
      resultado => {
        if (resultado != null) {
          this.mostrarMensajeExito('La contratación Nro.  '+ resultado.idContratacion + ' se registró con éxito.');
          this.verModal = false;
          this.buscarpadre.emit();
        }
      });
  }

  validar(): boolean {
    let valida = true;
    this.messageService.clear();

    if (this.estaVacio(this.contratacion.idModalidad)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione una modalidad' });
      valida = false;
    }
    if (this.estaVacio(this.contratacion.cargo)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un cargo' });
      valida = false;
    }
    if (this.contratacion.fechainicio == null) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de inicio es requerida' });
      valida = false;
    }
    if (this.contratacion.fechafin == null) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de fin es requerida' });
      valida = false;
    }
    if(this.contratacion.fechafin != null && this.contratacion.fechainicio != null){
      if(this.contratacion.fechafin < this.contratacion.fechainicio){
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha fin debe ser mayor que la de inicio' });
        valida = false;
      }
    }
    if (this.estaVacio(this.contratacion.numeroorden) && this.contratacion.idModalidad == 'OS') {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El número de orden es requerido' });
      valida = false;
    }
    return valida;
  }

  salir() {
    this.verModal = false;
  }
  cargarModalidades() {
    this.modalidades = [];
    this.modalidades.push({ label: '-- Seleccione --', value: null });
    this.modalidades.push({ label: 'ORDEN DE SERVICIO', value: 'OS' });
    this.modalidades.push({ label: 'CAS', value: 'CAS' });
    this.modalidades.push({ label: 'PRÁCTICAS', value: 'MDF' });
  }

  cargarCargos() {
    this.cargos = [];
    this.cargos.push({ label: '-- Seleccione --', value: null });
    this.miscelaneoServicio.listarActivos('MP', 'CARGOMIN').then(r => {
      r.forEach(
        rr => {
          this.cargos.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
        });
    });
  }

  auxorden: string;
  auxnumero: number;
  cambiarModalidad() {
    if (this.contratacion.idModalidad == 'OS') {
      this.auxmodalidad = false;
      this.contratacion.numeroorden = this.estaVacio(this.auxorden) ? this.contratacion.numeroorden : this.auxorden;
      this.contratacion.numeroplazo = this.estaVacioNumber(this.auxnumero) ? this.contratacion.numeroplazo : this.auxnumero;
      this.contratacion.numeroplazo = this.estaVacioNumber(this.contratacion.numeroplazo) ? 0 : this.contratacion.numeroplazo;
      this.auxorden = null;
      this.auxnumero = null;
    } else {
      this.auxmodalidad = true;
      this.auxorden = this.contratacion.numeroorden;
      this.auxnumero = this.contratacion.numeroplazo;
      this.contratacion.numeroorden = null;
      this.contratacion.numeroplazo = null;
    }
  }
}
