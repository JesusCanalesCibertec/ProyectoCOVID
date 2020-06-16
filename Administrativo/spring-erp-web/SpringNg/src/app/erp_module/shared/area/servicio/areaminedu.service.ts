import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { DtoTabla } from '../../dominio/dto/DtoTabla';
import { ParametroPaginacionGenerico } from '../../dominio/ParametroPaginacionGenerico';

@Injectable()
export class AreamineduServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/minedu/MpAreaminedu/';
    }



    public listarPaginacion(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
          .toPromise()
          .then(res => res as ParametroPaginacionGenerico)
          .catch(error => new ParametroPaginacionGenerico());
      }


}
