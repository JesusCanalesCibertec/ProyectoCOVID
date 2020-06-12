export class SeguridadperfilusuarioPk {
    perfil: string;
    usuario: string;
}
export class Seguridadperfilusuario extends SeguridadperfilusuarioPk {
    estado: string;
    creacionFecha: Date;
    ultimaconexion: Date;

    //no mapeados
    nombres: string;
    secuencia: number;
    activo:Boolean;
}
