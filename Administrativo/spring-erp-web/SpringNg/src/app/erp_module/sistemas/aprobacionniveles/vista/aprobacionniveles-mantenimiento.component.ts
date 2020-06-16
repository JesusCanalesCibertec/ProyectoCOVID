import { DataTable, SelectItem, ConfirmationService } from 'primeng/primeng';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CorreoMantenimientoComponent } from './correo-mantenimiento.component';
import { SyAprobacionNiveles, SyAprobacionNivelesPk } from '../dominio/SyAprobacionNiveles';
import { SyAprobacionNivelesDetPk, SyAprobacionNivelesDet } from '../dominio/syaprobacionnivelesdet';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { DtoNivelDetalle } from '../dominio/dto/DtoNivelDetalle';
import { DtoEmpleadoBasico } from '../../../shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { SyAprobacionnivelesServicio } from '../servicio/SyAprobacionnivelesServicio';
import { SyAprobacionprocesoServicio } from '../../aprobaciones/servicio/SyAprobacionprocesoServicio';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { EmpleadomastSelectorComponent } from '../../../shared/selectores/empleado/vista/empleadomastselector.component';
import { HrDepartamentoSelectorComponent } from '../../../shared/departamento/vista/hrdepartamentoselector.component';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
  templateUrl: './aprobacionniveles-mantenimiento.component.html'
})
export class AprobacionNivelesMantenimientoComponent extends PrincipalBaseComponent implements OnInit {

  @ViewChild(EmpleadomastSelectorComponent) empleadomastSelectorComponent: EmpleadomastSelectorComponent;
  @ViewChild(CorreoMantenimientoComponent) correoMantenimientoComponent: CorreoMantenimientoComponent;
  @ViewChild(HrDepartamentoSelectorComponent) hrDepartamentoSelectorComponent: HrDepartamentoSelectorComponent;

  lstAplicacion: SelectItem[] = [];
  lstProceso: SelectItem[] = [];
  nivel: SyAprobacionNiveles = new SyAprobacionNiveles();
  nombreUsuario: string = '';
  nombreAdmin: string = '';
  verMotivoRequerimiento: Boolean = false;
  lvl: DtoNivelDetalle = new DtoNivelDetalle();
  lstDetalle: SyAprobacionNivelesDet[] = [];

  constructor(private confirmationService: ConfirmationService,
    private route: ActivatedRoute,
    private router: Router,
    private syAprobacionNivelesService: SyAprobacionnivelesServicio,
    private syAprobacionProcesoService: SyAprobacionprocesoServicio,
    noAuthorizationInterceptor: NoAuthorizationInterceptor, messageService: MessageService
  ) { super(noAuthorizationInterceptor, messageService); }


  ini() {
    const codigo = this.route.snapshot.params['codigo'];

    if (codigo) {
      this.accion = this.ACCIONES.EDITAR;
      // tslint:disable-next-line:triple-equals
      if (codigo == 1) {
        this.verMotivoRequerimiento = true;
      }

      this.editar();
    } else {
      this.accion = this.ACCIONES.NUEVO;
      this.nuevo();
    }
  }

  ngOnInit() {
    this.listarAplicaciones().then(
      res => {
        this.ini();
      }
    );
  }

  cargarProcesos(): Promise<number> {

    this.lstProceso = [];

    if (this.nivel.aplicacion == null) {
      this.lstProceso.push({ label: ' -- Seleccionar -- ', value: null });
      return;
    }
    this.bloquearPagina();
    this.lstProceso = [];
    this.lstProceso.push({ label: ' -- Seleccionar -- ', value: null });
    return this.syAprobacionProcesoService.listarPorAplicacion(this.nivel.aplicacion).then(
      res => {
        res.forEach(r => this.lstProceso.push({ label: r.proceso, value: r.codigoproceso }));
        this.desbloquearPagina();
        return 1;
      }
    );
  }


  listarAplicaciones(): Promise<number> {
    this.bloquearPagina();
    this.lstAplicacion.push({ label: ' -- Seleccionar -- ', value: null });
    return this.syAprobacionNivelesService.listarAplicacionPorUsuario().then(
      res => {
        res.forEach(r => this.lstAplicacion.push({ label: r.nombre, value: r.codigo }));
        this.desbloquearPagina();
        return this.cargarProcesos();
      }
    );
  }





  salir() {
    this.router.navigate(['/spring/aprobacionniveles-listado']);
  }

  buscar(tbl: DataTable) {

  }

