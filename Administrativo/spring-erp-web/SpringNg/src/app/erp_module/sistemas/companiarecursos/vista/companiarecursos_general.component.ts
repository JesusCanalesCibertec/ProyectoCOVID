import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-companiarecursosgeneral',
    templateUrl: './companiarecursos_general.component.html'
})
export class CompaniaRecursosGeneralComponent implements OnInit {
    constructor(
        private router: Router
    ) { }

    ngOnInit(): void { }

}
