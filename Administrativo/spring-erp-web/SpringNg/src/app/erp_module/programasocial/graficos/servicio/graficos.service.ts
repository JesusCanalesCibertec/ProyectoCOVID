import { DtoResponsable } from './../dominio/dtoresponsable';
import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class GraficosService {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsGraficos/';
    }

    

  

    getUsers(): Observable<DtoResponsable[]> {
        return this.http.get<DtoResponsable[]>(this.rutaServicio + 'listarGrafico');
    }

    listarGraficoMultiple(): Observable<any[]> {
        return this.http.get<any[]>(this.rutaServicio + 'listarGraficoMultiple');
    }

}
