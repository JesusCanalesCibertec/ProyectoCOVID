import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DtoEmpleadoBasico } from '../../selectores/empleado/dominio/DtoEmpleadoBasico';
import { Personamast } from '../dominio/personamast';
import { DtoTabla } from '../../dominio/dto/DtoTabla';


@Injectable()
export class PersonamastService {

  private rutaServicio: string;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.rutaServicio = this.baseUrl + 'api/spring/core/Personamast/';
  }

  public esJefePorUnidadOperativa(personaId: number): Promise<DtoEmpleadoBasico> {
    const params = new HttpParams()
      .set('personaId', JSON.stringify(personaId));

    return this.http.get(this.rutaServicio + 'esJefePorUnidadOperativa', { params })
      .toPromise()
      .then(response => {
        return response as DtoEmpleadoBasico;
      });
  }

  public obtenerPorId(personaId: number): Promise<Personamast> {
    const params = new HttpParams()
      .set('personaId', JSON.stringify(personaId));

    return this.http.get(this.rutaServicio + 'obtenerPorId', { params })
      .toPromise()
      .then(response => {
        return response as Personamast;
      })
      .catch(error => new Personamast());
  }

  public obtenerNombrePorJefeUnidadOperativa(unidadoperativa: string): Promise<DtoTabla> {
    const params = new HttpParams()
      .set('unidadoperativa', unidadoperativa);

    return this.http.get(this.rutaServicio + 'obtenerNombrePorJefeUnidadOperativa', { params })
      .toPromise()
      .then(response => {
        return response as DtoTabla;
      });
  }

}
