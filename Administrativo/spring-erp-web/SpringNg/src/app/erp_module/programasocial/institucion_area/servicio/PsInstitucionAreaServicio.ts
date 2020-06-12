import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PsInstitucionArea, PsInstitucionAreaPk } from '../dominio/PsInstitucionArea';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';


@Injectable()
export class PsInstitucionAreaServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsInstitucionArea/';
    }

    public listarTodo(): Promise<PsInstitucionArea[]> {
        return this.http.get(this.rutaServicio + 'listarTodo')
            .toPromise()
            .then(response => {
                return response as PsInstitucionArea[];
            })
            .catch(error => []);
    }


    public listado(codigo: string): Promise<PsInstitucionArea[]> {
        const params = new HttpParams().set('pIdInstitucion', codigo);
        return this.http.get(this.rutaServicio + 'listado', { params })
            .toPromise()
            .then(response => {
                return response as PsInstitucionArea[];
            })
            .catch(error => []);
    }

    public listadoPorPrograma(codigo: string, idPrograma: string): Promise<PsInstitucionArea[]> {
        const params = new HttpParams().set('pIdInstitucion', codigo)
            .set('idPrograma', idPrograma);
        return this.http.get(this.rutaServicio + 'listadoPorPrograma', { params })
            .toPromise()
            .then(response => {
                return response as PsInstitucionArea[];
            })
            .catch(error => []);
    }

    public obtenerPorId(bean: PsInstitucionAreaPk): Promise<PsInstitucionArea> {

        return this.http.post(this.rutaServicio + 'obtenerPorId', bean)
            .toPromise()
            .then(response => {
                return response as PsInstitucionArea;
            })
            .catch(error => new PsInstitucionArea());
    }

    public eliminar(bean: PsInstitucionAreaPk): Promise<PsInstitucionArea> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => {
                return response as PsInstitucionArea;
            })
            .catch(error => new PsInstitucionArea());
    }

    public registrar(bean: PsInstitucionArea): Promise<PsInstitucionArea> {

        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as PsInstitucionArea)
            .catch(error => null);
    }

    public solicitudActualizar(bean: PsInstitucionArea): Promise<PsInstitucionArea> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsInstitucionArea)
            .catch(error => null);
    }

    public solicitudEliminar(bean: PsInstitucionArea): Promise<PsInstitucionArea> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as PsInstitucionArea)
            .catch(error => null);
    }

    public solicitudAnular(institucion: string, area: string): Promise<PsInstitucionArea> {
        const params = new HttpParams().set('pIdInstitucion', institucion).set('pIdArea', area);
        return this.http.get(this.rutaServicio + 'anular', { params })
            .toPromise()
            .then(response => response as PsInstitucionArea)
            .catch(error => null);
    }


}
