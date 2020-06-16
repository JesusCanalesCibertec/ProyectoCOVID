import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Personatitulo, PersonatituloPk } from '../dominio/personatitulo';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { dtoPersonatitulo } from '../dominio/dtoPersonatitulo';



@Injectable()
export class PersonatituloServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/minedu/MpPersonatitulo/';
    }

    public listarTodo(): Promise<Personatitulo[]> {
        return this.http.get(this.rutaServicio + 'listarTodo')
            .toPromise()
            .then(response => {
                return response as Personatitulo[];
            })
            .catch(error => []);
    }


    public listado(codigo: number): Promise<dtoPersonatitulo[]> {
        const params = new HttpParams().set('pIdPersona', JSON.stringify(codigo * 1) );
        return this.http.get(this.rutaServicio + 'listado', { params })
            .toPromise()
            .then(response => {
                return response as dtoPersonatitulo[];
            })
            .catch(error => []);
    }



    public obtenerPorId(bean: PersonatituloPk): Promise<Personatitulo> {

        return this.http.post(this.rutaServicio + 'obtenerPorId', bean)
            .toPromise()
            .then(response => {
                return response as Personatitulo;
            })
            .catch(error => new Personatitulo());
    }

    public eliminar(bean: Personatitulo): Promise<Personatitulo> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => {
                return response as Personatitulo;
            })
            .catch(error => new Personatitulo());
    }

    public registrar(bean: Personatitulo): Promise<Personatitulo> {

        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as Personatitulo)
            .catch(error => null);
    }

    public solicitudActualizar(bean: Personatitulo): Promise<Personatitulo> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as Personatitulo)
            .catch(error => null);
    }

    public solicitudEliminar(bean: Personatitulo): Promise<Personatitulo> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => response as Personatitulo)
            .catch(error => null);
    }



}
