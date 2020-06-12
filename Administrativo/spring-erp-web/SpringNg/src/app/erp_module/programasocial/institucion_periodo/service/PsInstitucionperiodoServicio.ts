import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { FiltroPsInstitucionperiodo } from '../dominio/filtroPsInstitucionperiodo';
import { DtoPsInstitucionperiodo } from '../dominio/dtoPsInstitucionperiodo';
import { PsInstitucionperiodo } from '../dominio/PsInstitucionperiodo';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';



@Injectable()
export class PsInstitucionperiodoServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsInstitucionperiodo/';
    }

    public obtenerPorId(codigo: string): Promise<PsInstitucionperiodo> {
        const params = new HttpParams().set('codigo', codigo);
        return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
            .toPromise()
            .then(response => {
                return response as PsInstitucionperiodo;
            })
            .catch(error => new PsInstitucionperiodo());
    }
    listarPaginacion(filtro: FiltroPsInstitucionperiodo): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }
    public registrar(bean: PsInstitucionperiodo): Promise<PsInstitucionperiodo> {
        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as PsInstitucionperiodo)
            .catch(error => null);
    }

    public copiarPeriodo(bean: DtoPsInstitucionperiodo): Promise<DtoPsInstitucionperiodo> {
        return this.http.post(this.rutaServicio + 'copiarPeriodo', bean).toPromise()
            .then(Response => Response as DtoPsInstitucionperiodo)
            .catch(error => null);
    }

    public solicitudAnular(bean: DtoPsInstitucionperiodo): Promise<DtoPsInstitucionperiodo> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as DtoPsInstitucionperiodo)
            .catch(error => null);
    }

    public solicitudActualizar(list: DtoPsInstitucionperiodo[]): Promise<DtoPsInstitucionperiodo[]> {

        return this.http.post(this.rutaServicio + 'actualizar', list)
            .toPromise()
            .then(response => response as DtoPsInstitucionperiodo[])
            .catch(error => null);
    }


    public listado(codigo: string, periodo: string): Promise<DtoPsInstitucionperiodo[]> {
        if (codigo === undefined || codigo == null) {
            codigo = '';
        }
        const params = new HttpParams().set('pIdInstitucion', codigo)
                                        .set('pIdPeriodo', periodo);

        return this.http.get(this.rutaServicio + 'listado', { params })
            .toPromise()
            .then(response => {
                return response as DtoPsInstitucionperiodo[];
            })
            .catch(error => new Array());
    }


    public periodoActivo(pIdInstitucion: string,
        pIdPrograma: string,
        pIdComponente: string): Promise<DtoTabla> {

        const params = new HttpParams().set('pIdInstitucion', pIdInstitucion)
            .set('pIdPrograma', pIdPrograma)
            .set('pIdComponente', pIdComponente);

        return this.http.get(this.rutaServicio + 'periodoActivo', { params })
            .toPromise()
            .then(response => {
                return response as DtoTabla;
            })
            .catch(error => null);
    }

    // public List<DtoTabla> listarPeriodoPorComponente(string pIdInstitucion, String pIdPrograma, String pIdComponente)


    public listarPeriodoPorComponente(pIdInstitucion: string,
        pIdPrograma: string,
        pIdComponente: string): Promise<DtoTabla[]> {

        const params = new HttpParams()
            .set('pIdInstitucion', pIdInstitucion)
            .set('pIdPrograma', pIdPrograma)
            .set('pIdComponente', pIdComponente);

        return this.http.get(this.rutaServicio + 'listarPeriodoPorComponente', { params })
            .toPromise()
            .then(response => {
                return response as DtoTabla[];
            })
            .catch(error => []);
    }

    public listarPeriodoSimple(pIdPeriodo: string): Promise<DtoTabla[]> {

        const params = new HttpParams()
            .set('idPeriodo', pIdPeriodo);

        return this.http.get(this.rutaServicio + 'periodoListarSimple', { params })
            .toPromise()
            .then(response => {
                return response as DtoTabla[];
            })
            .catch(error => []);
    }
}
