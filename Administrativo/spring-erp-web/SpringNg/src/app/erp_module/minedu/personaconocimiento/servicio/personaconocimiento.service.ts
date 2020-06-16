import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Personaconocimiento, PersonaconocimientoPk } from '../dominio/personaconocimiento';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';



@Injectable()
export class PersonaconocimientoServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/minedu/MpPersonaconocimiento/';
    }

    public listarTodo(): Promise<Personaconocimiento[]> {
        return this.http.get(this.rutaServicio + 'listarTodo')
            .toPromise()
            .then(response => {
                return response as Personaconocimiento[];
            })
            .catch(error => []);
    }

    public listado(codigo: number): Promise<DtoTabla[]> {
        const params = new HttpParams().set('pIdPersona', JSON.stringify(codigo * 1) );
        return this.http.get(this.rutaServicio + 'listado', { params })
            .toPromise()
            .then(response => {
                return response as DtoTabla[];
            })
            .catch(error => []);
    }

    public obtenerPorId(bean: PersonaconocimientoPk): Promise<Personaconocimiento> {

        return this.http.post(this.rutaServicio + 'obtenerPorId', bean)
            .toPromise()
            .then(response => {
                return response as Personaconocimiento;
            })
            .catch(error => new Personaconocimiento());
    }

    public eliminar(bean: PersonaconocimientoPk): Promise<Personaconocimiento> {

        return this.http.post(this.rutaServicio + 'eliminar', bean)
            .toPromise()
            .then(response => {
                return response as Personaconocimiento;
            })
            .catch(error => new Personaconocimiento());
    }

    public registrar(bean: Personaconocimiento): Promise<Personaconocimiento> {

        return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
            .then(Response => Response as Personaconocimiento)
            .catch(error => null);
    }

    public solicitudActualizar(bean: Personaconocimiento): Promise<Personaconocimiento> {
        return this.http.post(this.rutaServicio + 'actualizar', bean)
            .toPromise()
            .then(response => response as Personaconocimiento)
            .catch(error => null);
    }





}
