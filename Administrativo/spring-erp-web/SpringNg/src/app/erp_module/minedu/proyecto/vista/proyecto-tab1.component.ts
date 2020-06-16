import { Component, OnInit, ViewChild, Output } from "@angular/core";
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { AreamineduSelectorComponent } from "src/app/erp_module/shared/area/vista/areaminedu-selector.component";
import { Proyecto, ProyectoPk } from "../dominio/proyecto";
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { DataTable, SelectItem, ConfirmationService } from 'primeng/primeng';
import { MaMiscelaneosdetalleServicio } from "src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio";
import { ContratacionService } from "../../contratacion/servicio/contratacion.service";
import { ActivatedRoute } from "@angular/router";
import { EventEmitter } from "events";
import { ComunicacionService } from "../servicio/comunicacion.service";
import { MessageService } from "primeng/components/common/messageservice";
import { ProyectoService } from "../servicio/Proyecto.service";
import { YEAR } from "ngx-bootstrap/chronos/units/constants";
import { Sistema } from "../../sistema/dominio/sistema";
import { SistemaService } from "../../sistema/servicio/sistema.service";

@Component({
    selector: 'app-proyecto-datosgenerales',
    templateUrl: './proyecto-tab1.component.html'
})
export class ProyectoDatosgeneralesComponent extends PrincipalBaseComponent implements OnInit {

    @ViewChild(DataTable) dt: DataTable;
    @ViewChild(AreamineduSelectorComponent) AreamineduSelectorComponent: AreamineduSelectorComponent;

    proyecto: Proyecto = new Proyecto();
    ProyectoPk: ProyectoPk = new ProyectoPk();
    niveles: string;
    recursos: number;
    codProyecto: number;
    checked: boolean;
    auxIdSistema: number;
    auxSiglas: string;

    //COMBOBOX
    tiposproyect: SelectItem[] = [];
    coordinaciones: SelectItem[] = [];
    personas: SelectItem[] = [];
    sistemas: SelectItem[] = [];
    listaSistemas: Sistema[] = [];

    @Output() adicionar = new EventEmitter();

    constructor(
        private route: ActivatedRoute,
        private miscelaneoServicio: MaMiscelaneosdetalleServicio,
        private contratacionServicio: ContratacionService,
        public comunicacionServicio: ComunicacionService,
        private confirmationService: ConfirmationService,
        private proyectoServicio: ProyectoService,
        private sistemaServicio: SistemaService,
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService) {
        super(noAuthorizationInterceptor, messageService)
    }

