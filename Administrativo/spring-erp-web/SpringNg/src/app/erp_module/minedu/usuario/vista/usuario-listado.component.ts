import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent } from 'primeng/api';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { SelectItem, ConfirmationService } from 'primeng/primeng';
import { MessageService } from 'primeng/components/common/messageservice';
import { PersonaService } from '../../persona/servicio/persona.service';
import { Usuario } from 'src/app/erp_module/shared/usuario/dominio/Usuario';
import { Seguridadperfilusuario } from '../dominio/seguridadperfilusuario';
import { ContratacionService } from '../../contratacion/servicio/contratacion.service';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-usuario-listado',
  templateUrl: './usuario-listado.component.html'
})
export class UsuarioListadoComponent extends PrincipalBaseComponent implements OnInit {

  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,

    //servicios
    private cdref: ChangeDetectorRef,
    private personaServicio: PersonaService,
    private contratacionServicio: ContratacionService,
    private confirmationService: ConfirmationService
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  @ViewChild(Table) dt: Table;

  //Declaraciones
  personas: SelectItem[] = [];
  filtro: DtoTabla = new DtoTabla();
  usuario: Usuario = new Usuario();
  cols: any[] = [];
  perfiles: SelectItem[] = [];
  idPersona: number;
  rowUsuario: Seguridadperfilusuario = new Seguridadperfilusuario();
  auxUsuario: string;



  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  ngOnInit() {
    super.ngOnInit();
    this.cargarColumnas();
    this.cargarPerfiles();
    this.cargarPersonal();

  }

  cargarPaginacion(event: LazyLoadEvent) {
    this.bloquearPagina();
    this.filtro.paginacion.listaResultado = [];
    this.filtro.paginacion.registroInicio = event.first;
    this.filtro.paginacion.cantidadRegistrosDevolver = 1000000;
    this.personaServicio.listarPaginacionUsuario(this.filtro)
      .then(res => {
        this.filtro.paginacion = res;

        let lista = new Array<Seguridadperfilusuario>();
        lista = [...this.filtro.paginacion.listaResultado]
        lista.forEach(rd => rd.activo = rd.estado.trim() == 'A' ? true : false);
        this.filtro.paginacion.listaResultado = lista;
        if(this.auxUsuario != null){
          this.rowUsuario = this.filtro.paginacion.listaResultado.find(obj=> obj.usuario.trim() == this.auxUsuario);
          this.auxUsuario = null;
        }
        this.desbloquearPagina();
      })
  }

  cargarColumnas() {
    this.cols = [
      { field: 'perfil', header: 'Perfil', width: 150 },
      { field: 'nombres', header: 'Nombres', width: 450 },
      { field: 'usuario', header: 'Usuario', width: 250 },
      { field: 'ultimo', header: 'Última Conexión', width: 200 },
      { field: 'creacionfecha', header: 'Fecha de registro', width: 150 },
      { field: 'estado', header: 'Estado', width: 100 },
      { header: 'Acción', width: 100 }
    ];
  }

  cargarPerfiles() {
    this.perfiles.push({ value: null, label: '--Seleccione--' });
    this.perfiles.push({ value: 'GESTOR', label: 'GESTOR' });
    this.perfiles.push({ value: 'LECTOR', label: 'LECTOR' });
  }

  cargarPersonal() {
    this.personas = [];
    this.personas.push({ label: '--Seleccione--', value: null });
    this.contratacionServicio.Contratosactivos().then(r => {
      r.forEach(rr => {
        this.personas.push({ label: rr.persona, value: rr.idPersona });
      });
    });
  }
  cargarUsuario() {
    if (!this.estaVacioNumber(this.idPersona)) {
      this.personaServicio.obtenerPorId(this.idPersona).then(res => {
        this.filtro.paginacion.listaResultado.find(obj => obj.secuencia == 1).usuario = res.usuario;
      })
    } else {
      this.filtro.paginacion.listaResultado.find(obj => obj.secuencia == 1).usuario = null;
    }
  }

  nuevo() {
    let bean = this.filtro.paginacion.listaResultado.find(obj => obj.secuencia == 1);
    if (bean != undefined) {
      this.mostrarMensajeError("El nuevo usuario requiere ser registrado");
      return;
    }
    this.idPersona = null;
    let lista = new Array<Seguridadperfilusuario>();
    lista = [...this.filtro.paginacion.listaResultado]
    this.enumerar(lista);

    let fila = new Seguridadperfilusuario();
    fila.secuencia = 1;
    fila.creacionFecha = null;
    console.log(fila);
    lista.push(fila);
    lista = lista.sort((a, b) => Number(a.secuencia) - Number(b.secuencia));

    this.filtro.paginacion.listaResultado = lista;
  }

  cancelar() {
    let lst = [...this.filtro.paginacion.listaResultado];
    lst = lst.filter(p => p.secuencia !== 1);
    this.enumerar(lst);
    this.filtro.paginacion.listaResultado = lst;
  }

  cambiarestado(bean: Seguridadperfilusuario) {
    bean.estado = bean.activo == true ? 'A' : 'I';
    this.bloquearPagina();
    this.personaServicio.actualizarUsuario(bean).then(res => {
      this.desbloquearPagina();
    });
  }

  guardar(bean: Seguridadperfilusuario) {
    let arreglo = this.filtro.paginacion.listaResultado.filter(obj => obj.usuario == bean.usuario);
    if (arreglo.length > 1) {
      this.mostrarMensajeInfo("El usuario " + bean.usuario + " se encuentra en la lista");
      return;
    }
    if (!this.validar(bean)) {
      return;
    }
    this.bloquearPagina();
    this.personaServicio.registrarUsuario(bean).then(res => {
      if (res != null) {
        this.mostrarMensajeExito("El usuario " + res.usuario + " se agregó con éxito");
        this.auxUsuario = bean.usuario;
        this.dt.reset();
      }
      this.desbloquearPagina();
    })
  }

  validar(bean: Seguridadperfilusuario): boolean {
    let valida = true;
    this.messageService.clear();

    if (this.estaVacio(bean.perfil)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Perfil del usuario es requerido' });
      valida = false;
    }
    if (this.estaVacio(bean.usuario)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un personal' });
      valida = false;
    }
    return valida;
  }

  eliminar(bean: Seguridadperfilusuario) {
    this.confirmationService.confirm({
      header: 'Confirmación',
      icon: 'fa fa-question-circle',
      message: 'Desea eliminar al usuario ' + bean.usuario + '?',
      accept: () => {
        this.bloquearPagina();
        this.personaServicio.eliminarUsuario(bean).then(res => {
          if (res != null) {
            this.mostrarMensajeExito(this.getMensajeElilminadoEmpty());
            this.dt.reset();
          }
          this.desbloquearPagina();
        });
      }
    });
  }

  enumerar(lista: any[]) {
    let numero = 1;
    lista.forEach(res => {
      res.secuencia = numero + 1;
      numero = numero + 1;
    })
  }
}
