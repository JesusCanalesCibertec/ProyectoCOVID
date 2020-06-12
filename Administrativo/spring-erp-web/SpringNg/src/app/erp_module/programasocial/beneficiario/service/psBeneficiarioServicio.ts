import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ParametroPaginacionGenerico } from '../../../shared/dominio/ParametroPaginacionGenerico';
import { FiltroBeneficiario } from '../dominio/filtroBeneficiario';
import { PsBeneficiario, PsBeneficiarioPk } from '../dominio/psBeneficiario';
import { DtoBeneficiario } from '../dominio/dtoBeneficiario';
import { PsEntidad } from '../dominio/psentidad';
import { MaMiscelaneosdetalle } from 'src/app/erp_module/shared/miscelaneos/dominio/MaMiscelaneosdetalle';

@Injectable()
export class PsBeneficiarioServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/programasocial/PsBeneficiario/';
    }

    public obtenerPorId( institucion: string, codigo: number): Promise<PsBeneficiario> {

        const params = new HttpParams().set('pIdBeneficiario', JSON.stringify(codigo)) 
                                    .set('pIdInstitucion', institucion);

        return this.http.get(this.rutaServicio + 'obtenerporid', { params })
            .toPromise()
            .then(response => {
                return response as PsBeneficiario;
            })
            .catch(error => new PsBeneficiario());
    }


    public listarValoracionNutricional(idBeneficiario: number, idInstitucion: string): Promise<MaMiscelaneosdetalle[]> {
        const params = new HttpParams().set('IdInstitucion', idInstitucion)
            .set('IdBeneficiario', JSON.stringify(idBeneficiario));

        return this.http.get(this.rutaServicio + 'listarValoracionNutricional', { params })
            .toPromise()
            .then(response => {
                return response as MaMiscelaneosdetalle[];
            })
            .catch(error => []);
    }


    listarPaginacion(filtro: FiltroBeneficiario): Promise<ParametroPaginacionGenerico> {

        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public registrar(bean: PsBeneficiario): Promise<PsBeneficiario> {

        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as PsBeneficiario)
            .catch(error => null);
    }

    // para actualizar
    public solicitudActualizar(bean: PsBeneficiario): Promise<any> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as PsBeneficiario)
            .catch(error => null);
    }


    // para eliminar

    public solicitudAnular(bean: DtoBeneficiario): Promise<DtoBeneficiario> {

        return this.http.post(this.rutaServicio + 'eliminarfila', bean)
            .toPromise()
            .then(response => response as DtoBeneficiario)
            .catch(error => null);
    }


    public registrarCompleto(bean: PsEntidad): Promise<PsEntidad> {
        return this.http.post(this.rutaServicio + 'registrarCompleto', bean)
            .toPromise()
            .then(response => response as DtoBeneficiario)
            .catch(error => null);
    }
    public actualizarCompleto(bean: PsEntidad): Promise<PsEntidad> {
        return this.http.post(this.rutaServicio + 'actualizarCompleto', bean)
            .toPromise()
            .then(response => response as DtoBeneficiario)
            .catch(error => null);
    }

    public obtenerCompleto(bean: PsBeneficiarioPk): Promise<PsEntidad> {
        return this.http.post(this.rutaServicio + 'obtenerCompleto', bean)
            .toPromise()
            .then(response => response as PsEntidad)
            .catch(error => null);
    }

    public obtenerDatosBasicos(bean: PsEntidad): Promise<PsEntidad> {
        return this.http.post(this.rutaServicio + 'obtenerDatosBasicos', bean)
            .toPromise()
            .then(response => response as PsEntidad)
            .catch(error => null);
    }

    public exportar(): Promise<string> {
        return this.http.get(this.rutaServicio + 'exportar')
            .toPromise()
            .then(response => (response as any).url)
            .catch(error => null);
    }

    public anularBeneficiario(bean: DtoBeneficiario): Promise<DtoBeneficiario> {

        return this.http.post(this.rutaServicio + 'anularBeneficiario', bean)
            .toPromise()
            .then(response => response as DtoBeneficiario)
            .catch(error => null);
    }

    listarPaginacionBeneficiarioSelector(filtros: FiltroBeneficiario): Promise<ParametroPaginacionGenerico> {
        return this.http.post(this.rutaServicio + 'listarBeneficiarios', filtros)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

 

    
}
