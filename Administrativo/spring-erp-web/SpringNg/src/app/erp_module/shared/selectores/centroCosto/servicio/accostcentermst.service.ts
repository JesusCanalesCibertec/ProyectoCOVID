import { Injectable, Inject } from '@angular/core';
// import { AcCostcentermstPk, AcCostcentermst } from '../dominio/accostcentermst';
import { HttpClient, HttpParams } from '@angular/common/http';
import { FiltroPaginacionAcCostcentermst } from '../dominio/filtropaginacionaccostcentermst';
import { ParametroPaginacionGenerico } from '../../../dominio/ParametroPaginacionGenerico';
import { AcCostcentermst } from '../dominio/accostcentermst';

@Injectable()
export class AcCostcentermstService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/contabilidad/AcCostcentermst/';
    }

    public listarConPaginacion(filtro: FiltroPaginacionAcCostcentermst): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarBusquedas', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public obtenerPorId(codigo: string): Promise<AcCostcentermst> {
        const params = new HttpParams()
            .set('Costcenter', codigo);

        return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
            .toPromise()
            .then(response => {
                return response as AcCostcentermst;
            })
            .catch(error => new AcCostcentermst());
    }

}
