import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BootstrapOptions } from '@angular/core/src/application_ref';
import { Sid } from '../dominio/Sid';

@Injectable()
export class SidServicio {

    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    public enviar(sid: string, correo: string): Promise<Sid> {
        const params = new HttpParams()
            .set('sid', sid).set('correo', correo);
        return this.http.get(this.baseUrl + 'api/Sid/enviar', { params })
            .toPromise()
            .then(response => response as Sid)
            .catch(error => null);
    }
}
