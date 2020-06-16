import { Injectable, Inject } from '@angular/core';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Ciudadano, CiudadanoPk } from '../dominio/ciudadano';
import { FiltroCiudadano } from '../dominio/filtroCiudadano';

@Injectable({
  providedIn: 'root'
})
export class CiudadanoService {

  private rutaServicio: string;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.rutaServicio = this.baseUrl + 'api/spring/covid/Ciudadano/';
  }


  public listarPaginacion(filtro: FiltroCiudadano): Promise<ParametroPaginacionGenerico> {
    return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
      .toPromise()
      .then(res => res as ParametroPaginacionGenerico)
      .catch(error => new ParametroPaginacionGenerico());
  }


  public registrar(bean: Ciudadano): Promise<Ciudadano> {
    return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
      .then(Response => Response as Ciudadano)
      .catch(error => null);
  }

  public obtenerPorId(id: number): Promise<Ciudadano> {
    const params = new HttpParams().set('pIdCiudadano', JSON.stringify(id * 1));
    return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
      .toPromise()
      .then(response => {
        return response as Ciudadano;
      })
      .catch(error => new Ciudadano());
  }

  public actualizar(bean: Ciudadano): Promise<any> {
    return this.http.post(this.rutaServicio + 'actualizar', bean)
      .toPromise()
      .then(response => response as Ciudadano)
      .catch(error => null);
  }


  public cambiarestado(pk: CiudadanoPk): Promise<CiudadanoPk> {
    return this.http.post(this.rutaServicio + 'Cambiarestado', pk)
      .toPromise()
      .then(response => response as CiudadanoPk)
      .catch(error => null);
  }


  public listado(filtro: DtoTabla): Promise<Ciudadano[]> {
    return this.http.post(this.rutaServicio + 'listado', filtro)
      .toPromise()
      .then(res => res as Ciudadano[])
      .catch(error => new Array());
  }


}
