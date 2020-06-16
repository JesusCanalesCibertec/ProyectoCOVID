import { Component, OnInit, ViewChild, ChangeDetectorRef, AfterContentChecked } from '@angular/core';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/api';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { PersonaService } from '../servicio/persona.service';
import { DtoListaFechas } from '../dominio/dtoListaFechas';
import { DatePipe } from '@angular/common';
import { Schedule, Calendar } from 'primeng/primeng';
import { Table } from 'primeng/table';


@Component({
    selector: 'ocupacion-listado',
    templateUrl: './ocupacion-listado.component.html',
})
export class OcupacionlistadoComponent extends PrincipalBaseComponent implements OnInit {

    @ViewChild(Schedule) dt: Schedule;
    @ViewChild(Table) tabla: Table;

    cols: any[] = [];
    frozenCols: any[] = [];
    scrollableCols: any[] = [];
    dias: string[] = [];
    parametro: Date;
    listado: DtoListaFechas[] = [];
    meses: string[] = [];
    cadena: string;

    verModal: Boolean = false;
    events: any[] = [];
    header: any;
    locale: any;
    options: any;
    titulo: string = '';
    cadenaMes: string;
    mes: number;

    constructor(
        public personaServicio: PersonaService,
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private cdref: ChangeDetectorRef,
    ) { super(noAuthorizationInterceptor, messageService) }

    ngOnInit(): void {
        let date = new Date();
        let primerDia = new Date(date.getFullYear(), date.getMonth(), 1);
        if (this.parametro == null || this.parametro == undefined) { this.parametro = primerDia }
        this.listar();
        this.dt.initialize();
        this.cargarMeses();
        this.cambiarMes(date.getMonth());
    }

    cargarColumnas() {
        let totaldias = new Date(this.parametro.getFullYear(), this.parametro.getMonth() + 1, 0).getDate()
        this.cols = [];
        this.frozenCols = [];
        this.scrollableCols = [];
        this.dias = [];

        this.frozenCols.push({ field: 'codigo', header: 'Código', width: 70, orientacion: 'center', alto: 66, rowspan: 2 })
        this.frozenCols.push({ field: 'idModalidad', header: 'Modalidad', width: 100, orientacion: 'center', alto: 66, rowspan: 2 })
        this.frozenCols.push({ field: 'nombre', header: 'Nombre', width: 300, alto: 66, rowspan: 2 })
        this.frozenCols.push({ field: 'detalle', header: 'Detalle', width: 70, alto: 66, rowspan: 2, border: '3px solid olive' })
        for (let index = 1; index <= totaldias; index++) {
            let fecha = new Date();
            fecha = this.addDate("d", index - 1, this.parametro);
            this.dias.push(new DatePipe('es').transform(fecha, 'EE'));
            this.scrollableCols.push(
                {
                    field: index < 10 ? 'dia0' + index : 'dia' + index,
                    header: index < 10 ? '0' + index : index, width: 50,
                    orientacion: 'center',
                    alto: 33,
                    indice: fecha.getDay()
                })
        }
    }

    listar() {
        this.bloquearPagina();
        this.personaServicio.ListarPersonal(this.parametro).then(res => {
            this.cargarColumnas();
            this.listado = res;
            this.cambiarMes(this.parametro.getMonth());
            this.desbloquearPagina();
        });
    }

    filtrar(cadena) {
        this.tabla.filter(cadena, 'nombre', 'contains');
    }

    // filtrarPersonal(cadena: string) {
    //     let lst = [...this.listado];
    //     lst = lst.filter(p => p.nombre !== parametro);
    //     lst = this.bloquearUltimo(lst);
    //     this.contratacion.listadetalle = lst;
    // }

    obtenerEstilosEditar(parametro: any, ancho: number, indice: number) {
        if (!isNaN(parametro) && ancho == 50) {
            if (parametro == null) {
                return indice == 0 || indice == 6 ? { 'background-color': 'silver' } : { 'background-color': '#009624' };
            }
            let numero = (100 / 8) * parametro;
            return { 'background': 'linear-gradient(to top, #43deff ' + numero + '%, transparent 0%, white)' };
        } else {
            return;
        }
    }

    cargarMeses() {
        this.meses[0] = "Enero";
        this.meses[1] = "Febrero";
        this.meses[2] = "Marzo";
        this.meses[3] = "Abril";
        this.meses[4] = "Mayo";
        this.meses[5] = "Junio";
        this.meses[6] = "Julio";
        this.meses[7] = "Agosto";
        this.meses[8] = "Septiembre";
        this.meses[9] = "Octubre";
        this.meses[10] = "Noviembre";
        this.meses[11] = "Diciembre";
    }

    cambiarMes(parametro: number) {
        this.mes = parametro;
        this.cadenaMes = this.meses[this.mes].toUpperCase();
    }

    slideListar() {
        let fecha = new Date(this.parametro.getFullYear(), this.mes, 1);
        this.parametro = fecha;
        this.listar();
    }

    //del modal
    llenar() {
        this.options = {
            defaultDate: this.parametro,
            header: {
                left: 'today',
                right: 'prev,next',
                center: 'title'
            },
            locale: 'es',
            editable: false,
            hiddenDays: [0, 6]
        }
    }

    detallar(dto: DtoListaFechas) {
        this.llenar();
        this.bloquearPagina();
        this.events = [];
        this.personaServicio.ListarEventos(dto.codigo).then(res => {
            if (res.length != 0) {
                this.titulo = dto.nombre;
                this.events = res;
                this.verModal = true;
            } else {
                this.mostrarMensajeInfo('No hay periodos registrados');
            }
            this.desbloquearPagina();
            this.dt.ngOnDestroy();
        });
    }
}
