import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MaUnidadNegocio } from '../dominio/maunidadnegocio';

@Injectable()
export class MaUnidadNegocioService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/MaUnidadnegocio/';
    }

    listarTodos(): Promise<MaUnidadNegocio[]> {
        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as MaUnidadNegocio[]);
    }

    listarActivos(): Promise<MaUnidadNegocio[]> {
        return this.http.get(this.rutaServicio + 'listarActivos')
            .toPromise()
            .then(response => response as MaUnidadNegocio[]);
    }
}

