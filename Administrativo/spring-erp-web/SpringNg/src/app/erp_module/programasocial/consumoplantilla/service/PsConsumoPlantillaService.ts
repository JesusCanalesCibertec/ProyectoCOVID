import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { PsConsumoPlantilla } from '../dominio/PsConsumoPlantilla';
import { DtoConsumoPlantilla } from '../dominio/DtoConsumoPlantilla';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';




@Injectable()
export class PsConsumoPlantillaService {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsConsumoPlantilla/';
    }

    public listarPlantilla(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPlantilla', filtro)
            .toPromise()
            .then(response => {
                return response as ParametroPaginacionGenerico;
            })
            .catch(error => null);
    }

 

    public obtenerPorId(institucion: string, plantilla: number): Promise<PsConsumoPlantilla> {
        const params = new HttpParams().set('pPlantilla', JSON.stringify(plantilla * 1))
            .set('pIdInstitucion', institucion);
        return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
            .toPromise()
            .then(response => {
                return response as PsConsumoPlantilla;
            })
            .catch(error => new PsConsumoPlantilla());
    }


    public registrar(bean: PsConsumoPlantilla): Promise<PsConsumoPlantilla> {
        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as PsConsumoPlantilla)
            .catch(error => null);
    }
    // para actualizar
    public solicitudActualizar(bean: PsConsumoPlantilla): Promise<any> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsConsumoPlantilla)
            .catch(error => null);
    }
    // para eliminar
    public solicitudEliminar(bean: DtoConsumoPlantilla): Promise<DtoConsumoPlantilla> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as DtoConsumoPlantilla)
            .catch(error => null);
    }

    public listarConsumoPlantillaes(): Promise<PsConsumoPlantilla[]> {

        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as PsConsumoPlantilla[])
            .catch(error => null);
    }

    public listarPaginacion(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public eliminar(bean: DtoTabla): Promise<any> {
        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as any)
            .catch(error => null);

    }

    public actualizar(bean: PsConsumoPlantilla): Promise<PsConsumoPlantilla> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsConsumoPlantilla)
            .catch(error => null);

    }

    public listarPlantillas(institucion: string): Promise<PsConsumoPlantilla[]> {
        const params = new HttpParams().set('institucion', institucion);         
        return this.http.get(this.rutaServicio + 'listarPlantillas', { params })
            .toPromise()
            .then(response => {
                return response as PsConsumoPlantilla[];
            })
            .catch(error => []);
    }

    public listarPlantillasConsumo(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPlantillasConsumo', filtro)
            .toPromise()
            .then(response => {
                return response as ParametroPaginacionGenerico;
            })
            .catch(error => null);
    }

    public solicitudAnular(institucion: string, plantilla: number): Promise<PsConsumoPlantilla> {
        const params = new HttpParams().set('pPlantilla', JSON.stringify(plantilla * 1))
        .set('pIdInstitucion', institucion);
        return this.http.get(this.rutaServicio + 'anular', { params })
            .toPromise()
            .then(response => response as PsConsumoPlantilla)
            .catch(error => null);
    }


}
