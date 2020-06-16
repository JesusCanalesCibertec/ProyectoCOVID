import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpErrorResponse,
    HttpResponse
} from '@angular/common/http';
import { Observable, ReplaySubject } from "rxjs";
import { catchError, tap } from 'rxjs/operators';

import { Router } from '@angular/router';

import { MessageService } from 'primeng/components/common/messageservice';

@Injectable()
export class NoAuthorizationInterceptor implements HttpInterceptor {

    constructor(private messageService: MessageService, private route: Router) {

    }

    dataStream: ReplaySubject<any> = new ReplaySubject<any>();

    dataStream$(): Observable<any> {
        this.dataStream = new ReplaySubject<any>();
        return this.dataStream.asObservable();
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        let accessToken = sessionStorage.getItem("access_token");

        if (accessToken) {

            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${accessToken}`
                }
            });
        }

        return next.handle(request)
            .pipe(
                tap(event => {
                    if (event instanceof HttpResponse) {

                    }
                }, error => {
                    if (error.status === 401) { //token
                        // errores de acceso
                        this.route.navigate([""]);
                        // window.location.href = "./assets/pages/access.html"
                    }
                    else if (error.status === 606) {
                        // errores de UException son mostrados al usuario
                        this.dataStream.next(error);
                    }
                    else if (error.status === 500) { //caida
                        //maneja el error mandando al login
                        this.route.navigate([""]);
                        sessionStorage.removeItem('access_token');
                        //maneja el error mostrando el error
                        //this.messageService.add({ severity: 'error', summary: 'Mensaje', detail: error.error[0].Mensaje });

                        //maneja el error mandando a la pantalla de error
                        //window.location.href = "./assets/pages/error.html"
                    }
                })
            )
    }
}