  asignarJefeDirecto() {

    if (this.nivel.flagusuariosuperB) {
      this.mostrarMensajeAdvertencia('No se pueden hacer ediciones mientras el super usuario este activo');
      return;
    }

    if (this.lvl.auxCodigo == null) {
      this.messageService.clear();
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un nivel' });
      return;
    }

    const temp = [];

    const niv: SyAprobacionNivelesDet = new SyAprobacionNivelesDet();

    niv.auxSecuencia = 1;
    niv.codigo = this.nivel.codigo;
    niv.nivel = this.lvl.codigo;
    niv.usuario = 99999;
    niv.auxNombrePersona = 'Aprobación Jefe Directo';
    niv.companyownerusuario = this.nivel.companyOwner;
    niv.flagarea = 'S';
    niv.area = '';
    niv.correoelectronico = '';
    niv.flagsolicitante = 'N';
    niv.flagsuperior = 'N';

    temp.push(niv);


    this.lstDetalle = temp;
    this.lvl.listaDetalle = this.lstDetalle;

  }
  mostrarSelectorEmpleado(tag: string) {

    const u = tag !== 'U';
    const a = tag !== 'A';

    if (u && a && this.nivel.flagusuariosuperB) {
      this.mostrarMensajeAdvertencia('No se pueden hacer ediciones mientras el super usuario este activo');
      return;
    }

    if (this.lvl.auxCodigo == null && tag === 'NL') {
      this.messageService.clear();
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un nivel' });
      return;
    }
    this.empleadomastSelectorComponent.tag = tag;
    this.empleadomastSelectorComponent.estado = '0';
    this.empleadomastSelectorComponent.iniciarComponente();
  }

  mostrarCorreo(tag: string) {
    this.correoMantenimientoComponent.tag = tag;
    this.correoMantenimientoComponent.nivel = this.copiar(this.nivel);
    this.correoMantenimientoComponent.iniciarComponente();
  }

  nuevo() {
    this.nivel = new SyAprobacionNiveles();
    this.nivel.nroniveles = 0;
    this.nivel.aplicacion = '';


    this.nivel.estado = 'A';
    this.nivel.flagAplicaDetalle = 'N';
  }
  editar() {
    const pk: SyAprobacionNivelesPk = new SyAprobacionNivelesPk();

    pk.codigo = this.route.snapshot.params['codigo'];
    pk.companyOwner = this.route.snapshot.params['compania'];

    this.syAprobacionNivelesService.obtenerPorIdCompleto(pk).then(
      res => {
        this.nivel = res;
        this.nivel.ultimafechamodif = new Date(this.nivel.ultimafechamodif);
        if (this.nivel.listaNiveles.length > 0) {

          // let temp = [...this.lstDetalle];

          this.lvl = this.nivel.listaNiveles[0];
          this.lvl.auxCodigo = this.lvl.nombre.substring(6, this.lvl.nombre.length);

          this.lstDetalle = this.nivel.listaNiveles[0].listaDetalle;
        }

        this.indexarDetalles();

        this.nivel.flagusuariosuperB =
          this.nivel.flagusuariosuper === undefined ? false : this.nivel.flagusuariosuper == null ? false : this.nivel.flagusuariosuper === 'N' ? false : true;

        this.nivel.flagusuarioadministradorB =
          this.nivel.flagusuarioadministrador === undefined ?
            false : this.nivel.flagusuarioadministrador == null ?
              false : this.nivel.flagusuarioadministrador === 'N' ? false : true;

        this.nivel.ultimafechamodif = this.nivel.ultimafechamodif === undefined ? null : this.nivel.ultimafechamodif == null ? null : new Date(this.nivel.ultimafechamodif);

        this.nivel.flagsuperiorB = this.flagABoolean(this.nivel.flagsuperior);
        this.nivel.flagsolicitanteB = this.flagABoolean(this.nivel.flagsolicitante);
        this.nivel.nivel0FlagsolicitanteB = this.flagABoolean(this.nivel.nivel0Flagsolicitante);
        this.nivel.nivel0FlagsuperiorB = this.flagABoolean(this.nivel.nivel0Flagsuperior);

        const aux = this.nivel.codigoproceso;
        this.nivel.codigoproceso = null;

        this.cargarProcesos().then(r => {
          this.nivel.codigoproceso = aux;
        });

        let c = 1;
        this.nivel.listaCorreos.forEach(res => { res.id = c; c++; });
        if (this.nivel.flagusuariosuper === 'S') {
          this.nivel.flagusuariosuperB = true;
        } else { this.nivel.flagusuariosuperB = false; }

      }
    );
  }

  indexarDetalles() {
    this.nivel.listaNiveles.forEach(
      niv => {
        let num = 1;
        niv.listaDetalle.forEach(det => {
          det.auxSecuencia = num;
          num++;
        });
      }
    );
  }

