export interface UIMantenimientoComponent {
    inicializarDatos(): void;
    guardar(id?: any, datatable?: any): void;
    salir(routerLink?: any): void;
}
