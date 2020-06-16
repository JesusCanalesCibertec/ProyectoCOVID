import { DtoSolicitudes } from '../../../../shared/dominio/dto/DtoSolicitudes';

export class DtoRestSolicitudLista {

    constructor() {
        this.listaSolicitudes = [];
    }

    accion: string;
    listaSolicitudes: DtoSolicitudes[];
}