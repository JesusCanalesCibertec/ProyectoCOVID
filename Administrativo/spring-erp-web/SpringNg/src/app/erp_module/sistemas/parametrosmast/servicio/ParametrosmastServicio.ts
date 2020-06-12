import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ParametrosmastPk, Parametrosmast } from '../dominio/Parametrosmast';

@Injectable()
export class ParametrosmastServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/sistema/Parametrosmast/';
    }

    public obtenerPorId(pk: ParametrosmastPk): Promise<Parametrosmast> {

        return this.http.post(this.rutaServicio + 'listarPorAplicacion/', pk)
            .toPromise()
            .then(response => {
                return response as Parametrosmast;
            })
            .catch(error => new Parametrosmast());
    }

    public obtenerValorCadena(parametro: string, aplicacion?: string, compania?: string): Promise<string> {

        const params = new HttpParams()
            .set('parametro', parametro)
            .set('aplicacion', aplicacion)
            .set('compania', compania);

        return this.http.get(this.rutaServicio + 'obtenerValorCadena', { params })
            .toPromise()
            .then(response => {
                return (response as any).valor as string;
            })
            .catch(error => '');
    }

    public obtenerValorInteger(parametro: string, aplicacion?: string, compania?: string): Promise<number> {

        const params = new HttpParams()
            .set('parametro', parametro)
            .set('aplicacion', aplicacion)
            .set('compania', compania);

        return this.http.get(this.rutaServicio + 'obtenerValorInteger', { params })
            .toPromise()
            .then(response => {
                return (response as any).valor as number;
            })
            .catch(error => 0);
    }

    public obtenerValorExplicacion(parametro: string, aplicacion?: string, compania?: string): Promise<string> {

        const params = new HttpParams()
            .set('parametro', parametro)
            .set('aplicacion', aplicacion)
            .set('compania', compania);

        return this.http.get(this.rutaServicio + 'obtenerValorExplicacion', { params })
            .toPromise()
            .then(response => {
                return (response as any).valor as string;
            })
            .catch(error => '');
    }

    public obtenerValorTipoDatoFlag(parametro: string, aplicacion: string, compania: string): Promise<string> {

        const params = new HttpParams()
            .set('parametro', parametro)
            .set('aplicacion', aplicacion)
            .set('compania', compania);

        return this.http.get(this.rutaServicio + 'obtenerValorTipoDatoFlag', { params })
            .toPromise()
            .then(response => {
                return (response as any).valor as string;
            })
            .catch(error => 'N');
    }

}
