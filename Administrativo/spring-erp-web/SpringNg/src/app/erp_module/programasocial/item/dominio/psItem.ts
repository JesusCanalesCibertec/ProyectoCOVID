export class PsItemPk {
    idItem: string;
}
export class PsItem extends PsItemPk {

    idTipoItem: string;
    nombre: string;
    descripcion: string;
    idUnidadMedida: string;
    kilocalorias: number;
    idLinea: string;
    idFamilia: string;
    idSubfamilia: string;
    idClase: string;
    idGrupo: string;
    coeficiente: number;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;


}
