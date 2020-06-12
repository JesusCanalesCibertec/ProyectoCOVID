import { Injectable, Inject } from '@angular/core';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Contratacion, ContratacionPk } from '../dominio/contratacion';
import { dtoContratacion } from '../dominio/dtoContratacion';

@Injectable({
  providedIn: 'root'
})
export class ContratacionService {

  private rutaServicio: string;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.rutaServicio = this.baseUrl + 'api/spring/minedu/MpContratacion/';
  }


  public listarPaginacion(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
    return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
      .toPromise()
      .then(res => res as ParametroPaginacionGenerico)
      .catch(error => new ParametroPaginacionGenerico());
  }


  public registrar(bean: Contratacion): Promise<Contratacion> {
    return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
      .then(Response => Response as Contratacion)
      .catch(error => null);
  }

  public obtenerPorId(id: number): Promise<Contratacion> {
    const params = new HttpParams().set('pIdContratacion', JSON.stringify(id * 1));
    return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
      .toPromise()
      .then(response => {
        return response as Contratacion;
      })
      .catch(error => new Contratacion());
  }

  public actualizar(bean: Contratacion): Promise<any> {
    return this.http.post(this.rutaServicio + 'actualizar', bean)
      .toPromise()
      .then(response => response as Contratacion)
      .catch(error => null);
  }


  public cambiarestado(pk: ContratacionPk): Promise<ContratacionPk> {
    return this.http.post(this.rutaServicio + 'Cambiarestado', pk)
      .toPromise()
      .then(response => response as ContratacionPk)
      .catch(error => null);
  }



  public registrarlista(bean: Contratacion): Promise<Contratacion> {

    return this.http.post(this.rutaServicio + 'Registrarlista', bean)
      .toPromise()
      .then(response => response as Contratacion)
      .catch(error => null);
  }

  public Contratosactivos(): Promise<dtoContratacion[]> {
    return this.http.get(this.rutaServicio + 'Contratosactivos')
      .toPromise()
      .then(response => {
        return response as dtoContratacion[];
      })
      .catch(error => []);
  }

  public ListarPie(): Promise<DtoTabla[]> {
    return this.http.get(this.rutaServicio + 'listarpie')
      .toPromise()
      .then(response => {
        return response as DtoTabla[];
      })
      .catch(error => []);
  }

  public ListarPersonaldisponible(filtro: DtoTabla): Promise<dtoContratacion[]> {
    return this.http.post(this.rutaServicio + 'ListarPersonaldisponible', filtro)
      .toPromise()
      .then(response => (response as dtoContratacion[]))
      .catch(error => []);
  }

  public ListarContratoPorPersona(id: number): Promise<dtoContratacion[]> {
    const params = new HttpParams().set('pIdPersona', JSON.stringify(id * 1));
    return this.http.get(this.rutaServicio + 'ListarContratoPorPersona', { params })
      .toPromise()
      .then(response => {
        return response as dtoContratacion[];
      })
      .catch(error => []);
  }


  public ListarPersonaldisponible1(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
    return this.http.post(this.rutaServicio + 'ListarPersonaldisponible1', filtro)
      .toPromise()
      .then(res => res as ParametroPaginacionGenerico)
      .catch(error => new ParametroPaginacionGenerico());
  }



}
