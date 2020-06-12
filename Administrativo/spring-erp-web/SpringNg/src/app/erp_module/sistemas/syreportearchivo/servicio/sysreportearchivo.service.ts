import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { DtoSyReporte } from '../../syreporte/dominio/dtosyreporte';
import { SyReporteArchivoPk, SyReporteArchivo } from '../dominio/sysreportearchivo';

@Injectable()
export class SyReporteArchivoService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/sistema/SyReportearchivo/';
    }

    public listarConPaginacion(filtro: DtoSyReporte): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPorPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    solicitudObtenerPorId(bean: SyReporteArchivoPk): Promise<SyReporteArchivo> {
        return this.http.post(this.rutaServicio + 'obtenerPorId', bean)
            .toPromise()
            .then(response => response as SyReporteArchivo)
            .catch(error => null);
    }

    cambiarHtml(valor: string) {
        if (valor == undefined) return null;
        if (valor == null) return null;

        var cadena = valor.replace(new RegExp('&lt;', 'g'), '<');
        cadena = valor.replace(new RegExp('&gt;', 'g'), '>');

        return cadena;
    }

    public solicitudRegistrar(bean: SyReporteArchivo): Promise<SyReporteArchivo> {
        bean.auxString = this.cambiarHtml(bean.auxString);

        return this.http.post(this.rutaServicio + 'registrar', bean)
            .toPromise()
            .then(response => response as SyReporteArchivo)
            .catch(error => null);

    }

    public solicitudActualizar(bean: SyReporteArchivo): Promise<SyReporteArchivo> {
        bean.auxString = this.cambiarHtml(bean.auxString);
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as SyReporteArchivo)
            .catch(error => null);

    }

    public eliminarReporteArchivo(pk: SyReporteArchivoPk): Promise<SyReporteArchivo> {
        return this.http.post(this.rutaServicio + 'eliminarReporteArchivo', pk)
            .toPromise()
            .then(response => response as SyReporteArchivo)
            .catch(error => null);

    }

}
