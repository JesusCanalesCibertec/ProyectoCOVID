import { BaseComponent } from '../../../../../base_module/components/basecomponent';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { DtoPsBeneficiario } from '../dominio/DtoPsBeneficiario';
import { SelectItem, DataTable, LazyLoadEvent } from 'primeng/primeng';

import { CompanyownerServicio } from '../../../companyowner/servicio/CompanyownerServicio';
import { AcSucursalService } from '../../../sucursal/servicio/acsucursal.service';
import { MaUnidadNegocioService } from '../../../unidadnegocio/servicio/maunidadnegocio.service';
import { NoAuthorizationInterceptor } from '../../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { element } from 'protractor';
import { ISelectorComponent } from '../../../../../base_module/components/selector.component';
import { PsInstitucionServicio } from 'src/app/erp_module/programasocial/institucion/service/PsInstitucionServicio';
import { FiltroBeneficiario } from 'src/app/erp_module/programasocial/beneficiario/dominio/filtroBeneficiario';
import { PsBeneficiarioServicio } from 'src/app/erp_module/programasocial/beneficiario/service/psBeneficiarioServicio';
import { PsInstitucionAreaServicio } from 'src/app/erp_module/programasocial/institucion_area/servicio/PsInstitucionAreaServicio';
import { PsInstitucion } from 'src/app/erp_module/programasocial/institucion/dominio/PsInstitucion';
import { PsInstitucionArea } from 'src/app/erp_module/programasocial/institucion_area/dominio/PsInstitucionArea';

@Component({
    selector: 'app-psbeneficiariomultiple-selector',
    templateUrl: './psbeneficiariomultipleselector.component.html'
})
export class PsBeneficiarioMultipleSelectorComponent extends BaseComponent implements OnInit, ISelectorComponent {

    @ViewChild(DataTable) dataTableComponent: DataTable;

    tag: string = '';
    verSelector: boolean = false;
    max: number = 100000;

    filtro: FiltroBeneficiario;
    registroSeleccionado: DtoPsBeneficiario;
    verListado: boolean = false;

    lstCompania: SelectItem[] = [];
    lstSucursal: SelectItem[] = [];
    lstUnidad: SelectItem[] = [];
    lstEstado: SelectItem[] = [];
    lstInstituciones: SelectItem[] = [];

    verSelector2: boolean = false;

    seleccionados: DtoPsBeneficiario[] = [];

    @Output() block = new EventEmitter();
    @Output() unBlock = new EventEmitter();
    @Output() cargarSeleccionEvent = new EventEmitter();
    lstSexo: SelectItem[] = [];
    constructor(
        private psInstitucionAreaServicio: PsInstitucionAreaServicio,
        private PsBeneficiarioService: PsBeneficiarioServicio,
        private companyownerService: CompanyownerServicio,
        private acSucursalService: AcSucursalService,
        private maUnidadNegocioService: MaUnidadNegocioService,
        private psInstitucionServicio: PsInstitucionServicio,
        messageService: MessageService) {
        super(messageService);
    }

    ngOnInit() {
        this.lstSexo.push({ label: " -- Todos --", value: null });
        this.lstSexo.push({ label: "Masculino", value: 'M' });
        this.lstSexo.push({ label: "Femenino", value: 'F' });
        this.cargarSucursal();
        this.filtro = new FiltroBeneficiario();
        this.registroSeleccionado = new DtoPsBeneficiario();
        this.verSelector = false;
    }

    inicializarSelector(tag: string) {
        this.iniciarComponente(tag, [], 1000000);
    }

    iniciarComponente(tag: string, listaPrevia: DtoPsBeneficiario[], max: number) {
        this.block.emit();
        listaPrevia = listaPrevia == undefined || listaPrevia == null ? [] : listaPrevia.constructor === Array ? listaPrevia : [];

        this.tag = tag;
        this.seleccionados = listaPrevia;
        this.max = max == undefined || max == null ? 0 : max;
        this.cargarInstituciones();

        this.filtro = new FiltroBeneficiario();

        //this.cargarCombos(() => this.llenarFiltros());
        this.unBlock.emit();
        this.verListado = false;
        this.verSelector = true
    }

    eliminar(dto: DtoPsBeneficiario) {

        var listaNoElminar = [];

        for (var i = 0; i < this.seleccionados.length; i++) {

            if (this.seleccionados[i].idBeneficiario == dto.idBeneficiario && this.seleccionados[i].idBeneficiario == dto.idBeneficiario) {

            }
            else {

                listaNoElminar.push(this.seleccionados[i]);
            }
        }

        this.seleccionados = listaNoElminar;
    }
    /*
    llenarFiltros() {
        this.PsBeneficiarioService.obtenerInformacionPorIdPersonaUsuarioActual().then(
            Beneficiario => {
                
                this.filtro.idSucursal = Beneficiario.sucursalId;
                this.filtro.idCompaniaSocio = Beneficiario.companiaId;
                this.filtro.BeneficiarioJefe = Beneficiario.personaId;
                this.unBlock.emit();
                //this.dataTableComponent.reset();
            }
        );
    }
    

    cargarCombos(callBack?: () => void): void {
        this.verSelector = true;
        this.lstSucursal = [];
        this.lstSucursal.push({ label: ' -- Todos -- ', value: '' });
        const p1 = this.acSucursalService.listarTodos().then(respuesta => {
            respuesta.forEach(obj => this.lstSucursal.push({ label: obj.descripcionlocal, value: obj.sucursal.trim() }));
        });

        this.lstUnidad = [];
        this.lstUnidad.push({ label: ' -- Todos -- ', value: '' });
        const p3 = this.maUnidadNegocioService.listarTodos().then(respuesta => {
            respuesta.forEach(obj => this.lstUnidad.push({ label: obj.descripcionlocal, value: obj.unidadnegocio.trim() }));
            this.filtro.idUnidadNegocioAsignada = '';
        });

        this.lstCompania = [];
        this.lstCompania.push({ label: ' -- Todos -- ', value: '' });
        const p4 = this.companyownerService.listarCompaniasPorSeguridad()
            .then(td => {
                td.forEach(obj => this.lstCompania.push({ label: obj.descripcion, value: obj.codigo }));
            });

        Promise.all([p1, p3, p4]).then(
            r => {
                this.verSelector = true;
                this.dataTableComponent.reset();
                if (callBack) { callBack(); }
            }
        );
    }
    */
    lstArea: SelectItem[] = [];
    lstAreaBean: PsInstitucionArea[] = [];
    cargarSucursal(): any {
        this.lstArea = [];
        this.lstArea.push({ label: ' -- Todos -- ', value: '' });
        this.psInstitucionAreaServicio.listarTodo().then(respuesta => {
            this.lstAreaBean = respuesta;
            respuesta.forEach(obj => this.lstArea.push({ label: obj.nombre, value: obj.idArea }));
        });
    }

