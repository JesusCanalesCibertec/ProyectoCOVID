import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Header } from 'primeng/primeng';
import { DtoEmpleadoBasico } from '../dominio/DtoEmpleadoBasico';
import { FiltroPaginacionEmpleado } from '../dominio/filtropaginacionempleado';
import { ParametroPaginacionGenerico } from '../../../dominio/ParametroPaginacionGenerico';
import { FiltroPaginacionCumpleanio } from '../dominio/FiltroPaginacionCumpleanio';
import { FiltroPaginacionAniversario } from '../dominio/FiltroPaginacionAniversario';
import { DtoTablaEntero } from '../../../dominio/dto/DtoTablaEntero';

@Injectable()
export class EmpleadomastServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Empleadomast/';
    }

    public obtenerInformacionPorIdPersonaUsuarioActual(): Promise<DtoEmpleadoBasico> {

        return this.http.get(this.rutaServicio + 'obtenerInformacionPorIdPersonaUsuarioActual')
            .toPromise()
            .then(response => {
                return response as DtoEmpleadoBasico;
            })
            .catch(error => new DtoEmpleadoBasico());
    }

    public obtenerInformacionPorIdPersona(compania: string, empleado: number): Promise<DtoEmpleadoBasico> {

        if (compania === undefined) {
            compania = '';
        }

        if (compania === null) {
            compania = '';
        }

        const params = new HttpParams().set('compania', compania).set('empleado', JSON.stringify(empleado));

        return this.http.get(this.rutaServicio + 'obtenerInformacionPorIdPersona', { params })
            .toPromise()
            .then(response => {
                return response as DtoEmpleadoBasico;
            })
            .catch(error => new DtoEmpleadoBasico());
    }


    listarPaginacionEmpleadoSelector(filtros: FiltroPaginacionEmpleado): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarBusqueda', filtros)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    listarCumpleanios(filtros: FiltroPaginacionCumpleanio): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarCumpleanios', filtros)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    listarAniversarios(filtros: FiltroPaginacionAniversario): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarAniversarios', filtros)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    obtenerFotoActual(): Promise<string> {
        return this.http.get(this.rutaServicio + 'obtenerFotoActual')
            .toPromise()
            .then(response => {
                const d = (response as any).base64;

                return CONVERTIR_FOTO(d);
            })
            .catch(error => 'assets/layout/images/user.png');
    }

    obtenerCantidadDiaPorMes(nroMes: number): Promise<DtoTablaEntero[]> {
        const params = new HttpParams()
            .set('nroMes', JSON.stringify(nroMes * 1));

        return this.http.get(this.rutaServicio + 'obtenerDiasPorMes', { params })
            .toPromise()
            .then(response => {
                return response as DtoTablaEntero[];
            })
            .catch(error => []);
    }

}

export function CONVERTIR_FOTO(d) {
    if (d === undefined || d == null || d === '') {
        d = 'assets/layout/images/user.png';
    } else {
        d = 'data:image/jpg;base64,' + d;
    }
    return d;
}
