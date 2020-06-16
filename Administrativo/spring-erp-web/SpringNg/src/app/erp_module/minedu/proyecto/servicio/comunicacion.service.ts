import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Proyecto } from '../dominio/proyecto';



@Injectable({
  providedIn: 'root'
})
export class ComunicacionService {



  proyectocomunica: Proyecto = new Proyecto();

  estadoEnabled: boolean = true;
  private enviarEstado = new Subject<boolean>();
  enviareEstadoObs = this.enviarEstado.asObservable();
  sendEstado(bol: boolean){
    this.estadoEnabled = bol;
    this.enviarEstado.next(bol);
  }


  parametro: number = 0;
  private enviarSubject = new Subject<number>();
  enviarObservable = this.enviarSubject.asObservable();
  enviarParametro(parametro: number) {
    this.parametro = parametro;
    this.enviarSubject.next(parametro);
  }

  numRecursos: number;
  advertencia: string;
  deshabilitarTab: boolean;
  private enviarNumero = new Subject<number>();
  enviarNumeroObs = this.enviarNumero.asObservable();
  enviarCantidadrecursos(numero: number) {
    this.numRecursos = numero;
    if (this.numRecursos < 2 || this.numRecursos == null) {
      this.deshabilitarTab = true;
      this.advertencia = 'Para acceder ingrese una cantidad de recursos (min 2)';
    } else {
      this.deshabilitarTab = false;
      this.advertencia = null;
    }
  }

  borrar: boolean;
  auxlen: number;
  private enviarSubjectaux = new Subject<number>();
  enviarauxObservable = this.enviarSubjectaux.asObservable();
  enviarAux(parametro: number) {
    this.auxlen = parametro + 1;
    this.enviarSubject.next(parametro);
  }

}
