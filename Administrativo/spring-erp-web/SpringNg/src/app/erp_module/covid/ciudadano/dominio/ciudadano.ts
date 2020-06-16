export class CiudadanoPk {
    idCiudadano: number;

}

export class Ciudadano extends CiudadanoPk {
    nombre: string;
    apellido: string;
    idPais: string;
    tipoDocumento: string;
    nroDocumento: string;
    fechaNacimiento: Date;
    direccion: string;
    idDepartamento: string;
    idProvincia: string;
    idDistrito: string;
    estado: string;
}