  onRowSelect(event) {
    this.lstDetalle = this.lvl.listaDetalle;
    this.lvl.auxCodigo = this.lvl.nombre.substring(6, this.lvl.nombre.length);
  }
  onRowUnselect(event) {
    this.lvl.listaDetalle = this.lstDetalle;
  }

  agregarNivel() {

    if (this.nivel.flagusuariosuperB) {
      this.mostrarMensajeAdvertencia('No se pueden hacer ediciones mientras el super usuario este activo');
      return;
    }

    const temp = [...this.nivel.listaNiveles];

    const niv: DtoNivelDetalle = new DtoNivelDetalle();
    this.nivel.nroniveles++;

    niv.codigo = this.generarNuevoCodigo() + 1;
    niv.nombre = 'NIVEL ' + niv.codigo;

    temp.push(niv);

    this.nivel.listaNiveles = temp;
  }

  quitarNivelB() {


    if (this.nivel.flagusuariosuperB) {
      this.mostrarMensajeAdvertencia('No se pueden hacer ediciones mientras el super usuario este activo');
      return;
    }

    if (this.lvl.codigo != null) {
      const act = this.nivel.listaNiveles.find(x => x.codigo === this.lvl.codigo);
      const indice = this.nivel.listaNiveles.indexOf(act);
      this.nivel.listaNiveles = this.nivel.listaNiveles.filter((val, i) => i !== indice);
      this.nivel.nroniveles--;

      // asignar otro, el anterior

      let n = this.nivel.listaNiveles[0];
      if (n === undefined) {
        n = new DtoNivelDetalle();
      }
      this.lvl = n;
      this.lstDetalle = n.listaDetalle;
    }



  }

  generarSecuenciaDetalle(): number {
    if (this.lstDetalle.length === 0) {
      return 1;
    }

    let max = this.lstDetalle[0].auxSecuencia;

    this.lstDetalle.forEach(reg => {
      if (max < reg.auxSecuencia) {
        max = reg.auxSecuencia;
      }
    });

    return max + 1;
  }

  agregarLinea(data: any) {


    const temp = [...this.lstDetalle];

    const niv: SyAprobacionNivelesDet = new SyAprobacionNivelesDet();

    niv.auxSecuencia = this.generarSecuenciaDetalle();
    niv.codigo = this.nivel.codigo;
    niv.nivel = this.lvl.codigo;
    niv.usuario = data.personaId;
    niv.companyownerusuario = data.companiaId;
    niv.auxNombrePersona = data.personaNombre;
    niv.flagarea = 'S';
    niv.area = '';
    niv.correoelectronico = '';
    niv.flagsolicitante = 'N';
    niv.flagsuperior = 'N';

    temp.push(niv);

    this.lstDetalle = temp;
    this.lvl.listaDetalle = this.lstDetalle;


  }

  generarNuevoCodigo(): number {

    if (this.nivel.listaNiveles.length <= 0) { return 0; }

    let max = this.nivel.listaNiveles[0].codigo;

    this.nivel.listaNiveles.forEach(reg => {
      if (max < reg.codigo) {
        max = reg.codigo;
      }
    });

    return max;
  }

  cargarEmpleado(d: any) {

    const dto: DtoEmpleadoBasico = d.data;

    if (d.tag === 'NL') {
      if (!this.validarUsuarioRepetido(dto.personaId)) {
        this.messageService.clear();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Usuario Repetido' });
        return;
      }

      switch (this.empleadomastSelectorComponent.tag) {
        case 'NL': {
          this.agregarLinea(dto);
        }
      }
    } else if (d.tag === 'A') {
      this.nivel.auxUsuarioAdmNombre = dto.personaNombre;
      this.nivel.usuarioadministrador = dto.personaId;
    } else if (d.tag === 'U') {
      this.nivel.auxUsuarioNombre = dto.personaNombre;
      this.nivel.usuario = dto.personaId;
    } else {
      if (!this.validarUsuarioRepetido(dto.personaId)) {
        this.messageService.clear();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Usuario Repetido' });
        return;
      }
      this.cargarPersonaDetalle(dto, +d.tag);
    }


  }

  validarUsuarioRepetido(id: number): boolean {
    let valida: boolean = true;
    this.lstDetalle.forEach(reg => {
      if (reg.usuario === id) {
        valida = false;
      }
    });
    return valida;
  }

  mostrar() {
  }

