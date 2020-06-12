export interface UIListadoComponent {
    buscar(datatable?: any): void;
    inicializarDatos(): void;
    nuevo(): void;
    editar(id?: any): void;
    ver(id?: any): void;
}
