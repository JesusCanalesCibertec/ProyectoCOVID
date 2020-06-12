import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NoAuthorizationInterceptor } from '../../base_module/interceptor/NoAuthorizationInterceptor';
import { PrincipalBaseComponent } from '../../base_module/components/PrincipalBaseComponent';
import { SidServicio } from '../servicio/SidServicio';
// import { createLNodeObject } from '@angular/core/src/render3/instructions';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: "sid",
    templateUrl: './sid.component.html'
})
export class SidComponent extends PrincipalBaseComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private sidServicio: SidServicio,
        noAuthorizationInterceptor: NoAuthorizationInterceptor, messageService: MessageService
    ) { super(noAuthorizationInterceptor, messageService); }

    ngOnInit() {
        this.bloquearPagina();

        super.ngOnInit();

        const sid = this.route.snapshot.params['sid'];
        const correo = this.route.snapshot.params['correo'];

        this.sidServicio.enviar(sid, correo).then(
            obj => {
                if (obj != null) {
                    console.log(obj);
                    sessionStorage.setItem('access_token', obj.token);
                    this.router.navigate([obj.url, JSON.stringify(obj.dto)]);
                } else {
                    this.desbloquearPagina();
                    setTimeout(() => { this.router.navigate(['']); }, 2000);
                }
            }
        );
    }
}
