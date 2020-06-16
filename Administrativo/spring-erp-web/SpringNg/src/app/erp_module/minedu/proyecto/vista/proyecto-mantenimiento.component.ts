import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ProyectoService } from '../servicio/Proyecto.service';
import { DataTable, SelectItem } from 'primeng/primeng';
import { Proyecto, ProyectoPk } from '../dominio/proyecto';
import { Router, ActivatedRoute } from '@angular/router';
import { ProyectoDatosgeneralesComponent } from './proyecto-tab1.component';
import { ProyectoRecursosComponent } from './proyecto-tab2.component';
import { ProyectoSeguimientoComponent } from './proyecto-tab3.component';
import { ProyectoRiesgosComponent } from './proyecto-tab4.component';
import { AreamineduServicio } from 'src/app/erp_module/shared/area/servicio/areaminedu.service';
import { dtoAreaminedu } from 'src/app/erp_module/shared/area/dominio/dtoAreaminedu';
import { ComunicacionService } from '../servicio/comunicacion.service';
import { tick } from '@angular/core/src/render3';
import { Proyectorecurso } from '../dominio/proyectorecurso';



@Component({
  selector: 'app-proyecto-mantenimiento',
  templateUrl: './proyecto-mantenimiento.component.html'
})
export class ProyectoMantenimientoComponent extends PrincipalBaseComponent implements OnInit {

  @ViewChild(ProyectoDatosgeneralesComponent) proyectoDatosgeneralesComponent: ProyectoDatosgeneralesComponent;
  @ViewChild(ProyectoRecursosComponent) proyectoRecursosComponent: ProyectoRecursosComponent;
  @ViewChild(ProyectoSeguimientoComponent) proyectoSeguimientoComponent: ProyectoSeguimientoComponent;
  @ViewChild(ProyectoRiesgosComponent) proyectoRiesgosComponent: ProyectoRiesgosComponent;



  @ViewChild(DataTable) dataTableComponent: DataTable;



  //Declaraciones
  filtro: DtoTabla = new DtoTabla();
  instrucciones: SelectItem[] = [];
  verModal: Boolean = false;
  deshabilitar: Boolean = false;
  proyecto: Proyecto = new Proyecto();
  ProyectoPk: ProyectoPk = new ProyectoPk();
  arreglo: string[] = [];
  niveles: string;
  deshabilitarTab: boolean = true;;
  advertencia: string;
  auxEstado: string;
  auxListaInteresados: Proyectorecurso[] = [];

  codProyecto: number;

  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,


    //servicios
    private router: Router,
    private route: ActivatedRoute,
    private ProyectoServicio: ProyectoService,
    public comunicacionServicio: ComunicacionService,
    private areamineduServicio: AreamineduServicio,
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  ngOnInit() {
    super.ngOnInit();
    this.bloquearPagina();
    const codProyecto = this.route.snapshot.params['codigo'];
    this.accion = this.route.snapshot.params['accion'];


    this.comunicacionServicio.numRecursos = 0;
    this.comunicacionServicio.parametro = 0;
    this.comunicacionServicio.proyectocomunica = new Proyecto();


    this.inicializarDatos().then(f => {
      const codProyecto = this.route.snapshot.params['codigo'];
      if (codProyecto) {
        this.accion = this.ACCIONES.EDITAR;
        this.ProyectoPk = new ProyectoPk();
        this.ProyectoPk.idProyecto = codProyecto;
        this.obtener();
      } else {
        this.accion = this.ACCIONES.NUEVO;
        this.proyecto = new Proyecto();
        this.nuevo();
      }
    });
  }

  nuevo() {
    this.comunicacionServicio.enviarCantidadrecursos(this.comunicacionServicio.numRecursos);

    this.proyectoDatosgeneralesComponent.proyecto = this.proyecto;
    this.proyectoDatosgeneralesComponent.auxgestor = null;
    this.proyectoDatosgeneralesComponent.proyecto.numparticipantes = 0;


    this.proyectoRecursosComponent.proyecto = this.proyecto;

    this.proyectoSeguimientoComponent.proyecto = this.proyecto;
    this.proyectoSeguimientoComponent.proyecto.estadoatencion = '15';
    this.proyectoSeguimientoComponent.proyecto.fasegestion = '3';
    this.proyectoSeguimientoComponent.enabledEstado = true;

    this.proyectoRiesgosComponent.proyecto = this.proyecto;
    this.desbloquearPagina();
  }


