import { Component, OnInit, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { PsReporteServicio } from '../servicio/ps_reporteservicio';
import { DtoReportesGraficos } from '../dominio/dtoreportesgraficos';
import { MessageService } from 'primeng/components/common/messageservice';


@Component({
    selector: 'app-reportes',
    templateUrl: './ps_reportes.component.html'
})
export class PsReportesComponent implements OnInit {
    constructor(
        private psReporteServicio: PsReporteServicio,
        public messageService: MessageService,
        private cdref: ChangeDetectorRef,
    ) { }

    coloresNoRepetidos = {
        domain: ['#d50000', '#c51162', '#aa00ff', '#6200ea', '#00bfa5', '#aeea00', '#ffab00', '#3e2723', '#263238']
    };

    coloresPorPrograma = {
        domain: ['#72A2CF', '#EF8844', '#B1AAA6']
    };

    colorScheme = {
        domain: ['#72A2CF', '#EF8844']
    };

    colorSchemeProyectos = {
        domain: ['#70AD47', '#FFE699', '#9DC3E6', '#A6A6A6', '#BB64C4', '#767171', '#4472C4']
    };

    mostrarBarras: Boolean = false;
    view: any[] = [1200, 400];
    showXAxis = true;
    showYAxis = true;
    gradient = false;
    showLegend = true;
    showXAxisLabel = true;
    showYAxisLabel = true;
    legendPosition = 'right';
    legendTitle = 'Leyenda';

    filtro: any;

    done: Boolean = false;
    listaPoblacion: DtoReportesGraficos[] = [];
    listaPoblacionGrafico: any[] = [];
    listaSubVencionProgramas: DtoReportesGraficos[] = [];
    listaSubVencionProgramasGraficos: any[] = [];
    listaSubVencionInstitucion: DtoReportesGraficos[] = [];
    listaSubVencionInstitucionGrafico: any[] = [];
    PoblacionBeneficiariaGrafico: any[] = [];

    listaPoblacionBeneficiaria: DtoReportesGraficos[] = [];
    colsPoblacion: any[];
    colsAnios: any[];


    ngOnInit(): void {
    
        this.listarReportePoblacionPorInstitucionyAnio();
        this.listarReportelistaSubVencionProgramas();
        this.listarReporteSubVencionInstitucion();
        this.colsAnios = [
            { field: null, header: 'Años' }];
        this.colsPoblacion = [
            { field: null, header: 'Población' }];
    }

    // tslint:disable-next-line:use-life-cycle-interface
    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }

    buscarInstitucionAnio() {
        this.colsAnios = [
            { field: null, header: 'Años' }];
        this.colsPoblacion = [
            { field: null, header: 'Población' }];

        
        this.listarReportePoblacionPorInstitucionyAnio();
    }

    listarReportePoblacionPorInstitucionyAnio() {
        
    }

    mostrarPoblacionInstitucion(): void {
        this.psReporteServicio.mostarReportePoblacionPorInstitucionyAnio().subscribe(resp => {
            this.listaPoblacionGrafico = resp;
            this.done = true;
        });
    }

    listarReportelistaSubVencionProgramas() {
        
    }

    mostrarSubVencionProgramasGraficos(): void {
        this.psReporteServicio.MostarListarReporteSubvencionesPorPrograma().subscribe(resp => {
            this.listaSubVencionProgramasGraficos = resp;
            this.done = true;
        });
    }

    listarReporteSubVencionInstitucion() {
        
    }

    mostrarSubVencionInstitucionGrafico(): void {
        this.psReporteServicio.MostarReporteSubvencionesPorInstitucion().subscribe(resp => {
            this.listaSubVencionInstitucionGrafico = resp;
            this.done = true;
        });
    }

    ListarReportePoblacionBeneficiaria(listaPoblacion: DtoReportesGraficos[]) {
        this.MostarReportePoblacionBeneficiaria();
        if (listaPoblacion) {
            listaPoblacion.forEach(element => {
                const grupo: any = { field: element.anio, header: element.anio.toString() };
                this.colsAnios.push(grupo);
            });
        }

        if (listaPoblacion) {
            listaPoblacion.forEach(element => {
                const grupo2: any = { field: element.total, header: element.total.toString() };
                this.colsPoblacion.push(grupo2);
            });
        }
    }

    MostarReportePoblacionBeneficiaria(): void {
        this.psReporteServicio.MostarReportePoblacionBeneficiaria().subscribe(resp => {
            this.PoblacionBeneficiariaGrafico = resp;
            this.done = true;
        });
    }



}
