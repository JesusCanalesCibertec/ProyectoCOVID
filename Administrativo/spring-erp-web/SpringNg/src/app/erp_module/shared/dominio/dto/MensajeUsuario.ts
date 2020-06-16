export class MensajeUsuario {

    constructor() {
        this.mensajes = [];
    }

    TipoMensaje: tipo_mensaje;
    codigo: string;
    titulo: string;
    mensaje: string;
    Mensaje: string;
    fuente: string;
    solucion: string;
    mensajes: string[];
}

export enum tipo_mensaje {
    ERROR, ADVERTENCIA, EXITO, INFORMACION
}
