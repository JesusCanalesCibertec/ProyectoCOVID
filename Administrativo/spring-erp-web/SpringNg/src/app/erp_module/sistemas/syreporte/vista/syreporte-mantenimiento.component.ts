import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/primeng';
import { Router, ActivatedRoute } from '@angular/router';
import { SyReporteService } from '../servicio/syreporte.service';
import { SyReporte, SyReportePk } from '../dominio/sysreporte';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { AplicacionesmastServicio } from '../../aplicaciones/servicio/aplicacionesmast.service';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-syreportemantenimiento',
    templateUrl: './syreporte-mantenimiento.component.html'
})
export class SyReporteMantenimientoComponent extends BaseComponent implements OnInit {
    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private syReporteService: SyReporteService,
        private aplicacionesMastService: AplicacionesmastServicio,
        messageService: MessageService
    ) {
        super(messageService);
    }

    syReporte: SyReporte = new SyReporte();
    lstAplicacion: SelectItem[] = [];
    lstEstado: SelectItem[] = [];
    lstTipoReporte: SelectItem[] = [];

    ngOnInit() {
        const p1 = this.aplicacionesMastService.listarPorAplicacion().then(
            res => {
                this.lstAplicacion.push({ label: '-- Seleccionar --', value: null });
                res.forEach(r => this.lstAplicacion.push({ label: r.descripcionlarga, value: r.aplicacioncodigo }));

                this.lstEstado = [];
                this.lstEstado.push({ label: 'Activo', value: 'A' });
                this.lstEstado.push({ label: 'Inactivo', value: 'I' });

                this.lstTipoReporte = [];
                this.lstTipoReporte.push({ label: '-- Seleccionar --', value: null });
                this.lstTipoReporte.push({ label: 'HTML', value: 'HTML' });
                this.lstTipoReporte.push({ label: 'JASPER', value: 'JASPE' });
                this.lstTipoReporte.push({ label: 'POWER', value: 'POWER' });

                const ver = this.route.snapshot.params['ver'];
                const reporteCodigo = this.route.snapshot.params['reporteCodigo'];
                const aplicacioncodigo = this.route.snapshot.params['aplicacionCodigo'];

                if (aplicacioncodigo) {
                    this.editar();
                } else {
                    this.nuevo();
                }
            }
        );
    }

    editar() {
        this.accion = this.ACCIONES.EDITAR;
        const pk: SyReportePk = new SyReportePk();
        pk.aplicacioncodigo = this.route.snapshot.params['aplicacionCodigo'];
        pk.reportecodigo = this.route.snapshot.params['reporteCodigo'];

        this.syReporteService.solicitudObtenerPorId(pk).then(reg => {
            this.syReporte = reg;
        });
    }
    nuevo() {
        this.accion = this.ACCIONES.NUEVO;
        this.syReporte = new SyReporte();
        this.syReporte.aplicacioncodigo = null;
        this.syReporte.estado = 'A';
        this.syReporte.tiporeporte = 'HTML';
    }

    salir() {
        this.router.navigate(['spring/syreporte-listado']);
    }

    guardar() {
        if (!this.validar()) {
            return;
        }

        if (this.accion === this.ACCIONES.NUEVO) {
            this.syReporteService.solicitudRegistrar(this.syReporte).then(
                r => {
                    this.router.navigate(['spring/syreporte-listado']);
                    this.mostrarMensajeExito(this.getMensajeGrabado(this.syReporte.reportecodigo));
                }
            ).catch(

            );
        } else {
            this.syReporteService.solicitudActualizar(this.syReporte).then(
                r => {
                    this.router.navigate(['spring/syreporte-listado']);
                    this.mostrarMensajeExito(this.getMensajeActualizado(this.syReporte.reportecodigo));
                }
            );
        }
    }


    validar(): Boolean {
        let valida: Boolean = true;

        this.messageService.clear();

        if (this.syReporte.aplicacioncodigo == null) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La aplicación es requerida' });
            valida = false;
        }
        if (this.estaVacio(this.syReporte.descripcionlocal)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El nombre es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.syReporte.reportecodigo)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El codigo del reporte es requerido' });
            valida = false;
        }

        if (this.estaVacio(this.syReporte.tiporeporte)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Tipo de Reporte es requerido' });
            valida = false;
        }

        return valida;
    }

}
