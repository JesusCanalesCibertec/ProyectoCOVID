import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Contratacionadendaentregable, ContratacionadendaentregablePk } from '../dominio/contratacionadendaentregable';



@Injectable()
export class ContratacionadendaentregableServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/minedu/MpContratacionadendaentregable/';
    }

    public listarTodo(): Promise<Contratacionadendaentregable[]> {
        return this.http.get(this.rutaServicio + 'listarTodo')
            .toPromise()
            .then(response => {
                return response as Contratacionadendaentregable[];
            })
            .catch(error => []);
    }

    public listado(codigo: number): Promise<Contratacionadendaentregable[]> {
        const params = new HttpParams().set('pIdContratacion', JSON.stringify(codigo * 1) );
        return this.http.get(this.rutaServicio + 'listado', { params })
            .toPromise()
            .then(response => {
                return response as Contratacionadendaentregable[];
            })
            .catch(error => []);
    }

    public obtenerPorId(bean: ContratacionadendaentregablePk): Promise<Contratacionadendaentregable> {

        return this.http.post(this.rutaServicio + 'obtenerPorId', bean)
            .toPromise()
            .then(response => {
                return response as Contratacionadendaentregable;
            })
            .catch(error => new Contratacionadendaentregable());
    }

    public eliminar(bean: ContratacionadendaentregablePk): Promise<Contratacionadendaentregable> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => {
                return response as Contratacionadendaentregable;
            })
            .catch(error => new Contratacionadendaentregable());
    }

    public registrar(bean: Contratacionadendaentregable): Promise<Contratacionadendaentregable> {

        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as Contratacionadendaentregable)
            .catch(error => null);
    }

   

    public solicitudActualizar(bean: Contratacionadendaentregable): Promise<Contratacionadendaentregable> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as Contratacionadendaentregable)
            .catch(error => null);
    }





}