  obtener() {

    this.ProyectoServicio.obtenerPorId(this.ProyectoPk.idProyecto).then(r => {

      this.proyecto = r;
      this.auxListaInteresados = r.listaRecursos;

      if (this.proyecto.estadoatencion == '16') { this.proyecto.estadoatencion = '12' };

      //Tab 1
      this.proyecto.idSistema != null? this.proyectoDatosgeneralesComponent.checked = true: false;
      this.proyectoDatosgeneralesComponent.proyecto = this.proyecto;
      this.proyectoDatosgeneralesComponent.obtenerSiglas(this.proyecto.idSistema);
      this.proyectoDatosgeneralesComponent.auxgestor = this.proyecto.gestor;
      this.comunicacionServicio.numRecursos = this.proyecto.numparticipantes;
      this.comunicacionServicio.parametro = this.proyecto.listaRecursos.length;
      this.comunicacionServicio.enviarCantidadrecursos(this.comunicacionServicio.numRecursos);
      this.comunicacionServicio.proyectocomunica.idProyecto = this.proyecto.idProyecto;
      this.comunicacionServicio.proyectocomunica.fechainicio = this.proyecto.fechainicio;
      this.comunicacionServicio.proyectocomunica.fechafin = this.proyecto.fechafin;
      this.obtenerAreaProyecto(this.proyecto.area);

      //Tab 2
      this.dividirLista();
      this.proyectoRecursosComponent.proyecto = this.proyecto;
      this.obtenerPersonalInteresado();
      this.obtenerAreaInteresado();


      //Tab3
      this.proyectoSeguimientoComponent.proyecto = this.proyecto;
      this.proyectoSeguimientoComponent.fase = this.proyecto.faseingenieria;
      this.proyectoSeguimientoComponent.cargarFasesIngenierias();
      if (this.proyecto.estadoatencion == '15') {
        this.proyectoSeguimientoComponent.enabledEstado = true;
      } else {
        this.proyectoSeguimientoComponent.enabledEstado = false;
      }

      //Tab4
      this.proyectoRiesgosComponent.proyecto = this.proyecto;

      this.desbloquearPagina();

    });
  }

  inicializarDatos(): Promise<number> {
    // usar un invoke all
    return this.proyectoDatosgeneralesComponent.cargarPersonas().then(
      r => {
        return this.proyectoDatosgeneralesComponent.cargarTiposproyect().then(
          r => {
            return this.proyectoDatosgeneralesComponent.cargarCoordinaciones().then(
              r => {
                return this.proyectoDatosgeneralesComponent.cargarSistemas().then(
                  r => {
                    return this.proyectoRecursosComponent.cargarRoles().then(
                      r => {
                        return this.proyectoRecursosComponent.cargarRoles2().then(
                          r => {
                            return this.proyectoSeguimientoComponent.cargarEstados().then(
                              r => {
                                return this.proyectoSeguimientoComponent.cargarFasesGestion().then(
                                  r => {
                                    return this.proyectoSeguimientoComponent.cargarTipos().then(
                                      r => {
                                        return 1;
                                      }
                                    );
                                  }
                                );
                              }
                            );
                          }
                        );
                      }
                    );
                  }
                );
              }
            );
          }
        );
      }
    );
  }

  obtenerAreaProyecto(parametro: number) {
    let filtro = new DtoTabla();
    filtro.codigoNumerico = parametro;
    let dto = new dtoAreaminedu();
    this.areamineduServicio.listarPaginacion(filtro).then(res => {
      if (res != null) {
        dto = res.listaResultado[0];
        dto.ultimocorto = dto.ultimocorto == null ? '' : ' / ' + dto.ultimocorto;
        this.proyectoDatosgeneralesComponent.niveles = dto.primercorto + ' / ' + dto.segundocorto + dto.ultimocorto;
      }
    });
  }

  dividirLista() {
    this.proyecto.listaRecursos1 = [];
    this.proyecto.listaRecursos2 = [];

    this.proyecto.listaRecursos.forEach(res => {
      Number(res.rol) <= 3 ? this.proyecto.listaRecursos1.push(res) : this.proyecto.listaRecursos2.push(res);
    });

    this.proyectoRecursosComponent.enumerar(this.proyecto.listaRecursos1);
    this.proyectoRecursosComponent.enumerar(this.proyecto.listaRecursos2);
  }

  obtenerAreaInteresado() {
    this.proyecto.listaRecursos1.forEach(r => {
      let filtro = new DtoTabla();
      filtro.codigoNumerico = r.area;
      let dto = new dtoAreaminedu();
      this.areamineduServicio.listarPaginacion(filtro).then(res => {
        if (res != null) {
          dto = res.listaResultado[0];
          this.proyectoRecursosComponent.proyecto.listaRecursos1.find(obj => obj.idRecurso == r.idRecurso).nomArea = dto.primero;
          this.desbloquearPagina();
        }
      });
    });
  }

