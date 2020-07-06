import { Component, OnInit, ViewChild, Renderer2 } from '@angular/core';
import { Router, Routes, Route, ActivatedRoute } from '@angular/router';
import { ContraseniaMantenientoComponent } from './contrasenia-mantenimiento/contrasenia-mantenimiento.component';
import { PrincipalBaseComponent } from './PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from '../interceptor/NoAuthorizationInterceptor';
import { ScrollPanel, MessageService } from 'primeng/primeng';


@Component({
    selector: "layout",
    templateUrl: './layout.component.html'
})
export class LayoutComponent extends PrincipalBaseComponent implements OnInit {

    layoutMode = 'static';

    megaMenuMode = 'dark';

    profileMode = 'inline';

    menuMode = 'slim';

    topbarMenuActive: boolean;

    overlayMenuActive: boolean;

    slimMenuActive: boolean;

    slimMenuAnchor: boolean;

    toggleMenuActive: boolean;

    staticMenuDesktopInactive: boolean;

    staticMenuMobileActive: boolean;

    layoutMenuScroller: HTMLDivElement;

    lightMenu = true;

    menuClick: boolean;

    topbarItemClick: boolean;

    activeTopbarItem: any;

    resetMenu: boolean;

    menuHoverActive: boolean;

    rightPanelActive: boolean;

    rightPanelClick: boolean;

    @ViewChild(ContraseniaMantenientoComponent) contraseniaMantenientoComponent: ContraseniaMantenientoComponent;

    @ViewChild('layoutMenuScroller') layoutMenuScrollerViewChild: ScrollPanel;

    ngAfterViewInit() {
        setTimeout(() => { this.layoutMenuScrollerViewChild.moveBar(); }, 100);
    }

    constructor(
        public renderer: Renderer2,
        private route: ActivatedRoute, private router: Router,
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {
        super.ngOnInit();
        const msg = this.route.snapshot.params['msg'];

        if (msg !== undefined) {
            this.messageService.clear();
            this.messageService.add({ severity: 'info', summary: 'Información', detail: msg });
        }

    }

    // showDialog() {
    //     this.EmpleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
    //         resp => {
    //             this.contraseniaMantenientoComponent.iniciarComponente(resp.codigoUsuario);
    //         }
    //     );

    // }

    onLayoutClick() {
        if (!this.topbarItemClick) {
            this.activeTopbarItem = null;
            this.topbarMenuActive = false;
        }

        if (!this.rightPanelClick) {
            this.rightPanelActive = false;
        }

        if (!this.menuClick) {
            if (this.isHorizontal()) {
                this.resetMenu = true;
            }

            if (this.overlayMenuActive || this.staticMenuMobileActive) {
                this.hideOverlayMenu();
            }

            if (this.slimMenuActive) {
                this.hideSlimMenu();
            }

            if (this.toggleMenuActive) {
                this.hideToggleMenu();
            }

            this.menuHoverActive = false;
        }

        this.topbarItemClick = false;
        this.menuClick = false;
        this.rightPanelClick = false;
    }

    onMenuButtonClick(event) {
        this.menuClick = true;
        this.topbarMenuActive = false;

        if (this.isOverlay()) {
            this.overlayMenuActive = !this.overlayMenuActive;
        }
        if (this.isToggle()) {
            this.toggleMenuActive = !this.toggleMenuActive;
        }
        if (this.isDesktop()) {
            this.staticMenuDesktopInactive = !this.staticMenuDesktopInactive;
        } else {
            this.staticMenuMobileActive = !this.staticMenuMobileActive;
        }

        event.preventDefault();
    }

    onMenuClick($event) {
        this.menuClick = true;
        this.resetMenu = false;
    }

    onAnchorClick($event) {
        if (this.isSlim()) {
            this.slimMenuAnchor = !this.slimMenuAnchor;
        }
    }

    onMenuMouseEnter(event) {
        if (this.isSlim()) {
            this.slimMenuActive = true;
        }
    }

    onMenuMouseLeave(event) {
        if (this.isSlim()) {
            this.slimMenuActive = false;
        }
    }

    onTopbarMenuButtonClick(event) {
        this.topbarItemClick = true;
        this.topbarMenuActive = !this.topbarMenuActive;

        this.hideOverlayMenu();

        event.preventDefault();
    }

    onTopbarItemClick(event, item) {
        this.topbarItemClick = true;

        if (this.activeTopbarItem === item) {
            this.activeTopbarItem = null;
        } else {
            this.activeTopbarItem = item;
        }

        event.preventDefault();
    }

    onTopbarSubItemClick(event) {
        sessionStorage.removeItem('access_token');
        /*
                if (sessionStorage.getItem("modo") == 'I') {
                    this.router.navigate(["spring/institucion-login"]);
                }
                if (sessionStorage.getItem("modo") == 'F') {
                    this.router.navigate(["spring/fundacion-login"]);
                }
                else {
                    this.router.navigate([""]);
                }*/
        this.router.navigate([""]);
        sessionStorage.removeItem('modo');
        event.preventDefault();
    }

    onRightPanelButtonClick(event) {
        this.rightPanelClick = true;
        this.rightPanelActive = !this.rightPanelActive;
        event.preventDefault();
    }

    onRightPanelClick() {
        this.rightPanelClick = true;
    }

    isHorizontal() {
        return this.menuMode === 'horizontal';
    }

    isSlim() {
        return this.menuMode === 'slim';
    }

    isOverlay() {
        return this.menuMode === 'overlay';
    }

    isToggle() {
        return this.menuMode === 'toggle';
    }

    isStatic() {
        return this.menuMode === 'static';
    }

    isMobile() {
        return window.innerWidth < 1281;
    }

    isDesktop() {
        return window.innerWidth > 1280;
    }

    isTablet() {
        const width = window.innerWidth;
        return width <= 1280 && width > 640;
    }

    hideOverlayMenu() {
        this.overlayMenuActive = false;
        this.staticMenuMobileActive = false;
    }

    hideSlimMenu() {
        this.slimMenuActive = false;
        this.staticMenuMobileActive = false;
    }

    hideToggleMenu() {
        this.toggleMenuActive = false;
        this.staticMenuMobileActive = false;
    }

}
