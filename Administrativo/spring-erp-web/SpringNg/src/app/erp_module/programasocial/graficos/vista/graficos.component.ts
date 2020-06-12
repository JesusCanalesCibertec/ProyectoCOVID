import { Component, OnInit, AfterViewInit } from '@angular/core';
import { GraficosService } from '../servicio/graficos.service';

@Component({
    selector: 'app-graficos',
    templateUrl: './graficos.component.html'
})
export class GraficosComponent implements OnInit, AfterViewInit {

    listadoEvaluacion:  any[];
    listadoEvaluacionBar: any[] = [];

    UserList: any[] = [];
    done: Boolean = false;
    mostrarBarras: Boolean = false;
    view: any[] = [500, 300];

    // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Responsables';
  showYAxisLabel = true;
  yAxisLabel = 'Tareas';

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

    // pie
    showLabels = true;
    explodeSlices = false;
    doughnut = true;

    constructor(
        public graficosService: GraficosService
    ) {

    }

    displayAllUsers(): void {

        // this.graficosService.getUsers().subscribe(users => {
        //     users.forEach(element => {
        //         this.UserList.push({ 'name': element.responsable, 'value': element.tareas, 'estado': element.estado });  // can take only x y values
        //     });
        //     this.done = true;
        // });

        this.graficosService.listarGraficoMultiple().subscribe(users => {
            // Object.assign(this.UserList, {users});
            this.UserList = users;
            console.log(this.UserList);
            this.done = true;
        });
    }

    listarEvaluacion() {
        
    }

    listarEvaluacionBar() {
      
    }

    ngAfterViewInit(): void {
        this.listarEvaluacionBar();
    }

    ngOnInit() {
        this.displayAllUsers();
        this.listarEvaluacion();
    }

}
