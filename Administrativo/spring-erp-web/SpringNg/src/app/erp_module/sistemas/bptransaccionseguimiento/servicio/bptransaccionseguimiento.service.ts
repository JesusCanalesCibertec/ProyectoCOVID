import { BpTransaccionSeguimientoPk, BpTransaccionSeguimiento } from '../dominio/bptransaccionseguimiento';
import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable()
export class BpTransaccionSeguimientoService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/sistema/BpTransaccionSeguimiento/';
    }

    public listarPaginacionAuditoria(idTransaccion: number): Promise<BpTransaccionSeguimiento[]> {

        const params = new HttpParams()
        .set('idTransaccion', JSON.stringify(idTransaccion));

        return this.http.get(this.rutaServicio + 'listarAplicacionPorUsuario', {params})
            .toPromise()
            .then(response => {
                return response as BpTransaccionSeguimiento[];
            })
            .catch(error => new Array());
    }
}
