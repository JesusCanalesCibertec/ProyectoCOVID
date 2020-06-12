import { MaMiscelaneosdetalle } from './../../../shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { FiltroInstitucion } from '../dominio/filtroinstituto';
import { DtoInstitucion } from '../dominio/dtoinstitucion';
import { PsInstitucion } from '../dominio/PsInstitucion';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';


@Injectable()
export class PsInstitucionServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsInstitucion/';
    }

    public obtenerPorId(codigo: string): Promise<PsInstitucion> {
        const params = new HttpParams().set('codigo', codigo);
        return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
            .toPromise()
            .then(response => {
                return response as PsInstitucion;
            })
            .catch(error => new PsInstitucion());
    }

    public listarValoracionNutricional(idInstitucion: string): Promise<MaMiscelaneosdetalle[]> {
        const params = new HttpParams().set('idInstitucion', idInstitucion);

        return this.http.get(this.rutaServicio + 'listarValoracionNutricional', { params })
            .toPromise()
            .then(response => {
                return response as MaMiscelaneosdetalle[];
            })
            .catch(error => []);
    }


    public listarPeriodo(
        idInstitucion: string,
        idPrograma: string,
        idComponente: string,
        idUsuario: string,
        idBeneficiario: number): Promise<DtoTabla[]> {

        if (idComponente === undefined) {
            idComponente = '';
        }

        if (idUsuario === undefined) {
            idUsuario = '';
        }

        if (idPrograma === undefined) {
            idPrograma = '';
        }

        const params = new HttpParams()
            .set('Institucion', idInstitucion)
            .set('IdPrograma', idPrograma)
            .set('IdComponente', idComponente)
            .set('IdUsuario', idUsuario)
            .set('IdBeneficiario', JSON.stringify(idBeneficiario * 1));

        return this.http.get(this.rutaServicio + 'listarPeriodos', { params })
            .toPromise()
            .then(response => {
                return response as DtoTabla[];
            })
            .catch(error => null);
    }


    listarPaginacion(filtro: FiltroInstitucion): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }
    public registrar(bean: PsInstitucion): Promise<PsInstitucion> {
        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as PsInstitucion)
            .catch(error => null);
    }
    // para actualizar
    public solicitudActualizar(bean: PsInstitucion): Promise<any> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsInstitucion)
            .catch(error => null);
    }
    // para eliminar
    public solicitudEliminar(bean: DtoInstitucion): Promise<DtoInstitucion> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as DtoInstitucion)
            .catch(error => null);
    }
    public solicitudAnular(codigo: string): Promise<PsInstitucion> {
        const params = new HttpParams().set('pIdInstitucion', codigo);
        return this.http.get(this.rutaServicio + 'anular', { params })
            .toPromise()
            .then(response => response as PsInstitucion)
            .catch(error => null);
    }


    public listarInstitucionesDos(tipo: string, progama: string): Promise<PsInstitucion[]> {


        if (/*tipo == 'SATIS'&&(*/  progama != null && progama !== undefined) {
            return this.listarPorPrograma(progama);
        } else {
            return this.listarInstituciones();
        }
    }

    public listarInstituciones(): Promise<PsInstitucion[]> {

        return this.http.get(this.rutaServicio + 'listarTodosActivas')
            .toPromise()
            .then(response => response as PsInstitucion[])
            .catch(error => null);
    }

    public flgSeleccionarInstitucion(): Promise<DtoTabla> {

        return this.http.get(this.rutaServicio + 'flgSeleccionarInstitucion')
            .toPromise()
            .then(response => response as DtoTabla)
            .catch(error => new DtoTabla);
    }
    public listarPorPrograma(programa): Promise<PsInstitucion[]> {
        const params = new HttpParams().set('pIdPrograma', programa);
        return this.http.get(this.rutaServicio + 'listarPorPrograma', { params })
            .toPromise()
            .then(response => response as PsInstitucion[])
            .catch(error => []);
    }
}
