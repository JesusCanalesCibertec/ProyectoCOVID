import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UsuarioServicio } from '../../login_module/servicio/UsuarioServicio';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private usuarioServicio: UsuarioServicio) { }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot) {

        if (!this.usuarioServicio.tienePermiso(route.data['concepto'])) {
            window.location.href = "./assets/pages/access.html"
            return false;
        }

        return true;
    }
}
