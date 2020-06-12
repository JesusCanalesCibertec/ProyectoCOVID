import { Component, OnInit } from "@angular/core";
import { PrincipalBaseComponent } from "src/app/base_module/components/PrincipalBaseComponent";
import { NoAuthorizationInterceptor } from "src/app/base_module/interceptor/NoAuthorizationInterceptor";
import { Proyecto } from "../dominio/proyecto";
import { MessageService } from "primeng/components/common/messageservice";


@Component({
    selector: 'app-proyecto-riesgos',
    templateUrl: './proyecto-tab4.component.html'
})
export class ProyectoRiesgosComponent extends PrincipalBaseComponent implements OnInit{

    proyecto: Proyecto = new Proyecto();

    constructor(

        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService) {
        super(noAuthorizationInterceptor, messageService)
    }

    ngOnInit(){

    }
}