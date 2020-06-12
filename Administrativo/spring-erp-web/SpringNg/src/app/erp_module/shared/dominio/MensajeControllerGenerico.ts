export class MensajeControllerGenerico {

    constructor() {
        this.parametros = [];
    }

    parametros: SpringMap[];

    public static CONSTANTE_WORKFLOW_ACCION_TIPO_EXTERNO_GUARDAR: string = "workflow_externo_guardar";
    public static CONSTANTE_WORKFLOW_ACCION_TIPO_EXTERNO_SALIR: string = "workflow_externo_salir";
    public static CONSTANTE_WORKFLOW_ACCION_TIPO_EXTERNO_OTRO: string = "workflow_externo_otro";
    public static CONSTANTE_WORKFLOW_ACCION_TIPO_INSERTAR: string = "workflow_insertar";
    public static CONSTANTE_WORKFLOW_ACCION_TIPO_ACTUALIZAR: string = "workflow_actualizar";
    public static CONSTANTE_WORKFLOW_ACCION: string = "workflow_accion";
    public static CONSTANTE_WORKFLOW_ELEMENTO_ENTRADA: string = "workflow_elemento_entrada";
    public static CONSTANTE_WORKFLOW_ELEMENTO_SALIDA: string = "workflow_elemento_salida";
}

export class SpringMap {
    clave: string;
    valor: any;
}