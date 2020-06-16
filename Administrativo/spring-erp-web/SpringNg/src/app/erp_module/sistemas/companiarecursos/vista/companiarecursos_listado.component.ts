import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CompaniaRecursosService } from '../servicio/companiarecursos.service';
import { SelectItem, ConfirmationService } from 'primeng/primeng';
import { PrincipalBaseComponent } from '../../../../base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from '../../../../base_module/interceptor/NoAuthorizationInterceptor';
import { MaMiscelaneosdetalle, MaMiscelaneosdetallePk } from '../../../shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { MaMiscelaneosdetalleServicio } from '../../../shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { FiltroMiscelaneosDetalle } from '../../../shared/miscelaneos/dominio/filtromiscelaneosdetalle';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'app-companiarecursoslistado',
    templateUrl: './companiarecursos_listado.component.html'
})
export class CompaniaRecursosListadoComponent extends PrincipalBaseComponent implements OnInit {
    listaRecursos: MaMiscelaneosdetalle[];
    filtro: MaMiscelaneosdetalle;
    estados: SelectItem[] = [];

    constructor(
        private router: Router,
        private confirmationService: ConfirmationService,
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        private maMiscelaneoDetalleService: MaMiscelaneosdetalleServicio, messageService: MessageService
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit(): void {
        super.ngOnInit();
        this.filtro = new MaMiscelaneosdetalle();
        this.buscar();
        this.cargarEstados();
    }

    cargarEstados() {
        this.estados.push({ label: '--Seleccione--', value: '' });
        this.estados.push({ label: 'Activo', value: 'A' });
        this.estados.push({ label: 'Inactivo', value: 'I' });
    }

    preBuscar(event?: any) {
        if (event.keyCode === 13) {
            this.buscar();
        }
    }

    buscar() {
        const filtro = new FiltroMiscelaneosDetalle();
        filtro.CodigoAplicacion = 'SY';
        filtro.CodigoTabla = 'TIPRECCOMP';
        filtro.CodigoCompania = '999999';
        filtro.CodigoElemento = this.filtro.codigoelemento;
        filtro.Estado = this.filtro.estado;
        filtro.Nombre = this.filtro.descripcionlocal;


        this.maMiscelaneoDetalleService.listar(filtro).then(
            resp => this.listaRecursos = resp);
    }

    editar(data: MaMiscelaneosdetalle) {
        this.router.navigate(['spring/companiarecursos/mantenimiento', JSON.stringify(data)], { skipLocationChange: true });
    }

    eliminar(bean: MaMiscelaneosdetalle) {
        this.confirmationService.confirm({
            message: '¿Desea Eliminar el registro?',
            accept: () => {
                this.bloquearPagina();
                const pk: MaMiscelaneosdetallePk = new MaMiscelaneosdetallePk();
                pk.aplicacioncodigo = bean.aplicacioncodigo;
                pk.codigoelemento = bean.codigoelemento;
                pk.codigotabla = bean.codigotabla;
                pk.compania = bean.compania;
                this.maMiscelaneoDetalleService
                    .eliminar(pk).then(res => {
                        this.messageService.clear();
                        this.mostrarMensaje(`Se Eliminó El Recurso` + bean.codigoelemento, 'info');
                        this.buscar();
                        this.desbloquearPagina();
                    })
                    .catch(errors => {
                        this.messageService.clear();
                        this.desbloquearPagina();
                    });
            }
        });
    }
}


