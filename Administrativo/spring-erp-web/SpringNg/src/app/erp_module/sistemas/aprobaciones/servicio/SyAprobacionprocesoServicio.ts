import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Header } from 'primeng/primeng';
import { DtoTabla } from '../../../shared/dominio/dto/DtoTabla';
import { SyAprobacionproceso } from '../dominio/SyAprobacionproceso';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { FiltroSolicitudes } from '../dominio/filtro/FiltroSolicitudes';
import { DtoRestSolicitudLista } from '../dominio/dto/DtoRestSolicitudLista';
import { DtoSolicitudes } from '../../../shared/dominio/dto/DtoSolicitudes';

@Injectable()
export class SyAprobacionprocesoServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/sistema/SyAprobacionproceso/';
    }

    public listarPorAplicacion(aplicacion: string): Promise<SyAprobacionproceso[]> {

        return this.http.get(this.rutaServicio + 'listarPorAplicacion/' + aplicacion)
            .toPromise()
            .then(response => {
                return response as SyAprobacionproceso[];
            })
            .catch(error => new Array());
    }

    public listarSolicitudes(aplicacion: FiltroSolicitudes): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarSolicitudes', aplicacion)
            .toPromise()
            .then(response => {
                return response as ParametroPaginacionGenerico;
            })
            .catch(error => new ParametroPaginacionGenerico());
    }

    public solicitudEventoPreparar(aplicacion: DtoRestSolicitudLista): Promise<DtoSolicitudes[]> {
        return this.http.post(this.rutaServicio + 'solicitudEventoPreparar', aplicacion)
            .toPromise()
            .then(response => {
                return response as DtoSolicitudes[];
            })
            .catch(error => new Array());
    }

    public solicitudEventoEjecutar(aplicacion: DtoRestSolicitudLista): Promise<DtoSolicitudes[]> {
        return this.http.post(this.rutaServicio + 'solicitudEventoEjecutar', aplicacion)
            .toPromise()
            .then(response => {
                return response as DtoSolicitudes[];
            })
            .catch(error => new Array());
    }


}
