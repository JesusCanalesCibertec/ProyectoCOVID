export class dtoEventos{
    id?: number;
    title: string;
    start: Date;
    end: Date;
    allDay?: boolean = true;
    backgroundColor?: string;
}