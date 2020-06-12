import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { DtoSyReporte } from '../dominio/dtosyreporte';
import { SyReportePk, SyReporte } from '../dominio/sysreporte';
import { SyReporteproceso } from '../dominio/sysreporteproceso';

@Injectable()
export class SyReporteService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/sistema/SyReporte/';
    }


    public listarConPaginacion(filtro: DtoSyReporte): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPorPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    solicitudObtenerPorId(bean: SyReportePk): Promise<SyReporte> {
        return this.http.post(this.rutaServicio + 'obtenerPorId', bean)
            .toPromise()
            .then(response => response as SyReporte)
            .catch(error => null);
    }

    public solicitudRegistrar(bean: SyReporte): Promise<SyReporte> {
        return this.http.post(this.rutaServicio + 'registrar', bean)
            .toPromise()
            .then(response => response as SyReporte)
            .catch(error => null);

    }

    public solicitudActualizar(bean: SyReporte): Promise<SyReporte> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as SyReporte)
            .catch(error => null);
    }

    public listar(): Promise<SyReporte[]> {

        return this.http.get(this.rutaServicio + 'listar')
            .toPromise()
            .then(response => response as SyReporte[])
            .catch(error => null);
    }

}
