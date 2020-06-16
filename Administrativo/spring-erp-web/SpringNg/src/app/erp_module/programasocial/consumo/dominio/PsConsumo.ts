import { PsConsumoNutricional } from "../../consumonutricional/dominio/PsConsumoNutricional";


export class PsConsumoPk {
    idInstitucion: string;
    idConsumo: number;
}

export class PsConsumo extends PsConsumoPk {

    idPeriodo: string;
    descripcion: string;
    estado: string;
    aportante: string;
    fechainicio: Date;
    fechafin: Date;
    fechaCierre: Date;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;
    comentario: string;
    valoracion: number;
    inicioConsumo: Date;
    finConsumo: Date;
    fechaDespacho: Date;

    constructor() {
        super();
        this.listadetalle = [];
    }

    listadetalle: PsConsumoNutricional[];


}