import { Injectable, Inject } from '@angular/core';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Sistema } from '../../Sistema/dominio/Sistema';


@Injectable({
  providedIn: 'root'
})
export class SistemaService {

  private rutaServicio: string;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.rutaServicio = this.baseUrl + 'api/spring/minedu/MpSistema/';
  }

  public ListarSistemas(): Promise<Sistema[]> {
    return this.http.get(this.rutaServicio + 'ListarSistemas')
      .toPromise()
      .then(response => {
        return response as Sistema[];
      })
      .catch(error => []);
  }


}
