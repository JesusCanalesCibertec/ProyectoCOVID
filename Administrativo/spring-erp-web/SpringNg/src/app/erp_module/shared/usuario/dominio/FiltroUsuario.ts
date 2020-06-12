import { ParametroPaginacionGenerico } from "../../dominio/ParametroPaginacionGenerico";



export class FiltroUsuario {

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;

    codUsuario: string;
    nombre: string;

    
}
