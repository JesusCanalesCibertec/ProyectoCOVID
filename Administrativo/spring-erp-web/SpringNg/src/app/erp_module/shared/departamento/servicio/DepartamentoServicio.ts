import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Departamento } from '../dominio/Departamento';

@Injectable()
export class DepartamentoServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Departamento/';
    }

    
    listarTodos(): Promise<Departamento[]> {
        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as Departamento[])
            .catch(error => new Array());
    }

    listarActivosPorPais(idPais: string): Promise<Departamento[]> {

        const params = new HttpParams()
            .set('idPais', idPais);

        return this.http.get(this.rutaServicio + 'listarActivosPorPais', { params })
            .toPromise()
            .then(response => response as Departamento[])
            .catch(error => new Array());
    }


    public obtenerPorId(departamento: string): Promise<Departamento> {

        const params = new HttpParams()
            .set('departamento', departamento);

        return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
            .toPromise()
            .then(response => {
                return response as Departamento;
            })
            .catch(error => new Departamento());
    }


}