    cargarInstituciones() {
        this.lstInstituciones = [];
        this.lstInstituciones.push({ label: '--Todos--', value: null });
        return this.psInstitucionServicio.listarInstituciones()
            .then(td => {
                td.forEach(obj => this.lstInstituciones.push({ label: obj.nombre, value: obj.idInstitucion }));
                return 1;
            });
    }

    cargarAras() {
        this.lstArea = [];
        this.lstArea.push({ label: ' -- Todos -- ', value: '' });
        this.lstAreaBean.filter(x => x.idInstitucion == this.filtro.institucion || this.filtro.institucion == null).forEach(obj => this.lstArea.push({ label: obj.nombre, value: obj.idArea }));
    }
    /*
    cargarUnidad() {
        this.lstUnidad = [];
        this.lstUnidad.push({ label: ' -- Todos -- ', value: '' });
        this.maUnidadNegocioService.listarTodos().then(respuesta => {
            respuesta.forEach(obj => this.lstUnidad.push({ label: obj.descripcionlocal, value: obj.unidadnegocio.trim() }));
            this.filtro.idUnidadNegocioAsignada = '';
        });
    }

    cargarCompania() {
        this.lstCompania = [];
        this.lstCompania.push({ label: ' -- Todos -- ', value: '' });
        this.companyownerService.listarCompaniasPorSeguridad()
            .then(td => {
                td.forEach(obj => this.lstCompania.push({ label: obj.descripcion, value: obj.codigo }));
            });
    }

    */

    onRowSelect(event: any) {
        /*
        if (this.seleccionados.length >= this.max) {
            this.mostrarMensajeAdvertencia("El máximo número de Beneficiarios es " + this.max);
            return;
        }*/
        const flag = event.flag == undefined ? false : event.flag;
        const reg = event.data;

        var duplicado = this.seleccionados.find(s => s.idBeneficiario == reg.idBeneficiario);

        if (duplicado == undefined) {
            let lst = [...this.seleccionados];
            lst.push(reg);
            this.seleccionados = lst;
        }
        else if (!flag) {
            this.mostrarMensajeAdvertencia("El Beneficiario ya ha sido ingresado a la lista");
        }
    }

    aceptar() {
        const reg: any = new Object();
        reg.tag = this.tag;
        reg.data = this.seleccionados;
        this.cargarSeleccionEvent.emit(reg);
        this.salir();
    }

    salir() {
        this.verSelector = false;
        this.unBlock.emit();
    }

    buscar(datatable?: any) {
        this.verListado = true;
        datatable.reset();
    }

    cargarListadoEvent(event: LazyLoadEvent) {
        if (!this.verSelector) {
            return;
        }

        this.block.emit();
        this.filtro.paginacion.listaResultado = [];
        this.filtro.paginacion.registroInicio = event.first;
        this.filtro.paginacion.cantidadRegistrosDevolver = event.rows;
        this.PsBeneficiarioService.listarPaginacionBeneficiarioSelector(this.filtro)
            .then(pg => {
                this.filtro.paginacion = pg;
                this.unBlock.emit();
                // this.verSelector2 = this.verSelector;
            });
    }
    quitarTodos() {
        this.seleccionados = [];
    }
    todos() {
        this.block.emit();

        var filtro2 = new FiltroBeneficiario();
        filtro2.area = this.filtro.area;
        filtro2.codigo = this.filtro.codigo;
        filtro2.desdeNac = this.filtro.desdeNac;
        filtro2.desdeReg = this.filtro.desdeReg;
        filtro2.dni = this.filtro.dni;
        filtro2.edad = this.filtro.edad;
        filtro2.estado = this.filtro.estado;
        filtro2.hastaNac = this.filtro.hastaNac;
        filtro2.hastaReg = this.filtro.hastaReg;
        filtro2.institucion = this.filtro.institucion;
        filtro2.nombre = this.filtro.nombre;
        filtro2.programa = this.filtro.programa;
        filtro2.sexo = this.filtro.sexo;

        filtro2.paginacion.listaResultado = [];
        filtro2.paginacion.registroInicio = 0;
        filtro2.paginacion.cantidadRegistrosDevolver = 10000000;
        this.PsBeneficiarioService.listarPaginacionBeneficiarioSelector(filtro2)
            .then(pg => {

                pg.listaResultado.forEach(
                    rr => {
                        this.onRowSelect({ data: rr, flag: true });
                    }
                );

                this.unBlock.emit();
            });
    }

}








