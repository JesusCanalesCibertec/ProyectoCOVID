import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-syreporte',
    templateUrl: './syreporte-general.component.html'
})
export class SyReporteComponent implements OnInit {

    verDetalle: Boolean = false;
    constructor(
        private route: ActivatedRoute,
    ) { }

    ngOnInit() {
        const aplicacioncodigo = this.route.snapshot.params['aplicacionCodigo'];

        if (aplicacioncodigo) {
            this.verDetalle = true;
        }
    }
}
