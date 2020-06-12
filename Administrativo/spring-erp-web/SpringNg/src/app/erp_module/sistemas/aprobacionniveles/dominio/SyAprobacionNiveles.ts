import { SyAprobacionNivelesDet } from './syaprobacionnivelesdet';
import { DtoTabla } from '../../../shared/dominio/dto/DtoTabla';
import { DtoNivelDetalle } from './dto/DtoNivelDetalle';

export class SyAprobacionNivelesPk {
    codigo: number;
    companyOwner: string;
}

export class SyAprobacionNiveles extends SyAprobacionNivelesPk {

    constructor() {
        super();
        this.listaCorreos = [];
        this.listaNiveles = [];
        this.listaCorreosConfirmacion = [];
    }

    aplicacion: string;
    codigoproceso: number;
    descripcion: string;
    correoelectronico: string;
    estado: string;
    ultimousuario: string;
    ultimafechamodif: Date;
    flagusuariosuper: string;
    usuario: number;
    flagusuarioadministrador: string;
    usuarioadministrador: number;
    flagsolicitante: string;
    flagsuperior: string;
    nroniveles: number;
    flagAplicaDetalle: string;

    auxAplicacionNombre: string;
    auxEstadoNombre: string;
    listaNiveles: DtoNivelDetalle[];
    listaCorreos: DtoTabla[];
    listaCorreosConfirmacion: DtoTabla[];
    flagusuariosuperB: boolean;
    flagusuarioadministradorB: boolean;
    flagsuperiorB: boolean;
    flagsolicitanteB: boolean;

    nivel0FlagsolicitanteB: boolean;
    nivel0Flagsolicitante: string;
    nivel0FlagsuperiorB: boolean;
    nivel0Flagsuperior: string;

    nivel0Correoelectronico: string;

    auxUsuarioNombre: string;
    auxUsuarioAdmNombre: string;
}