import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Header } from 'primeng/primeng';
import { StringifyOptions } from 'querystring';
import { Zonapostal } from '../dominio/Zonapostal';

@Injectable()
export class ZonapostalServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Zonapostal/';
    }

    public listarActivosPorProvincia(idDepartamento: string, idProvincia: string): Promise<Zonapostal[]> {

        const params = new HttpParams()
            .set('idDepartamento', idDepartamento)
            .set('idProvincia', idProvincia);

        return this.http.get(this.rutaServicio + 'listarActivosPorProvincia', { params })
            .toPromise()
            .then(response => {
                return response as Zonapostal[];
            })
            .catch(error => new Array());
    }

    public obtenerNombrePorId(departamento: string, provincia: string, distrito: string): Promise<string> {

        const params = new HttpParams()
            .set('departamento', departamento)
            .set('provincia', provincia)
            .set('distrito', distrito);

        return this.http.get(this.rutaServicio + 'obtenerNombrePorId', { params })
            .toPromise()
            .then(response => {
                return (response as any).nombre;
            })
            .catch(error => '');
    }



}
