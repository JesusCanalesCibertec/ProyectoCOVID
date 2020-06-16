import { Injectable, Inject } from '@angular/core';
import { DepartmentMst } from '../dominio/departmentmst';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DepartmentMstService {
    private rutaServicio: string;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.rutaServicio = this.baseUrl + 'api/spring/core/Departmentmst/';
    }

    listarActivos(): Promise<DepartmentMst[]> {
        return this.http.get(this.rutaServicio + 'listarTodos')
            .toPromise()
            .then(response => response as DepartmentMst[]);
    }
}
