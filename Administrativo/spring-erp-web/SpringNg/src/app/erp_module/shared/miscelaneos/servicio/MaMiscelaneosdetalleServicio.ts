import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Header } from 'primeng/primeng';
import { MaMiscelaneosdetalle, MaMiscelaneosdetallePk } from '../dominio/MaMiscelaneosdetalle';
import { FiltroMiscelaneosDetalle } from '../dominio/filtromiscelaneosdetalle';
import { DtoTabla } from '../../dominio/dto/DtoTabla';

@Injectable()
export class MaMiscelaneosdetalleServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/MaMiscelaneosdetalle/';
    }

    public listarActivos(aplicacionCodigo: string, codigoTabla: string): Promise<MaMiscelaneosdetalle[]> {

        return this.http.get(this.rutaServicio + 'listarActivos/' + aplicacionCodigo + '/' + codigoTabla)
            .toPromise()
            .then(response => response as MaMiscelaneosdetalle[])
            .catch(error => []);
    }

    public listarActivosBean(aplicacionCodigo: string, codigoTabla: string): Promise<MaMiscelaneosdetalle[]> {

        return this.http.get(this.rutaServicio + 'listarActivosBean/' + aplicacionCodigo + '/' + codigoTabla)
            .toPromise()
            .then(response => response as MaMiscelaneosdetalle[])
            .catch(error => []);
    }

    public listarHeaderPorAplicacion(aplicacionCodigo: string, compania: string): Promise<DtoTabla[]> {

        return this.http.get(this.rutaServicio + 'listarHeaderPorAplicacion/' + aplicacionCodigo + '/' + compania)
            .toPromise()
            .then(response => response as DtoTabla[])
            .catch(error => []);
    }

    public obtenerPorId(pk: MaMiscelaneosdetallePk): Promise<MaMiscelaneosdetalle> {

        return this.http.post(this.rutaServicio + 'obtenerPorId', pk)
            .toPromise()
            .then(response => response as MaMiscelaneosdetalle)
            .catch(error => new MaMiscelaneosdetalle());
    }


    public listar(filtro: FiltroMiscelaneosDetalle): Promise<MaMiscelaneosdetalle[]> {

        return this.http.post(this.rutaServicio + 'listar', filtro)
            .toPromise()
            .then(response => response as MaMiscelaneosdetalle[])
            .catch(error => []);
    }

    public actualizar(bean: MaMiscelaneosdetalle): Promise<MaMiscelaneosdetalle> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as MaMiscelaneosdetalle)
            .catch(error => null);

    }

    public registrar(bean: MaMiscelaneosdetalle): Promise<MaMiscelaneosdetalle> {
        return this.http.post(this.rutaServicio + 'registrar', bean)
            .toPromise()
            .then(response => response as MaMiscelaneosdetalle)
            .catch(error => null);

    }

    public eliminar(pk: MaMiscelaneosdetallePk): Promise<any> {
        return this.http.post(this.rutaServicio + 'eliminar', pk)
            .toPromise()
            .then(response => response as any)
            .catch(error => null);

    }



}
