import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Header } from 'primeng/primeng';
import { DtoEmpleadoBasico } from '../../selectores/empleado/dominio/DtoEmpleadoBasico';
import { DtoTabla } from '../../dominio/dto/DtoTabla';

@Injectable()
export class CompanyownerServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Companyowner/';
    }

    public listarActivos(): Promise<DtoTabla[]> {
        return this.http.get(this.rutaServicio + 'listarActivos')
            .toPromise()
            .then(response => {
                return response as DtoTabla[];
            })
            .catch(error => new Array());
    }

    public listarCompaniasPorSeguridad(): Promise<DtoTabla[]> {
        return this.http.get(this.rutaServicio + 'listarCompaniasPorSeguridad')
            .toPromise()
            .then(response => {
                return response as DtoTabla[];
            })
            .catch(error => new Array());
    }
}
