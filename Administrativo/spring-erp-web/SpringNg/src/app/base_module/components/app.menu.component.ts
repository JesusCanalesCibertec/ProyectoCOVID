import { Component, Input, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { MenuItem } from 'primeng/primeng';
import { LayoutComponent } from './layout.component';
import { UsuarioServicio } from 'src/app/erp_module/shared/usuario/servicio/UsuarioServicio';

@Component({
    selector: 'app-menu',
    template: `
        <ul app-submenu [item]="model" root="true" class="layout-menu"
            [reset]="reset" visible="true" parentActive="true"></ul>
    `
})
export class AppMenuComponent implements OnInit {

    @Input() reset: boolean;

    model: any[];

    constructor(public app: LayoutComponent, private usuarioServicio: UsuarioServicio) { }

    ngOnInit() {

        this.model = [
            { label: 'Cargando...', icon: 'fa fa-fw fa-spinner' }];

        this.model = [];
        this.model.push(
            { label: 'Inicio', icon: 'fas fa-home', routerLink: ['/spring/dashboard'] },
            { label: 'Ciudadanos', icon: 'fas fa-users', routerLink: ['/spring/ciudadano-listado'] },
            // { label: 'Triajes', icon: 'fas fa-home', routerLink: ['/spring/dashboard'] }
        );

        /*this.model = [
            { label: 'Dashboard', icon: 'fa fa-fw fa-home', routerLink: ['/spring/'] },
            {
                label: 'Gestión', icon: 'fa fa-fw fa-book', items: [
                    { label: 'FUN', icon: 'fa fa-fw fa-book', routerLink: ['/spring/beneficiario-listado'] },
                    { label: 'FURH', icon: 'fa fa-fw fa-book', routerLink: ['/spring/psfurh-listado'] },
                    { label: 'RAS', icon: 'fa fa-fw fa-book', routerLink: ['/spring/ras-mantenimiento'] },
                    { label: 'Capacitación', icon: 'fa fa-fw fa-book', routerLink: ['/spring/capacitacion-listado'] },
                    { label: 'Consumos', icon: 'fa fa-fw fa-book', routerLink: ['/spring/consumo-listado'] },
                ]
            },
            {
                label: 'Componentes', icon: 'fa fa-fw fa-book', items: [
                    { label: 'Nutrición', icon: 'fa fa-fw fa-book', routerLink: ['/spring/psnutricion-listado'] },
                    { label: 'Salud', icon: 'fa fa-fw fa-book', routerLink: ['/spring/salud-listado'] },
                    { label: 'Capacidades', icon: 'fa fa-fw fa-book', routerLink: ['/spring/capacidades-mantenimiento'] },
                    { label: 'Socio Ambiental', icon: 'fa fa-fw fa-book', routerLink: ['/spring/pssocioambiental-listado'] },

                ]
            },
            {
                label: 'Encuestas/Guías', icon: 'fa fa-fw fa-book', items: [
                    { label: 'Preguntas', icon: 'fa fa-fw fa-book', routerLink: ['/spring/encuestapregunta-listado'] },
                    { label: 'Encuestas/Guías', icon: 'fa fa-fw fa-book', routerLink: ['/spring/encuesta-bean-listado'] },
                    { label: 'Ejecución', icon: 'fa fa-fw fa-book', routerLink: ['/spring/encuesta-listado'] },
                ]
            },
            {
                label: 'Proyectos', icon: 'fa fa-fw fa-book', items: [
                    { label: 'Proyecto', icon: 'fa fa-fw fa-book', routerLink: ['/spring/proyecto-listado/PLAN-MANT'] },
                    { label: 'Análisis', icon: 'fa fa-fw fa-book', routerLink: ['/spring/graficos'] },
                ]
            },
            {
                label: 'Indicadores', icon: 'fa fa-fw fa-book', items: [
                    { label: 'Marco lógico', icon: 'fa fa-fw fa-book', routerLink: ['/spring/marco-logico-listado'] },
                ]
            },
            {
                label: 'Consultas', icon: 'fa fa-fw fa-book', items: [
                    { label: 'Registro de Atenciones', icon: 'fa fa-fw fa-book', routerLink: ['/spring/ras-consulta'] },
                    { label: 'Socio Ambiental', icon: 'fa fa-fw fa-book', routerLink: ['/spring/socioambiental-consulta'] },
                    { label: 'Nutrición', icon: 'fa fa-fw fa-book', routerLink: ['/spring/nutricion-consulta'] },
                    { label: 'Capacidades', icon: 'fa fa-fw fa-book', routerLink: ['/spring/capacidades-consulta'] },
                    { label: 'Salud', icon: 'fa fa-fw fa-book', routerLink: ['/spring/salud-consulta'] },
                ]
            },
            {
                label: 'Maestros', icon: 'fa fa-fw fa-book', items: [
                    { label: 'Componentes Periodo', icon: 'fa fa-fw fa-book', routerLink: ['/spring/institucionperiodo-listado'] },
                    { label: 'Items', icon: 'fa fa-fw fa-book', routerLink: ['/spring/item-listado'] },
                    { label: 'Diagnósticos', icon: 'fa fa-fw fa-book', routerLink: ['/spring/diagnostico-listado'] },
                    { label: 'Cursos', icon: 'fa fa-fw fa-book', routerLink: ['/spring/curso-listado'] },
                    { label: 'Instituciones', icon: 'fa fa-fw fa-book', routerLink: ['/spring/institucion-listado'] },
                    { label: 'Programas', icon: 'fa fa-fw fa-book', routerLink: ['/spring/programa-listado'] },
                    { label: 'Componentes', icon: 'fa fa-fw fa-book', routerLink: ['/spring/componente-listado'] },
                    { label: 'Indicadores', icon: 'fa fa-fw fa-book', routerLink: ['/spring/indicador-listado'] },
                    { label: 'Misceláneos', icon: 'fa fa-fw fa-book', routerLink: ['/spring/miscelaneo-listado'] },
                    { label: 'Procesos', icon: 'fa fa-fw fa-book', routerLink: ['/spring/proceso-listado'] },
                    { label: 'Publicaciones', icon: 'fa fa-fw fa-book', routerLink: ['/spring/publicacion-listado'] },
                    { label: 'Reportes/Plantillas', icon: 'fa fa-fw fa-book', routerLink: ['/spring/syreporte-listado'] },
                    {
                        label: 'Plantillas', icon: 'fa fa-fw fa-book', items: [
                            { label: 'Tareas', icon: 'fa fa-fw fa-book', routerLink: ['/spring/plantilla-tarea-listado'] },
                            { label: 'Encuestas', icon: 'fa fa-fw fa-book', routerLink: ['/spring/encuesta-plantilla-listado'] },
                            { label: 'Consumos', icon: 'fa fa-fw fa-book', routerLink: ['/spring/consumo-plantilla-listado'] },

                        ]
                    },
                ]
            },
            {
                label: 'Reportes', icon: 'fa fa-fw fa-book', items: [
                    { label: 'Prevención de Salud', icon: 'fa fa-fw fa-book', routerLink: ['/spring/prevencion-salud'] },
                    { label: 'Lista de Beneficiarios sin Evaluación', icon: 'fa fa-fw fa-book', routerLink: ['/spring/sin-evaluacion'] },
                ]
            },

        ];
        */


        // this.usuarioServicio.obtenerMenu().then(
        //     menu => {
        //         this.model = [];
        //         this.model.push(
        //             { label: 'Inicio', icon: 'fas fa-home', routerLink: ['/spring/dashboard'] }
        //         );
        //         if (menu.items) {
        //             menu.items.forEach(element => {

        //                 let grupo: any = { label: element.label, icon: element.icon, routerLink: ['/spring/'] };

        //                 if (element.items) {

        //                     grupo = { label: element.label, icon: element.icon, items: [] };

        //                     element.items.forEach(element2 => {

        //                         if (element2.routerLink) {

        //                             element2.routerLink = element2.routerLink == null ? null : element2.routerLink.trim();

        //                             grupo.items.push({
        //                                 label: element2.label, icon: element2.icon,
        //                                 command: event => this.app.lightMenu = true, routerLink: [element2.routerLink]
        //                             });
        //                         } else if (element2.action) {
        //                             grupo.items.push({
        //                                 label: element2.label, icon: element2.icon,
        //                                 command: event => { this.app.lightMenu = true, this.ejecutarAccion(element2.action); }
        //                             });
        //                         } else {
        //                             grupo.items.push({
        //                                 label: element2.label, icon: element2.icon,
        //                                 command: event => this.app.lightMenu = true
        //                             });
        //                         }
        //                     });
        //                 }
        //                 this.model.push(grupo);
        //             });
        //         }

        //         this.model.push(
        //             { label: 'Reportes', icon: 'fa fa-fw fa-book', routerLink: ['/spring/reportes'] },

        //         );
        //     }
        // );
    }


    ejecutarAccion(accion: string) {
        if (accion) {
            accion = accion === undefined ? '' : accion == null ? '' : accion.trim();
            /*switch (accion) {
                case ConstanteMenuAccion.CAMBIO_CLAVE:
                    this.verContrasenia();
                    break;
                default:

            }*/
        }
    }
    changeTheme(theme: string, scheme: string) {
        const layoutLink: HTMLLinkElement = <HTMLLinkElement>document.getElementById('layout-css');
        layoutLink.href = 'assets/layout/css/layout-' + theme + '.css';

        const themeLink: HTMLLinkElement = <HTMLLinkElement>document.getElementById('theme-css');
        themeLink.href = 'assets/theme/theme-' + theme + '.css';

        this.app.menuMode = scheme;
    }
}

@Component({
    /* tslint:disable:component-selector */
    selector: '[app-submenu]',
    /* tslint:enable:component-selector */
    template: `
        <ng-template ngFor let-child let-i="index" [ngForOf]="(root ? item : item.items)">
            <li [ngClass]="{'active-menuitem': isActive(i)}" [class]="child.badgeStyleClass" *ngIf="child.visible === false ? false : true">
                <a [href]="child.url||'#'" (click)="itemClick($event,child,i)"
                   class="ripplelink" *ngIf="!child.routerLink"
                   [attr.tabindex]="!visible ? '-1' : null" [attr.target]="child.target">
                    <i [ngClass]="child.icon"></i><span>{{child.label}}</span>
                    <span class="menuitem-badge" *ngIf="child.badge">{{child.badge}}</span>
                    <i class="fa fa-fw fa-angle-down layout-menuitem-toggler" *ngIf="child.items"></i>
                </a>

                <a (click)="itemClick($event,child,i)" class="ripplelink" *ngIf="child.routerLink"
                   [routerLink]="child.routerLink" routerLinkActive="active-menuitem-routerlink"
                   [routerLinkActiveOptions]="{exact: true}" [attr.tabindex]="!visible ? '-1' : null" [attr.target]="child.target">
                    <i [ngClass]="child.icon"></i><span>{{child.label}}</span>
                    <span class="menuitem-badge" *ngIf="child.badge">{{child.badge}}</span>
                    <i class="fa fa-fw fa-angle-down layout-menuitem-toggler" *ngIf="child.items"></i>
                </a>
                <div class="submenu-arrow" *ngIf="child.items"></div>
                <ul app-submenu [item]="child" *ngIf="child.items" [visible]="isActive(i)" [reset]="reset" [parentActive]="isActive(i)"
                    [@children]=" isActive(i) ? 'visibleAnimated' : 'hiddenAnimated'"></ul>
            </li>
        </ng-template>
    `,
    animations: [
        trigger('children', [
            state('hiddenAnimated', style({
                height: '0px',
                opacity: 0
            })),
            state('visibleAnimated', style({
                height: '*',
                opacity: 1
            })),
            transition('visibleAnimated => hiddenAnimated', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)')),
            transition('hiddenAnimated => visibleAnimated', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)'))
        ])
    ]
})
export class AppSubMenuComponent {

    @Input() item: MenuItem;

    @Input() root: boolean;

    @Input() visible: boolean;

    _reset: boolean;

    _parentActive: boolean;

    activeIndex: number;

    constructor(public app: LayoutComponent) { }

    itemClick(event: Event, item: MenuItem, index: number) {

        // avoid processing disabled items
        if (item.disabled) {
            event.preventDefault();
            return true;
        }

        // activate current item and deactivate active sibling if any
        this.activeIndex = (this.activeIndex === index) ? null : index;

        // execute command
        if (item.command) {
            item.command({ originalEvent: event, item: item });
        }

        // prevent hash change
        if (item.items || (!item.url && !item.routerLink)) {
            setTimeout(() => {
                this.app.layoutMenuScrollerViewChild.moveBar();
            }, 450);
            event.preventDefault();
        }

        // hide menu
        if (!item.items) {
            this.app.overlayMenuActive = false;
            this.app.staticMenuMobileActive = false;
        }
    }

    isActive(index: number): boolean {
        return this.activeIndex === index;
    }

    @Input() get reset(): boolean {
        return this._reset;
    }

    set reset(val: boolean) {
        this._reset = val;

        if (this._reset) {
            this.activeIndex = null;
        }
    }

    @Input() get parentActive(): boolean {
        return this._parentActive;
    }

    set parentActive(val: boolean) {
        this._parentActive = val;

        if (!this._parentActive) {
            this.activeIndex = null;
        }
    }
}
