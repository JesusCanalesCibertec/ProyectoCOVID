import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { PsItem } from '../dominio/psItem';
import { FiltroItem } from '../dominio/filtroitem';
import { DtoItem } from '../dominio/dtoItem';






@Injectable()
export class PsItemServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsItem/';
    }

    public obtenerPorId(codigo: string): Promise<PsItem> {

        const params = new HttpParams().set('pIdItem', codigo);

        return this.http.get(this.rutaServicio + 'obtenerporid', { params })
            .toPromise()
            .then(response => {
                return response as PsItem;
            })
            .catch(error => new PsItem());
    }



    listarPaginacion(filtro: FiltroItem): Promise<ParametroPaginacionGenerico> {

        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public registrar(bean: PsItem): Promise<PsItem> {

        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as PsItem)
            .catch(error => null);
    }

    // para actualizar
    public solicitudActualizar(bean: PsItem): Promise<any> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsItem)
            .catch(error => null);
    }

    // para eliminar

  

    public solicitudEliminar(bean: DtoItem): Promise<DtoItem> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as DtoItem)
            .catch(error => null);
    }

    public solicitudAnular(codigo: string): Promise<PsItem> {
        const params = new HttpParams().set('pIdItem', codigo);
        return this.http.get(this.rutaServicio + 'anular', { params })
            .toPromise()
            .then(response => response as PsItem)
            .catch(error => null);
    }


    public listarItems(): Promise<PsItem[]> {

        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as PsItem[])
            .catch(error => null);
    }

}
