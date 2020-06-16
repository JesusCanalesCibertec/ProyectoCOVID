import { AcSucursal } from '../dominio/acsucursal';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable()
export class AcSucursalService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/contabilidad/AcSucursal/';
    }

    listarTodos(): Promise<AcSucursal[]> {
        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as AcSucursal[]);
    }
}
