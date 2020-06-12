import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { SyReporteArchivo, SyReporteArchivoPk } from '../../syreportearchivo/dominio/sysreportearchivo';
import { SyReporte, SyReportePk } from '../dominio/sysreporte';
import { SelectItem, DataTable } from 'primeng/primeng';
import { SyReporteArchivoService } from '../../syreportearchivo/servicio/sysreportearchivo.service';
import { Router } from '@angular/router';
import { DtoSyReporteArchivo } from '../../syreportearchivo/dominio/dtosyreporteArchivo';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { CompanyownerServicio } from '../../../shared/companyowner/servicio/CompanyownerServicio';
import { AplicacionesmastServicio } from '../../aplicaciones/servicio/aplicacionesmast.service';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-popsyreportedetalle',
    templateUrl: './syreporte-detalle-mantenimiento.component.html'
})
export class SyReporteDetalleMantenimientoComponent extends BaseComponent implements OnInit {
    constructor(
        private companyownerService: CompanyownerServicio,
        private aplicacionesMastService: AplicacionesmastServicio,
        private syReporteArchivoService: SyReporteArchivoService,
        private router: Router,
        messageService: MessageService
    ) { super(messageService); }

    verSelector: Boolean = false;
    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @Output() cargarSeleccionEvent = new EventEmitter();
    syReporteArchivo: SyReporteArchivo;
    lstCompania: SelectItem[] = [];
    lstAplicacion: SelectItem[] = [];
    tipo: string;


    ngOnInit() {
        this.syReporteArchivo = new SyReporteArchivo();


        this.lstCompania = [];
        this.lstCompania.push({ label: '999999', value: '999999' });
        const p4 = this.companyownerService.listarCompaniasPorSeguridad()
            .then(td => {
                td.forEach(obj => this.lstCompania.push({ label: obj.descripcion, value: obj.codigo }));
            });

        this.listarAplicaciones();

    }



    listarAplicaciones() {
        this.bloquearPagina();
        this.lstAplicacion.push({ label: ' -- Todos -- ', value: '' });
        this.aplicacionesMastService.listarPorAplicacion().then(
            res => {
                res.forEach(r => this.lstAplicacion.push({ label: r.descripcionlarga, value: r.aplicacioncodigo }));
            }
        );
    }

    nuevo(syReportePk: SyReportePk, tipo: string) {
        this.tipo = tipo;
        this.verSelector = true;
        this.accion = this.ACCIONES.NUEVO;
        this.syReporteArchivo = new SyReporteArchivo();
        this.syReporteArchivo.aplicacioncodigo = syReportePk.aplicacioncodigo;
        this.syReporteArchivo.reportecodigo = syReportePk.reportecodigo;
        this.syReporteArchivo.estado = 'A';
        this.syReporteArchivo.companiasocio = '999999';
        this.syReporteArchivo.periodo = '999999';
        this.syReporteArchivo.version = '999999';
        this.syReporteArchivo.auxString = '';
    }

    editar(dto: DtoSyReporteArchivo, tipo: string) {
        this.tipo = tipo;
        this.verSelector = true;
        this.accion = this.ACCIONES.EDITAR;
        const pk: SyReporteArchivoPk = new SyReporteArchivoPk();
        pk.aplicacioncodigo = dto.aplicacionCodigo;
        pk.reportecodigo = dto.reporteCodigo;
        pk.companiasocio = dto.companiaSocio;
        pk.periodo = dto.periodo;
        pk.version = dto.version;

        this.syReporteArchivoService.solicitudObtenerPorId(pk).then(reg => {
            this.syReporteArchivo = reg;
        });

    }


    guardar() {
        if (!this.validar()) {
            return;
        }

        if (this.accion === this.ACCIONES.NUEVO) {
            this.syReporteArchivoService.solicitudRegistrar(this.syReporteArchivo).then(
                r => {
                    this.getMensajeGrabado(this.syReporteArchivo.reportecodigo);
                    this.cargarSeleccionEvent.emit();
                    this.verSelector = false;
                }
            ).catch(
            );
        } else {
            this.syReporteArchivoService.solicitudActualizar(this.syReporteArchivo).then(
                r => {
                    this.getMensajeActualizado(this.syReporteArchivo.reportecodigo);
                    this.cargarSeleccionEvent.emit();
                    this.verSelector = false;
                }
            );
        }
    }

    validar() {
        let valida: Boolean = true;
        this.messageService.clear();
        if (this.estaVacio(this.syReporteArchivo.periodo)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El periodo es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.syReporteArchivo.version)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La version es requerida' });
            valida = false;
        }
        return valida;
    }
}
