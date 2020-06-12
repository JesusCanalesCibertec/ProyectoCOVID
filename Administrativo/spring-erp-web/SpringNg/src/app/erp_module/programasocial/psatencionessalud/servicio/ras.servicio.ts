import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DtoAtencion } from '../dominio/dtoatencion';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { FiltroRas } from '../dominio/filtroras';

@Injectable()
export class RasService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsAntencion/';
    }

    public registrar(bean: DtoAtencion): Promise<DtoAtencion> {

        bean.fechaAtencion.setHours(0, 0, 0);
        return this.http.post(this.rutaServicio + 'registrar', bean)
            .toPromise()
            .then(Response => Response as DtoAtencion)
            .catch(error => null);
    }

    public listarPaginacion(filtro: FiltroRas): Promise<ParametroPaginacionGenerico> {

        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public listarPaginacionConsulta(filtro: FiltroRas): Promise<ParametroPaginacionGenerico> {

        return this.http.post(this.rutaServicio + 'listarPaginacionConsulta', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public actualizar(bean: DtoAtencion): Promise<DtoAtencion> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as DtoAtencion)
            .catch(error => null);
    }

    public anular(bean: DtoAtencion): Promise<DtoAtencion> {

        return this.http.post(this.rutaServicio + 'anular', bean)
            .toPromise()
            .then(response => response as DtoAtencion)
            .catch(error => null);
    }


    public obtenerPorId(idInstitucion: string, idEmpleado: number): Promise<DtoAtencion> {

        const params = new HttpParams()
            .set('idInstitucion', idInstitucion)
            .set('idEmpleado', JSON.stringify(idEmpleado * 1));

        return this.http.get(this.rutaServicio + 'obtenerporid', { params })
            .toPromise()
            .then(response => response as DtoAtencion)
            .catch(error => null);
    }


    public exportar(): Promise<string> {
        return this.http.get(this.rutaServicio + 'exportar')
            .toPromise()
            .then(response => (response as any).url)
            .catch(error => null);
    }
}
