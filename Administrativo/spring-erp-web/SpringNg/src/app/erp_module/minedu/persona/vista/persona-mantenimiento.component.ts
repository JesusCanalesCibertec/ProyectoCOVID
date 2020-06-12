import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { PersonaService } from '../servicio/persona.service';
import { DataTable, SelectItem } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { accionSolicitada } from 'src/app/base_module/components/basecomponent';
import { Persona, PersonaPk } from '../dominio/persona';
import { MessageService } from 'primeng/components/common/messageservice';
import { Car } from 'src/app/demo/domain/car';
import { CarService } from 'src/app/demo/service/carservice';
import { Contratacion, ContratacionPk } from '../../contratacion/dominio/contratacion';
import { ContratacionService } from '../../contratacion/servicio/contratacion.service';
import { dtoContratacion } from '../../contratacion/dominio/dtoContratacion';
import { dtoPersona } from '../dominio/dtoPersona';
import { UsuarioServicio } from 'src/app/login_module/servicio/UsuarioServicio';


@Component({
  selector: 'app-persona-mantenimiento',
  templateUrl: './persona-mantenimiento.component.html'
})
export class PersonaMantenimientoComponent extends PrincipalBaseComponent implements OnInit {

  @ViewChild(DataTable) dataTableComponent: DataTable;

  //Declaraciones
  filtro: DtoTabla = new DtoTabla();
  instrucciones: SelectItem[] = [];
  verModal: Boolean = false;
  deshabilitar: Boolean = false;
  persona: Persona = new Persona();
  personaPk: PersonaPk = new PersonaPk();
  action: number;
  arreglo: string[] = [];

  //Contratacion
  contratacion = new Contratacion();
  idContratacion: number;
  modalidades: SelectItem[] = [];
  cargos: SelectItem[] = [];
  listacontratos: dtoContratacion[] = [];
  cols: any[] = [];
  auxmodalidad: Boolean = true;
  auxverificacion: Boolean;
  internet: Boolean;
  auxultimafecha: Date;
  cantaux: number;

  cars: Car[];

  //Emitter
  @Output() buscarpadre = new EventEmitter;
  @Output() bloquear = new EventEmitter();
  @Output() desbloquear = new EventEmitter();
  @Output() consultar = new EventEmitter();

  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,
    //servicios
    private personaServicio: PersonaService,
    private miscelaneoServicio: MaMiscelaneosdetalleServicio,
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  ngOnInit() {
    super.ngOnInit();
    this.cargarModalidades();
    this.cargarCargos();
    this.cargarColumnas();
  }

  iniciarComponente(accion: accionSolicitada, bean: dtoPersona) {
    this.bloquear.emit();
    this.accion = accion;
    this.deshabilitar = false;
    this.auxverificacion = false;
    this.internet = true;
    this.cantaux = 0;

    if (this.accion != this.ACCIONES.NUEVO) {
      this.cantaux = bean.cantadendasentregable;
      this.personaPk.idPersona = bean.id;
      this.idContratacion = bean.idContratacion;
      this.auxultimafecha = bean.ultimafecha;
      this.obtener();
      if (this.accion == this.ACCIONES.VER) {
        this.deshabilitar = true;
      }
    } else {

      accion = this.ACCIONES.NUEVO;
      this.persona = new Persona();
      this.contratacion = new Contratacion();
      this.verModal = true;
      this.desbloquear.emit();
    }
  }

  guardar() {
    if (!this.validar()) {
      return;
    }
    if (this.accion === this.ACCIONES.NUEVO) {
      this.bloquear.emit();
      this.personaServicio.registrar(this.persona).then(resultado => {
        if (resultado != null) {
          this.mostrarMensajeExito("El registro n° " + resultado.idPersona + " de DNI " + resultado.dni + ' se guardó con éxito');
          this.buscarpadre.emit();
          this.consultar.emit(resultado);
          this.salir();
        }
        this.desbloquear.emit();
      });
    } else {
      this.bloquear.emit();
      this.personaServicio.actualizar(this.persona).then(resultado => {
        if (resultado != null) {
          this.mostrarMensajeExito("El registro n° " + resultado.idPersona + " de DNI " + resultado.dni + ' se actualizó con éxito');
          this.buscarpadre.emit();
          this.salir();
        }
        this.desbloquear.emit();
      });
    }
  }

