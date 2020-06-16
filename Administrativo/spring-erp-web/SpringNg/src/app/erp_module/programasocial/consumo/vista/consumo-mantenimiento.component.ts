import { Component, OnInit, ViewChild } from '@angular/core';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router, ActivatedRoute } from '@angular/router';
import { EmpleadomastServicio } from '../../../shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from '../../../shared/selectores/empleado/dominio/DtoEmpleadoBasico';

import { DtoConsumo, DtoConsumoCarga } from '../dominio/dtoConsumo';
import { PsConsumoService } from '../service/PsConsumoService';

import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { DtoConsumoPlantilla } from '../../consumoplantilla/dominio/DtoConsumoPlantilla';

import { PsConsumoNutricional } from '../../consumonutricional/dominio/PsConsumoNutricional';
import { DtoConsumoNutricional } from '../../consumonutricional/dominio/DtoConsumoNutricional';
import { PsConsumoNutricionalService } from '../../consumonutricional/service/PsConsumoService';
import { PsConsumo, PsConsumoPk } from '../dominio/psConsumo';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { SelectItem } from 'primeng/primeng';
import { PsItemSelectorComponent } from 'src/app/erp_module/shared/selectores/item/item-selector.component';
import { getMonth } from 'ngx-bootstrap';
import { PsConsumoPlantillaService } from '../../consumoplantilla/service/PsConsumoPlantillaService';
import { ConsumoPlantillaSelectorComponent } from '../../consumoplantilla/vista/seelctor/consumo-plantilla-selector.component';



@Component({
    selector: 'app-name',
    templateUrl: './consumo-mantenimiento.component.html'

})
export class PsConsumomantenimientoComponent extends PrincipalBaseComponent implements OnInit {

    @ViewChild(PsItemSelectorComponent) PsItemSelectorComponent: PsItemSelectorComponent;
    @ViewChild(ConsumoPlantillaSelectorComponent) ConsumoPlantillaSelectorComponent: ConsumoPlantillaSelectorComponent;

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();

    psConsumo: PsConsumo = new PsConsumo();
    psConsumoNutricional: PsConsumoNutricional = new PsConsumoNutricional();
    dtoConsumo: DtoConsumo = new DtoConsumo();
    dtoConsumoNutricional: DtoConsumoNutricional = new DtoConsumoNutricional();
    dtoConsumoPlantilla: DtoConsumoPlantilla = new DtoConsumoPlantilla();

    listado: DtoConsumoNutricional[] = [];
    cambiarInstitucion: Boolean = false;

    cargarFoto(event: any) {

        console.log("subiendo");

        this.bloquearPagina();

        var files = event.files;

        if (files.length == 0) {
            this.desbloquearPagina();
            return;
        }

        var filePath = files[files.length - 1].name;
        var allowedExtensions = /(.xlsx)$/i;
        if (!allowedExtensions.exec(filePath)) {
            this.mostrarMensajeAdvertencia('Solo puede subir archivos con estas extensiones .xlsx');
            files[0].value = '';
            this.desbloquearPagina();
            return null;
        }
        
        var reader = new FileReader();
        reader.readAsDataURL(files[files.length - 1]);

        reader.onloadend = (evt) => {
            var result = reader.result as string;
            var inicio = result.indexOf(',') + 1;
            var fin = result.length;
            var archivo = result.substring(inicio, fin);
            var e = new DtoConsumoCarga();
            e.excel = archivo;

            this.psConsumoService.importar(e).then(
                r => {
                    let lst = [...this.listado];
                    r.forEach(reg => {
                        lst.push(reg);
                    });

                    this.listado = lst;

                    this.desbloquearPagina();
                }
            );

        };


    }

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,

        private router: Router,
        /*para actualizar*/
        private route: ActivatedRoute,

