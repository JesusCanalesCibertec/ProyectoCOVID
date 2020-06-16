import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Pais } from '../dominio/Pais';
import { DtoTabla } from '../../dominio/dto/DtoTabla';
import { ParametroPaginacionGenerico } from '../../dominio/ParametroPaginacionGenerico';

@Injectable()
export class PaisServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Pais/';
    }

    listarTodos(): Promise<Pais[]> {
        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as Pais[])
            .catch(error => new Array());
    }

    public listarUbigeoPorFiltro(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarUbigeoPorFiltro', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    obtenerUbigeo(ubigeo: string): Promise<DtoTabla> {
        return this.http.get(this.rutaServicio + 'obtenerUbigeo/' + ubigeo)
            .toPromise()
            .then(response => response as DtoTabla)
            .catch(error => null);
    }


}
