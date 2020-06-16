import { Component, OnInit } from '@angular/core';
import { DtoSyReporteArchivo } from '../../syreportearchivo/dominio/dtosyreporteArchivo';
import { SyReporteArchivoPk, SyReporteArchivo } from '../../syreportearchivo/dominio/sysreportearchivo';
import { SyReporteArchivoService } from '../../syreportearchivo/servicio/sysreportearchivo.service';
import { BaseComponent } from '../../../../base_module/components/basecomponent';
import { Message } from 'primeng/primeng';
import { MessageService } from 'primeng/components/common/messageservice';


@Component({
    selector: 'app-syreportearchivo',
    templateUrl: './syreporte-archivo.component.html'
})
export class SyReporteArchivoDatoComponent extends BaseComponent implements OnInit {
    verSelector: Boolean = false;
    api: string;

    constructor(
        private syReporteArchivoService: SyReporteArchivoService,
        messageService: MessageService
    ) { super(messageService); }
    reporte: string;
    syReporteArchivo: SyReporteArchivo;
    nombreArchivo: string;

    ngOnInit() {
    }

    mostrarSelector(dto: DtoSyReporteArchivo) {
        this.verSelector = true;

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
        this.syReporteArchivoService.solicitudActualizar(this.syReporteArchivo).then(
            r => {
                this.getMensajeActualizado(this.syReporteArchivo.reportecodigo);
                this.verSelector = false;
            }
        );
    }

    onUpload(event) {
        const img = event.files[0].objectURL;
        this.reporte = img;
        this.nombreArchivo = event.files[0].name;

        const reader = new FileReader();
        reader.readAsDataURL(event.files[0]);
        reader.onloadend = (evt) => {
            this.syReporteArchivo.auxString = reader.result.toString();

        };
    }
}
