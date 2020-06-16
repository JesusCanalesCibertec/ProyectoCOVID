import { PsConsumoPlantillaDetalle } from "./PsConsumoPlantillaDetalle";

export class PsConsumoPlantillaPk {
    idInstitucion: string;
    plantilla: number;
}
export class PsConsumoPlantilla extends PsConsumoPlantillaPk {

    descripcion: string;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

    constructor() {
        super();
        this.listadetalle = [];
    }

    listadetalle: PsConsumoPlantillaDetalle[];

}
