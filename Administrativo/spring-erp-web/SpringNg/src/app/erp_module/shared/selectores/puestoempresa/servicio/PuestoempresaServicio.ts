import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Puestoempresa, PuestoempresaPk } from '../dominio/Puestoempresa';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { FiltroPuestoempresa } from '../dominio/FiltroPuestoempresa';





@Injectable()
export class PuestoempresaServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/rrhh/HrPuestoempresa/';
    }

    listarPaginacion(filtro: FiltroPuestoempresa): Promise<ParametroPaginacionGenerico> {

        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public obtenerPorId(bean: PuestoempresaPk): Promise<Puestoempresa> {
        
        return this.http.post(this.rutaServicio + 'obtenerPorId', bean)
            .toPromise()
            .then(response => {
                return response as Puestoempresa;
            })
            .catch(error => new Puestoempresa());
    }

}
