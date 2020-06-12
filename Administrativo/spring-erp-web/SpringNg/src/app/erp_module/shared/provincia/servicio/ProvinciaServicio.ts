import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Header } from 'primeng/primeng';
import { Provincia } from '../dominio/Provincia';

@Injectable()
export class ProvinciaServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Provincia/';
    }

    public listarActivosPorDepartamento(idDepartamento: string): Promise<Provincia[]> {

        const params = new HttpParams()
            .set('idDepartamento', idDepartamento);

        return this.http.get(this.rutaServicio + 'listarActivosPorDepartamento', { params })
            .toPromise()
            .then(response => {
                return response as Provincia[];
            })
            .catch(error => new Array());
    }

    public obtenerNombrePorId(departamento: string, provincia: string): Promise<string> {

        const params = new HttpParams()
            .set('departamento', departamento)
            .set('provincia', provincia);

        return this.http.get(this.rutaServicio + 'obtenerNombrePorId', { params })
            .toPromise()
            .then(response => {
                return (response as any).nombre;
            })
            .catch(error => '');
    }


}
