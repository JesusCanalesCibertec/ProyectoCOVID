import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { CarService } from '../service/carservice';
import { Router } from '@angular/router';
import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { dtoPie, detaPie, dtoBarra, detaBarra } from 'src/app/erp_module/shared/dominio/dto/dtoSerie';
import { CiudadanoService } from 'src/app/erp_module/covid/ciudadano/servicio/Ciudadano.service';
import { FiltroCiudadano } from 'src/app/erp_module/covid/ciudadano/dominio/filtroCiudadano';
import { DepartamentoServicio } from 'src/app/erp_module/shared/departamento/servicio/DepartamentoServicio';
import { ProvinciaServicio } from 'src/app/erp_module/shared/provincia/servicio/ProvinciaServicio';
import { ZonapostalServicio } from 'src/app/erp_module/shared/zonapostal/servicio/ZonapostalServicio';
import { SelectItem, MessageService } from 'primeng/api';
import { ChartComponent } from '@swimlane/ngx-charts';


@Component({
    templateUrl: './dashboard.component.html',
    styles: [`
        .contenido{
            background-color: white;
            border-style: inset;
            justify-content: center; 
            display: flex; 
            align-items: center;
            border-radius:5%;
        }
        `]
})
export class DashboardDemoComponent extends PrincipalBaseComponent implements OnInit {

    @ViewChild(ChartComponent) chartComponent: ChartComponent

    lineData: any;
    barData: any;
    barData2: any;
    pieData: any;
    pieDatadepa: any;
    pieDataprov: any;
    pieDatadist: any;
    polarData: any;
    radarData: any;
    data: any;
    options: any;
    optionsDepa: any;
    optionsProv: any;
    optionsDist: any;
    optionsBar: any;
    optionsBar2: any;
    filtro: FiltroCiudadano = new FiltroCiudadano();

    departamentos: SelectItem[] = [];
    provincias: SelectItem[] = [];
    distritos: SelectItem[] = [];

    colorScheme = ["green", "#8f9a9f", "yellow", "orange", "red", "#f5f5dc", "#aeea00", "#ffab00", "#2B4ED1", "#E94F4F"]

    cars: any[];

    responsiveOptions;



