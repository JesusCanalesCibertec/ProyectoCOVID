import { DtoTabla } from '../../dominio/dto/DtoTabla';

export class FiltroMiscelaneosDetalle {
    CodigoAplicacion: string;
    CodigoTabla: string;
    CodigoCompania: string;
    CodigoElemento: string;
    Nombre: string;
    Estado: string;
    NotIn: DtoTabla[];
    ValorCodigo1: string;
}
