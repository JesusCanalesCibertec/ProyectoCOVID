export class HrDepartamentoPk {
    departamento: string;

}
export class HrDepartamento extends HrDepartamentoPk {
    descripcion: string;
    estado: string;
    ultimousuario: string;
    ultimafechamodif: Date;
}
