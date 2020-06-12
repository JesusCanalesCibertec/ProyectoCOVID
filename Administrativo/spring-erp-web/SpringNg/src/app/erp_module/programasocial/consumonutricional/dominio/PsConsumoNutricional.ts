
class PsConsumoNutricionalPk {
    idInstitucion: string;
    idConsumo: number;
    idConsumoNutricional: number;

}

export class PsConsumoNutricional extends PsConsumoNutricionalPk {

    idItem: string;
    cantidadsolicitada: number;
    cantidadfundacion: number;
    cantidadotros: number;
    comentarios: string;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;


}