        private psConsumoService: PsConsumoService,
        private psConsumoPlantillaService: PsConsumoPlantillaService,
        private psConsumoNutricionalService: PsConsumoNutricionalService,
        private psInstitucionServicio: PsInstitucionServicio,
        private empleadomastServicio: EmpleadomastServicio,
        private maMiscelaneosdetalleService: MaMiscelaneosdetalleServicio
    ) {
        super(noAuthorizationInterceptor, messageService);
    }

    // combos
    instituciones: SelectItem[] = [];
    estados: SelectItem[] = [];
    aportantes: SelectItem[] = [];
    institucion: string = null;


    // primary key
    psConsumoPk: PsConsumoPk;

    /*para actualizar*/
    puedeEditar: Boolean = true;
    editacantidad: Boolean = true;
    preguntaEditar: Boolean = true;
    comentarioEditar: Boolean = true;
    insertarEditar: Boolean = true;
    true: Boolean = false;

    // inicializaciones
    nomaplicacion: string = null;
    listadoPlantilla: DtoConsumoPlantilla[];








    ngOnInit() {

        super.ngOnInit();
        this.bloquearPagina();
        this.cargarEstados();
        this.cargarAportantes();


        const p1 = this.cargarInstituciones();





        const codInstitucion = this.route.snapshot.params['codInstitucion'];
        const codConsumo = this.route.snapshot.params['codConsumo'];
        this.institucion = this.route.snapshot.params['codInstitucion'];



        Promise.all([p1]).then(f => {
            const ver = this.route.snapshot.params['ver'];

            if (codInstitucion) {
                this.puedeEditar = ver !== 'true' ? true : false;
                this.psConsumoPk = new PsConsumoPk();
                this.psConsumoPk.idInstitucion = codInstitucion;
                this.psConsumoPk.idConsumo = codConsumo;
                this.editar();

                if (!this.puedeEditar) {
                    this.accion = this.ACCIONES.VER;

                }

            } else {
                const institucion = this.route.snapshot.params['codigo'];
                this.accion = this.ACCIONES.NUEVO;
                this.psConsumo = new PsConsumo();
                this.psConsumo.idInstitucion = institucion;
                this.psConsumo.fechainicio = new Date();
                this.desbloquearPagina();
            }

        });

    }

    salir() {
        this.router.navigate(['spring/consumo-listado']);
    }

    puedeCambiarInstitucion() {
        this.psInstitucionServicio.flgSeleccionarInstitucion().then(
            resp => {
                this.cambiarInstitucion = this.flagABoolean(resp.valorFlag);
            }
        );
    }



    // CARGA COMBOS CON LA INFORMACIÓN REQUERIDA

    cargarInstituciones() {
        this.instituciones.push({ label: '--Seleccione--', value: null });
        return this.psInstitucionServicio.listarInstituciones()
            .then(td => {
                td.forEach(obj => this.instituciones.push({ label: obj.nombre, value: obj.idInstitucion }));
                return 1;
            });
    }


    cargarEstados() {
        this.estados.push({ label: 'Activo', value: 'A' });
        this.estados.push({ label: 'Inactivo', value: 'I' });
        this.estados.push({ label: 'Cerrado', value: 'C' });

    }

    cargarAportantes() {
        this.aportantes.push({ label: '--Seleccione--', value: null });
        this.aportantes.push({ label: 'Fundación', value: 'F' });
        this.aportantes.push({ label: 'Otros', value: 'O' });
    }


    /*
        cargarPlantilla() {
    
            this.bloquearPagina();
    
            let valida = true;
            this.messageService.clear();
    
            if (this.psConsumo.idInstitucion === undefined || this.psConsumo.idInstitucion == '') {
                this.mostrarMensajeAdvertencia('Seleccione una institución');
                valida = false;
                this.desbloquearPagina();
                return;
            }
    
    
            this.psConsumoPlantillaService.listarPlantilla(this.psConsumo.idInstitucion)
                .then(res => {
    
    
                    if (res.length == 0) {
                        this.mostrarMensajeAdvertencia('El instituto no cuenta con plantilla registrada');
                    }
                    let cantidad = 0;
                    if (this.listado.length != 0) {
    
    
    
                        let lst = [...this.listado];
    
                        for (let index = 0; index < res.length; index++) {
    
                            var registro = new DtoConsumoNutricional;
    
                            registro.codItem = res[index].codItem;
                            registro.nomItem = res[index].nomItem;
                            registro.nomUnidad = res[index].nomUnidad;
    
    
                            const cadena = this.listado.filter(p => p.codItem == registro.codItem);
    
    
                            if (cadena.length == 0) {
                                lst.push(registro);
                                cantidad = cantidad + 1;
                            }
    
                        }
                        if (cantidad == 0) {
                            this.mostrarMensajeAdvertencia('La plantilla se encuentra en la lista');
                        }
    
                        this.listado = lst;
                        this.desbloquearPagina();
                        return;
    
                    }
    
    
    
                    let lst = [...this.listado];
                    lst.push.apply(lst, res);
                    this.listado = lst;
    
    
    
    
                    this.desbloquearPagina();
                });
    
        }
    */


    borrarFila(cod: string) {


        let lst = [...this.listado];

        lst = lst.filter(p => p.codItem !== cod);

        this.listado = lst;

    }


    cargarUsuario() {
        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
            res => {
                this.solicitante = res;
                this.desbloquearPagina();
            }
        );
    }

    // para obtener y setear datos del empleado
    editar() {
        this.accion = this.ACCIONES.EDITAR;

        this.psConsumoService.obtenerPorId(this.psConsumoPk).then(
            res => {

                this.psConsumo = res;


            }
        );

        this.psConsumoNutricionalService.listardetalle(this.psConsumoPk).then(res => {
            this.listado = res;

        });

        this.desbloquearPagina();
    }


    guardar() {

        if (!this.validar()) {
            return;
        }
        if (!this.validarDetalle()) {
            return;
        }

        var fecha = getMonth(new Date());

        if (fecha < 7) {
            this.psConsumo.idPeriodo = 'S1';
        }
        else {
            this.psConsumo.idPeriodo = 'S2';
        }

        this.listado.forEach(
            res => {

                var registro = new PsConsumoNutricional();
                registro.idInstitucion = this.psConsumo.idInstitucion;
                registro.idItem = res.codItem;
                registro.cantidadsolicitada = res.cantidadsolicitada;
                registro.cantidadfundacion = res.cantidadfundacion;
                registro.cantidadotros = res.cantidadotros;
                registro.comentarios = res.comentarios;

                this.psConsumo.listadetalle.push(registro);
            }
        );



        if (this.accion === this.ACCIONES.NUEVO) {
            this.bloquearPagina();
            this.psConsumoService.registrar(this.psConsumo).then(
                resultado => {
                    if (resultado != null) {
                        this.desbloquearPagina();
                        this.mostrarMensajeExito(this.getMensajeGrabado(resultado.idConsumo));
                        this.router.navigate(['spring/consumo-listado']);
                    }
                });

        } else {
            this.bloquearPagina();
            this.psConsumoService.solicitudActualizar(this.psConsumo).then(resultado => {
                this.desbloquearPagina();
                if (resultado != null) {

                    this.desbloquearPagina();
                    this.router.navigate(['spring/consumo-listado']);
                    this.mostrarMensajeExito(this.getMensajeActualizado(resultado.idConsumo));
                }
            });
        }
    }

    validar(): boolean {
        let valida = true;
        this.messageService.clear();

        if (this.psConsumo.idInstitucion === undefined || this.psConsumo.idInstitucion == null) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Seleccione una institución' });
            valida = false;
        }

        if (this.psConsumo.fechainicio === undefined || this.psConsumo.fechainicio == null) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'la fecha inicial es requerida' });
            valida = false;
        }

        if (this.psConsumo.aportante === undefined || this.psConsumo.aportante == null) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La institución aportante es requerida' });
            valida = false;
        }
        if (this.estaVacio(this.psConsumo.descripcion)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La descripción del consumo es requerida' });
            valida = false;
        }

        if (this.listado.length == 0) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Agrege al menos un item' });
            valida = false;
        }
        return valida;
    }

    validarDetalle(): boolean {
        let valida = true;
        this.messageService.clear();


        this.listado.forEach(
            td => {

                if (td.cantidadsolicitada == null) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La cantidad solicitada de ' + td.nomItem + " es requerido" });
                    valida = false;
                }


            }
        );

        return valida;
    }

    valida(event) {




        if (event.cantidadsolicitada <= 0 && event.cantidadsolicitada != null) {

            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La cantidad solicitada debe ser mayor a 0' });
            event.cantidadsolicitada = null;
            this.editacantidad = true;
            return;

        }



        if (event.cantidadotros < 0) {

            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La cantidad otros debe ser mayor a 0' });
            this.editacantidad = true;
            event.cantidadotros = 0;
            return;
        }
        if (event.cantidadfundacion < 0) {

            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La cantidad fundacion debe ser mayor a 0' });
            this.editacantidad = true;
            event.cantidadfundacion = 0;
            return;
        }

        if (event.cantidadsolicitada == null && event.cantidadfundacion > 0) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La cantidad solicitada debe ser ingresada primero' });
            this.editacantidad = true;
            event.cantidadfundacion = 0;
            event.cantidadotros = 0;
            return;

        }
        if (event.cantidadfundacion == null) {
            this.editacantidad = true;
            event.cantidadfundacion = 0;
            event.cantidadotros = 0;
            return;
        }



        if (event.cantidadsolicitada > 0 && event.cantidadfundacion >= 0) {


            this.editacantidad = false;


        }

        if (event.cantidadfundacion == event.cantidadsolicitada) {

            event.cantidadotros = 0;
            this.editacantidad = true;


        }

        if (event.cantidadotros + event.cantidadfundacion > event.cantidadsolicitada) {

            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Excede la cantidad solicitada' });
            event.cantidadotros = null;
            this.editacantidad = false;
            return;

        }

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

    modalItem() {
        this.PsItemSelectorComponent.iniciarComponente();
    }

    modalPlantilla() {

        this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
            r => {
                this.institucion = r.idInstitucion;

                this.psConsumoPlantillaService.listarPlantillas(this.institucion).then(
                    r => {
                        if (r.length == 0 || r === undefined) {
                            this.mostrarMensajeAdvertencia("La institución no cuenta con plantillas registradas");
                            return;
                        }
                        this.ConsumoPlantillaSelectorComponent.iniciarComponente();
                    }
                );


            }
        );



    }

    cargar(bean: any) {

        const data = bean.data;

        var cadena = this.listado.filter(p => p.codItem === data.idItem);

        if (cadena.length == 1) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El item seleccionado se encuentra en la lista' });
            return;
        }

        let lst = [...this.listado];

        var detalle = new DtoConsumoNutricional();
        detalle.codItem = data.idItem;
        detalle.nomItem = data.nomItem;
        detalle.nomUnidad = data.nomUnidad;
        detalle.cantidadfundacion = 0;
        detalle.cantidadotros = 0;



        lst.push(detalle);

        this.listado = lst;


        this.PsItemSelectorComponent.salir();
    }

    cargarPlantilla(d: any) {
        const dtos: DtoConsumoNutricional[] = d.data;

        let lst = [...this.listado];
        dtos.forEach(res => {

            var encontrado = this.listado.find(d => d.codItem == res.codItem);

            if (encontrado == null || encontrado == undefined) {

                var detalle = new DtoConsumoNutricional();
                detalle.codItem = res.codItem;
                detalle.nomItem = res.nomItem;
                detalle.nomUnidad = res.nomUnidad;
                detalle.cantidadfundacion = 0;
                detalle.cantidadotros = 0;


                lst.push(detalle);



            }
        });

        this.listado = lst;


    }

}

