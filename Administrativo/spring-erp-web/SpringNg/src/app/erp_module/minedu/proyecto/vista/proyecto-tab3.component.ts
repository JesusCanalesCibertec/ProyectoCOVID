import { Component, OnInit } from "@angular/core";
import { NoAuthorizationInterceptor } from "src/app/base_module/interceptor/NoAuthorizationInterceptor";
import { PrincipalBaseComponent } from "src/app/base_module/components/PrincipalBaseComponent";
import { MaMiscelaneosdetalleServicio } from "src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio";
import { Proyecto } from "../dominio/proyecto";
import { LabelFromItem } from "src/app/base_module/util/pipes/LabelFromItem.pipe";
import { promise } from "protractor";
import { MessageService } from "primeng/components/common/messageservice";
import { SelectItem } from "primeng/api";


@Component({
    selector: 'app-proyecto-seguimiento',
    templateUrl: './proyecto-tab3.component.html'
})
export class ProyectoSeguimientoComponent extends PrincipalBaseComponent implements OnInit {

    estados: SelectItem[] = [];
    gestiones: SelectItem[] = [];
    tipos: SelectItem[] = [];
    ingenierias: SelectItem[] = [];
    enabledEstado: boolean = false;


    proyecto: Proyecto = new Proyecto();


    constructor(
        private miscelaneoServicio: MaMiscelaneosdetalleServicio,

        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService) {
        super(noAuthorizationInterceptor, messageService)
    }

    ngOnInit() {

    }
    cargarEstados(): Promise<number> {
        this.estados = [];
        this.estados.push({ label: '--Seleccione--', value: null });
        return this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_ESTADO_PROYECTO).then(r => {
            r = r.sort((a,b)=> Number(a.codigoelemento)-Number(b.codigoelemento));
            r.forEach(rr => {
                    this.estados.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
            });
            return 1;
        });
    }
    cargarFasesGestion(): Promise<number>  {
        this.gestiones = [];
        this.gestiones.push({ label: '--Seleccione--', value: null });
        return this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_FASE_GESTIONES).then(r => {
            r.forEach(rr => {
                this.gestiones.push({ label: rr.descripcionlocal, value: rr.codigoelemento })
            });
            return 1;
        });
    }
    cargarTipos(): Promise<number>  {
        this.tipos = [];
        this.tipos.push({ label: '--Seleccione--', value: null });
        return this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_TIPO_INGENIERIA).then(r => {
            r.forEach(rr => {
                this.tipos.push({ label: rr.descripcionlocal, value: rr.codigoelemento })
            });
            return  1;
        });
    }

    fase: string;
    cargarFasesIngenierias() {
        this.bloquearPagina();
        this.proyecto.faseingenieria = null;
        this.ingenierias = [];
        if (this.proyecto.tipoanalisis == null) {
            this.ingenierias = [];
            this.desbloquearPagina();
        } else if (this.proyecto.tipoanalisis == '1') {
            this.ingenierias.push({ label: '--Seleccione--', value: null });
            this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_FASE_INGENIERIAS).then(r => {
                r.forEach(rr => {
                    if (rr.codigoelemento.charAt(0) == 'R') {
                        this.ingenierias.push({ label: rr.descripcionlocal, value: rr.codigoelemento })
                    }
                });
                this.proyecto.faseingenieria = this.fase;
                this.fase = null;
                this.desbloquearPagina();
            });
        } else {
            this.ingenierias.push({ label: '--Seleccione--', value: null });
            this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_FASE_INGENIERIAS).then(r => {
                r.forEach(rr => {
                    if (rr.codigoelemento.charAt(0) == 'A') {
                        this.ingenierias.push({ label: rr.descripcionlocal, value: rr.codigoelemento })
                    }
                });
                this.proyecto.faseingenieria = this.fase;
                this.fase = null;
                this.desbloquearPagina();
            });
        }
    }


}