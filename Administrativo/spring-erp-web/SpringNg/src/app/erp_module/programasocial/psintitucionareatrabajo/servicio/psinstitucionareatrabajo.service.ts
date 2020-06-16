import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { PsInstitucionAreaTrabajo } from '../dominio/psinstitucionareatrabajo';

@Injectable()
export class PsInstitucionAreaTrabajoServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsInstitucionAreaTrabajo/';
    }

    public listarActivos(): Promise<PsInstitucionAreaTrabajo[]> {

        return this.http.get(this.rutaServicio + 'listarActivos')
            .toPromise()
            .then(response => {
                return response as PsInstitucionAreaTrabajo[];
            })
            .catch(error => []);
    }
}
