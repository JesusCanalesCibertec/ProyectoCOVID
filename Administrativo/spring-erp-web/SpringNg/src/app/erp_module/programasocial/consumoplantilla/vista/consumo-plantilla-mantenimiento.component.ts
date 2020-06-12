import { Component, OnInit, ViewChild } from '@angular/core';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router, ActivatedRoute } from '@angular/router';
import { SelectItem } from 'primeng/primeng';
import { EmpleadomastServicio } from '../../../shared/selectores/empleado/servicio/EmpleadomastServicio';
import { DtoEmpleadoBasico } from '../../../shared/selectores/empleado/dominio/DtoEmpleadoBasico';
import { PsConsumoPlantilla, PsConsumoPlantillaPk } from '../dominio/PsConsumoPlantilla';
import { PsConsumoPlantillaDetalle } from '../dominio/PsConsumoPlantillaDetalle';
import { DtoTabla } from 'src/app/erp_module/shared/dominio/dto/DtoTabla';
import { PsConsumoPlantillaService } from '../service/PsConsumoPlantillaService';
import { PsItemSelectorComponent } from 'src/app/erp_module/shared/selectores/item/item-selector.component';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';


@Component({
    selector: 'app-name',
    templateUrl: './consumo-plantilla-mantenimiento.component.html'

})
export class PsConsumoPlantillamantenimientoComponent extends PrincipalBaseComponent implements OnInit {

    @ViewChild(PsItemSelectorComponent) PsItemSelectorComponent: PsItemSelectorComponent;

    solicitante: DtoEmpleadoBasico = new DtoEmpleadoBasico();

    PsConsumoPlantilla: PsConsumoPlantilla = new PsConsumoPlantilla();
    dtoPsConsumoPlantilla: DtoTabla = new DtoTabla();
    PsConsumoPlantillaDetalle: PsConsumoPlantillaDetalle[] = [];

    constructor(
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,

        private router: Router,
        /*para actualizar*/
        private route: ActivatedRoute,

        private PsConsumoPlantillaService: PsConsumoPlantillaService,
        private psInstitucionServicio: PsInstitucionServicio,
        private empleadomastServicio: EmpleadomastServicio,
        private empleadomastService: EmpleadomastServicio,
    ) { super(noAuthorizationInterceptor, messageService); }

    // combos
    estados: SelectItem[] = [];
    instituciones: SelectItem[] = [];


    // primary key
    PsConsumoPlantillaPk: PsConsumoPlantillaPk;

    /*para actualizar*/
    puedeEditar: Boolean = true;
    areaEditar: Boolean = true;
    preguntaEditar: Boolean = true;
    comentarioEditar: Boolean = true;
    insertarEditar: Boolean = true;
    true: Boolean = false;

    // inicializaciones
    plantilla: number = null;
    institucion: string = null;
    areaT;


    expresion: RegExp = /^[a-zA-Z0-9\s]+$/;



    ngOnInit() {

        super.ngOnInit();
        this.bloquearPagina();
        this.cargarEstados();
        this.cargarInstituciones();



        this.plantilla = this.route.snapshot.params['codigo'];
        this.institucion = this.route.snapshot.params['institucion'];
        const ver = this.route.snapshot.params['ver'];





        //this.nomaplicacion = this.dtoPsConsumoPlantilla.nomaplicacion;

        if (this.plantilla) {
            this.puedeEditar = ver !== 'true' ? true : false;
            this.PsConsumoPlantillaPk = new PsConsumoPlantillaPk();
            this.PsConsumoPlantillaPk.plantilla = this.plantilla;
            this.PsConsumoPlantillaPk.idInstitucion = this.institucion;
            this.editar();



            if (!this.puedeEditar) {
                this.accion = this.ACCIONES.VER;

            }

        }
        else {
            this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
                r => {
                    this.puedeEditar = true;
                    this.accion = this.ACCIONES.NUEVO;
                    this.PsConsumoPlantilla = new PsConsumoPlantilla();
                    this.PsConsumoPlantilla.idInstitucion = r.idInstitucion;
                    this.desbloquearPagina();

                    return 1;
                }
            );


        }


    }

    salir() {
        this.router.navigate(['spring/consumo-plantilla-listado']);
    }



    // CARGA COMBOS CON LA INFORMACIÓN REQUERIDA

    cargarEstados() {
        this.estados.push({ label: 'Activo', value: 'A' });
        this.estados.push({ label: 'Inactivo', value: 'I' });
    }

    cargarInstituciones() {
        this.instituciones.push({ label: 'Seleccione', value: null });
        this.psInstitucionServicio.listarInstituciones().then(
            res=>{
                res.forEach(obj => {
                    this.instituciones.push({label:obj.nombre, value:obj.idInstitucion})
                });
            }
        );
        
    }





    adicionarFila() {

        this.PsItemSelectorComponent.iniciarComponente();

    }

    cargar(bean: any) {

        const data = bean.data;

        var cadena = this.PsConsumoPlantilla.listadetalle.filter(p => p.idItem === data.idItem);

        if (cadena.length == 1) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El item seleccionado se encuentra en la lista' });
            return;
        }

        let lst = [...this.PsConsumoPlantilla.listadetalle];

        var detalle = new PsConsumoPlantillaDetalle();

        detalle.idItem = data.idItem;
        detalle.auxNombre = data.nomItem;

        lst.push(detalle);

        this.PsConsumoPlantilla.listadetalle = lst;

        this.PsItemSelectorComponent.salir();

    }



    borrarFila(cod: string) {


        let lst = [...this.PsConsumoPlantilla.listadetalle];

        lst = lst.filter(p => p.idItem !== cod);

        this.PsConsumoPlantilla.listadetalle = lst;


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
        this.PsConsumoPlantillaService.obtenerPorId(
            this.PsConsumoPlantillaPk.idInstitucion, this.PsConsumoPlantillaPk.plantilla
        ).then(
            res => {
                this.PsConsumoPlantilla = res;

                console.log(res);

            }
        );
        this.desbloquearPagina();
    }


    guardar() {

        if (!this.validar()) {
            return;
        }




        if (this.accion === this.ACCIONES.NUEVO) {
            this.bloquearPagina();
            this.PsConsumoPlantillaService.registrar(this.PsConsumoPlantilla).then(

                resultado => {
                    // valida el null seteado de nuestro uexception
                    if (resultado != null) {
                        this.desbloquearPagina();
                        this.mostrarMensajeExito(this.getMensajeGrabado(resultado.plantilla));
                        this.router.navigate(['spring/consumo-plantilla-listado']);

                    }
                });
            // entra si la acción es diferente a la acción del botón  "NUEVO"
        } else {
            this.bloquearPagina();
            this.PsConsumoPlantillaService.actualizar(this.PsConsumoPlantilla).then(resultado => {
                this.desbloquearPagina();
                if (resultado != null) {
                    this.desbloquearPagina();
                    this.router.navigate(['spring/consumo-plantilla-listado']);
                    this.mostrarMensajeExito(this.getMensajeActualizado(resultado.plantilla));
                }
            });
        }




    }









    validar(): boolean {
        let valida = true;
        this.messageService.clear();
        if (this.estaVacio(this.PsConsumoPlantilla.descripcion)) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'La descripción de la plantilla es requerida' });
            valida = false;
        }
        if (this.PsConsumoPlantilla.listadetalle.length < 1) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Adicione al menos un item' });
            valida = false;
        }

        return valida;
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

}

