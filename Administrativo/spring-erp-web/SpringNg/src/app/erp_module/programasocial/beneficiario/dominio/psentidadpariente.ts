export class PsEntidadParientePk {
    idEntidad: number;
    idPariente: number;
}
export class PsEntidadPariente extends PsEntidadParientePk {
    idParentesco: string;
    pariente: string;
    auxParentesco: string;
}