  validar(): boolean {
    let valida = true;
    this.messageService.clear();

    if (this.estaVacio(this.persona.usuario)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Usuario es requerido' });
      valida = false;
    }
    // if (this.estaVacio(this.persona.correo)) {
    //   this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El correo de la persona es requerido' });
    //   valida = false;
    // }
    if (this.estaVacio(this.persona.dni)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El DNI es requerido' });
      valida = false;
    }
    // if (this.estaVacio(this.persona.nombre)) {
    //   this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El nombre de la persona es requerido' });
    //   valida = false;
    // }
    // if (this.estaVacio(this.persona.apellido)) {
    //   this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El apellido de la persona es requerido' });
    //   valida = false;
    // }
    if (this.estaVacio(this.persona.celular)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El celular es requerido' });
      valida = false;
    }
    if (this.estaVacio(this.persona.idModalidad)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione una modalidad' });
      valida = false;
    }
    if (this.estaVacio(this.persona.cargo)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un cargo' });
      valida = false;
    }
    if (this.persona.fechainicio == null) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de inicio es requerida' });
      valida = false;
    }
    // if (this.persona.fechafin == null) {
    //   this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha de fin es requerida' });
    //   valida = false;
    // }
    if (this.persona.fechafin != null && this.persona.fechainicio != null) {
      if (this.persona.fechafin < this.persona.fechainicio) {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La fecha fin debe ser mayor que la de inicio' });
        valida = false;
      }
    }
    if (this.estaVacio(this.persona.numeroorden) && this.persona.idModalidad == 'OS') {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El número de orden es requerido' });
      valida = false;
    }
    return valida;
  }

  obtener() {
    this.personaServicio.obtenerPorId(this.personaPk.idPersona, this.idContratacion)
      .then(res => {
        this.persona = res;
        this.cambiarModalidad();
        if (this.diferenciadias(res.fechafin, this.auxultimafecha) > 0) {
          this.auxverificacion = true;
        } else {
          this.auxverificacion = false;
        }
        this.verModal = true;
        this.desbloquear.emit();
      });
  }

  aceptar() {
    const reg: any = new Object();
    reg.data = this.persona;
    this.consultar.emit(reg);
    this.salir();
  }

  salir() {
    this.verModal = false;
  }

  limpiarDNI() {
    this.persona.dni = null;
    this.persona.nombre = null;
    this.persona.apellido = null;
  }

  limpiarUsuario() {
    this.persona.usuario = null;
    this.persona.correo = null;
  }

  limpiarAnexo() {
    if (this.persona.anexo.trim().length < 5 && !this.estaVacio(this.persona.anexo)) {
      this.mostrarMensajeAdvertencia('El Anexo de la persona requiere de 5 dígitos');
      this.persona.anexo = null;
    }
  }

  limpiarCelular() {
    if (this.persona.celular.trim().length < 9 && !this.estaVacio(this.persona.celular)) {
      this.mostrarMensajeAdvertencia('El Celular de la persona requiere de 9 dígitos');
      this.persona.celular = null;
    }
  }

  validarDNI() {
    if (this.estaVacio(this.persona.dni)) {
      this.mostrarMensajeInfo('Ingrese un DNI');
      return;
    }
    if (this.persona.dni.trim().length < 8) {
      this.mostrarMensajeInfo('DNI de 8 dígitos requerido');
      return;
    }
    this.bloquear.emit();
    this.personaServicio.ObtenerDatos(this.persona.dni).then(res => {
      if (res != null) {
        this.internet = true;
        if (this.estaVacio(res.apellidoPaterno) || this.estaVacio(res.apellidoMaterno)) {
          this.mostrarMensajeError('Documento inválido');
          this.desbloquear.emit();
          return;
        }
        this.persona.nombre = res.nombres;
        this.persona.apellido = res.apellidoPaterno + ' ' + res.apellidoMaterno;
        this.desbloquear.emit();
      } else {
        this.mostrarMensajeInfo('Habilitado para ingresar de forma manual los nombres y apellidos');
        this.internet = false;
        this.persona.nombre = null;
        this.persona.apellido = null;
        this.desbloquear.emit();
      }
    });
  }

  validarUsuario() {
    if (this.estaVacio(this.persona.usuario)) {
      this.mostrarMensajeInfo('Ingrese un usuario');
      return;
    }
    this.bloquear.emit();
    this.personaServicio.ValidarDirectorio(this.persona.usuario).then(res => {
      if (res != null) {
        this.persona.correo = res.descripcion;

      } else {
        this.mostrarMensajeError('Usuario inválido');
      }
      this.desbloquear.emit();
    });
  }

  //contrataciones
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
    if (this.persona.idModalidad == 'OS') {
      this.auxmodalidad = false;
      this.persona.numeroorden = this.estaVacio(this.auxorden) ? this.persona.numeroorden : this.auxorden;
      this.persona.numeroplazo = this.estaVacioNumber(this.auxnumero) ? this.persona.numeroplazo : this.auxnumero;
      this.persona.numeroplazo = this.estaVacioNumber(this.persona.numeroplazo) ? 0 : this.persona.numeroplazo;
      this.auxorden = null;
      this.auxnumero = null;
    } else {
      this.auxmodalidad = true;
      this.auxorden = this.persona.numeroorden;
      this.auxnumero = this.persona.numeroplazo;
      this.persona.numeroorden = null;
      this.persona.numeroplazo = null;
    }
  }

  cargarColumnas() {
    this.cols = [
      { field: 'secuencia', header: 'N°', width: 50 },
      { field: 'fechainicio', header: 'Desde', width: 80 },
      { field: 'fechafin', header: 'Hasta', width: 80 },
      { field: 'modalidad', header: 'Modalidad', width: 150 },
      { field: 'cargo', header: 'Cargo', width: 200 },
    ];
  }

}
