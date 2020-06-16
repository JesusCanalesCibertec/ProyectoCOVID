import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { PsConsumoNutricional } from '../dominio/PsConsumoNutricional';
import { DtoConsumoNutricional } from '../dominio/DtoConsumoNutricional';
import { PsConsumoPk } from '../../consumo/dominio/psConsumo';




@Injectable()
export class PsConsumoNutricionalService {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsConsumoNutricional/';
    }

    public listardetalle(llave: PsConsumoPk): Promise<DtoConsumoNutricional[]> {
        
        return this.http.post(this.rutaServicio + 'listardetalle', llave)
            .toPromise()
            .then(response => {
                return response as DtoConsumoNutricional[];
            })
            .catch(error => new Array());
    }

    public obtenerPorId(codigo: string): Promise<PsConsumoNutricional> {
        const params = new HttpParams().set('codigo', codigo);
        return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
            .toPromise()
            .then(response => {
                return response as PsConsumoNutricional;
            })
            .catch(error => new PsConsumoNutricional());
    }

   
    public registrar(bean: PsConsumoNutricional): Promise<PsConsumoNutricional> {
        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as PsConsumoNutricional)
            .catch(error => null);
    }
    // para actualizar
    public solicitudActualizar(bean: PsConsumoNutricional): Promise<any> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsConsumoNutricional)
            .catch(error => null);
    }
    // para eliminar
    public solicitudAnular(bean: DtoConsumoNutricional): Promise<DtoConsumoNutricional> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as DtoConsumoNutricional)
            .catch(error => null);
    }

    public listarConsumoNutricionales(): Promise<PsConsumoNutricional[]> {

        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as PsConsumoNutricional[])
            .catch(error => null);
    }

}
