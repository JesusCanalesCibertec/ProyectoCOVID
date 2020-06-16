import { Injectable, Inject } from '@angular/core';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Proyecto, ProyectoPk } from '../dominio/proyecto';
import { dtoProyecto } from '../dominio/dtoProyecto';
import { Proyectorecurso } from '../dominio/proyectorecurso';


@Injectable({
  providedIn: 'root'
})
export class ProyectoService {

  private rutaServicio: string;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.rutaServicio = this.baseUrl + 'api/spring/minedu/MpProyecto/';
  }


  public listarPaginacion(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
    return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
      .toPromise()
      .then(res => res as ParametroPaginacionGenerico)
      .catch(error => new ParametroPaginacionGenerico());
  }


  public registrar(bean: Proyecto): Promise<Proyecto> {
    return this.http.post(this.rutaServicio + 'Registrar', bean).toPromise()
      .then(Response => Response as Proyecto)
      .catch(error => null);
  }

  public obtenerPorId(id: number): Promise<Proyecto> {
    const params = new HttpParams().set('pIdProyecto', JSON.stringify(id * 1));
    return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
      .toPromise()
      .then(response => {
        return response as Proyecto;
      })
      .catch(error => new Proyecto());
  }

  public actualizar(bean: Proyecto): Promise<any> {
    return this.http.post(this.rutaServicio + 'actualizar', bean)
      .toPromise()
      .then(response => response as Proyecto)
      .catch(error => null);
  }


  public cambiarestado(pk: ProyectoPk): Promise<ProyectoPk> {
    return this.http.post(this.rutaServicio + 'Cambiarestado', pk)
      .toPromise()
      .then(response => response as ProyectoPk)
      .catch(error => null);
  }

  public listarNombres(): Promise<Proyecto[]> {
    return this.http.get(this.rutaServicio + 'ListarNombres')
      .toPromise()
      .then(response => {
        return response as Proyecto[];
      })
      .catch(error => []);
  }

  public listardetalle(tipo: string, anio: number, estado:string): Promise<dtoProyecto[]> {
    const params = new HttpParams().set('pTipo', tipo)
                                   .set('pAnio', JSON.stringify(anio))
                                   .set('pEstado', estado);
    return this.http.get(this.rutaServicio + 'Listardetalle', { params }) 
      .toPromise()
      .then(response => {
        return response as dtoProyecto[];
      })
      .catch(error => []);
  }

  public ListarBarraEstados(): Promise<DtoTabla[]> {
    return this.http.get(this.rutaServicio + 'ListarBarraEstados') 
      .toPromise()
      .then(response => {
        return response as DtoTabla[];
      })
      .catch(error => []);
  }

  public ListarBarraTipos(): Promise<DtoTabla[]> {
    return this.http.get(this.rutaServicio + 'ListarBarraTipos') 
      .toPromise()
      .then(response => {
        return response as DtoTabla[];
      })
      .catch(error => []);
  }

  public ListarEquipotecnico(idProyecto: number): Promise<Proyectorecurso[]> {
    const params = new HttpParams().set('pIdProyecto', JSON.stringify(idProyecto));
    return this.http.get(this.rutaServicio + 'ListarEquipotecnico', { params }) 
      .toPromise()
      .then(response => {
        return response as Proyectorecurso[];
      })
      .catch(error => []);
  }

}
