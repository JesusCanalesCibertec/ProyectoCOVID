import { Component, OnInit, ViewChild } from "@angular/core";
import { SelectItem, ConfirmationService } from "primeng/api";
import { PrincipalBaseComponent } from "src/app/base_module/components/PrincipalBaseComponent";
import { NoAuthorizationInterceptor } from "src/app/base_module/interceptor/NoAuthorizationInterceptor";
import { Proyecto, ProyectoPk } from "../dominio/proyecto";
import { Proyectorecurso } from "../dominio/proyectorecurso";
import { AreamineduSelectorComponent } from "src/app/erp_module/shared/area/vista/areaminedu-selector.component";
import { MaMiscelaneosdetalleServicio } from "src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio";
import { ContratacionService } from "../../contratacion/servicio/contratacion.service";
import { dtoContratacion } from "../../contratacion/dominio/dtoContratacion";
import { AreamineduServicio } from "src/app/erp_module/shared/area/servicio/areaminedu.service";
import { ActivatedRoute } from "@angular/router";
import { ProyectoService } from "../servicio/Proyecto.service";
import { SelectorEquipoComponent } from "./selector-equipo.component";
import { partition } from "rxjs/operators";
import { ComunicacionService } from "../servicio/comunicacion.service";
import { MaMiscelaneosdetallePk } from "src/app/erp_module/shared/miscelaneos/dominio/MaMiscelaneosdetalle";
import { MessageService } from "primeng/components/common/messageservice";
import { OverlayPanel } from "primeng/primeng";


@Component({
    selector: 'app-proyecto-recursos',
    templateUrl: './proyecto-tab2.component.html'
})
export class ProyectoRecursosComponent extends PrincipalBaseComponent implements OnInit {

    @ViewChild(AreamineduSelectorComponent) AreamineduSelectorComponent: AreamineduSelectorComponent;
    @ViewChild(SelectorEquipoComponent) SelectorEquipoComponent: SelectorEquipoComponent

    proyecto: Proyecto = new Proyecto();
    ProyectoPk: ProyectoPk = new ProyectoPk();

    recurso: Proyectorecurso = new Proyectorecurso();


    //combobox
    roles: SelectItem[] = [];
    roles2: SelectItem[] = [];
    


    //listas
    auxlista: dtoContratacion[] = [];

    constructor(
        private route: ActivatedRoute,
        private miscelaneoServicio: MaMiscelaneosdetalleServicio,
        private contratacionServicio: ContratacionService,
        private confirmationService: ConfirmationService,
        public comunicacionServicio: ComunicacionService,
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService) {
        super(noAuthorizationInterceptor, messageService)
    }

    ngOnInit() {
    }


    //llenado de combos: comienzo

    cargarPersonas(bean: Proyectorecurso, parametro: number) {
        bean.personas = [];
        bean.idPersona = null;
        bean.personas.push({ label: '--Seleccione--', value: null });
        this.contratacionServicio.Contratosactivos().then(r => {
            this.auxlista = r;
            r.forEach(rr => {
                //bean.personas.push({ label: rr.persona, value: rr.idPersona, auxlabel: this.filtrar_tildes(rr.persona)});  
                bean.personas.push({ label: rr.persona, value: rr.idPersona});   
            });
            bean.idPersona = parametro;
        });
        return bean;
    }

