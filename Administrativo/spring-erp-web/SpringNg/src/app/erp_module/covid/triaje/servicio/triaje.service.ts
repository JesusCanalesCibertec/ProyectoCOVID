import { Injectable, Inject } from '@angular/core';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Triaje, TriajePk } from '../dominio/triaje';

@Injectable({
  providedIn: 'root'
})
export class TriajeService {

  private rutaServicio: string;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.rutaServicio = this.baseUrl + 'api/spring/covid/Triaje/';
  }


  // public listarPaginacion(filtro: FiltroTriaje): Promise<ParametroPaginacionGenerico> {
  //   return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
  //     .toPromise()
  //     .then(res => res as ParametroPaginacionGenerico)
  //     .catch(error => new ParametroPaginacionGenerico());
  // }


  public registrar(bean: Triaje): Promise<Triaje> {
    return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
      .then(Response => Response as Triaje)
      .catch(error => null);
  }

  public obtenerPorId(id: number): Promise<Triaje> {
    const params = new HttpParams().set('pIdTriaje', JSON.stringify(id * 1));
    return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
      .toPromise()
      .then(response => {
        return response as Triaje;
      })
      .catch(error => new Triaje());
  }

  public actualizar(bean: Triaje): Promise<any> {
    return this.http.post(this.rutaServicio + 'actualizar', bean)
      .toPromise()
      .then(response => response as Triaje)
      .catch(error => null);
  }


  public cambiarestado(pk: TriajePk): Promise<TriajePk> {
    return this.http.post(this.rutaServicio + 'Cambiarestado', pk)
      .toPromise()
      .then(response => response as TriajePk)
      .catch(error => null);
  }


  public listado(filtro: DtoTabla): Promise<Triaje[]> {
    return this.http.post(this.rutaServicio + 'listado', filtro)
      .toPromise()
      .then(res => res as Triaje[])
      .catch(error => new Array());
  }

  
  public listarporciudadano(idciudadano: number): Promise<DtoTabla[]> {    
    const params = new HttpParams().set('pIdCiudadano', JSON.stringify(idciudadano * 1));
    return this.http.get(this.rutaServicio + 'listado', { params })
        .toPromise()
        .then(response => {
            return response as DtoTabla[];
        })
        .catch(error => new Array());
}


}
