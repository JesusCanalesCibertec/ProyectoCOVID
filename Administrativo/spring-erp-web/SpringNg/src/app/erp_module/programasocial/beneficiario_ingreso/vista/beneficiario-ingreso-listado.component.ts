import { Component, OnInit, EventEmitter, Output, ViewChild, ChangeDetectorRef } from '@angular/core';

import { DataTable, LazyLoadEvent, ConfirmationService, SelectItem } from 'primeng/primeng';


import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from 'src/app/erp_module/shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { DtoBeneficiarioIngreso } from '../dominio/DtoBeneficiarioIngreso';
import { PsBeneficiarioIngreso, PsBeneficiarioIngresoPk } from '../../beneficiario/dominio/psbeneficiarioingreso';
import { PsBeneficiarioIngresoService } from '../service/PsBeneficiarioingresoService';
import { DtoBeneficiario } from '../../beneficiario/dominio/dtoBeneficiario';
import { MessageService } from 'primeng/components/common/messageservice';






@Component({
    selector: 'app-beneficiario-ingreso-listado',
    templateUrl: './beneficiario-ingreso-listado.component.html'
})

export class PsBeneficiarioIngresoListadoComponent extends PrincipalBaseComponent implements OnInit {


    @ViewChild(DataTable) dataTableComponent: DataTable;
    
    puedeEditar: Boolean = true;
    verSelector: Boolean = false;
    verModalFolio: boolean;
    listado : DtoBeneficiarioIngreso[];
    tipos : SelectItem[] = [];
    registrosPorPagina: number = 5;

    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();

    PsBeneficiarioIngreso: PsBeneficiarioIngreso = new PsBeneficiarioIngreso();
    PsBeneficiarioIngresoPk: PsBeneficiarioIngresoPk = new PsBeneficiarioIngresoPk();
    DtoBeneficiarioIngreso : DtoBeneficiarioIngreso;
    titulo : string = null;
    idProceso : string = null;
    idTipoSeguridad: string = null;
    fechaactual: Date = null;

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();
    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private confirmationService: ConfirmationService,
        private cdref: ChangeDetectorRef,
        private PsBeneficiarioIngresoService: PsBeneficiarioIngresoService,
        private empleadomastServicio: EmpleadomastServicio,
        ) {super(noAuthorizationInterceptor, messageService);}

    ngOnInit() {
       
    }

    iniciarComponente(bean: DtoBeneficiario) {
        this.PsBeneficiarioIngreso.idBeneficiario = bean.idBeneficiario;
        this.PsBeneficiarioIngreso.idInstitucion = bean.idInstitucion;
        this.titulo = bean.beneficiario;
        this.dataTableComponent.reset();
        this.block.emit();
        this.listarDefecto();
    }

    listarDefecto() {
        this.PsBeneficiarioIngresoService.listado(this.PsBeneficiarioIngreso.idBeneficiario,
                                                  this.PsBeneficiarioIngreso.idInstitucion).then(pg => { 
                
                this.listado = pg;
                this.verSelector = true;
                this.unBlock.emit();

            });
    }

    salir() {
        this.verSelector = false;
    }

    /*****************************del modal***************************************/

    nuevo() {
        this.bloquearPagina();
        this.accion = this.ACCIONES.NUEVO;
        this.puedeEditar = true;
        this.PsBeneficiarioIngreso = new PsBeneficiarioIngreso();
        //this.PsBeneficiarioIngreso.idProceso = this.idProceso;
        
        this.verModalFolio = true;
        this.desbloquearPagina();  
    }

   

    estaVacio(cadena: string): boolean {
        if (cadena == null) {
            return true;
        }
        if (cadena.trim() === '') {
            return true;
        }
        return false;
    }



    // para eliminar con dto de nuestra lista
    anular(bean: DtoBeneficiarioIngreso) {
        this.confirmationService.confirm({
            header: 'Confirmación',
            icon: 'fa fa-question-circle',
            message: this.getMensajePreguntaAnular(),
            accept: () => {
                this.bloquearPagina();
                this.PsBeneficiarioIngresoService.solicitudAnular(bean).then(
                    respose => {
                        this.desbloquearPagina();

                        if (respose != null) {
                            this.messageService.clear();
                            this.messageService.add({
                                severity: 'info', summary: 'Información',
                                detail: this.getMensajeEliminado2(bean.idIngreso)
                            });
                            this.listarDefecto();
                        }

                    }
                );
            }
        });
    }





}



