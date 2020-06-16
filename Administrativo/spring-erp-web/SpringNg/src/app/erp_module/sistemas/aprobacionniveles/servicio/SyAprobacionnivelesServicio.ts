import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { DtoTabla } from '../../../shared/dominio/dto/DtoTabla';
import { SyAprobacionNiveles, SyAprobacionNivelesPk } from '../dominio/SyAprobacionNiveles';
import { MensajeUsuario } from '../../../shared/dominio/dto/MensajeUsuario';
import { FiltroNivelAprobacion } from '../dominio/FiltroNivelAprobacion';

@Injectable()
export class SyAprobacionnivelesServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/sistema/SyAprobacionniveles/';
    }

    public listarAplicacionPorUsuario(): Promise<DtoTabla[]> {

        return this.http.get(this.rutaServicio + 'listarAplicacionPorUsuario')
            .toPromise()
            .then(response => {
                return response as DtoTabla[];
            })
            .catch(error => new Array());
    }

    public listar(filtro: FiltroNivelAprobacion): Promise<SyAprobacionNiveles[]> {

        return this.http.post(this.rutaServicio + 'listar', filtro)
            .toPromise()
            .then(response => {
                return response as SyAprobacionNiveles[];
            })
            .catch(error => new Array());
    }

    public registrar(bean: SyAprobacionNiveles): Promise<SyAprobacionNiveles> {
        return this.http.post(this.rutaServicio + 'registrar', bean)
            .toPromise()
            .then(response => {
                return response as SyAprobacionNiveles[];
            })
            .catch(error => null);
    }

    public eliminar(bean: SyAprobacionNiveles): Promise<MensajeUsuario> {

        const params = new HttpParams()
            .set('codigo', JSON.stringify(bean.codigo))
            .set('compania', bean.companyOwner);

        return this.http.get(this.rutaServicio + 'eliminar', { params })
            .toPromise()
            .then(response => {
                return new MensajeUsuario();
            })
            .catch(error => null);
    }

    public actualizar(bean: SyAprobacionNiveles): Promise<MensajeUsuario> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => {
                return new MensajeUsuario();
            })
            .catch(error => null);
    }

    public obtenerPorIdCompleto(bean: SyAprobacionNivelesPk): Promise<SyAprobacionNiveles> {
        return this.http.post(this.rutaServicio + 'obtenerPorIdCompleto', bean)
            .toPromise()
            .then(response => {
                return response as SyAprobacionNiveles
            })
            .catch(error => null);
    }
}
