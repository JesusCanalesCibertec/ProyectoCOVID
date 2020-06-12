import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';

export class DtoComponenteFurh {
    idInstitucion: string;
    idEmpleado: number;
    idEntidad: number;
    fechaIngreso: Date;
    apellidoPaterno: string;
    apellidoMaterno: string;
    nombres: string;
    correo1: string;
    nombrecompleto: string;
    busqueda: string;
    idTipoDocumento: string;
    documento: string;
    fechanacimiento: Date;
    idSexo: string;
    idEstadoCivil: string;
    estado: string;
    estadoNombre: string;
    idDiscapacidad: string;
    idNivelAcademico: string;
    idEspecialidadAcademica: string;
    tiempoPermanencia: string;
    idNivelAcademicoNombre: string;
    idEspecialidadAcademicaNombre: string;
    idInstitucionNombre: string;
    idAreaTrabajo: string;
    codigoUsuario: string;
    comentarios: string;
    idFactorRh: string;
    idGrupoSanguineo: string;
    conusuario: boolean;
    orden: number;
    sentido: number;
    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;
}
