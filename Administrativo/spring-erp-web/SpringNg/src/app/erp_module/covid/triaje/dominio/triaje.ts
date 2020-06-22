export class TriajePk {
    idTriaje: number;

}

export class Triaje extends TriajePk {
    idCiudadano: number;
    disgus: string;
    tos: string;
    dolor: string;
    difi: string;
    nasal: string;
    fiebre: string;
    fechainicio: Date;
    situacion1: string;
    situacion2: string;
    situacion3: string;
    obesidad: string;
    pulmonar: string;
    asma: string;
    diabetes: string;
    hipertension: string;
    cardio: string;
    renal: string;
    cancer: string;
    estado: number;
}

