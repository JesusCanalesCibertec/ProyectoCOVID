import { Injectable, Inject } from '@angular/core';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Persona, PersonaPk } from '../dominio/persona';
import { Personareniec } from '../dominio/personareniec';
import { Contratacion } from '../../contratacion/dominio/contratacion';
import { DtoListaFechas } from '../dominio/dtoListaFechas';
import { dtoEventos } from '../dominio/dtoEventos';
import { Seguridadperfilusuario } from '../../usuario/dominio/seguridadperfilusuario';


@Injectable({
  providedIn: 'root'
})
export class PersonaService {

  private rutaServicio: string;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.rutaServicio = this.baseUrl + 'api/spring/minedu/MpPersona/';
  }


  public listarPaginacion(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
    return this.http.post(this.rutaServicio + 'listarPaginacion', filtro)
      .toPromise()
      .then(res => res as ParametroPaginacionGenerico)
      .catch(error => new ParametroPaginacionGenerico());
  }

  public listarPaginacionUsuario(filtro: DtoTabla): Promise<ParametroPaginacionGenerico> {
    return this.http.post(this.rutaServicio + 'listarPaginacionUsuario', filtro)
      .toPromise()
      .then(res => res as ParametroPaginacionGenerico)
      .catch(error => new ParametroPaginacionGenerico());
  }


  public registrar(bean: Persona): Promise<Persona> {
    return this.http.post(this.rutaServicio + 'registrar', bean).toPromise()
      .then(Response => Response as Persona)
      .catch(error => null);
  }

  public registrarUsuario(bean: Seguridadperfilusuario): Promise<Seguridadperfilusuario> {
    return this.http.post(this.rutaServicio + 'registrarUsuario', bean).toPromise()
      .then(Response => Response as Seguridadperfilusuario)
      .catch(error => null);
  }

  public eliminarUsuario(bean: Seguridadperfilusuario): Promise<Seguridadperfilusuario> {
    return this.http.post(this.rutaServicio + 'eliminarUsuario', bean).toPromise()
      .then(Response => Response as Seguridadperfilusuario)
      .catch(error => null);
  }

  public actualizarUsuario(bean: Seguridadperfilusuario): Promise<Seguridadperfilusuario> {
    return this.http.post(this.rutaServicio + 'actualizarUsuario', bean).toPromise()
      .then(Response => Response as Seguridadperfilusuario)
      .catch(error => null);
  }


  public obtenerPorId(idPersona: number, idContratacion?: number): Promise<Persona> {
    const params = new HttpParams().set('pIdPersona', JSON.stringify(idPersona * 1)).set('pIdContratacion', JSON.stringify(idContratacion * 1));
    return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
      .toPromise()
      .then(response => {
        return response as Persona;
      })
      .catch(error => new Persona());
  }

  public actualizar(bean: Persona): Promise<any> {
    return this.http.post(this.rutaServicio + 'actualizar', bean)
      .toPromise()
      .then(response => response as Persona)
      .catch(error => null);
  }

  public cambiarestado(dto: Contratacion): Promise<Contratacion> {
    return this.http.post(this.rutaServicio + 'Cambiarestado', dto)
      .toPromise()
      .then(response => response as Contratacion)
      .catch(error => null);
  }

  public listarNombres(): Promise<Persona[]> {
    return this.http.get(this.rutaServicio + 'ListarNombres')
      .toPromise()
      .then(response => {
        return response as Persona[];
      })
      .catch(error => []);
  }

  public ObtenerDatos(dni: string): Promise<Personareniec> {
    const params = new HttpParams().set('pDni', dni).set('pToken', Personareniec.TOKENDNI);
    return this.http.get(this.rutaServicio + 'ObtenerDatos', { params })
      .toPromise()
      .then(response => { return response as Personareniec })
      .catch(error => null);
  }

  public ListarPersonal(parametro: Date): Promise<DtoListaFechas[]> {
    const params = new HttpParams().set('parametro', parametro.toDateString())
    return this.http.get(this.rutaServicio + 'ListarPersonal', { params })
      .toPromise()
      .then(response => {
        return response as DtoListaFechas[];
      })
      .catch(error => []);
  }

  public ListarEventos(pIdPersona: number): Promise<dtoEventos[]> {
    const params = new HttpParams().set('pIdPersona', JSON.stringify(pIdPersona))
    return this.http.get(this.rutaServicio + 'ListarEventos', { params })
      .toPromise()
      .then(response => {
        return response as dtoEventos[];
      })
      .catch(error => []);
  }

  public ValidarDirectorio(pUsuario: string): Promise<DtoTabla> {
    const params = new HttpParams()
      .set('pUsuario', pUsuario);
    return this.http.get(this.rutaServicio + 'ValidarDirectorio', { params })
      .toPromise()
      .then(response => { return response as DtoTabla })
      .catch(error => null);
  }

}
