import { FiltroCapacidades } from './../dominio/FiltroCapacidades';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { PsCapacidad } from '../dominio/pscapacidad';
import { DtoCapacidad } from '../dominio/dtocapacidad';

@Injectable()
export class PsCapacidadesService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsCapacidad/';
    }

    public registrar(bean: DtoCapacidad): Promise<PsCapacidad> {

        return this.http.post(this.rutaServicio + 'registrar', bean)
            .toPromise()
            .then(Response => Response as PsCapacidad)
            .catch(error => null);
    }

    public listarPaginacion(filtro: FiltroCapacidades): Promise<ParametroPaginacionGenerico> {

        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public listarPaginacionConsulta(filtro: FiltroCapacidades): Promise<ParametroPaginacionGenerico> {

        return this.http.post(this.rutaServicio + 'listarPaginacionConsulta', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public actualizar(bean: DtoCapacidad): Promise<PsCapacidad> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsCapacidad)
            .catch(error => null);
    }

    public calcularAniosRetraso(bean: DtoCapacidad): Promise<DtoCapacidad> {
        return this.http.post(this.rutaServicio + 'calcularAniosRetraso', bean)
            .toPromise()
            .then(response => response as DtoCapacidad)
            .catch(error => null);
    }


    public anular(bean: PsCapacidad): Promise<PsCapacidad> {

        return this.http.post(this.rutaServicio + 'anular', bean)
            .toPromise()
            .then(response => response as PsCapacidad)
            .catch(error => null);
    }

    public obtenerPorId(idInstitucion: string, idBeneficiario: number, idCapacidad: number): Promise<DtoCapacidad> {

        const params = new HttpParams()
            .set('pIdInstitucion', idInstitucion)
            .set('pIdBeneficiario', JSON.stringify(idBeneficiario * 1))
            .set('pIdCapacidad', JSON.stringify(idCapacidad * 1));

        return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
            .toPromise()
            .then(response => response as DtoCapacidad)
            .catch(error => null);
    }

    public calcularBarthel(bean: DtoCapacidad): Promise<DtoCapacidad> {

        return this.http.post(this.rutaServicio + 'calcularBarthel', bean)
            .toPromise()
            .then(Response => Response as DtoCapacidad)
            .catch(error => null);
    }

    public calcularKatz(bean: DtoCapacidad): Promise<DtoCapacidad> {

        return this.http.post(this.rutaServicio + 'calcularKatz', bean)
            .toPromise()
            .then(Response => Response as DtoCapacidad)
            .catch(error => null);
    }

    public exportar(esNino: Boolean): Promise<string> {
        const params = new HttpParams()
            .set('esNino', JSON.stringify(esNino));

        return this.http.get(this.rutaServicio + 'exportar', { params })
            .toPromise()
            .then(response => (response as any).url)
            .catch(error => null);
    }


}




