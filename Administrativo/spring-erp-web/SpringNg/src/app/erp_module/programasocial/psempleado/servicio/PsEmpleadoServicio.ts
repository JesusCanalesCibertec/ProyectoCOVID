import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Header } from 'primeng/primeng';
import { DtoPsEmpleado } from '../dominio/DtoPsEmpleado';
import { FiltroPsEmpleado } from '../dominio/FiltroPsEmpleado';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { DtoTablaEntero } from 'src/app/erp_module/shared/dominio/dto/DtoTablaEntero';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';

@Injectable()
export class PsEmpleadoServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsEmpleado/';
    }

    public obtenerInformacionPorIdPersonaUsuarioActual(): Promise<DtoPsEmpleado> {

        return this.http.get(this.rutaServicio + 'obtenerInformacionPorIdPersonaUsuarioActual')
            .toPromise()
            .then(response => {
                return response as DtoPsEmpleado;
            })
            .catch(error => new DtoPsEmpleado());
    }


    public listarAreas(): Promise<DtoTabla[]> {
        return this.http.get(this.rutaServicio + 'listarAreas')
            .toPromise()
            .then(response => {
                return response as DtoTabla[];
            })
            .catch(error => []);
    }

    public obtenerInformacionPorIdPersona(compania: string, empleado: number): Promise<DtoPsEmpleado> {

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
                return response as DtoPsEmpleado;
            })
            .catch(error => new DtoPsEmpleado());
    }


    listarPaginacionEmpleadoSelector(filtros: FiltroPsEmpleado): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPaginacion', filtros)
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
