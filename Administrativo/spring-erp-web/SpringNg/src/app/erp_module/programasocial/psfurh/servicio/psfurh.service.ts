import { Injectable, Inject } from '@angular/core';
import { DtoComponenteFurh } from '../dominio/DtoComponenteFurh';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';

@Injectable()
export class PsFurhService {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/psfurh/PsFurh/';
    }

    public registrar(bean: DtoComponenteFurh): Promise<DtoComponenteFurh> {

        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as DtoComponenteFurh)
            .catch(error => null);
    }

    public exportar(): Promise<string> {
        return this.http.get(this.rutaServicio + 'exportar')
            .toPromise()
            .then(response => (response as any).url)
            .catch(error => null);
    }

    public listarFurh(filtro: DtoComponenteFurh): Promise<ParametroPaginacionGenerico> {
        if (filtro.estado === undefined) {
            filtro.estado = 'A';
        }
        return this.http.post(this.rutaServicio + 'listarFurh', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public actualizar(bean: DtoComponenteFurh): Promise<any> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as DtoComponenteFurh)
            .catch(error => null);
    }

    public anular(bean: DtoComponenteFurh): Promise<DtoComponenteFurh> {

        return this.http.post(this.rutaServicio + 'anular', bean)
            .toPromise()
            .then(response => response as DtoComponenteFurh)
            .catch(error => null);
    }


    public obtenerPorId(idInstitucion: string, idEmpleado: number): Promise<DtoComponenteFurh> {

        const params = new HttpParams()
            .set('idInstitucion', idInstitucion)
            .set('idEmpleado', JSON.stringify(idEmpleado * 1));

        return this.http.get(this.rutaServicio + 'obtenerporid', { params })
            .toPromise()
            .then(response => response as DtoComponenteFurh)
            .catch(error => null);
    }


}
