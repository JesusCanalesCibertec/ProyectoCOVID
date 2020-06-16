import { PsBeneficiario } from "./psBeneficiario";
import { PsEntidadPariente } from "./psentidadpariente";
import { PsEntidadDocumento } from "./psentidaddocumento";
import { PsEntidadEquipamiento } from "./psentidadequipamiento";
import { PsEntidadSeguroSocial } from "./psentidadsegurosocial";
import { PsEntidadProgramaSocial } from "./psentidadprogramasocial";
import { PsBeneficiarioIngresoCasual } from "./psbeneficiarioingresocasual";
import { PsBeneficiarioIngreso } from "./psbeneficiarioingreso";

export class PsEntidadPk {
    idEntidad;
}
export class PsEntidad extends PsEntidadPk {

    //listaIngresos: PsBeneficiarioIngreso[];

    listaPariente: PsEntidadPariente[];
    listaDocumento: PsEntidadDocumento[];
    listaEquipamiento: PsEntidadEquipamiento[];
    listaSeguro: PsEntidadSeguroSocial[];
    listaPrograma: PsEntidadProgramaSocial[];

    ingreso: PsBeneficiarioIngreso;
    auxInstitucion: string;
    auxInstitucionOrigen: string;

    auxUbigeo: string;
    auxUbigeoConocido: string;
    auxPrograma: string;

    constructor() {
        super();
        this.listaPariente = [];
        this.listaDocumento = [];
        this.listaEquipamiento = [];
        this.listaSeguro = [];
        this.listaPrograma = [];
        this.ingreso = new PsBeneficiarioIngreso();
        //this.listaIngresos = [];
    }

    apellidoPaterno: string;
    apellidoMaterno: string;
    nombres: string;
    nombrecompleto: string;
    idNacimientoPais: string;
    idNacimientoUbigeo: string;
    idEstadoCivil: string;
    idSexo: string;
    idDiscapacidad: string;
    idGrupoSanguineo: string;
    idFactorRh: string;
    idTipoDocumento: string;
    documento: string;
    idCarnetConadis: string;
    comentarios: string;
    correo1: string;
    idNivelAcademico: string;
    idEspecialidadAcademica: string;
    domicilioIdUbigeo: string;
    domicilioDireccion: string;
    domicilioReferencia: string;
    telefono1: string;
    telefono2: string;
    idTenencia: string;
    idMaterialConstruccion: string;
    idServicioAgua: string;
    idServicioDesague: string;
    idServicioElectricidad: string;
    padreNombre: string;
    padreIdVive: string;
    padreNroDocumento: string;
    padreIdOcupacion: string;
    madreNombre: string;
    madreIdVive: string;
    madreNroDocumento: string;
    madreIdOcupacion: string;
    apoderadoNombre: string;
    apoderadoNroDocumento: string;
    apoderadoEsposoa: string;
    apoderadoEsposoaNroDocumento: string;
    comentariosReferenciaFamiliar: string;
    estado: string;
    creacionUsuario: string;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionTerminal: string;
    flgPensionista: string;
    flgFamiliares: string;

    creacionFecha: Date;
    fechaNacimiento: Date;
    modificacionFecha: Date;
    edad: number;
    ingresoMensual: number;

    estadoBeneficiario: string;

}
