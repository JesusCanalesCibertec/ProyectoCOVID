import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'edadCadena'
})

export class EdadCadenaPipe implements PipeTransform {

    constructor() { }

    transform(value: any) {
        if (value == null) {
            return '0 Años';
        }

        let v: Date = new Date(value);

        const now = new Date();
        let anios = 0;

        if (v.getTime() > now.getTime()) {
            return '0 Años';
        }

        while (this.addDate('y', 1, v).getTime() < now.getTime()) {
            v = this.addDate('y', 1, v);
            anios++;
        }

        return  anios + ' Años';
    }

    addDate(pattern: string, mount: number, fecha: Date): Date {

        const f2 = new Date(fecha);

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
