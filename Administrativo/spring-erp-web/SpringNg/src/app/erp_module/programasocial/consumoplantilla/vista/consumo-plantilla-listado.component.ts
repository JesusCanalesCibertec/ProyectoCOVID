import { Component, OnInit, ViewChild, ChangeDetectorRef, ViewChildren, Output } from '@angular/core';
import { SelectItem, LazyLoadEvent, ConfirmationService } from 'primeng/api';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router } from '@angular/router';
import { DataTable } from 'primeng/primeng';

import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { PsConsumoPlantillaService } from '../service/PsConsumoPlantillaService';
import { PsConsumoPlantilla } from '../dominio/PsConsumoPlantilla';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';



@Component({
    selector: 'app-name',
    templateUrl: './consumo-plantilla-listado.component.html'

})

export class PsConsumoPlantillalistadoComponent extends PrincipalBaseComponent implements OnInit {

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private router: Router,
        private confirmationService: ConfirmationService,
        private cdref: ChangeDetectorRef,
        private PsConsumoPlantillaService: PsConsumoPlantillaService,
        private psInstitucionServicio: PsInstitucionServicio,
        private empleadomastService: EmpleadomastServicio,
    ) { super(noAuthorizationInterceptor, messageService);
        this.filtro.estado= "A"; }

    filtro: DtoTabla = new DtoTabla();
    estados: SelectItem[] = [];
    instituciones: SelectItem[] = [];
    areas2: SelectItem[] = [];
    aplicaciones: SelectItem[] = [];
    listaArea: SelectItem[] = [];

    PsConsumoPlantilla: PsConsumoPlantilla;

    puedeEditar: Boolean = true;


    paginacion: any = new Object();
    @ViewChild(DataTable) dt: DataTable;

    filtroConRango: Boolean = false;
   
    sololetras: RegExp = /^[a-zA-Z\s]+$/;

   

    ngOnInit() {
        this.bloquearPagina();
        super.ngOnInit();
        this.cargarEstados();
        this.cargarInstituciones();
        this.cdref.detectChanges();

       


    }


    cargarPaginacion(event: LazyLoadEvent) {
        this.bloquearPagina();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;

        this.empleadomastService.obtenerInformacionPorIdPersonaUsuarioActual().then(
            empleado => {
                this.filtro.codigo = empleado.idInstitucion;

                this.PsConsumoPlantillaService.listarPaginacion(this.filtro)
                .then(
                    res => {
                        this.filtro.paginacion = res;
                        this.desbloquearPagina();
                    });
            }
        );

       

    }

    cargarEstados() {
        this.estados.push({ label: 'Activo', value: 'A' });
        this.estados.push({ label: 'Inactivo', value: 'I' });
    }
    
    cargarInstituciones() {
        this.instituciones.push({ label: 'Todos', value: null });
        this.psInstitucionServicio.listarInstituciones().then(
            res=>{
                res.forEach(obj => {
                    this.instituciones.push({label:obj.nombre, value:obj.idInstitucion})
                });
            }
        );
        
    }



    buscar(dt: any) {
        dt.reset();
    }
    defaultBuscar(event?: any) {
        if (event.keyCode === 13) {
            this.buscar(this.dt)
        }
    }


    // para abrir a nuestro mantenimiento y poder registrar uno nuevo.

    nuevo() {
        this.router.navigate(['spring/consumo-plantilla-mantenimiento']);
    }

    // se trabaja con el bean del paginado, en este caso nuestro dtoEmpleadoJoin
    obtener(bean: DtoTabla) {
        this.router.navigate(['spring/consumo-plantilla-mantenimiento',
            bean.codigoNumerico, bean.codigo, false], {
                skipLocationChange: true  });
    }
    ver(bean: DtoTabla) {
        this.router.navigate(['spring/consumo-plantilla-mantenimiento',
        bean.codigoNumerico, bean.codigo, true], { skipLocationChange: true });
    }


    onRowSelect(event) {

       this.ver(event.data);

    }

    

    // para eliminar con dto de nuestra lista
    anular(bean: DtoTabla) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaAnular(),
            accept: () => {
                this.bloquearPagina();
                this.PsConsumoPlantillaService.solicitudAnular(bean.codigo, bean.codigoNumerico).then(
                    respose => {
                        this.desbloquearPagina();
                        if (respose != null) {
                            this.messageService.clear();
                            this.messageService.add({
                                severity: 'info', summary: 'Información',
                                detail: this.getMensajeAnulado(bean.codigoNumerico)
                            });
                            this.dt.reset();
                        }

                    }
                );
            }
        });
    }




}

