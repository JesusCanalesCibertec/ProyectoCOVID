import { ParametroPaginacionGenerico } from "src/app/erp_module/shared/dominio/ParametroPaginacionGenerico";

export class DtoConsumoPlantilla {

  codInstitucion: string;
  codItem: string;
  nomItem: string;
  nomUnidad: string;

  paginacion: ParametroPaginacionGenerico;

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
  
}
