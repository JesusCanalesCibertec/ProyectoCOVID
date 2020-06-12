import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { MaMiscelaneosdetalleServicio } from '../servicio/MaMiscelaneosdetalleServicio';
import { MaMiscelaneosdetalle } from '../dominio/MaMiscelaneosdetalle';
import { BaseComponent } from 'src/app/base_module/components/basecomponent';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-popup-mantenimiento',
    templateUrl: './miscelaneo-mantenimiento-popup.component.html'
})
export class PopUpMantenimientoMiscelaneoComponent extends BaseComponent implements OnInit {
    constructor(
        private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
        messageService: MessageService,
    ) { super(messageService); }
    maMiscelaneosdetalle: MaMiscelaneosdetalle = new MaMiscelaneosdetalle();
    verModal: Boolean = false;
    tipo: string;

    @Output() cargarSeleccionEvent = new EventEmitter();
    @Output() cargarSeleccionPorTipoEvent = new EventEmitter();

    ngOnInit(): void {

    }


    mostrarVentana(maMiscelaneosdetalle: MaMiscelaneosdetalle, tipo: string) {
        this.verModal = true;
        this.tipo = tipo;
        this.maMiscelaneosdetalle = maMiscelaneosdetalle;
    }

    guardar() {
        if (!this.validar()) {
            this.desbloquearPagina();
            return;
        }

        this.maMiscelaneosdetalleServicio.registrar(this.maMiscelaneosdetalle).then(
            resp => {
                this.verModal = false;
                if (this.tipo === 'PRO' || this.tipo === 'TEC') {
                    this.cargarSeleccionPorTipoEvent.emit();
                } else {
                    this.cargarSeleccionEvent.emit(this.maMiscelaneosdetalle);
                }

            }
        );
    }


    validar() {
        this.messageService.clear();

        let valida = true;

        if (this.maMiscelaneosdetalle.codigoelemento == null || this.maMiscelaneosdetalle.codigoelemento === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Código Elemento es requerido' });
            valida = false;
        }

        if (this.maMiscelaneosdetalle.descripcionlocal == null || this.maMiscelaneosdetalle.descripcionlocal === undefined) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'El Descripcionlocal es requerido' });
            valida = false;
        }

        return valida;
    }


}