  obtenerPersonalInteresado() {
    this.proyectoRecursosComponent.proyecto.listaRecursos1.forEach(res => {
      let pyr = this.auxListaInteresados.find(obj => obj.idPersona == res.idPersona);
      res = this.proyectoRecursosComponent.cargarPersonas(res, pyr.idPersona);
    })
  }

  guardar() {
    this.bloquearPagina();
    if (!this.validar()) {
      this.desbloquearPagina();
      return;
    }

    this.proyecto.numparticipantes = this.comunicacionServicio.numRecursos;
    this.proyecto.fechainicio = this.comunicacionServicio.proyectocomunica.fechainicio;
    this.proyecto.fechafin = this.comunicacionServicio.proyectocomunica.fechafin;

    if (this.comunicacionServicio.numRecursos < 2 || this.comunicacionServicio.numRecursos == null) {
      this.proyecto.listaRecursos1 = [];
      this.proyecto.listaRecursos2 = [];
    }

    this.cargarLista();


    if (this.accion === this.ACCIONES.NUEVO) {
      if ((this.comunicacionServicio.parametro < this.comunicacionServicio.numRecursos) || this.comunicacionServicio.numRecursos < 2) {
        this.proyectoSeguimientoComponent.proyecto.estadoatencion = '15';
      } else {
        this.proyectoSeguimientoComponent.proyecto.estadoatencion = '16';
      }
      this.ProyectoServicio.registrar(this.proyecto).then(
        resultado => {
          if (resultado != null) {
            this.mostrarMensajeExito(this.getMensajeGrabado2(resultado.codigoproyecto));
            this.router.navigate(['spring/proyecto-listado', resultado.tipoproyecto], { skipLocationChange: true });

          }
          this.desbloquearPagina();
        });
    } else {
      if ((this.comunicacionServicio.parametro < this.comunicacionServicio.numRecursos) || this.comunicacionServicio.numRecursos < 2) {
        this.proyectoSeguimientoComponent.proyecto.estadoatencion = '15';
      } else {
        if (this.auxEstado == '15') {
          this.proyectoSeguimientoComponent.proyecto.estadoatencion = '16';
        }
      }
      this.ProyectoServicio.actualizar(this.proyecto).then(resultado => {
        if (resultado != null) {
          this.mostrarMensajeExito(this.getMensajeActualizado2(resultado.codigoproyecto));
          this.router.navigate(['spring/proyecto-listado', resultado.tipoproyecto], { skipLocationChange: true });

        }
        this.desbloquearPagina();
      });
    }
  }

  cargarLista() {
    this.proyecto.listaRecursos = [];
    if (this.proyecto.listaRecursos1.length > 0) {
      this.proyecto.listaRecursos1.forEach(res => {
        res.idRecurso = null;
        this.proyecto.listaRecursos.push(res);
      })
    }
    if (this.proyecto.listaRecursos2.length > 0) {
      this.proyecto.listaRecursos2.forEach(res => {
        res.idRecurso = null;
        this.proyecto.listaRecursos.push(res);
      })
    }
    this.proyecto.listaRecursos = this.proyecto.listaRecursos.sort((a, b) => Number(a.rol) - Number(b.rol));
    this.proyectoRecursosComponent.enumerar(this.proyecto.listaRecursos);
  }

  validar(): boolean {
    let valida = true;
    this.messageService.clear();

    if (!this.proyectoDatosgeneralesComponent.validar()) {
      valida = false;
    }
    if (!this.proyectoRecursosComponent.validarInteresados()) {
      valida = false;
    }
    if (!this.proyectoRecursosComponent.validarParticipantes()) {
      valida = false;
    }
    return valida;
  }

  salir() {
    this.router.navigate(['spring/proyecto-listado']);
  }

  cambiarTab(event) {
    if (event.index == 1) {
      this.proyectoRecursosComponent.borrarTodo(this.comunicacionServicio.borrar);
    }
    if (event.index == 2 && (this.proyecto.estadoatencion == '15' || this.accion == this.ACCIONES.NUEVO)) {
      if ((this.comunicacionServicio.parametro < this.comunicacionServicio.numRecursos) || this.comunicacionServicio.numRecursos < 2) {
        this.proyectoSeguimientoComponent.proyecto.estadoatencion = '15';
      } else {
        this.proyectoSeguimientoComponent.proyecto.estadoatencion = '16';
      }
    }
  }

}
