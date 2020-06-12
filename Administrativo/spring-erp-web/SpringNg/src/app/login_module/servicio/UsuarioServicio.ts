import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { LoginUser } from '../dominio/LoginUser';
import { DtoTabla } from '../../erp_module/shared/dominio/dto/DtoTabla';
import { PsInstitucion } from 'src/app/erp_module/programasocial/institucion/dominio/PsInstitucion';

@Injectable()
export class UsuarioServicio {

    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    public login(usuario: LoginUser): Promise<any> {
        return this.http.post(this.baseUrl + 'api/Usuario/login', usuario)
            .toPromise()
            .then(response => (response as any).token as string)
            .catch(error => null);
    }


    public listarInstituciones(): Promise<PsInstitucion[]> {

        return this.http.get(this.baseUrl + 'api/Usuario/listarTodosActivas')
            .toPromise()
            .then(response => response as PsInstitucion[])
            .catch(error => null);
    }


    public tienePermiso(concepto: string): Promise<any> {

        const params = new HttpParams()
            .set('concepto', concepto);

        return this.http.get(this.baseUrl + 'api/Usuario/tienePermiso', { params })
            .toPromise()
            .then(response => response as String)
            .catch(error => null);
    }
    public obtenerMenu(): Promise<any> {

        return this.http.get(this.baseUrl + 'api/Usuario/obtenerMenu')
            .toPromise()
            .then(response => response as any)
            .catch(error => new Object());
    }

    cambiarClave(usuario: string, clave: string, clave1: string, clave2: string) {
        return this.http.get(this.baseUrl + 'api/Usuario/cambiarClave/' + usuario + '/' + clave + '/' + clave1 + '/' + clave2)
            .toPromise()
            .then(response => true)
            .catch(error => null);
    }

    public enviarRecuperarClave(correo: string, origen: string): Promise<any> {

        var dtoCorreo: DtoCorreo = new DtoCorreo();
        dtoCorreo.correo = correo;
        dtoCorreo.compania = origen;

        return this.http.post(this.baseUrl + 'api/Usuario/enviarRecuperarClave', dtoCorreo)
            .toPromise()
            .then(response => (response as any).mensaje as string)
            .catch(error => null);
    }

    public listarCompanias(usuario: string): Promise<DtoTabla[]> {
        var loginUser: LoginUser = new LoginUser();
        loginUser.usuario = usuario;

        return this.http.post(this.baseUrl + 'api/Usuario/listarCompanias', loginUser)
            .toPromise()
            .then(response => (response as DtoTabla[]))
            .catch(error => []);
    }

}

export class DtoCorreo {
    empleado: number;
    compania: string;
    correo: string;
}