  mostrarCorreoDetalle(det: SyAprobacionNivelesDet) {

    if (this.nivel.flagusuariosuperB) {
      this.mostrarMensajeAdvertencia('No se pueden hacer ediciones mientras el super usuario este activo');
      return;
    }

    this.correoMantenimientoComponent.tag = 'D';
    this.correoMantenimientoComponent.det = this.copiar(det);
    this.correoMantenimientoComponent.iniciarComponente();
  }

  cargarCorreosDetalle(det: SyAprobacionNivelesDet) {

    // cambio en la lista de la tabla
    const lst = [...this.lstDetalle];
    const aux = this.lstDetalle.find(obj => obj.auxSecuencia === det.auxSecuencia);
    const index = this.lstDetalle.indexOf(aux);
    lst[index] = det;
    this.lstDetalle = lst;
    // cambio en la lista del bean principal
    const ni: DtoNivelDetalle = this.nivel.listaNiveles.find(reg => reg.auxCodigo === this.lvl.auxCodigo);

    ni.listaDetalle.forEach(reg => {
      if (reg.auxSecuencia === det.auxSecuencia) {
        reg.correoelectronico = det.correoelectronico;
      }
    });

    this.correoMantenimientoComponent.verModal = false;

    this.messageService.clear();
    this.messageService.add({ severity: 'success', summary: 'Actualizado', detail: 'Se han realizado los cambios' });

  }

  cargarCorreosCabecera(n: SyAprobacionNiveles) {
    this.nivel.correoelectronico = n.correoelectronico;

    this.correoMantenimientoComponent.verModal = false;

    this.messageService.clear();
    this.messageService.add({ severity: 'success', summary: 'Actualizado', detail: 'Se han realizado los cambios' });
  }

  cargarCorreosConfirmacion(n: SyAprobacionNiveles) {
    this.nivel.nivel0Correoelectronico = n.correoelectronico;
    this.correoMantenimientoComponent.verModal = false;
    this.mostrarMensajeExito('Se han realizado los cambios');
  }

  quitarNivel(n: SyAprobacionNivelesDet) {

    if (this.nivel.flagusuariosuperB) {
      this.mostrarMensajeAdvertencia('No se pueden hacer ediciones mientras el super usuario este activo');
      return;
    }

    this.confirmationService.confirm({
      header: 'Confirmación',
      message: '¿Desea remover el registro?',
      key: 'eliminarNivel',
      accept: () => {
        // eliminar de la listatemp
        const index = this.lstDetalle.indexOf(n);
        this.lstDetalle = this.lstDetalle.filter((val, i) => i !== index);

        const listaAct = this.nivel.listaNiveles.find(rr => rr.auxCodigo === this.lvl.auxCodigo);
        const lst = [...listaAct.listaDetalle];
        const aux = listaAct.listaDetalle.find(obj => obj.auxSecuencia === n.auxSecuencia);
        const index2 = lst.indexOf(aux);
        listaAct.listaDetalle = lst.filter((val, i) => i !== index2);

      }
    });
  }

  mostrarSelectorJefatura(d: SyAprobacionNivelesDet) {
    this.hrDepartamentoSelectorComponent.det = this.copiar(d);
    this.hrDepartamentoSelectorComponent.iniciarComponente();
  }

  mostrarSelectorMotivos(d: SyAprobacionNivelesDet) {

  }

  cargarTipoContrato(d: SyAprobacionNivelesDet) {
    const lst = [...this.lstDetalle];
    const aux = this.lstDetalle.find(obj => obj.auxSecuencia === d.auxSecuencia);
    const index = this.lstDetalle.indexOf(aux);
    lst[index] = d;
    this.lstDetalle = lst;
    const ni: DtoNivelDetalle = this.nivel.listaNiveles.find(reg => reg.auxCodigo === this.lvl.auxCodigo);

    ni.listaDetalle.forEach(reg => {
      if (reg.auxSecuencia === d.auxSecuencia) {
        reg.valor = d.valor;

      }
    });

    this.hrDepartamentoSelectorComponent.verSelector = false;

    this.messageService.clear();
  }


  cargarAreas(d: SyAprobacionNivelesDet) {
    const lst = [...this.lstDetalle];
    const aux = this.lstDetalle.find(obj => obj.auxSecuencia === d.auxSecuencia);
    const index = this.lstDetalle.indexOf(aux);
    lst[index] = d;
    this.lstDetalle = lst;
    const ni: DtoNivelDetalle = this.nivel.listaNiveles.find(reg => reg.auxCodigo === this.lvl.auxCodigo);

    ni.listaDetalle.forEach(reg => {
      if (reg.auxSecuencia === d.auxSecuencia) {
        reg.area = d.area;
        reg.listaAreas = d.listaAreas;
      }
    });

    this.hrDepartamentoSelectorComponent.verSelector = false;

    this.messageService.clear();
    this.messageService.add({ severity: 'success', summary: 'Actualizado', detail: 'Se han realizado los cambios' });
  }

