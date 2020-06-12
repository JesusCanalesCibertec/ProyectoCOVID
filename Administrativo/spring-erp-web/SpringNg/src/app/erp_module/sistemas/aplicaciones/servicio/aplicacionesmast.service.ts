import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Aplicacionesmast } from '../dominio/aplicacionesmast';

@Injectable()
export class AplicacionesmastServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/sistema/Aplicacionesmast/';
    }

    public listarPorAplicacion(): Promise<Aplicacionesmast[]> {

        return this.http.get(this.rutaServicio + 'ListarActivos')
            .toPromise()
            .then(response => {
                return response as Aplicacionesmast[];
            })
            .catch(error => new Array());
    }
}
