
import { ActivatedRoute, Router } from '@angular/router';
import { OnInit } from '@angular/core';
import { Message, SelectItem } from 'primeng/primeng';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscriber } from 'rxjs';
import { MessageService } from 'primeng/components/common/messageservice';
import { MensajeUsuario, tipo_mensaje } from '../../erp_module/shared/dominio/dto/MensajeUsuario';
import { getISODayOfWeek } from 'ngx-bootstrap/chronos/units/day-of-week';

export const enum accionSolicitada {
    NADA, NUEVO, EDITAR, VER, COPYINTERMEDIO, TRANSACCION, GUARDAR, ACTUALIZAR
}
export const COMPANIA_GENERICA: string = '999999';
export const CODIGO_APP_LOGISTICA: string = 'WH';

export function DestroySubscribers() {
    return function (target: any) {
        // decorating the function ngOnDestroy
        target.prototype.ngOnDestroy = ngOnDestroyDecorator(target.prototype.ngOnDestroy);
        // decorator function
        function ngOnDestroyDecorator(f) {
            return function () {
                // saving the result of ngOnDestroy performance to the variable superData
                const superData = f ? f.apply(this, arguments) : null;
                // unsubscribing
                for (const subscriberKey in this.subscribers) {
                    const subscriber = this.subscribers[subscriberKey];
                    if (subscriber instanceof Subscriber) {
                        subscriber.unsubscribe();
                    }
                }
                // returning the result of ngOnDestroy performance
                return superData;
            };
        }
        // returning the decorated class
        return target;
    };
}

@DestroySubscribers()
export class BaseComponent {
    accion: accionSolicitada;
    blocked: boolean = false;
    es: any;
    fr: any;
    languaje: string;
    proxy: string;

    constructor(public messageService: MessageService) {
        this.init();
    }

    init() {
        this.es = {
            firstDayOfWeek: 1,
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
            dayNamesMin: ['D', 'L', 'M', 'X', 'J', 'V', 'S'],
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
        };
        this.fr = {
            firstDayOfWeek: 1,
            dayNames: ['dimanche', 'lundi', 'mardi', 'mercredi', 'jeudi', 'vendredi', 'samedi'],
            dayNamesShort: ['dim', 'lun', 'mar', 'mer', 'jeu', 'ven', 'sam'],
            dayNamesMin: ['D', 'L', 'M', 'X', 'J', 'V', 'S'],
            monthNames: ['Janvier', 'Février', 'Mars', 'Avril', 'mai', 'juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'],
            monthNamesShort: ['jan', 'fev', 'mar', 'avr', 'mai', 'jui', 'jul', 'aou', 'sep', 'oct', 'nov', 'dec']
        };
        window.onbeforeunload = null;
        this.languaje = localStorage.getItem('lang');
    }

    confirmarRecargarPagina() {
        window.onbeforeunload = this.confirmOnPageExit;
    }

    setAuditoriaNuevo(): void { }
    setAuditoriaModificar(): void { }
    getAuditoriaNuevo(): void { }
    getAuditoriaModificar(): void { }

    mostrarMensaje(mensaje: string, tipo: string): void {
        this.messageService.clear();
        this.messageService.add({ severity: tipo, summary: 'Mensaje', detail: mensaje });
    }
    mostrarMensajeError(mensaje: string): void {
        this.mostrarMensaje(mensaje, 'error');
    }

    mostrarMensajeExito(mensaje: string): void {
        this.mostrarMensaje(mensaje, 'success');
    }

    transform(value: any): number {
        if (value == null) {
            return 0;
        }

        var v: Date = new Date(value);

        var now = new Date();
        var anios = 0;

        if (v.getTime() > now.getTime()) {
            return 0;
        }

        while (this.addDate('y', 1, v).getTime() < now.getTime()) {
            v = this.addDate('y', 1, v);
            anios++;

        };

        return anios;
    }

