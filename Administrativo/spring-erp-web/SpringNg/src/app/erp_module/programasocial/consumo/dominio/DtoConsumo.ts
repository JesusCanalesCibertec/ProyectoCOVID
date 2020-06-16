export class DtoConsumo {

  codConsumo: number;
  codInstitucion: string;
  nomInstitucion: string;
  codPeriodo: string;
  fechainicio: Date;
  fechafin: Date;
  descripcion: string;
  estado: string;
  aportante: string;
}

export class DtoConsumoCarga {
  excel: string;
}
