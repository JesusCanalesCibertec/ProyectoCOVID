import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Usuario, UsuarioPk } from '../dominio/Usuario';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { DtoTabla } from '../../dominio/dto/DtoTabla';
import { FiltroUsuario } from '../dominio/FiltroUsuario';






@Injectable()
export class UsuarioServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/Usuario/';
    }

    listarPaginacion(filtro: FiltroUsuario): Promise<ParametroPaginacionGenerico> {

        return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
            .toPromise()
            .then(response => response as ParametroPaginacionGenerico)
            .catch(error => new ParametroPaginacionGenerico());
    }

    public obtenerPorId(bean: UsuarioPk): Promise<Usuario> {

        return this.http.post(this.rutaServicio + 'obtenerPorId', bean)
            .toPromise()
            .then(response => {
                return response as Usuario;
            })
            .catch(error => new Usuario());
    }
    public obtenerMenu(): Promise<any> {

        return this.http.get(this.baseUrl + 'api/Usuario/obtenerMenu')
            .toPromise()
            .then(response => response as any)
            .catch(error => new Object());
    }
}
