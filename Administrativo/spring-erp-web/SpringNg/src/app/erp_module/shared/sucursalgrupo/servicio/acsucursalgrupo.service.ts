import { Injectable, Inject } from '@angular/core';
import { AcSucursalgrupo } from '../dominio/acsucursalgrupo';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AcSucursalGrupoService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/contabilidad/AcCostcentermst/';
    }

    listarActivoOrdenadoPorNombre(): Promise<AcSucursalgrupo[]> {
        return this.http.get(this.rutaServicio + 'listaractivoordenadopornombre')
            .toPromise()
            .then(response => response as AcSucursalgrupo[]);
    }

}
