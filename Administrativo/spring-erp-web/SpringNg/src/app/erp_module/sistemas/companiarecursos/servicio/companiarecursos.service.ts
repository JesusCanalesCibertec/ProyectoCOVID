import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Companyownerrecurso, CompanyownerrecursoPk } from '../dominio/companyownerrecurso';

@Injectable()
export class CompaniaRecursosService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Companyownerrecurso/';
    }

    obtenerListaRecursosDetalle(tipoRecurso: string): Promise<Companyownerrecurso[]> {
        return this.http.get(this.rutaServicio + 'listarPorRecurso/' + tipoRecurso)
            .toPromise()
            .then(response => {
                return response as Companyownerrecurso[];
            })
            .catch(error => new Array());
    }


    obtenerPorId(compania: string, tipoRecurso: string, periodo: string): Promise<Companyownerrecurso> {
        const params = new HttpParams();
        params.set('tipoRecurso', tipoRecurso);
        params.set('compania', compania);
        params.set('periodo', periodo);

        return this.http.get(this.rutaServicio + 'obtenerPorId/' + tipoRecurso + '/' + compania  + '/' + periodo)
            .toPromise()
            .then(response => {
                return response as Companyownerrecurso;
            });
    }

    registrar(bean: Companyownerrecurso): Promise<Companyownerrecurso> {

        return this.http.post(this.rutaServicio + 'registrar', bean)
            .toPromise()
            .then(response => {
                return response as Companyownerrecurso;
            })
            .catch(error => null);
    }

    actualizar(bean: Companyownerrecurso): Promise<Companyownerrecurso> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => {
                return response as Companyownerrecurso;
            })
            .catch(error => null);
    }

    eliminar(bean: CompanyownerrecursoPk): Promise<any> {
        return this.http.post(this.rutaServicio + 'eliminarPorTipoRecurso', bean)
            .toPromise()
            .then(response => {
                return response as any;
            })
            .catch(error => null);
    }

}
