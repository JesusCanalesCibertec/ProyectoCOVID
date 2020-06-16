import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Header } from 'primeng/primeng';
import { Banco } from '../dominio/Banco';

@Injectable()
export class BancoServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Banco/';
    }

    public obtenerPorId(banco: string): Promise<Banco> {

        const params = new HttpParams()
            .set('banco', banco);

        return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
            .toPromise()
            .then(response => {
                return response as Banco;
            })
            .catch(error => new Banco());
    }


}