    //creadas por EACH1: inicio
    diferenciadias(desde: any, hasta: any): number {
        if (desde == null || hasta == null) {
            return 0;
        }
        var v1: Date = new Date(desde);
        var v2: Date = new Date(hasta);
        var dias = 0;
        if (v1.getTime() > v2.getTime()) {
            return 0;
        }
        while (this.addDate('d', 1, v1).getTime() < v2.getTime()) {
            v1 = this.addDate('d', 1, v1);
            dias++;
        };
        return dias;
    }

    remove(lista: any, item: any) {
        return lista == null ? [] : lista.filter(e => e !== item);
    }

    filtrar_tildes(cadena: string) {
        var acentos = "ÃÀÁÄÂÈÉËÊÌÍÏÎÒÓÖÔÙÚÜÛãàáäâèéëêìíïîòóöôùúüûÇç";
        var original = "AAAAAEEEEIIIIOOOOUUUUaaaaaeeeeiiiioooouuuucc";
        for (var i = 0; i < acentos.length; i++) {
            cadena = cadena.replace(new RegExp( acentos.charAt(i), 'g' ) , original.charAt(i)).toUpperCase();
        };
        return cadena;
    }

    //creadas por EACH1: fin


    addDate(pattern: string, mount: number, fecha: Date): Date {

        var f2 = new Date(fecha);

        switch (pattern) {
            case 'y': {
                f2.setFullYear(f2.getFullYear() + mount);
                break;
            }
            case 'm': {
                f2.setMonth(f2.getMonth() + mount);
                break;
            }
            case 'd': {
                f2.setDate(f2.getDate() + mount);
                break;
            }
            default:
                break;
        }

        return f2;
    }

    mostrarMensajeInfo(mensaje: string): void {
        this.mostrarMensaje(mensaje, 'info');
    }

    mostrarMensajeAdvertencia(mensaje: string): void {
        this.mostrarMensaje(mensaje, 'warn');
    }

    bloquearPagina(): void {
        if (!this.blocked) {
            this.blocked = true;
        }
    }

    desbloquearPagina(): void {
        if (this.blocked) { this.blocked = false; }
    }



    obtenerParametroRutaPorClave(rutaActiva: ActivatedRoute, clave: string) {
        return rutaActiva.snapshot.params[clave];
    }

    flagABoolean(flag: string): boolean {
        if (flag === 'S') {
            return true;
        } else { return false; }
    }

    flagAInteger(flag: string): boolean {
        if (flag === '1') {
            return true;
        } else { return false; }
    }

    booleanAFlag(bool: boolean): string {
        if (bool) {
            return 'S';
        } else { return 'N'; }
    }

    integerAFlag(bool: boolean): string {
        if (bool) {
            return '1';
        } else { return '0'; }
    }

    flagAEstado(flag: string): Boolean {
        if (this.estaVacio(flag)) {
            return true;
        } else { return false; }
    }

    estadoAFlag(bool: Boolean): string {
        if (bool) {
            return 'A';
        } else { return 'I'; }
    }


    padDigits(numero: number, cantidadCeros: number): string {
        return Array(Math.max(cantidadCeros - String(numero).length + 1, 0)).join('0') + numero;
    }

    getAccionNombre() {
        if (this.accion) {
            switch (this.accion) {
                case accionSolicitada.EDITAR: return 'Edición';
                case accionSolicitada.VER: return 'Vista Previa';
                case accionSolicitada.NUEVO: return 'Nuevo';
                case accionSolicitada.COPYINTERMEDIO: return 'Copy';
            }
        }
        return '';
    }

    confirmOnPageExit = function (e: Event) {
        e = e || window.event;
        const message = '¿Deseas volver a cargar este sitio?';
        if (e) { e.returnValue = message ? true : false; }
        return message;
    };

    copiar<T>(instance: T): T {
        const copy = new (instance.constructor as { new(): T })();
        Object.assign(copy, instance);
        return copy;
    }

    toDate(date: Date): Date {
        if (date == null) {
            return null;
        }
        return new Date(date);
    }