  chkUsuarioOnChange() {
    this.nombreUsuario = '';
    this.nivel.usuario = null;
  }

  chkAdminOnChange() {
    this.nombreAdmin = '';
    this.nivel.usuarioadministrador = null;
  }

  cargarPersonaDetalle(dt: DtoEmpleadoBasico, sec: number) {

    const lst = [...this.lstDetalle];
    const aux = this.lstDetalle.find(obj => obj.auxSecuencia === sec);
    const index = this.lstDetalle.indexOf(aux);
    lst[index].usuario = dt.personaId;
    lst[index].auxNombrePersona = dt.personaNombre;
    this.lstDetalle = lst;
    const ni: DtoNivelDetalle = this.nivel.listaNiveles.find(reg => reg.auxCodigo === this.lvl.auxCodigo);

    ni.listaDetalle.forEach(reg => {
      if (reg.auxSecuencia === sec) {
        reg.usuario = dt.personaId;
        reg.companyownerusuario = dt.companiaId;
        reg.auxNombrePersona = dt.personaNombre;
      }
    });

    this.hrDepartamentoSelectorComponent.verSelector = false;

    this.messageService.clear();
    this.messageService.add({ severity: 'success', summary: 'Actualizado', detail: 'Se han realizado los cambios' });
  }

  guardar() {

    this.bloquearPagina();

    if (!this.validar()) {
      this.desbloquearPagina();
      return;
    }

    this.nivel.flagAplicaDetalle = 'N';
    this.nivel.flagusuariosuper = this.nivel.flagusuariosuperB ? 'S' : 'N';
    this.nivel.flagusuarioadministrador = this.nivel.flagusuarioadministradorB ? 'S' : 'N';
    this.nivel.nivel0Flagsolicitante = this.nivel.nivel0FlagsolicitanteB ? 'S' : 'N';
    this.nivel.nivel0Flagsuperior = this.nivel.nivel0FlagsuperiorB ? 'S' : 'N';
    this.nivel.flagsuperior = this.booleanAFlag(this.nivel.flagsuperiorB);
    this.nivel.flagsolicitante = this.booleanAFlag(this.nivel.flagsolicitanteB);

    if (this.accion === this.ACCIONES.NUEVO) {
      this.syAprobacionNivelesService.registrar(this.nivel).then(
        res => {
          this.desbloquearPagina();
          if (res != null) {
            this.router.navigate(['/spring/aprobacionniveles-listado']);
            this.mostrarMensajeExito(this.getMensajeGrabado(this.nivel.codigo));
          }
        }
      );
    } else {
      this.syAprobacionNivelesService.actualizar(this.nivel).then(
        res => {
          this.desbloquearPagina();
          if (res != null) {
            this.router.navigate(['/spring/aprobacionniveles-listado']);
            this.mostrarMensajeExito(this.getMensajeActualizado(this.nivel.codigo));
          }
        }
      );
    }

  }

  validar(): boolean {
    let valida: boolean = true;

    this.messageService.clear();

    if (this.estaVacio(this.nivel.aplicacion)) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La Aplicación es requerida' });
      valida = false;
    }

    if (this.nivel.codigoproceso == null) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Proceso es requerido' });
      valida = false;
    }

    if (!this.nivel.flagusuariosuperB) {
      if (this.nivel.nroniveles === 0) {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Un nivel como mínimo es requerido' });
        valida = false;
      } else {
        this.nivel.listaNiveles.forEach(
          nivel => {
            if (nivel.listaDetalle.length === 0) {
              this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se encontraron usuarios en el nivel ' + nivel.codigo });
              valida = false;
            }
          }
        );
      }
    } else {
      if (this.nivel.usuario == null) {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Ingresar un usuario' });
        valida = false;
      }
    }


    this.nivel.listaNiveles.forEach(
      nivel => {
        if (nivel.listaDetalle[0].flagarea === 'N') {
          if (nivel.listaDetalle[0].area === null || nivel.listaDetalle[0].area === undefined || nivel.listaDetalle[0].area === '') {
            this.messageService.add({
              severity: 'error', summary: 'Error',
              detail: 'debe seleccionar al menos una unidad de trabajo en  la línea 1 del nivel  ' + nivel.listaDetalle[0].nivel
            });
            valida = false;
          }
        } else {
          nivel.listaDetalle[0].area = '';
        }
      }
    );

    return valida;
  }

}
