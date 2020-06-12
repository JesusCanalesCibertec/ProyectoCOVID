import { DomSanitizer } from '@angular/platform-browser';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'edadCompleta'
})

export class EdadCompletaPipe implements PipeTransform {

    constructor() { }

    transform(value: any) {
        if (value == null) {
            return "Años: Meses: Días: ";
        }

        var v: Date = new Date(value);

        var now = new Date();
        var anios = 0;
        var meses = 0;
        var dias = 0;

        if (v.getTime() > now.getTime()) {
            return "Años: Meses: Días: ";
        }

        while (this.addDate('y', 1, v).getTime() < now.getTime()) {
            v = this.addDate('y', 1, v);
            anios++;

        };
        while (this.addDate('m', 1, v).getTime() < now.getTime()) {
            v = this.addDate('m', 1, v);
            meses++;
        };

        while (this.addDate('d', 1, v).getTime() < now.getTime()) {
            v = this.addDate('d', 1, v);
            dias++;
        };

        return "Años: " + anios + " Meses: " + meses + " Días: " + dias;
    }

    addDate(pattern: string, mount: number, fecha: Date): Date {

        var f2 = new Date(fecha);

        switch (pattern) {
            case 'y': {
                f2.setFullYear(f2.getFullYear() + mount);
                break;
            }
            case 'm': {
                f2.setMonth(f2.getMonth() + mount);
                break;
            }
            case 'd': {
                f2.setDate(f2.getDate() + mount);
                break;
            }
            default:
                break;
        }

        return f2;
    }


}