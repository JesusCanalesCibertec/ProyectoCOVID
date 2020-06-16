import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompaniaRecursosService } from '../servicio/companiarecursos.service';
import { SelectItem } from 'primeng/primeng';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MaMiscelaneosdetalleServicio } from '../../../shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { MaMiscelaneosdetalle, MaMiscelaneosdetallePk } from '../../../shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-companiarecursosmantenimiento',
    templateUrl: './companiarecursos_mantenimiento.component.html'
})
export class CompaniaRecursosMantenimientoComponent extends PrincipalBaseComponent implements OnInit {
    constructor(
        private rutaActiva: ActivatedRoute,
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        private maMiscelaneoDetalleService: MaMiscelaneosdetalleServicio,
        private router: Router, messageService: MessageService
    ) { super(noAuthorizationInterceptor, messageService); }

    recurso: MaMiscelaneosdetalle;
    estados: SelectItem[] = [];

    ngOnInit(): void {
        super.ngOnInit();
        this.recurso = new MaMiscelaneosdetalle();
        const dtoMaMiscelaneo = JSON.parse(this.obtenerParametroRutaPorClave(this.rutaActiva, 'dtoMaMiscelaneo'));

        this.buscar(dtoMaMiscelaneo);
        this.cargarEstados();
    }

    cargarEstados() {
        this.estados.push({ label: '--Seleccione--', value: '' });
        this.estados.push({ label: 'Activo', value: 'A' });
        this.estados.push({ label: 'Inactivo', value: 'I' });
    }

    buscar(maMiscelaneosdetalle: MaMiscelaneosdetalle) {

        const pk = new MaMiscelaneosdetallePk();
        pk.codigoelemento = maMiscelaneosdetalle.codigoelemento;
        pk.aplicacioncodigo = maMiscelaneosdetalle.aplicacioncodigo;
        pk.codigotabla = maMiscelaneosdetalle.codigotabla;
        pk.compania = maMiscelaneosdetalle.compania;

        this.maMiscelaneoDetalleService.obtenerPorId(pk).then(
            resp => this.recurso = resp
        );
    }

    guardar() {
        this.maMiscelaneoDetalleService.actualizar(this.recurso).then(
            tx => {
                this.messageService.clear();
                this.mostrarMensaje('Se Actualizó la compañia : ' + tx.codigoelemento, 'info');
            }).catch(error => {
                this.desbloquearPagina();
            });
    }

    salir() {
        this.router.navigate(['spring/companiarecursos/listado'], { skipLocationChange: true });
    }

}