    get ACCIONES() {
        return {
            NUEVO: accionSolicitada.NUEVO,
            EDITAR: accionSolicitada.EDITAR,
            VER: accionSolicitada.VER,
            GUARDAR: accionSolicitada.GUARDAR,
            ACTUALIZAR: accionSolicitada.ACTUALIZAR
        };
    }

    clone(obj): any {
        let copy;

        // Handle the 3 simple types, and null or undefined
        if (null == obj || 'object' !== typeof obj) { return obj; }

        // Handle Date
        if (obj instanceof Date) {
            copy = new Date();
            copy.setTime(obj.getTime());
            return copy;
        }

        // Handle Array
        if (obj instanceof Array) {
            copy = [];
            for (let i = 0, len = obj.length; i < len; i++) {
                copy[i] = this.clone(obj[i]);
            }
            return copy;
        }

        // Handle Object
        if (obj instanceof Object) {
            copy = {};
            for (const attr in obj) {
                if (obj.hasOwnProperty(attr)) { copy[attr] = this.clone(obj[attr]); }
            }
            return copy;
        }

        throw new Error('Unable to copy obj! Its type isn t supported.');
    }

    obtenerLabelPorCombo(lista: SelectItem[], val: any): string {

        if (val == null) {
            return '';
        }
        if (Number(val) === NaN) {
            if (val.trim() === '') {
                return '';
            }
        }

        let label: string = '';

        lista.forEach(item => {
            if (item.value === val) {
                label = item.label;
            }
        });

        return label;
    }


    estaVacio(cadena: string): boolean {
        if (cadena == null) {
            return true;
        }
        if (cadena.trim() === '') {
            return true;
        }
        if (cadena === undefined) {
            return true;
        }
        return false;
    }

    estaVacioNumber(parametro: number): boolean {
        if (parametro == null) {
            return true;
        }
        if (parametro == undefined) {
            return true;
        }

        return false;
    }

    resetAllFormFields(formGroup: FormGroup) {
        Object.keys(formGroup.controls).forEach(field => {
            const control = formGroup.get(field);
            if (control instanceof FormControl) {
                control.markAsPristine();
                control.markAsUntouched();
            } else if (control instanceof FormGroup) {
                this.resetAllFormFields(control);
            }
        });
    }

    validateAllFormFields(formGroup: FormGroup) {
        Object.keys(formGroup.controls).forEach(field => {
            const control = formGroup.get(field);
            if (control instanceof FormControl) {
                control.markAsTouched({ onlySelf: true });
            } else if (control instanceof FormGroup) {
                this.validateAllFormFields(control);
            }
        });
    }

    defaultBuscarPaginacion(tb: any, event) {
        if (event.keyCode === 13) {
            tb.reset();
        }
    }

    defaultBuscar(event) {
        if (event.keyCode === 13) {
            this.buscarSinPaginacion();
        }

    }

    buscarSinPaginacion() {

    }

    buscar(tb: any) {
        tb.reset();
    }

    getMensajeAgregado(value: any): string {
        return 'El registro Nro. ' + value + ' se agregó con éxito';

    }

    getMensajeAgregadoEmpty(): string {
        return 'El registro se agregó con éxito';

    }

    getMensajeGrabado(value: any): string {
        return 'El registro Nro. ' + value + ' se guardó con éxito';

    }

    /*getMensajeGrabadoBeneficiario(value: any): string {
        return 'El Beneficiario ' + value + ' se guardó con éxito';

    }*/
    getMensajeActualizadoBeneficiario(value: any): string {
        return 'El Beneficiario ' + value + ' se actualizó con éxito';

    }

    getMensajeGrabado2(value: any): string {
        return 'El registro de código ' + value + ' se guardó con éxito';

    }

    getMensajeGrabadoEmpty(): string {
        return 'El registro se guardó con éxito';

    }
    getMensajeGrabadoBeneficiario(beneficiario): string {
        return 'El/la beneficiario(a) ' + beneficiario + ' se guardó con éxito';
    }

    getMensajeActualizado(value: any): string {
        return 'La actualización del Nro. ' + value + ' se guardó con éxito';
    }

