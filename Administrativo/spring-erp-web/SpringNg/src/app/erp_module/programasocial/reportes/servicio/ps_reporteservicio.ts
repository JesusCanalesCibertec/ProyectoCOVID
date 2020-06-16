import { MaMiscelaneosdetalle } from '../../../shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
// import { DtoReportesGraficos } from '../dominio/DtoReportesGraficos';
// import { FiltroGraficos } from '../dominio/filtrografico';

import { DtoReportesGraficos } from '../dominio/dtoreportesgraficos';
import { Observable } from 'rxjs';




@Injectable()
export class PsReporteServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsReporte/';
    }

    mostarReportePoblacionPorInstitucionyAnio(): Observable<any[]> {
        return this.http.get<any[]>(this.rutaServicio + 'MostarReportePoblacionPorInstitucionyAnio');
    }



    MostarListarReporteSubvencionesPorPrograma(): Observable<any[]> {
        return this.http.get<any[]>(this.rutaServicio + 'MostarListarReporteSubvencionesPorPrograma');
    }



    MostarReporteSubvencionesPorInstitucion(): Observable<any[]> {
        return this.http.get<any[]>(this.rutaServicio + 'MostarReporteSubvencionesPorInstitucion');
    }



    // public ListarReportePoblacionBeneficiaria(filtro: FiltroGraficos): Promise<DtoReportesGraficos[]> {
    //     return this.http.post(this.rutaServicio + 'ListarReportePoblacionBeneficiaria', filtro)
    //         .toPromise()
    //         .then(response => response as DtoReportesGraficos[])
    //         .catch(error => []);
    // }

    MostarReportePoblacionBeneficiaria(): Observable<any[]> {
        return this.http.get<any[]>(this.rutaServicio + 'MostarReportePoblacionBeneficiaria');
    }

}

