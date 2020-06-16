import { DtoTabla } from '../../../shared/dominio/dto/DtoTabla';
import { HrDepartamento } from '../../../shared/departamento/dominio/hrdepartamento';

export class SyAprobacionNivelesDetPk {

    codigo: number;
    nivel: number;
    usuario: number;

}
export class SyAprobacionNivelesDet extends SyAprobacionNivelesDetPk {

    constructor() {
        super();
        this.listaAreas = [];
        this.listaCorreos = [];
    }

    flagarea: string;
    flagsolicitante: string;
    flagsuperior: string;
    area: string;
    correoelectronico: string;
    auxNombrePersona: string;
    listaCorreos: DtoTabla[];
    listaAreas: HrDepartamento[];
    companyownerusuario: string;
    auxSecuencia: number;
    valor: string;
}