    getMensajeActualizado2(value: any): string {
        return 'La actualización de código ' + value + ' se guardó con éxito';
    }
    getMensajeActualizadoEmpty(): string {
        return 'La actualización se guardó con éxito';
    }
    getMensajeElilminadoEmpty(): string {
        return 'El registro fue eliminado';
    }

    getMensajeEliminado(value: any): string {
        return 'El registro Nro. ' + value + ' fue eliminado';

    }

    getMensajeEliminado2(value: any): string {
        return 'El registro de código ' + value + ' fue eliminado';

    }

    getMensajeInactivado(value: any): string {
        return 'El registro Nro. ' + value + ' fue inactivado';

    }

    getMensajeInactivadoCodigoString(value: any): string {
        return 'El registro ' + value + ' fue inactivado';

    }

    getMensajeActivado(value: any): string {
        return 'El registro Nro. ' + value + ' fue activado';

    }

    getMensajeActivadoCodigoString(value: any): string {
        return 'El registro ' + value + ' fue inactivado';

    }

    getMensajeAnulado(value: any): string {
        return 'El registro Nro. ' + value + ' fue anulado';

    }

    getMensajeAnulado2(value: any): string {
        return 'El registro de código ' + value + ' fue anulado';

    }

    getMensajeAnuladoNombre(value: any): string {
        return 'Se anuló al beneficiario ' + value;

    }
    getMensajeAnuladoEmpty(): string {
        return 'El registro fue rechazado';

    }

    getMensajePreguntaInactivar(): string {
        return 'Desea inactivar este registro?';

    }

    getMensajePreguntaActivar(): string {
        return 'Desea activar este registro?';

    }

    getMensajePreguntaCerrar(): string {
        return 'Desea cerrar este consumo?';

    }

    getMensajePreguntaEliminar(): string {
        return 'Desea eliminar este registro?';

    }

    getMensajePreguntaAnular(): string {
        return 'Desea anular este registro?';

    }

    borrarHoras(fecha: Date): Date {
        if (fecha == null) { return null; }
        fecha.setHours(0);
        fecha.setMinutes(0);
        fecha.setSeconds(0);
        fecha.setMilliseconds(0);

        fecha.setUTCHours(0);
        fecha.setUTCMinutes(0);
        fecha.setUTCSeconds(0);
        fecha.setUTCMilliseconds(0);
        return fecha;
    }

    borrarUTCHoras(fecha: Date): Date {
        if (fecha == null) { return null; }
        fecha.setUTCMilliseconds(fecha.getMilliseconds());
        fecha.setUTCMinutes(fecha.getMinutes());
        fecha.setUTCHours(fecha.getHours());
        fecha.setUTCDate(fecha.getDate());
        return fecha;
    }

    trim(cadena: string): string {
        if (this.estaVacio(cadena)) {
            return null;
        }
        return cadena.trim();
    }

    getMensajesUsuario(mensajes: MensajeUsuario[]): Message[] {
        const messages: Message[] = [];
        mensajes.forEach(
            mensaje => {

                let severidad = '';
                switch (mensaje.TipoMensaje) {
                    case tipo_mensaje.ADVERTENCIA: { severidad = 'warn'; break; }
                    case tipo_mensaje.ERROR: { severidad = 'error'; break; }
                    case tipo_mensaje.EXITO: { severidad = 'success'; break; }
                    case tipo_mensaje.INFORMACION: { severidad = 'info'; break; }
                    default: { severidad = 'info'; break; }
                }

                if (mensaje.mensaje === undefined && mensaje.Mensaje !== undefined) {
                    mensaje.mensaje = mensaje.Mensaje;
                }
                messages.push({ severity: severidad, summary: 'Mensaje', detail: mensaje.mensaje });
            }
        );



        return messages;
    }

    dataURIToBlob(dataURI) {
        var binStr = atob(dataURI.split(',')[1]),
            len = binStr.length,
            arr = new Uint8Array(len),
            mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0]

        for (var i = 0; i < len; i++) {
            arr[i] = binStr.charCodeAt(i);
        }

        return new Blob([arr], {
            type: mimeString
        });

    }



}
