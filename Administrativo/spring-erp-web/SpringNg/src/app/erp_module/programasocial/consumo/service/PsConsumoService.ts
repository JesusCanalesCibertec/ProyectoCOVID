import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { DtoConsumo, DtoConsumoCarga } from '../dominio/dtoConsumo';

import { FiltroConsumo } from '../dominio/filtroConsumo';
import { PsConsumo, PsConsumoPk } from '../dominio/psConsumo';
import { DtoConsumoNutricional } from '../../consumonutricional/dominio/DtoConsumoNutricional';




@Injectable()
export class PsConsumoService {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsConsumo/';
    }

    public obtenerPorId(llave: PsConsumoPk): Promise<PsConsumo> {

        return this.http.post(this.rutaServicio + 'solicitarObtenerporid', llave)
            .toPromise()
            .then(response => {
                return response as PsConsumo;
            })
            .catch(error => new PsConsumo());
    }

    public obtenerPorid(llave: PsConsumoPk): Promise<PsConsumo> {

        return this.http.post(this.rutaServicio + 'obtenerPorId', llave)
            .toPromise()
            .then(response => {
                return response as PsConsumo;
            })
            .catch(error => new PsConsumo());
    }

    listarPaginacion(filtro: FiltroConsumo): Promise<ParametroPaginacionGenerico> {
        if (filtro.estado == null) {
            filtro.estado = 'A';
        }
        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }
    public registrar(bean: PsConsumo): Promise<PsConsumo> {
        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as PsConsumo)
            .catch(error => null);
    }
    // para actualizar
    public solicitudActualizar(bean: PsConsumo): Promise<any> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsConsumo)
            .catch(error => null);
    }
    // para eliminar
    public solicitudEliminar(bean: DtoConsumo): Promise<DtoConsumo> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as DtoConsumo)
            .catch(error => null);
    }
    public solicitudAnular(institucion: string, consumo: number): Promise<PsConsumo> {
        const params = new HttpParams().set('pIdInstitucion', institucion).set('pIdConsumo', JSON.stringify(consumo * 1));
        return this.http.get(this.rutaServicio + 'anular', { params })
            .toPromise()
            .then(response => response as PsConsumo)
            .catch(error => null);
    }

    public listarConsumoes(): Promise<PsConsumo[]> {

        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as PsConsumo[])
            .catch(error => null);
    }

    public exportar(bean: DtoConsumo): Promise<string> {
        return this.http.post(this.rutaServicio + 'exportar', bean)
            .toPromise()
            .then(response => (response as any).url)
            .catch(error => null);
    }
    public importar(bean: DtoConsumoCarga): Promise<DtoConsumoNutricional[]> {
        return this.http.post(this.rutaServicio + 'importar', bean)
            .toPromise()
            .then(response => (response as DtoConsumoNutricional[]))
            .catch(error => []);
    }
}
