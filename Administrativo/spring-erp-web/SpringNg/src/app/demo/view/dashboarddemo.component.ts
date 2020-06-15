import { Component, OnInit, ViewChild } from '@angular/core';
import { CarService } from '../service/carservice';
import { EventService } from '../service/eventservice';
import { Car } from '../domain/car';
import { SelectItem } from 'primeng/primeng';
import { MenuItem } from 'primeng/primeng';
import { routes } from '../../app.routes';
import { Router } from '@angular/router';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { DomSanitizer } from '@angular/platform-browser';
import { MessageService } from 'primeng/components/common/messageservice';
import { ContratacionService } from 'src/app/erp_module/minedu/contratacion/servicio/contratacion.service';
import { ProyectoService } from 'src/app/erp_module/minedu/proyecto/servicio/Proyecto.service';
import { SeriesHorizontal } from '@swimlane/ngx-charts';
import { dtoPie, detaPie, dtoBarra, detaBarra } from 'src/app/erp_module/shared/dominio/dto/dtoSerie';

@Component({
    templateUrl: './dashboard.component.html',
})
export class DashboardDemoComponent extends PrincipalBaseComponent implements OnInit {

    lineData: any;
    barData: any;
    barData2: any;
    pieData: any;
    polarData: any;
    radarData: any;
    data: any;
    options: any;
    optionsBar: any;
    optionsBar2: any;

    colorScheme = ["#AAAAAA", "#1794A1", "#19DE16", "#B213EA", "#19DDF1", "#00bfa5", "#aeea00", "#ffab00", "#2B4ED1", "#E94F4F"]



    constructor(
        private contratacionService: ContratacionService,
        private proyectoService: ProyectoService,
        private domSanitizer: DomSanitizer,
        noAuthorizationInterceptor: NoAuthorizationInterceptor,
        messageService: MessageService,
        private router: Router,


    ) {
        super(noAuthorizationInterceptor, messageService);

        this.data = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [
                {
                    label: 'First Dataset',
                    data: [65, 59, 80, 81, 56, 55, 40],
                    fill: true,
                    borderColor: '#4bc0c0'
                },
                {
                    label: 'Second Dataset',
                    data: [28, 48, 40, 19, 86, 27, 90],
                    fill: true,
                    borderColor: '#565656'
                }
            ]
        }
    }

    ngOnInit() {
        super.ngOnInit();
        this.listarPie1();

        this.listarBarraestados();

        this.listarBarratipos();

        this.lineData = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [
                {
                    label: 'First Dataset',
                    data: [65, 59, 80, 81, 56, 55, 40],
                    fill: false,
                    borderColor: '#2162b0'
                },
                {
                    label: 'Second Dataset',
                    data: [28, 48, 40, 19, 86, 27, 90],
                    fill: false,
                    borderColor: '#e02365'
                }
            ]
        };

        this.polarData = {
            datasets: [{
                data: [
                    11,
                    16,
                    7,
                    3,
                    14
                ],
                backgroundColor: [
                    '#2162b0',
                    '#e02365',
                    '#eeb210',
                    '#17AFC2',
                    '#AB44BC'
                ],
                label: 'My dataset'
            }],
            labels: [
                'Red',
                'Green',
                'Yellow',
                'Grey',
                'Blue'
            ]
        };

        this.radarData = {
            labels: ['Eating', 'Drinking', 'Sleeping', 'Designing', 'Coding', 'Cycling', 'Running'],
            datasets: [
                {
                    label: 'My First dataset',
                    backgroundColor: 'rgba(179,181,198,0.2)',
                    borderColor: 'rgba(179,181,198,1)',
                    pointBackgroundColor: 'rgba(179,181,198,1)',
                    pointBorderColor: '#fff',
                    pointHoverBackgroundColor: '#fff',
                    pointHoverBorderColor: 'rgba(179,181,198,1)',
                    data: [65, 59, 90, 81, 56, 55, 40]
                },
                {
                    label: 'My Second dataset',
                    backgroundColor: 'rgba(255,99,132,0.2)',
                    borderColor: 'rgba(255,99,132,1)',
                    pointBackgroundColor: 'rgba(255,99,132,1)',
                    pointBorderColor: '#fff',
                    pointHoverBackgroundColor: '#fff',
                    pointHoverBorderColor: 'rgba(255,99,132,1)',
                    data: [28, 48, 40, 19, 96, 27, 100]
                }
            ]
        };
    }

    listarPie1() {
        // let bean = new dtoPie();
        // this.contratacionService.ListarPie().then(res => {
        //     let secundario = new detaPie();
        //     res.forEach(d => {
        //         bean.labels.push(d.nombre + ' (' + d.valorNumerico + ')');
        //         secundario.data.push(d.porcentaje);
        //     })
        //     secundario.backgroundColor = this.colorScheme;
        //     bean.datasets.push(secundario);

        //     this.pieData = bean;

        //     this.options = {
        //         title: {
        //             display: true,
        //             text: 'CONTRATACIONES',
        //             fontSize: 24
        //         },
        //         legend: {
        //             position: 'bottom',
        //             onClick: function (evt, item) {
        //             }
        //         }
        //     };

        // });
    }

    listarBarraestados() {
        // let bean = new dtoBarra();
        // this.proyectoService.ListarBarraEstados().then(res => {
        //     let secundario = new detaBarra();
        //     res.forEach(d => {
        //         bean.labels.push(d.nombre + ' (' + d.secuencia + ')');
        //         secundario.data.push(d.secuencia);
        //         secundario.backgroundColor = this.colorScheme[1];  
        //     })
        //     secundario.label = "Proyectos";
        //     bean.datasets.push(secundario);

        //     this.barData = bean;

        //     this.optionsBar = {
        //         title: {
        //             display: true,
        //             text: 'CANTIDAD DE PROYECTOS POR ESTADO DE ATENCIÓN',
        //             fontSize: 24
        //         },
        //         scales: {
        //             xAxes: [{
        //                 beginAtZero: true,
        //                 position: 'bottom',
        //                 gridLines: {
        //                     display: false
        //                 },
        //             }]
        //         }
        //     };
        // });
    }

    listarBarratipos() {
        // let bean = new dtoBarra();
        // this.proyectoService.ListarBarraTipos().then(res => {
        //     bean.labels = ['Coordinación de Mantenimiento', 'Coordinación de Proyectos', 'Coordinación de Demanda']
        //     res.forEach(d => {
        //         let secundario = new detaBarra();
        //         secundario.label = d.nombre;
        //         secundario.data.push(d.num1)
        //         secundario.data.push(d.num2)
        //         secundario.data.push(d.num3)
        //         secundario.backgroundColor = this.colorScheme[parseInt(d.codigo)];  
        //         bean.datasets.push(secundario);
        //     })
        //     this.barData2 = bean;
        //     this.optionsBar2 = {
        //         title: {
        //             display: true,
        //             text: 'CANTIDAD DE TIPOS DE PROYECTOS POR COORDINACIÓN',
        //             fontSize: 24
        //         },
        //         scales: {
        //             xAxes: [{
        //                 beginAtZero: true,
        //                 position: 'bottom'
        //             }]
        //         }
        //     };
        // });
    }

}
