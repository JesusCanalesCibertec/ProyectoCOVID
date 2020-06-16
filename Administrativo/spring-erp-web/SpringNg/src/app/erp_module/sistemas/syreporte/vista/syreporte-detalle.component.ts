import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { SyReporteDetalleMantenimientoComponent } from './syreporte-detalle-mantenimiento.component';
import { ActivatedRoute, Router } from '@angular/router';
import { SyReportePk } from '../dominio/sysreporte';
import { SyReporteArchivo, SyReporteArchivoPk } from '../../syreportearchivo/dominio/sysreportearchivo';
import { SyReporteArchivoService } from '../../syreportearchivo/servicio/sysreportearchivo.service';
import { DtoSyReporteArchivo } from '../../syreportearchivo/dominio/dtosyreporteArchivo';
import { ConfirmationService, DataTable, LazyLoadEvent } from 'primeng/primeng';
import { SyReporteArchivoDatoComponent } from './syreporte-archivo.component';
import { SyReporteService } from '../servicio/syreporte.service';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { DtoSyReporte } from '../dominio/dtosyreporte';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-syreportedetalle',
    templateUrl: './syreporte-detalle.component.html'
})
export class SyReporteDetalleComponent extends BaseComponent implements OnInit {
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private syReporteArchivoService: SyReporteArchivoService,
        private confirmationService: ConfirmationService,
        private syReporteService: SyReporteService,
        messageService: MessageService
    ) { super(messageService); }

    @ViewChild(SyReporteDetalleMantenimientoComponent)
    syReporteDetalleMantenimientoComponent: SyReporteDetalleMantenimientoComponent;

    @ViewChild(SyReporteArchivoDatoComponent)
    syReporteArchivoDatoComponent: SyReporteArchivoDatoComponent;

    syReportePk: SyReportePk;
    registrosPorPagina: number = 7;
    filtro: DtoSyReporte;
    tipoReporte: string;
    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @ViewChild(DataTable) dt: DataTable;

    ngOnInit() {
        this.syReportePk = new SyReportePk();
        const reporteCodigo = this.route.snapshot.params['reporteCodigo'];
        const aplicacioncodigo = this.route.snapshot.params['aplicacionCodigo'];
        if (aplicacioncodigo) {
            this.syReportePk.aplicacioncodigo = aplicacioncodigo;
            this.syReportePk.reportecodigo = reporteCodigo;

            this.syReporteService.solicitudObtenerPorId(this.syReportePk).then(reg => {
                this.tipoReporte = reg.tiporeporte;
            });
        }

        this.listar();
    }



    nuevo() {
        this.syReporteDetalleMantenimientoComponent.nuevo(this.syReportePk, this.tipoReporte);
    }

    editar(dto: DtoSyReporteArchivo) {
        this.syReporteDetalleMantenimientoComponent.editar(dto, this.tipoReporte);
    }

    subiendoDto: DtoSyReporteArchivo = null;

    subirArchivo(dto: DtoSyReporteArchivo, fs: any) {
        fs.click();
        this.subiendoDto = dto;
    }
    cargarArchivo(event: any) {
        this.bloquearPagina();

        var files = event.files;

        if (files.length !== 1) {
            this.desbloquearPagina();
            return;
        }
        if (files[0].size > 1000000) {
            this.mostrarMensajeAdvertencia('El tamaño supera el límite de ' + 1 + 'mb');
            this.desbloquearPagina();
            return null;
        }

        if (files[0].type != 'text/html') {
            this.mostrarMensajeAdvertencia('Solo se permiten archivos de extensión .html');
            this.desbloquearPagina();
            return null;
        }

        var reader = new FileReader();
        reader.onloadend = (evt) => {


            var result = reader.result;

            var lines = result.toString().split('\n');
            var cadena = '';
            for (var line = 0; line < lines.length; line++) {
                cadena = cadena + (lines[line]);
            }

            const pk: SyReporteArchivoPk = new SyReporteArchivoPk();
            pk.aplicacioncodigo = this.subiendoDto.aplicacionCodigo;
            pk.reportecodigo = this.subiendoDto.reporteCodigo;
            pk.companiasocio = this.subiendoDto.companiaSocio;
            pk.periodo = this.subiendoDto.periodo;
            pk.version = this.subiendoDto.version;

            this.syReporteArchivoService.solicitudObtenerPorId(pk).then(reg => {
                reg.auxString = cadena;
                this.syReporteArchivoService.solicitudActualizar(reg).then(
                    r => {
                        this.mostrarMensajeExito("Se ha subido el archivo");
                        this.desbloquearPagina();
                    }
                );
            });


        }
        reader.readAsText(files[0]);
    }

    listar() {
        // this.block.emit();
        this.filtro = new DtoSyReporte();
        this.filtro.estado = 'A';
        this.filtro.aplicacionCodigo = this.syReportePk.aplicacioncodigo;
        this.filtro.reporteCodigo = this.syReportePk.reportecodigo;

        this.filtro.paginacion.registroInicio = 0;
        this.filtro.paginacion.cantidadRegistrosDevolver = this.registrosPorPagina;
        this.syReporteArchivoService.listarConPaginacion(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.unBlock.emit();
            });
    }

    cargar(event: LazyLoadEvent) {
        this.bloquearPagina();
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.syReporteArchivoService.listarConPaginacion(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.desbloquearPagina();
            });
    }

    eliminar(dto: DtoSyReporteArchivo) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaInactivar(),
            accept: () => {

                this.bloquearPagina();
                const pk: SyReporteArchivoPk = new SyReporteArchivoPk();
                pk.aplicacioncodigo = dto.aplicacionCodigo;
                pk.reportecodigo = dto.reporteCodigo;
                pk.companiasocio = dto.companiaSocio;
                pk.periodo = dto.periodo;
                pk.version = dto.version;

                this.syReporteArchivoService.eliminarReporteArchivo(pk).then(r => {
                    this.dt.reset();
                    this.messageService.clear();
                    this.messageService.add({
                        severity: 'info', summary: 'Información',
                        detail: this.getMensajeInactivadoCodigoString(dto.reporteCodigo)

                    });
                });
            }
        });
    }



    salir() {
        this.router.navigate(['spring/syreporte-listado']);
    }


    bajarArchivo(dto: DtoSyReporteArchivo) {

        const pk: SyReporteArchivoPk = new SyReporteArchivoPk();
        pk.aplicacioncodigo = dto.aplicacionCodigo;
        pk.reportecodigo = dto.reporteCodigo;
        pk.companiasocio = dto.companiaSocio;
        pk.periodo = dto.periodo;
        pk.version = dto.version;

        this.syReporteArchivoService.solicitudObtenerPorId(pk).then(reg => {

            if (reg === null) {
                return;
            }

            if (reg.auxString === null) {
                this.mostrarMensajeAdvertencia('No se encontró el documento');
                return;
            }


            reg.auxString = reg.auxString.replace(new RegExp('&lt;', 'g'), '<');
            reg.auxString = reg.auxString.replace(new RegExp('&gt;', 'g'), '>');

            const a = document.createElement('a');

            a.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(reg.auxString));
            a.setAttribute('download', reg.reportecodigo + "_" + reg.version + '.html');
            a.style.display = 'none';

            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
        });
    }

}
