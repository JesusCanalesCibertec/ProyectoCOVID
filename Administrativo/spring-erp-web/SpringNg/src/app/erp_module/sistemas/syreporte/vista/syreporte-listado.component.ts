import { Component, OnInit, ViewChild } from '@angular/core';
import { SelectItem, ConfirmationService, LazyLoadEvent, DataTable } from 'primeng/primeng';
import { Router, ActivatedRoute } from '@angular/router';
import { SyReporteService } from '../servicio/syreporte.service';
import { SyReporte, SyReportePk } from '../dominio/sysreporte';
import { DtoSyReporte } from '../dominio/dtosyreporte';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { AplicacionesmastServicio } from '../../aplicaciones/servicio/aplicacionesmast.service';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-syreportelistado',
    templateUrl: './syreporte-listado.component.html'
})
export class SyReporteListadoComponent extends BaseComponent implements OnInit {
    constructor(
        private confirmationService: ConfirmationService,
        private route: ActivatedRoute,
        private syReporteService: SyReporteService,
        private aplicacionesMastService: AplicacionesmastServicio,
        private router: Router,
        messageService: MessageService
    ) {
        super(messageService);
    }

    filtro: DtoSyReporte = new DtoSyReporte();
    lstAplicacion: SelectItem[] = [];
    lstEstado: SelectItem[] = [];
    @ViewChild(DataTable) dt: DataTable;

    ngOnInit() {
        this.bloquearPagina();
        this.listarAplicaciones();
        this.listarEstados();
    }

    listarAplicaciones() {
        this.bloquearPagina();
        this.lstAplicacion.push({ label: ' -- Todos -- ', value: '' });
        this.aplicacionesMastService.listarPorAplicacion().then(
            res => {
                res.forEach(r => this.lstAplicacion.push({ label: r.descripcionlarga, value: r.aplicacioncodigo }));
                this.desbloquearPagina();
            }
        );
    }

    listarEstados() {
        this.lstEstado.push({ label: ' -- Todos -- ', value: null });
        this.lstEstado.push({ label: 'Activo', value: 'A' });
        this.lstEstado.push({ label: 'Inactivo', value: 'I' });
    }

    cargar(event: LazyLoadEvent) {
        if (this.filtro.aplicacionCodigo == 'x') { this.filtro.aplicacionCodigo = 'SN' }
        this.bloquearPagina();
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.syReporteService.listarConPaginacion(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.desbloquearPagina();
            });
    }

    editar(dto: DtoSyReporte) {
        this.router.navigate(['spring/syreporte-mantenimiento', dto.aplicacionCodigo, dto.reporteCodigo], { skipLocationChange: true });
    }

    nuevo() {
        this.router.navigate(['spring/syreporte-mantenimiento'], { skipLocationChange: true });
    }

    inactivar(dto: DtoSyReporte) {

        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaInactivar(),
            accept: () => {

                this.bloquearPagina();
                const pk: SyReportePk = new SyReportePk();
                pk.aplicacioncodigo = dto.aplicacionCodigo;
                pk.reportecodigo = dto.reporteCodigo;

                this.syReporteService.solicitudObtenerPorId(pk).then(reg => {
                    const pl = reg;
                    pl.estado = 'I';
                    this.syReporteService.solicitudActualizar(pl).then(r => {
                        this.dt.reset();
                        this.messageService.clear();
                        this.messageService.add({
                            severity: 'info', summary: 'Información',
                            detail: this.getMensajeInactivadoCodigoString(dto.reporteCodigo)
                        });
                    });
                });
            }
        });
    }



}
