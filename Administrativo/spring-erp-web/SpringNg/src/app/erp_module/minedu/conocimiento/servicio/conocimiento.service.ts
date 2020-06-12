import { Injectable, Inject } from '@angular/core';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Conocimiento, ConocimientoPk } from '../dominio/conocimiento';

@Injectable({
  providedIn: 'root'
})
export class ConocimientoService {

  private rutaServicio: string;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.rutaServicio = this.baseUrl + 'api/spring/minedu/MpConocimiento/';
  }


  public listarPaginacion(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
    return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
      .toPromise()
      .then(res => res as ParametroPaginacionGenerico)
      .catch(error => new ParametroPaginacionGenerico());
  }


  public registrar(bean: Conocimiento): Promise<Conocimiento> {
    return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
      .then(Response => Response as Conocimiento)
      .catch(error => null);
  }

  public obtenerPorId(id: number): Promise<Conocimiento> {
    const params = new HttpParams().set('pIdConocimiento', JSON.stringify(id * 1));
    return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
      .toPromise()
      .then(response => {
        return response as Conocimiento;
      })
      .catch(error => new Conocimiento());
  }

  public actualizar(bean: Conocimiento): Promise<any> {
    return this.http.post(this.rutaServicio + 'actualizar', bean)
      .toPromise()
      .then(response => response as Conocimiento)
      .catch(error => null);
  }


  public cambiarestado(pk: ConocimientoPk): Promise<ConocimientoPk> {
    return this.http.post(this.rutaServicio + 'Cambiarestado', pk)
      .toPromise()
      .then(response => response as ConocimientoPk)
      .catch(error => null);
  }


  public listado(filtro: DtoTabla): Promise<Conocimiento[]> {
    return this.http.post(this.rutaServicio + 'listado', filtro)
      .toPromise()
      .then(res => res as Conocimiento[])
      .catch(error => new Array());
  }


}
