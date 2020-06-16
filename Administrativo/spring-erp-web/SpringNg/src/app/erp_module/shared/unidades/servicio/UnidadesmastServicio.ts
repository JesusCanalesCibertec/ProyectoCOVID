import { Inject, Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Unidadesmast } from '../dominio/Unidadesmast';








@Injectable()
export class UnidadesmastServicio {

    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Unidadesmast/';
    }

   
    public listarTodos(): Promise<Unidadesmast[]> {

        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as Unidadesmast[])
            .catch(error => null);
    }

}
