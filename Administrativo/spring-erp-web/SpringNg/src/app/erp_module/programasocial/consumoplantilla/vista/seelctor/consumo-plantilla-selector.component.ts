import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { SelectItem, LazyLoadEvent } from 'primeng/primeng';

import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';

import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { BaseComponent } from 'src/app/base_module/components/basecomponent';
import { PsConsumoPlantilla } from '../../dominio/PsConsumoPlantilla';
import { PsConsumoPlantillaService } from '../../service/PsConsumoPlantillaService';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoConsumo } from '../../../consumo/dominio/dtoConsumo';
import { DtoConsumoNutricional } from '../../../consumonutricional/dominio/DtoConsumoNutricional';
import { DtoConsumoPlantilla } from '../../dominio/DtoConsumoPlantilla';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'consumo-plantilla-selector',
    templateUrl: './consumo-plantilla-selector.component.html'
})
export class ConsumoPlantillaSelectorComponent extends BaseComponent implements OnInit {

    tag: string = '';
    verSelector: Boolean = false;
    listaPlantilla: SelectItem[] = [];
    filtro: DtoTabla = new DtoTabla();
    registrosPorPagina: number = 7;

    institucion: string = null;

    seleccionados: DtoConsumoPlantilla[] = [];

    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @Output() cargarSeleccionEvent = new EventEmitter();

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private empleadomastServicio: EmpleadomastServicio,
        private psConsumoPlantillaService: PsConsumoPlantillaService) {
        super(messageService);
    }

    ngOnInit() {

    }
    cargarEstados() {
        this.listaPlantilla = [];
        this.listaPlantilla.push({ label: " -- Seleccione -- ", value: null });



        this.psConsumoPlantillaService.listarPlantillas(this.institucion).then(
            r => {
                if(r.length ==0){
                    this.verSelector = true;
                    return;
                }
                r.forEach(x => {
                    this.listaPlantilla.push({ label: x.descripcion, value: x.plantilla });
                    this.verSelector = true;
                    this.unBlock.emit();
                });
            }
        );
    }
    inicializarSelector(tag: string) {
        this.tag = tag;
        this.iniciarComponente();
    }
    iniciarComponente() {
        this.block.emit();
        this.filtro = new DtoTabla();

        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
            r => {
                
                
                this.institucion = r.idInstitucion;
                this.filtro.codigo =  r.idInstitucion;
                this.cargarEstados();

                return 1;
            });

        
        
        //this.listarDefecto();
    }

    aceptar() {
        if (this.filtro.codigoNumerico == null) {
            this.mostrarMensajeAdvertencia("Seleccione una plantilla");
            return;
        }
       

        const reg: any = new Object();
        reg.tag = this.tag;
        reg.data = this.filtro.paginacion.listaResultado;
        this.cargarSeleccionEvent.emit(reg);
        this.salir();
       
    }

    salir() {
        this.verSelector = false;
    }

    buscar(datatable?: any) {
        datatable.reset();
    }

    cargarCostCenter(event: LazyLoadEvent) {
        if (!this.verSelector) {
            return;
        }
        this.block.emit();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.psConsumoPlantillaService.listarPlantilla(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                
                this.unBlock.emit();
            });
    }

    listarDefecto() {

        this.filtro.paginacion.registroInicio = 0;
        this.filtro.paginacion.cantidadRegistrosDevolver = this.registrosPorPagina;
        this.psConsumoPlantillaService.listarPlantilla(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.unBlock.emit();
                this.verSelector = true;
            });
    }

    preBuscar(event?: any, tb?: any) {
        if (event.keyCode === 13) {
            this.buscar(tb);
        }
    }

}
