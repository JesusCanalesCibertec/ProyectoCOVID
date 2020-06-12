import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { LazyLoadEvent } from 'primeng/api';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { DataTable, SelectItem, ConfirmationService } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { dtoProyecto } from '../dominio/dtoProyecto';
import { ProyectoService } from '../servicio/Proyecto.service';
import { Proyecto, ProyectoPk } from '../dominio/proyecto';
import { MaMiscelaneosdetalle } from 'src/app/erp_module/shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { Router, ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/components/common/messageservice';



@Component({
  selector: 'app-proyecto',
  templateUrl: './proyecto-listado.component.html'
})
export class ProyectoListadoComponent extends PrincipalBaseComponent implements OnInit {




  constructor(
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService,

    //servicios
    private cdref: ChangeDetectorRef,
    private ProyectoServicio: ProyectoService,
    private miscelaneoServicio: MaMiscelaneosdetalleServicio,
    private confirmationService: ConfirmationService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    super(noAuthorizationInterceptor, messageService);
  }

  @ViewChild(DataTable) dt: DataTable;

  //Declaraciones
  lista: MaMiscelaneosdetalle[] = [];
  listadetalle = [];
  filtro: DtoTabla = new DtoTabla();
  numFilas: number;
  estados: SelectItem[] = [];
  Proyecto: Proyecto = new Proyecto();
  Proyectopk: ProyectoPk = new ProyectoPk();
  tipo: string;
  estadoatencion: string = '';

  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  ngOnInit() {
    super.ngOnInit();
    this.bloquearPagina();
    this.tipo = this.route.snapshot.params['tipo'];
    this.listarTipos();
    this.cargarEstados();

  }

  listarTipos() {
    this.miscelaneoServicio.listarActivos('MP', 'TIPOPROYEC').then(
      res => {
        this.lista = res;
        if (this.tipo) {
          this.lista.forEach(r => { r.auxCampo = r.codigoelemento == this.tipo ? true : false })
          this.ProyectoServicio.listardetalle(this.tipo,null,null).then(
            res => {
              this.listadetalle = res;
              this.numFilas = this.listadetalle.length;
              this.desbloquearPagina();
            }
          )
        }
        this.desbloquearPagina();
      }
    )
  }

  cargarLista(event: any) {
    this.bloquearPagina();
    this.tipo = null;
    this.listadetalle = [];
    let codigo = event.index + 1
    //if(this.estaVacio(this.estadoatencion)){this.estadoatencion = null};
    this.ProyectoServicio.listardetalle(codigo.toString(),null,this.estadoatencion).then(
      res => {
        this.tipo = codigo.toString();
        this.listadetalle = res;
        this.numFilas = this.listadetalle.length;
        this.desbloquearPagina();
      }
    )
  }

  cambioEstado(){
    if(this.tipo != null){
      this.bloquearPagina();
      this.ProyectoServicio.listardetalle(this.tipo,null,this.estadoatencion).then(
        res => {
          this.listadetalle = res;
          this.numFilas = this.listadetalle.length;
          this.desbloquearPagina();
        }
      )
    }
  }

  obtenerEstilos(parametro: number) {

    if (parametro > -15) {
      return { 'background-color': 'green', 'border-radius': '50%', 'width': '20px', 'height': '20px', 'margin': 'auto', 'border': '1px solid black' };
    } else if (parametro > -41 && parametro <= -15) {
      return { 'background-color': 'yellow', 'border-radius': '50%', 'width': '20px', 'height': '20px', 'margin': 'auto', 'border': '1px solid black' };
    } else {
      return { 'background-color': 'red', 'border-radius': '50%', 'width': '20px', 'height': '20px', 'margin': 'auto', 'border': '1px solid black' };
    }
  }

  cargarEstados(): Promise<number> {
      this.estados = [];
      return this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_ESTADO_PROYECTO).then(r => {
          r = r.sort((a,b)=> Number(a.codigoelemento)-Number(b.codigoelemento));
          r.forEach(rr => {
                  this.estados.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
          });
          return 1;
      });
  }


  cargarPaginacion(event: LazyLoadEvent) {
    this.bloquearPagina();
    this.filtro.paginacion.listaResultado = [];
    this.filtro.paginacion.registroInicio = event.first;
    this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
    this.ProyectoServicio.listarPaginacion(this.filtro)
      .then(res => {
        this.filtro.paginacion = res;
        this.desbloquearPagina();
      })
  }

  buscar(dt: any) {
    dt.reset();
  }

  nuevo() {
    this.router.navigate(['spring/proyecto-mantenimiento']);
  }

  editar(bean: dtoProyecto) {
    this.router.navigate(['spring/proyecto-mantenimiento', bean.idproyecto, this.ACCIONES.EDITAR], { skipLocationChange: true });
  }

  ver(bean: dtoProyecto) {


  }


  cambiarestado(bean: dtoProyecto) {
    this.Proyectopk.idProyecto = bean.idproyecto;
    if (bean.estado == 'A') {
      this.confirmationService.confirm({
        header: 'Confirmación',
        icon: 'fa fa-question-circle',
        message: this.getMensajePreguntaInactivar(),
        accept: () => {
          this.bloquearPagina();
          this.ProyectoServicio.cambiarestado(this.Proyectopk).then(res => {
            if (res != null) {
              this.mostrarMensajeExito(this.getMensajeInactivado(res.idProyecto));
              this.desbloquearPagina();
              this.dt.reset();
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
          this.ProyectoServicio.cambiarestado(this.Proyectopk).then(res => {
            if (res != null) {
              this.mostrarMensajeExito(this.getMensajeActivado(res.idProyecto));
              this.desbloquearPagina();
              this.dt.reset();
            }
          });
        }
      });
    }

  }

}
