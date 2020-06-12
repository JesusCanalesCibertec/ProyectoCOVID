import { SyAprobacionNivelesDet } from '../syaprobacionnivelesdet';
import { DtoTablaEntero } from '../../../../shared/dominio/dto/DtoTablaEntero';

export class DtoNivelDetalle extends DtoTablaEntero {

    constructor() {
        super();
        this.listaDetalle = [];
    }

    listaDetalle: SyAprobacionNivelesDet[];
}
