export class ConocimientoPk{
    idConocimiento: number;

}

export class Conocimiento extends ConocimientoPk {
 
    tipo: string;
    area: string;
    nombre: string;
    descripcion:string;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

    //no mapeados
    cabecera: string;
}

