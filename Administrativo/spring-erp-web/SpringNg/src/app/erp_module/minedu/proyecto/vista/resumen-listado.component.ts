import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { CarService } from 'src/app/demo/service/carservice';
import { Car } from 'src/app/demo/domain/car';
import { SelectItem } from 'primeng/api';
import { Proyecto } from '../dominio/proyecto';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { ProyectoService } from '../servicio/Proyecto.service';
import { dtoProyecto } from '../dominio/dtoProyecto';
import { transition, trigger, state, style, animate } from '@angular/animations';
import { Proyectorecurso } from '../dominio/proyectorecurso';
import { DatePipe } from '@angular/common';


@Component({
    selector: 'app-name',
    templateUrl: './resumen-listado.component.html'
})
export class ResumenlistadoComponent extends PrincipalBaseComponent implements OnInit {

    listado: dtoProyecto[];
    dto: dtoProyecto = new dtoProyecto();
    lista: Proyectorecurso[] = [];
    estadoatencion: string = null;

    cols: any[] = [];
    selectedColumns: any[] = [];
    frozenCols: any[];
    scrollableCols: any[];


    estados: SelectItem[] = [];
    columnas: any[] = [];
    numFilas: number;

    expandedRows: {} = {};

    constructor(
        private proyectoServicio: ProyectoService,
        private miscelaneoServicio: MaMiscelaneosdetalleServicio,
        private cdref: ChangeDetectorRef,
        noAuthotizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
    ) { super(noAuthotizationInterceptor, messageService) }

    ngOnInit() {
        super.ngOnInit();
        this.bloquearPagina();
        this.cargarEstados();
        this.listar();
        this.cargarColumnas();
    }

    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }

    cargarColumnas() {
        this.cols = [
            { field: 'secuencia', header: 'N°', width: 50, orientacion: 'center' },
            { field: 'nomcortoSIS', header: 'Nombre corto APP', width: 100, orientacion: 'center' },
            { field: 'nombreSIS', header: 'Nombre largo APP', width: 250, orientacion: 'left' },
            { field: 'gestor', header: 'Líder de Proyecto', width: 250, orientacion: 'left' },
            { field: 'expediente', header: 'Expediente', width: 200, orientacion: 'left' },
            { field: 'auxfechaexpediente', header: 'Fecha de Expediente', width: 100, orientacion: 'center' },
            { field: 'nombre', header: 'Nombre de Proyecto', width: 350, orientacion: 'left' },
            { field: 'asunto', header: 'Descripción', width: 300, orientacion: 'left' },
            { field: 'proceso', header: 'Proceso que atiende', width: 250, orientacion: 'left' },
            { field: 'nomArea', header: 'Dep. Nivel 3', width: 100, orientacion: 'center' },
            { field: 'auxplanificado', header: 'Avance Planificado', width: 100, orientacion: 'center' },
            { field: 'auxreal', header: 'Avance Real', width: 100, orientacion: 'center' },
            { field: 'desviacion', header: 'Desviación', width: 100, orientacion: 'center' },
            { field: 'auxfechafin', header: 'Fecha Fin Probable', width: 100, orientacion: 'center' },
            { field: 'observacion', header: 'Observaciones', width: 300, orientacion: 'left' }
        ];

        this.selectedColumns = this.cols;
        this.columnas = this.selectedColumns;
    }

    listar() {
        this.proyectoServicio.listardetalle(null, null, this.estadoatencion).then(
            res => {
                this.listado = res;
                this.listado.forEach(registro => {
                    var fecha = new DatePipe('en-US');
                    registro.auxfechaexpediente = fecha.transform(registro.fechaexpediente, 'dd/MM/yyyy');
                    registro.auxfechafin = fecha.transform(registro.fechafin, 'dd/MM/yyyy');
                    registro.auxdesviacion = registro.desviacion == null ? '' : registro.desviacion + '%';
                    registro.auxplanificado = registro.planificado == null ? '' : registro.planificado + '%';
                    registro.auxreal = registro.real == null ? '' : registro.real + '%';
                });
                this.numFilas = this.listado.length;
                this.cerrarTodo();
                this.desbloquearPagina();
            }
        );
    }



    cargarEstados() {
        this.estados = [];
        this.miscelaneoServicio.listarActivos(Proyecto.APLICACION_CODIGO, Proyecto.MISCELANEO_ESTADO_PROYECTO).then(r => {
            r = r.sort((a, b) => Number(a.codigoelemento) - Number(b.codigoelemento));
            r.forEach(rr => {
                this.estados.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
            });
        });
    }

    buscarChange() {
        this.listar();
    }


    obtenerEstilos(parametro: number) {
        if (parametro > -15) {
            return { 'background-color': 'green', 'border-radius': '50%', 'width': '20px', 'height': '20px', 'margin': 'auto', 'border': '1px solid black' };
        } else if (parametro > -41 && parametro <= -15) {
            return { 'background-color': 'yellow', 'border-radius': '50%', 'width': '20px', 'height': '20px', 'margin': 'auto', 'border': '1px solid black' };
        } else {
            return { 'background-color': 'red', 'border-radius': '50%', 'width': '20px', 'height': '20px', 'margin': 'auto', 'border': '1px solid black' };
        }
    }

    mostrarEquipo(e) {
        this.bloquearPagina();
        this.proyectoServicio.ListarEquipotecnico(e.data.idproyecto).then(res => {
            if (res != null) {
                this.listado.find(obj => obj.idproyecto == e.data.idproyecto).listaEquipo = res;
                this.desbloquearPagina();
            }
        })
    }

    cerrarTodo() {
        const thisRef = this;
        this.listado.forEach(res => {
            thisRef.expandedRows[res.idproyecto] = 0;
        });
    }

    buscarCabecera() {
        if (this.selectedColumns.length < 8) {
            this.selectedColumns = [];
            this.selectedColumns.push({ field: 'secuencia', header: 'N°', width: 50, orientacion: 'center' })
            this.selectedColumns.push({ field: 'nomcortoSIS', header: 'Nombre corto APP', width: 100, orientacion: 'center' })
            this.selectedColumns.push({ field: 'nombreSIS', header: 'Nombre largo APP', width: 250, orientacion: 'left' })
            this.selectedColumns.push({ field: 'gestor', header: 'Líder de Proyecto', width: 250, orientacion: 'left' })
            this.selectedColumns.push({ field: 'nombre', header: 'Nombre de Proyecto', width: 350, orientacion: 'left' })
            this.selectedColumns.push({ field: 'asunto', header: 'Descripción', width: 300, orientacion: 'left' })
            this.selectedColumns.push({ field: 'nomArea', header: 'Dep. Nivel 3', width: 100, orientacion: 'center' })
            this.selectedColumns.push({ field: 'desviacion', header: 'Desviación', width: 100, orientacion: 'center' })
            this.mostrarMensajeInfo('Se requieren como mínimo 8 columnas');

            return;
        }
        console.log(this.selectedColumns);
    }



}