    ngOnInit() {
    }
    cargarPersonas(): Promise<number> {
        this.personas = [];
        this.personas.push({ label: '--Seleccione--', value: null });
        return this.contratacionServicio.Contratosactivos().then(r => {
            r.forEach(rr => {
                this.personas.push({ label: rr.persona, value: rr.idPersona });
            });
            return 1;
        });
    }
    cargarTiposproyect(): Promise<number> {
        this.tiposproyect = [];
        this.tiposproyect.push({ label: '-- Seleccione --', value: null });
        return this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_TIPO_PROYECTO).then(r => {
            r.forEach(
                rr => {
                    this.tiposproyect.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
                });
            return 1;
        });
    }
    cargarCoordinaciones(): Promise<number> {
        this.coordinaciones = [];
        this.coordinaciones.push({ label: '--Seleccione--', value: null });
        return this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_COORDINACIONES_MINEDU).then(r => {
            r.forEach(
                rr => {
                    this.coordinaciones.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
                });
            return 1;
        });
    }
    cargarSistemas(): Promise<number> {
        this.sistemas = [];
        this.auxIdSistema = null;
        this.auxSiglas = null;
        this.sistemas.push({ label: '--Seleccione--', value: null });
        return this.sistemaServicio.ListarSistemas().then(r => {
            this.listaSistemas = r;
            r.forEach(
                rr => {
                    //this.sistemas.push({ label: rr.nombre, value: rr.idSistema, auxlabel: this.filtrar_tildes(rr.nombre) });
                    this.sistemas.push({ label: rr.nombre, value: rr.idSistema });
                });
            return 1;
        });
    }
    mostrarSelector() {
        if (this.estaVacio(this.proyecto.nombre)) {
            this.mostrarMensajeInfo('Ingrese un nombre de proyecto');
            return;
        }

        if (this.estaVacio(this.proyecto.nombrecorto)) {
            this.mostrarMensajeInfo('Ingrese un nombre corto para el proyecto');
            return;
        }
        this.AreamineduSelectorComponent.iniciarComponente(null);
    }
    cargarArea(dto: any) {

        let numero;
        var today = new Date();
        var year = today.getFullYear();
        this.proyectoServicio.listardetalle('', year, '').then(res => {
            numero = res != null ? res.length : 0;

            this.proyecto.area = dto.data.codigo;
            dto.data.ultimocorto = dto.data.ultimocorto == null ? '' : ' / ' + dto.data.ultimocorto;
            this.niveles = dto.data.primercorto + ' / ' + dto.data.segundocorto + dto.data.ultimocorto;
            this.proyecto.codigoproyecto = 'PROYECTO-' + (numero + 1) + '-' + year + '-' + dto.data.primercorto + '-' + this.proyecto.nombrecorto.toUpperCase();
            this.AreamineduSelectorComponent.salir();
        })


    }
    validarRecursos(e) {
        if (this.comunicacionServicio.parametro > e) {
            this.confirmationService.confirm({
                header: 'Reducción de cantidad',
                icon: 'fa fa-question-circle',
                message: 'Hay ' + this.comunicacionServicio.parametro + ' agregados, al reducir la cantidad de recursos se eliminarán los participantes añadidos, continuar?',
                accept: () => {
                    this.comunicacionServicio.borrar = true;
                    this.comunicacionServicio.parametro = 0;
                    this.auxgestor = null;
                    // this.adicionarGestor(this.proyecto.gestor);
                },
                reject: () => {
                    this.comunicacionServicio.numRecursos = this.comunicacionServicio.parametro;
                    this.comunicacionServicio.borrar = false;
                }
            });
        }
        this.comunicacionServicio.enviarCantidadrecursos(e);

    }

    auxgestor: number;
    adicionarGestor(e) {

        // if (e != null) {
        //     if (this.auxgestor == null) {
        //         if (this.comunicacionServicio.numRecursos > 0) {
        //             if (this.comunicacionServicio.parametro == this.comunicacionServicio.numRecursos) {
        //                 this.personas = [];
        //                 this.cargarPersonas();
        //                 this.mostrarMensajeError('El número máximo de recursos es ' + this.comunicacionServicio.numRecursos);
        //                 return;
        //             }
        //         }
        //         this.comunicacionServicio.parametro = this.comunicacionServicio.parametro + 1;

        //     }
        //     this.auxgestor = e;
        // } else {
        //     this.comunicacionServicio.parametro = this.comunicacionServicio.parametro > 0 ? this.comunicacionServicio.parametro - 1 : this.comunicacionServicio.parametro;
        //     this.auxgestor = null;
        // }

    }

    obtenerSiglas(e) {
        this.proyecto.siglas = null;

        if (e != null) {
            this.proyecto.siglas = this.listaSistemas.find(obj => obj.idSistema == e).siglas;
            this.auxIdSistema = e;
            this.auxSiglas = this.proyecto.siglas;
        }
    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        if (this.estaVacioNumber(this.proyecto.idSistema) && this.checked == true) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un Sistema' });
            valida = false;
        }
        if (this.estaVacio(this.proyecto.siglas) && this.checked == true) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Las siglas del sistema es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.proyecto.codigoproyecto)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Código de Proyecto requerido' });
            valida = false;
        }
        if (this.estaVacio(this.proyecto.nombre)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Nombre del Proyecto es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.proyecto.nombre)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Nombre corto del Proyecto es requerido' });
            valida = false;
        }
        if (this.estaVacio(this.proyecto.tipoproyecto)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un Tipo de Proyecto' });
            valida = false;
        }
        if (this.estaVacio(this.proyecto.coordinacion)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione una Coordinación' });
            valida = false;
        }
        if (this.estaVacioNumber(this.proyecto.area)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione un Área' });
            valida = false;
        }
        return valida;
    }

    vincular() {
        if (!this.checked) {
            this.proyecto.idSistema = null;
            this.proyecto.siglas = null;
        } else {
            this.proyecto.idSistema = this.auxIdSistema;
            this.proyecto.siglas = this.auxSiglas;
        }
    }



}