import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { HrDepartamento, HrDepartamentoPk } from '../dominio/hrdepartamento';
import { DtoFiltro } from '../../dominio/dto/dtofiltro';

@Injectable()
export class HrDepartamentoService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/rrhh/HrDepartamento/';
    }

    listar(filtro: DtoFiltro): Promise<HrDepartamento[]> {
        return this.http.post(this.rutaServicio + 'listar', filtro)
            .toPromise()
            .then(response => response as HrDepartamento[])
            .catch(error => new Array());
    }


    public obtenerPorId(departamento: string): Promise<HrDepartamento> {

        const params = new HttpParams()
            .set('departamento', departamento);

        return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
            .toPromise()
            .then(response => {
                return response as HrDepartamento;
            })
            .catch(error => new HrDepartamento());
    }

}
