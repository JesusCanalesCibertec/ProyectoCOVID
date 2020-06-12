import { Component, OnInit, ChangeDetectorRef, Output, EventEmitter, ViewChild } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { SelectItem, OverlayPanel, DataTable, ConfirmationService, LazyLoadEvent, SelectItemGroup } from 'primeng/primeng';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { ContratacionService } from '../../contratacion/servicio/contratacion.service';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { ConocimientoService } from '../../conocimiento/servicio/conocimiento.service';
import { dtoContratacion } from '../../contratacion/dominio/dtoContratacion';
import { MessageService } from 'primeng/components/common/messageservice';
import { DatePipe } from '@angular/common';
import { Proyectorecursoperiodo } from '../dominio/proyectorecursoperiodo';
import { Proyecto } from '../dominio/proyecto';
import { Proyectorecurso } from '../dominio/proyectorecurso';
import { Conocimiento } from '../../conocimiento/dominio/conocimiento';

@Component({
    selector: 'app-selector-equipo',
    templateUrl: './selector-equipo.component.html'
})
export class SelectorEquipoComponent extends PrincipalBaseComponent implements OnInit {

    @ViewChild(DataTable) dt: DataTable;

    filtro: DtoTabla = new DtoTabla();
    filtrofecha: Boolean = false;
    verSelector: boolean = false;
    listado: dtoContratacion[] = [];
    seleccionados: dtoContratacion[] = [];
    seleccionado: dtoContratacion = new dtoContratacion();
    numMaximo: number;
    proyecto: Proyecto = new Proyecto();
    titulo: string;
    titulo1: string;
    titulo2: string;
    verModal: Boolean = false;
    registroSeleccionado: dtoContratacion = new dtoContratacion();
    periodorecurso: Proyectorecursoperiodo = new Proyectorecursoperiodo();
    deshabilitarBoton: Boolean;
    oListaperiodos: Proyectorecursoperiodo[] = [];
    listaRecursos1: Proyectorecurso[] = [];


    cargos: SelectItem[] = [];
    conocimientos: SelectItem[] = [];
    niveles: SelectItem[] = [];
    conocimientosGrupo: SelectItemGroup[];
    flagnivel: Boolean;