    constructor(
        private ciudadanoServicio: CiudadanoService,
        private departamentoServicio: DepartamentoServicio,
        private provinciaServicio: ProvinciaServicio,
        private distritoServicio: ZonapostalServicio,
        private carServicio: CarService,
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
        this.bloquearPagina();
        this.depa = true;
        this.prov = true;
        this.dist = true;
        this.listarPie1();
        this.cargarDepartamentos();

        this.carServicio.getCarsSmall().then(cars => {
            this.cars = cars
        });

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

    depa: Boolean;
    listarPiedepa() {
        this.pieDatadepa = [];
        this.depa = true;
        let bean = new dtoPie();
        let total = 0;
        this.ciudadanoServicio.ListarPiexDepartamento(this.filtro.departamento).then(res => {
            if (res != null) {
                let secundario = new detaPie();
                res.forEach(d => {
                    bean.labels.push(d.nombre + ' (' + d.porcentaje + ' %)');
                    secundario.data.push(d.valorNumerico);
                    secundario.borderColor.push('#b8bfc2');
                    total += d.valorNumerico;
                })
                secundario.backgroundColor = this.colorScheme;

                bean.datasets.push(secundario);

                this.pieDatadepa = bean;
            }
            else {
                this.depa = false;
                this.provincias = [];
                this.distritos = [];
            }
            this.optionsDepa = {
                title: {
                    display: true,
                    text: 'TOTAL '+ total,
                    fontSize: 12,
                    position: 'bottom'
                },
                legend: {
                    position: 'right',
                    onClick: function (evt, item) {
                    }
                }
            };
            this.desbloquearPagina();
        });
    }

    prov: Boolean;
    listarPieprov() {
        let total = 0;
        this.pieDataprov = [];
        this.prov = true;
        let bean = new dtoPie();
        this.ciudadanoServicio.ListarPiexProvincia(this.filtro.departamento, this.filtro.provincia).then(res => {
            if (res != null) {
                let secundario = new detaPie();
                res.forEach(d => {
                    bean.labels.push(d.nombre + ' (' + d.porcentaje + ' %)');
                    secundario.data.push(d.valorNumerico);
                    secundario.borderColor.push('#b8bfc2');
                    total += d.valorNumerico;
                })
                secundario.backgroundColor = this.colorScheme;
                bean.datasets.push(secundario);

                this.pieDataprov = bean;
            }
            else {
                this.prov = false;
                this.distritos = []
            }
            this.optionsProv = {
                title: {
                    display: true,
                    text: 'TOTAL '+total,
                    fontSize: 12,
                    position: 'bottom'
                },
                legend: {
                    position: 'right',
                    onClick: function (evt, item) {
                    }
                }
            };
            this.desbloquearPagina();
        });
    }

    dist: Boolean;
    listarPiedist() {
        this.bloquearPagina();
        let total = 0;
        this.pieDatadist = [];
        this.dist = true;
        let bean = new dtoPie();
        this.ciudadanoServicio.ListarPiexDistrito(this.filtro.departamento, this.filtro.provincia, this.filtro.distrito).then(res => {
            if (res != null) {
                let secundario = new detaPie();
                res.forEach(d => {
                    bean.labels.push(d.nombre + ' (' + d.porcentaje + ' %)');
                    secundario.data.push(d.valorNumerico);
                    secundario.borderColor.push('#b8bfc2');
                    total += d.valorNumerico;
                })
                secundario.backgroundColor = this.colorScheme;
                bean.datasets.push(secundario);

                this.pieDatadist = bean;
            } else { this.dist = false; }
            this.optionsDist = {
                title: {
                    display: true,
                    text: 'TOTAL '+total,
                    fontSize: 12,
                    position: 'bottom'
                },
                legend: {
                    position: 'right',
                    onClick: function (evt, item) {
                    }
                }
            };
            this.desbloquearPagina();
        });
    }

    listarPie1() {
        this.pieData = [];
        let bean = new dtoPie();
        let total = 0;
        this.ciudadanoServicio.ListarPie().then(res => {
            let secundario = new detaPie();
            res.forEach(d => {
                bean.labels.push(d.nombre + ' (' + d.porcentaje + ' %)');
                secundario.data.push(d.valorNumerico);
                secundario.borderColor.push('#b8bfc2');
                total += d.valorNumerico;
            })
            secundario.backgroundColor = this.colorScheme;
            bean.datasets.push(secundario);
            this.pieData = bean;
            this.options = {
                title: {
                    display: true,
                    text: 'CONTEO NACIONAL TOTAL '+ total,
                    fontSize: 14,
                    position: 'top'
                },
                legend: {
                    position: 'right',
                },
                plugins: {
                    labels: {
                        render: 'percentage',
                        fontColor: ['green', 'white', 'red', 'green', 'white', 'red'],
                        precision: 2
                    }
                },
            };
            this.desbloquearPagina();
        });
    }

    cargarDepartamentos() {
        this.departamentos = [];
        //this.departamentos.push({ value: null, label: '--Todos--' });
        this.departamentoServicio.listarTodos().then(res => {
            res.forEach(obj => this.departamentos.push({ value: obj.departamento, label: obj.descripcion }))
            this.filtro.departamento = '15';
            return this.cargarProvincias().then(
                r => {
                    this.filtro.provincia = '01';
                    return this.cargarDistritos().then(
                        r => {
                            this.filtro.distrito = '01';
                            this.listarPiedist();
                        }
                    );
                }
            );
        })
    }

    cargarProvincias(): Promise<number> {
        this.bloquearPagina();
        this.provincias = [];
        this.distritos = [];
        this.pieDataprov = [];
        this.optionsProv = [];
        this.pieDatadist = [];
        this.optionsDist = [];
        this.filtro.provincia = null;
        this.filtro.distrito = null;
        this.prov = true;
        this.dist = true;
        //this.provincias.push({ value: null, label: '--Todos--' });
        return this.provinciaServicio.listarActivosPorDepartamento(this.filtro.departamento).then(res => {
            res.forEach(obj => this.provincias.push({ value: obj.provincia, label: obj.descripcion }));
            this.listarPiedepa();
            return 1;
        })
    }

    cargarDistritos(): Promise<number> {
        this.bloquearPagina();
        this.distritos = [];
        this.pieDatadist = [];
        this.optionsDist = [];
        this.filtro.distrito = null;
        this.dist = true;
        //this.distritos.push({ value: null, label: '--Todos--' });
        return this.distritoServicio.listarActivosPorProvincia(this.filtro.departamento, this.filtro.provincia).then(res => {
            res.forEach(obj => this.distritos.push({ value: obj.codigopostal, label: obj.descripcion }));
            this.listarPieprov();
            return 1;
        })
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




    // ngAfterViewInit() {

    //     this.options = {
    //         plugins: {
    //             labels: {
    //                 // render 'label', 'value', 'percentage', 'image' or custom function, default is 'percentage'
    //                 render: "percentage",
    //                 // precision for percentage, default is 0
    //                 precision: 0,
    //                 // identifies whether or not labels of value 0 are displayed, default is false
    //                 showZero: true,
    //                 // font size, default is defaultFontSize
    //                 fontSize: 12,
    //                 // font color, can be color array for each data or function for dynamic color, default is defaultFontColor
    //                 fontColor: "#000000",
    //                 // font style, default is defaultFontStyle
    //                 fontStyle: "normal",
    //                 // font family, default is defaultFontFamily
    //                 fontFamily:
    //                     "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
    //                 // draw text shadows under labels, default is false
    //                 // textShadow: true,
    //                 // text shadow intensity, default is 6
    //                 shadowBlur: 10,
    //                 // text shadow X offset, default is 3
    //                 shadowOffsetX: -5,
    //                 // text shadow Y offset, default is 3
    //                 shadowOffsetY: 5,
    //                 // text shadow color, default is 'rgba(0,0,0,0.3)'
    //                 // shadowColor: "rgba(255,0,0,0.75)",
    //                 // draw label in arc, default is false
    //                 // bar chart ignores this
    //                 arc: true,
    //                 // position to draw label, available value is 'default', 'border' and 'outside'
    //                 // bar chart ignores this
    //                 // default is 'default'
    //                 position: "outside",
    //                 // draw label even it's overlap, default is true
    //                 // bar chart ignores this
    //                 overlap: true,
    //                 // show the real calculated percentages from the values and don't apply the additional logic to fit the percentages to 100 in total, default is false
    //                 showActualPercentages: true,
    //                 // add padding when position is `outside`
    //                 // default is 2
    //                 outsidePadding: 4,
    //                 // add margin of text when position is `outside` or `border`
    //                 // default is 2
    //                 textMargin: 4
    //             }
    //         }
    //     }
    //     //.........
        

    // }

}
