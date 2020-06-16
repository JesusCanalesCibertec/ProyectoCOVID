import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { PsBeneficiarioIngresoPk, PsBeneficiarioIngreso } from '../../beneficiario/dominio/psbeneficiarioingreso';
import { DtoBeneficiarioIngreso } from '../dominio/DtoBeneficiarioIngreso';





@Injectable()
export class PsBeneficiarioIngresoService {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsBeneficiarioIngreso/';
    }

    public obtenerUltimoIngreso(llave: PsBeneficiarioIngresoPk): Promise<PsBeneficiarioIngreso> {

        
        return this.http.post(this.rutaServicio + 'obtenerUltimoIngreso', llave)
            .toPromise()
            .then(response => {
                return response as PsBeneficiarioIngreso;
            })
            .catch(error => new PsBeneficiarioIngreso());
    }

    
    public registrar(bean: PsBeneficiarioIngreso): Promise<PsBeneficiarioIngreso> {
        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as PsBeneficiarioIngreso)
            .catch(error => null);
    }

    // para actualizar
    public solicitudActualizar(bean: PsBeneficiarioIngreso): Promise<any> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsBeneficiarioIngreso)
            .catch(error => null);
    }
    // para eliminar

    public solicitudAnular(bean: DtoBeneficiarioIngreso): Promise<DtoBeneficiarioIngreso> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as DtoBeneficiarioIngreso)
            .catch(error => null);
    }

    public listado(codigo: number, institucion: string): Promise<DtoBeneficiarioIngreso[]> {
        const params = new HttpParams().set('pIdBeneficiario', JSON.stringify(codigo * 1))
                                     .set('pIdInstitucion',institucion);
        
        return this.http.get(this.rutaServicio + 'listado', { params })
            .toPromise()
            .then(response => {
                return response as DtoBeneficiarioIngreso[];
            })
            .catch(error => new Array());
    }
   
    

}
