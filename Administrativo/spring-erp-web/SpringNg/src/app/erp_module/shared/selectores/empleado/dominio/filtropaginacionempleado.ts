import { ParametroPaginacionGenerico } from '../../../dominio/ParametroPaginacionGenerico';

export class FiltroPaginacionEmpleado {
    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    idCompaniaSocio: string;
    idSucursal: string;
    idUnidadNegocioAsignada: string;
    empleadoId: number;
    empleaadoNombreCompleto: string;
    empleadoEstado: string;
    empleadoDocumento: string;
    empleadoJefe: number;
    empleadoUsuario: string;
    empleadoNoValida: string;
    fechaDesde: Date;
    fechaHasta: Date;
    empleadoConceptoAcceso: string;
    estado: string;
    jefeUnidad: number;
    idUnidadOperativa: string;
    puesto: number;
    esAdministradorWeb: string;
}