    cargarRoles(): Promise<number> {
        this.roles = [];
        this.roles.push({ label: '--Selecione--', value: null });
        return this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_ROLES_PROYECTO).then(r => {
            r = r.sort((a, b) => Number(a.codigoelemento) - Number(b.codigoelemento));
            r.forEach(rr => {
                if (parseInt(rr.codigoelemento) <= 3) {
                    this.roles.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
                }
            });
            return 1;
        });
    }

    cargarRoles2(): Promise<number> {
        this.roles2 = [];
        this.roles2.push({ label: '--Selecione--', value: null });
        return this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_ROLES_PROYECTO).then(r => {
            r = r.sort((a, b) => Number(a.codigoelemento) - Number(b.codigoelemento));
            r.forEach(rr => {
                if (parseInt(rr.codigoelemento) > 3) {
                    this.roles2.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
                }
            });
            return 1;
        });
    }
    //llenado de combos: fin

    cargarCargo(bean: Proyectorecurso) {
        if (bean.idPersona != null) {
            let auxListaInteresados = this.proyecto.listaRecursos1.filter(obj => obj.idPersona == bean.idPersona);
            let auxListaEquipo = this.proyecto.listaRecursos2.find(obj => obj.idPersona == bean.idPersona);
            let auxpersonas = this.proyecto.listaRecursos1.find(obj => obj.idRecurso === bean.idRecurso).personas;
            if (auxListaInteresados.length > 1) {
                this.mostrarMensajeError('El personal seleccionado se encuentra en la lista');
                bean = this.cargarPersonas(bean, null);
                this.proyecto.listaRecursos1.find(obj => obj.idRecurso === bean.idRecurso).personas = bean.personas;
                this.proyecto.listaRecursos1.find(obj => obj.idRecurso === bean.idRecurso).cargo = null;
                return;
            }
            if (auxListaEquipo == undefined) {
              
            }else{
                this.mostrarMensajeError('El personal seleccionado forma parte del Equipo de Desarrollo');
                bean = this.cargarPersonas(bean, null);
                this.proyecto.listaRecursos1.find(obj => obj.idRecurso === bean.idRecurso).personas = bean.personas;
                this.proyecto.listaRecursos1.find(obj => obj.idRecurso === bean.idRecurso).cargo = null;
                return;
            }

            let auxCargo = this.auxlista.find(obj => obj.idPersona === bean.idPersona).cargo;
            this.proyecto.listaRecursos1.find(obj => obj.idRecurso === bean.idRecurso).cargo = auxCargo;
        } else {
            this.proyecto.listaRecursos1.find(obj => obj.idRecurso == bean.idRecurso).cargo = null;
        }
    }


    //Interesados comienzo
    adicionarFila() {
        if (this.comunicacionServicio.parametro >= this.comunicacionServicio.numRecursos) {
            this.mostrarMensajeError('El número máximo de recursos es ' + this.comunicacionServicio.numRecursos);
            return;
        }
        let lst = [...this.proyecto.listaRecursos1];
        var interesado = new Proyectorecurso();
        interesado.idRecurso = this.generarSecuencia(this.proyecto.listaRecursos1);
        let bean = new Proyectorecurso();
        bean = this.cargarPersonas(interesado, null);
        interesado.personas = bean.personas;
        lst.push(interesado);
        this.proyecto.listaRecursos1 = lst;
        console.log(interesado.personas);

        this.comunicacionServicio.parametro = this.comunicacionServicio.parametro + 1;
    }


    generarSecuencia(lista: any[]): number {
        return lista.length == 0 ? 1 : lista.length + 1
    }

    enumerar(lista: any[]) {
        let numero = 0;
        lista.forEach(res => {
            res.idRecurso = numero + 1;
            numero = numero + 1;
        })
    }

    mostrarSelector(codigo: number) {
        this.AreamineduSelectorComponent.iniciarComponente(codigo);
    }

    cargarArea(dto: any) {
        this.proyecto.listaRecursos1.find(obj => obj.idRecurso == dto.tag).area = dto.data.codigo;
        this.proyecto.listaRecursos1.find(obj => obj.idRecurso == dto.tag).nomArea = dto.data.primero;
        this.proyecto.listaRecursos1.find(obj => obj.idRecurso == dto.tag).nombre = null;
        this.proyecto.listaRecursos1.find(obj => obj.idRecurso == dto.tag).cargo = null;
        this.proyecto.listaRecursos1.find(obj => obj.idRecurso == dto.tag).idContratacion = null;
        this.AreamineduSelectorComponent.salir();
    }

    validarInteresados(): boolean {
        let valida = true;
        this.messageService.clear();

        this.proyecto.listaRecursos1.forEach(res => {
            if (this.estaVacio(res.nomArea)) {
                this.mostrarMensajeError('El interesado N° ' + res.idRecurso + ' requiere un área');
                valida = false;
            }
            if (res.area != 989 && this.estaVacio(res.nombre)) {
                this.mostrarMensajeError('El interesado N° ' + res.idRecurso + ' requiere un nombre');
                valida = false;
            }
            if (this.estaVacio(res.cargo)) {
                this.mostrarMensajeError('El interesado N° ' + res.idRecurso + ' requiere un cargo');
                valida = false;
            }
            if (this.estaVacio(res.rol)) {
                this.mostrarMensajeError('El interesado N° ' + res.idRecurso + ' requiere un rol');
                valida = false;
            }
        });
        return valida;
    }

    validarParticipantes(): boolean {
        let valida = true;
        this.messageService.clear();

        this.proyecto.listaRecursos2.forEach(res => {

            if (this.estaVacio(res.rol)) {
                this.mostrarMensajeError('El equipo N° ' + res.idRecurso + ' requiere un rol');
                valida = false;
            }
        });
        return valida;
    }

    //Interesados fin

    //Equipo Técnico comienzo
    agregar() {
        if (!this.validar()) {
            return;
        }
        let numero = this.comunicacionServicio.parametro - this.proyecto.listaRecursos2.length;
        let listaBase: dtoContratacion[] = [];
        this.proyecto.listaRecursos2.forEach(res => {
            var bean = new dtoContratacion();
            bean.idPersona = res.idPersona;
            bean.idContratacion = res.idContratacion;
            bean.persona = res.nombre;
            bean.cargo = res.cargo;
            bean.conocimientos = res.auxConocimientos;
            bean.listaPeriodos = res.listaPeriodos;
            bean.rol = res.rol;
            listaBase.push(bean);
        });
        this.SelectorEquipoComponent.iniciarComponente(listaBase, this.comunicacionServicio.numRecursos - numero,
            this.comunicacionServicio.proyectocomunica, this.proyecto.listaRecursos1);
    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();
        if (this.proyecto.listaRecursos1.length >= this.comunicacionServicio.numRecursos) {
            this.mostrarMensajeError('El número máximo de recursos es ' + this.comunicacionServicio.numRecursos);
            return;
        }
        if (this.comunicacionServicio.proyectocomunica.fechainicio == null) {
            this.mostrarMensajeInfo('Seleccione una fecha de inicio del proyecto');
            valida = false;
        }
        if (this.comunicacionServicio.proyectocomunica.fechafin == null) {
            this.mostrarMensajeInfo('Seleccione una fecha de fin del proyecto');
            valida = false;
        }
        if (this.comunicacionServicio.proyectocomunica.fechainicio != null && this.comunicacionServicio.proyectocomunica.fechafin != null) {
            if (this.comunicacionServicio.proyectocomunica.fechafin < this.comunicacionServicio.proyectocomunica.fechainicio) {
                this.mostrarMensajeError('La fecha fin del proyecto debe ser mayor que la de inicio');
                valida = false;
            }
        }
        return valida;
    }

    cargarEquipo(lista: any): number {
        const dtos: dtoContratacion[] = lista;
        var listaAuxiliar: Proyectorecurso[] = [];

        this.comunicacionServicio.parametro = this.comunicacionServicio.parametro - this.proyecto.listaRecursos2.length;
        this.proyecto.listaRecursos2 = [];
        dtos.forEach(seleccionado => {

            const participante: Proyectorecurso = new Proyectorecurso();
            participante.idRecurso = this.generarSecuencia(listaAuxiliar);
            participante.idPersona = seleccionado.idPersona;
            participante.idContratacion = seleccionado.idContratacion;
            participante.area = 989;
            participante.nombre = seleccionado.persona;
            participante.cargo = seleccionado.cargo;
            participante.auxConocimientos = seleccionado.conocimientos;
            participante.rol = seleccionado.rol;
            participante.listaPeriodos = seleccionado.listaPeriodos;
            listaAuxiliar.push(participante);
            this.comunicacionServicio.parametro = this.comunicacionServicio.parametro + 1;

        });
        this.proyecto.listaRecursos2 = listaAuxiliar;
        return 1;
    }

    listarPeriodos(event, registro: Proyectorecurso, overlaypanel: OverlayPanel) {
        overlaypanel.toggle(event);
        this.recurso.listaPeriodos = [];
        this.recurso.listaPeriodos = registro.listaPeriodos;
    }
    //Equipo Técnico fin

    borrarTodo(e) {
        if (e) {
            this.proyecto.listaRecursos1 = [];
            this.proyecto.listaRecursos2 = [];
            this.comunicacionServicio.borrar = false;
        }
    }

    borrarFila(numero: number, parametro) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: "Desea eliminar la fila " + numero + ' ?',
            accept: () => {
                if (parametro == 'I') {
                    let lst = [...this.proyecto.listaRecursos1];
                    lst = lst.filter(p => p.idRecurso !== numero);
                    this.enumerar(lst);
                    this.proyecto.listaRecursos1 = lst;
                }
                else {
                    let lst = [...this.proyecto.listaRecursos2];
                    lst = lst.filter(p => p.idRecurso !== numero);
                    this.enumerar(lst);
                    this.proyecto.listaRecursos2 = lst;
                }
                this.comunicacionServicio.parametro = this.comunicacionServicio.parametro - 1;
                if (this.comunicacionServicio.parametro < this.comunicacionServicio.numRecursos) {
                    this.comunicacionServicio.estadoEnabled = true;
                }
            }
        });
    }


}


