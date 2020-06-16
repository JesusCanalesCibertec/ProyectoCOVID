import { DataTable, SelectItem, ConfirmationService } from 'primeng/primeng';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FiltroNivelAprobacion } from '../dominio/FiltroNivelAprobacion';
import { SyAprobacionNiveles, SyAprobacionNivelesPk } from '../dominio/SyAprobacionNiveles';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { SyAprobacionnivelesServicio } from '../servicio/SyAprobacionnivelesServicio';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { CompanyownerServicio } from '../../../shared/companyowner/servicio/CompanyownerServicio';
import { EmpleadomastServicio } from '../../../shared/selectores/empleado/servicio/EmpleadomastServicio';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
  templateUrl: './aprobacionniveles-listado.component.html'
})
export class AprobacionNivelesComponent extends PrincipalBaseComponent implements OnInit {

  lstAplicacion: SelectItem[] = [];
  filtro: FiltroNivelAprobacion = new FiltroNivelAprobacion();
  lstNiveles: SyAprobacionNiveles[] = [];
  lstEstados: SelectItem[] = [];
  companias: SelectItem[] = [];
  buscarB: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private confirmationService: ConfirmationService,
    private router: Router,
    private syAprobacionNivelesService: SyAprobacionnivelesServicio,
    private companyownerServicio: CompanyownerServicio,
    private empleadomastServicio: EmpleadomastServicio,
    noAuthorizationInterceptor: NoAuthorizationInterceptor, messageService: MessageService
  ) { super(noAuthorizationInterceptor, messageService); }

  ngOnInit() {

    let msg = this.route.snapshot.params['msg'];

    if (msg != undefined) {
      this.messageService.clear();
      this.messageService.add({ severity: 'info', summary: 'Información', detail: msg });
    }
    this.listarAplicaciones();
    this.listarEstados();
    this.listarCompanias();


  }
  listarCompanias() {
    this.companias.push({ label: ' -- Todos -- ', value: '' });
    this.companyownerServicio.listarActivos().then(
      res => {
        res.forEach(r => this.companias.push({ label: r.descripcion, value: r.codigo }));
        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
          res => {
            // this.filtro.compania = res.companiaId;
            this.buscarB = true;
            this.buscar();
          }
        );
      }
    );
  }

  listarAplicaciones() {
    this.lstAplicacion.push({ label: ' -- Todos -- ', value: '' });
    this.syAprobacionNivelesService.listarAplicacionPorUsuario().then(
      res => {
        res.forEach(r => this.lstAplicacion.push({ label: r.nombre, value: r.codigo }));
      }
    );
  }

  listarEstados() {
    this.lstEstados.push({ label: ' -- Seleccionar -- ', value: '' });
    this.lstEstados.push({ label: 'Activo', value: 'A' });
    this.lstEstados.push({ label: 'Inactivo', value: 'I' });
  }

  nuevo() {
    this.router.navigate(['/spring/aprobacionniveles-mantenimiento']);
  }


  buscar() {
    if (!this.buscarB) {
      return;
    }
    this.bloquearPagina();
    this.syAprobacionNivelesService.listar(this.filtro).then(
      res => {
        this.lstNiveles = res;
        this.desbloquearPagina();
      }
    );
  }

  editar(dto: SyAprobacionNiveles) {
    this.router.navigate(['/spring/aprobacionniveles-mantenimiento', dto.codigo, dto.companyOwner], { skipLocationChange: true });
  }

  inactivar(dto: SyAprobacionNiveles) {

    this.confirmationService.confirm({
      header: 'Confirmación',
      message: this.getMensajePreguntaInactivar(),
      key: 'inactivarNivel',
      accept: () => {
        this.bloquearPagina();
        var act: SyAprobacionNiveles;
        this.syAprobacionNivelesService.obtenerPorIdCompleto(dto).then(
          r => {
            act = r;
            act.estado = 'I';
            this.syAprobacionNivelesService.actualizar(act).then(
              res => {
                if (res != null) {
                  this.buscar();
                }
              }
            );
          }
        );
      }
    });

  }
  eliminar(dto: SyAprobacionNiveles) {

    this.confirmationService.confirm({
      header: 'Confirmación',
      message: this.getMensajePreguntaEliminar(),
      key: 'eliminarNivel',
      accept: () => {
        this.bloquearPagina();
        this.syAprobacionNivelesService.eliminar(dto).then(
          r => {
            if (r != null) {
              this.messageService.clear();
              this.messageService.add({ severity: 'info', summary: 'Información', detail: this.getMensajeEliminado(dto.codigo) });
              this.buscar();
            }
          }
        );
      }
    });

  }



}