    @Output() bloquear = new EventEmitter();
    @Output() desbloquear = new EventEmitter();
    @Output() cargarSeleccionados = new EventEmitter();

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private conocimientoServicio: ConocimientoService,
        private miscelaneoServicio: MaMiscelaneosdetalleServicio,
        private contratacionServicio: ContratacionService,
        private confirmationService: ConfirmationService
    ) {
        super(noAuthorizationInterceptor, messageService)
    }

    ngOnInit(): void {

    }

    iniciarComponente(listaPrevia: dtoContratacion[], numero: number, proyecto: Proyecto, auxListarecursos1: Proyectorecurso[]) {
        this.bloquear.emit();
        this.numMaximo = numero;
        this.proyecto = proyecto;
        this.listaRecursos1 = auxListarecursos1;

        var fecha = new DatePipe('en-US');
        this.titulo = 'SELECTOR DE RECURSOS  desde ' +
            fecha.transform(this.proyecto.fechainicio, 'dd/MM/yyyy') + ' hasta ' + fecha.transform(this.proyecto.fechafin, 'dd/MM/yyyy');

        this.nuevo();
        listaPrevia = listaPrevia == undefined || listaPrevia == null ? [] : listaPrevia.constructor === Array ? listaPrevia : [];
        this.filtro.paginacion.listaResultado = [];
        this.titulo1 = this.filtro.paginacion.listaResultado.length + ' Disponibles';
        this.titulo2 = listaPrevia.length + ' Seleccionados (máximo ' + this.numMaximo + ')';
        this.seleccionados = listaPrevia;
    }

    nuevo() {
        this.seleccionados = [];
        this.cargarNiveles();
        this.cargarCargos();
        this.cargarConocimientos();
        this.registroSeleccionado = new dtoContratacion();
        this.filtro = new DtoTabla();
        this.filtro.codigoNumerico = this.proyecto.idProyecto;
        this.filtro.fechainicio1 = this.proyecto.fechainicio;
        this.filtro.fechafin1 = this.proyecto.fechafin;
        this.filtro.secuencia = 8;
        this.filtrofecha = false;
        this.verSelector = true;
        this.deshabilitarBoton = true;
    }

    obtenerEstilosEditar(parametro: number) {
        if (parametro == 0) {
            return { 'pointer-events': 'none', opacity: 0.3 };
        } else {
            return { 'pointer-events': '', opacity: 10 };
        }
    }

    buscar(dt: any) {
        dt.reset();
    }

    listar(event: LazyLoadEvent) {
        if (!this.verSelector) {
            return;
        }
        this.bloquear.emit();
        this.deshabilitarBoton = true;
        this.registroSeleccionado = new dtoContratacion();

        if (!this.filtrofecha) {
            this.filtro.fechainicio2 = null;
            this.filtro.fechafin2 = null;
        }
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.recargar(this.filtro);
    }

    recargar(filtro: any) {
        this.contratacionServicio.ListarPersonaldisponible1(filtro).then(resultado => {
            this.filtro.paginacion = resultado;
            if (resultado != null) {

                let lista = new Array<dtoContratacion>();
                lista = this.filtro.paginacion.listaResultado;
                this.listaRecursos1.forEach(res => {
                    if (res.idPersona != null) {
                        lista = lista.filter(obj => obj.idPersona != res.idPersona);
                    }
                })
                this.filtro.paginacion.listaResultado = lista;

                this.deshabilitarBoton = false;
                this.filtro.paginacion.listaResultado.forEach(res => {
                    res.horas = this.filtro.secuencia;
                })

                if (this.seleccionados.length > 0) {
                    this.filtro.paginacion.listaResultado.forEach(res => {
                        let auxSeleccionado = this.seleccionados.find(obj => obj.idPersona == res.idPersona);

                        if (auxSeleccionado != undefined) {

                            auxSeleccionado.listaPeriodos.forEach(rd => {
                                res.diasocupados = this.afectar_dias(rd.fechainicio, rd.fechafin, res.diasocupados, 'bloquear');
                            });
                        }
                    });
                }
            }
            this.titulo1 = this.filtro.paginacion.registroEncontrados + ' Disponibles';
            this.desbloquear.emit();
        });
    }

    defaultBuscar(event?: any) {
        if (event.keyCode === 13) {
            this.buscar(this.dt);
        }
    }

    buscarChange() {
        this.bloquearNivel();
        this.buscar(this.dt);     
    }

    //combos inicio
    cargarCargos() {
        this.cargos = [];
        this.cargos.push({ value: null, label: '--Todos--' });
        this.miscelaneoServicio.listarActivos('MP', 'CARGOMIN').then(r => {
            r.forEach(
                rr => {
                    this.cargos.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
                });
            this.desbloquear.emit();
        });
    }

    cargarConocimientos() {
        this.conocimientosGrupo = [];
        let filtro = new DtoTabla();
        filtro.paginacion.registroInicio = 0;
        filtro.paginacion.cantidadRegistrosDevolver = 10000000;
        filtro.estado = 'A';
        this.conocimientosGrupo.push({ items: [{label:'--Todos--',value: null}], label: '--Todos--' });
        this.conocimientoServicio.listarPaginacion(filtro).then(r => {
            r.listaResultado.forEach(
                rr => {
                    let data = this.conocimientosGrupo.find(obj => obj.label == rr.cabecera);
                    if (data === undefined) {
                        this.conocimientosGrupo.push(
                            {
                                label: rr.cabecera,
                                items: this.cargarParte(rr.cabecera, r.listaResultado)
                            })
                    }
                });
                this.conocimientosGrupo.sort((a, b) => a.label.localeCompare(b.label));
                this.bloquearNivel();
        });
    }

    cargarParte(cabecera: string, lista: any[]) {
        this.conocimientos = [];
        lista.forEach(res => {
            if (res.cabecera == cabecera) {
                this.conocimientos.push({ label: res.nombre, value: res.idConocimiento })
            }
        })
        return this.conocimientos;
    }

    bloquearNivel(){
        if(this.estaVacioNumber(this.filtro.valorNumerico)){
            this.filtro.valor1 = null;
            this.flagnivel = true;
        }else{
            this.flagnivel = false;
        }
    }

    cargarNiveles() {
        this.niveles = [];
        this.niveles.push({ value: null, label: '--Todos--' })
        this.miscelaneoServicio.listarActivos('MP', 'NIVELCON')
            .then(res => {
                res.forEach(obj => this.niveles.push({ value: obj.codigoelemento, label: obj.descripcionlocal }))
            });
    }
    //combos fin
    aceptar() {
        if (!this.validarAceptar()) {
            return;
        }
        this.cargarSeleccionados.emit(this.seleccionados);
        this.salir();
    }

    salir() {
        this.verSelector = false;
    }

    seleccionar() {
        let seleccion = this.registroSeleccionado;
        if (seleccion.idPersona == null) {

            this.mostrarMensajeInfo("Seleccione un personal");
            return;

        } else {
            if (seleccion.rango == null) {
                this.mostrarMensajeError("El personal seleccionado requiere un rango de días");
                return;
            }

            if (!this.validarPeriodos(seleccion)) {
                this.mostrarMensajeError('Rango de fecha inválido');
                this.mostrarMensajeInfo('Seleccione un rango de días consecutivos.');
                return;
            }

            let oPeriodo = new Proyectorecursoperiodo();
            oPeriodo.fechainicio = seleccion.rango[0];
            oPeriodo.fechafin = seleccion.rango[1];
            oPeriodo.horas = seleccion.horas;
            oPeriodo.idPersona = seleccion.idPersona;
            oPeriodo.idContratacion = seleccion.idContratacion;

            var duplicado = this.seleccionados.find(s => s.idPersona == seleccion.idPersona);

            if (duplicado == undefined) {
                if (this.seleccionados.length == this.numMaximo) {
                    this.mostrarMensajeInfo('Máximo permitido ' + this.numMaximo);
                    return;
                }

                seleccion.listaPeriodos = [];
                oPeriodo.idPeriodo = this.generarSecuencia(seleccion.listaPeriodos);
                seleccion.listaPeriodos.push(oPeriodo);

                let lst = [...this.seleccionados];
                lst.push(seleccion);
                this.seleccionados = lst;
                this.mostrarMensajeExito("El personal con su periodo se agregaron correctamente");
                this.titulo2 = this.seleccionados.length + ' Seleccionados (máximo ' + this.numMaximo + ')';

            } else {

                let lst = [...this.seleccionados];
                let auxlista = lst.find(s => s.idPersona == seleccion.idPersona).listaPeriodos;
                auxlista.push(oPeriodo);
                auxlista = auxlista.sort((a, b) => Number(a.fechainicio) - Number(b.fechainicio));

                //reordenamos y enumeramos
                let numero = 1;
                auxlista.forEach(res => {
                    res.idPeriodo = numero;
                    numero = numero + 1;
                });
                /*************************/

                lst.find(s => s.idPersona == seleccion.idPersona).listaPeriodos = auxlista;
                this.seleccionados = lst;
                this.mostrarMensajeExito("El periodo se ha agregado correctamente");
            }

            this.filtro.paginacion.registroInicio = this.filtro.paginacion.registroInicio - 1;
            this.recargar(this.filtro);
        }
    }

    eliminarSeleccion(seleccion: dtoContratacion) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: "Desea retirar al personal " + seleccion.persona + '?',
            accept: () => {
                let lst = [...this.seleccionados];
                lst = lst.filter(p => p.idPersona !== seleccion.idPersona);
                this.seleccionados = lst;
                this.titulo2 = this.seleccionados.length + ' Seleccionados (máximo ' + this.numMaximo + ')';

                this.filtro.paginacion.registroInicio = this.filtro.paginacion.registroInicio - 1;
                this.recargar(this.filtro);
            }
        });
    }

    eliminarPeriodo(registro: Proyectorecursoperiodo) {

        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: "Desea eliminar el periodo N° " + registro.idPeriodo + '?',
            accept: () => {
                let lst = [...this.seleccionados];
                let auxSeleccionado = lst.find(obj => obj.idPersona === registro.idPersona);

                let dtlst = [...auxSeleccionado.listaPeriodos];
                dtlst = dtlst.filter(obj => obj.idPeriodo != registro.idPeriodo);

                //reordenamos y enumeramos
                let numero = 1;
                dtlst.forEach(res => {
                    res.idPeriodo = numero;
                    numero = numero + 1;
                });
                /*************************/

                lst.find(obj => obj.idPersona == registro.idPersona).listaPeriodos = dtlst;

                this.seleccionado.listaPeriodos = [];
                dtlst.forEach(res => {
                    this.seleccionado.listaPeriodos.push(res);
                })

                this.filtro.paginacion.registroInicio = this.filtro.paginacion.registroInicio - 1;
                this.recargar(this.filtro);
            }
        });
    }

    validarPeriodos(registro: dtoContratacion): boolean {
        let valida = true;
        this.messageService.clear()
        let count = this.diferenciadias(registro.rango[0], registro.rango[1]) + 1;
        for (let index = 0; index <= count; index++) {
            let auxfecha = new Date();
            auxfecha = this.addDate("d", index, registro.rango[0]);
            let duplicado = registro.diasocupados.find(obj => Number(obj) == Number(auxfecha));
            if (duplicado != undefined) {
                valida = false;
                break;
            }
        }
        return valida;
    }

    validarAceptar() {
        let valida = true;
        this.messageService.clear()
        this.seleccionados.forEach(res => {
            if (res.listaPeriodos.length == 0) {
                this.mostrarMensajeError('El seleccionado ' + res.persona + ' requiere como mínimo un periodo');
                valida = false;
            }
        });
        return valida;
    }

    afectar_dias(desde: Date, hasta: Date, dias: Date[], modo: any): Date[] {
        let count = this.diferenciadias(desde, hasta) + 1;
        for (let index = 0; index <= count; index++) {

            let auxfecha = new Date();
            auxfecha = this.addDate("d", index, desde);

            let duplicado = dias.find(obj => obj == auxfecha);
            if (duplicado == undefined) {
                modo == 'bloquear' ? dias.push(auxfecha) : dias = dias.filter(obj => Number(obj) != Number(auxfecha));
            }
        }
        return dias;
    }

    listarPeriodos(event, dto: dtoContratacion, overlaypanel: OverlayPanel) {
        this.bloquear.emit();
        overlaypanel.toggle(event);
        this.seleccionado.listaPeriodos = [];
        let auxlista = this.seleccionados.find(obj => obj.idPersona === dto.idPersona).listaPeriodos;
        auxlista.forEach(res => {
            this.seleccionado.listaPeriodos.push(res);
            this.desbloquear.emit();
        })
    }

    generarSecuencia(lista: any[]): number {
        return lista.length == 0 ? 1 : lista.length + 1
    }
}


