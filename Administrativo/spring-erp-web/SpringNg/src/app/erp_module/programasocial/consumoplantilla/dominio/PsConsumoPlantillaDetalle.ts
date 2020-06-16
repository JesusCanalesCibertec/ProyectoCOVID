export class PsConsumoPlantillaDetallePk {
    idInstitucion: string;
    plantilla: number;
    idItem: string;
}
export class PsConsumoPlantillaDetalle extends PsConsumoPlantillaDetallePk {

    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

    auxNombre:string;

}
