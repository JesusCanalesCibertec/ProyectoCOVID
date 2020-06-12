import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Afemst, AfemstPk } from '../dominio/afemst';
import { ParametroPaginacionGenerico } from '../../dominio/ParametroPaginacionGenerico';
import { DtoTabla } from '../../dominio/dto/DtoTabla';


@Injectable()
export class AfemstService {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Afemst/';
    }

    listarTodos(): Promise<Afemst[]> {
        return this.http.get(this.rutaServicio + 'listartodos')
            .toPromise()
            .then(response => response as Afemst[]);
    }
    listarPaginacion(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico);
    }

    obtenerPorId(pk: AfemstPk): Promise<Afemst> {
        return this.http.post(this.rutaServicio + 'obtenerporid', pk)
            .toPromise()
            .then(response => response as Afemst);
    }


}
