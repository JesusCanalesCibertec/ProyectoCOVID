const obtenerPrimerDiaDelMes = function (fecha: Date): Date {
    return new Date(fecha.getFullYear(), fecha.getMonth(), 1);
};

const obtenerUltimoDiaDelMes = function (fecha: Date): Date {
    return new Date(fecha.getFullYear(), fecha.getMonth() + 1, 0);
};

const obtenerPorDiaFechaHoraFin = function (fecha: Date): Date {
    return new Date(fecha.setHours(23, 59, 59));
};

const obtenerPorDiaFechaHoraInicio = function (fecha: Date): Date {
    return new Date(fecha.setHours(0, 0, 0));
};

const obtenerFormatoPeriodo = function (fecha: Date): string {
    return fecha.toISOString().slice(0, 7).replace(/-/g, '');
};

const regexIso8601 = /^([\+-]?\d{4}(?!\d{2}\b))((-?)((0[1-9]|1[0-2])(\3([12]\d|0[1-9]|3[01]))?|W([0-4]\d|5[0-2])(-?[1-7])?|(00[1-9]|0[1-9]\d|[12]\d{2}|3([0-5]\d|6[1-6])))([T\s]((([01]\d|2[0-3])((:?)[0-5]\d)?|24\:?00)([\.,]\d+(?!:))?)?(\17[0-5]\d([\.,]\d+)?)?([zZ]|([\+-])([01]\d|2[0-3]):?([0-5]\d)?)?)?)?$/;

const convertDateStringsToDates = function (input: any): any {
    if (typeof input !== 'object') {
        return input;
    }


    for (const key in input) {
        if (!input.hasOwnProperty(key)) {
            continue;
        }

        const value = input[key];
        let match: any;
        if (typeof value === 'string' && (match = value.match(regexIso8601))) {
            const milliseconds = Date.parse(match[0]);
            if (!isNaN(milliseconds)) {
                input[key] = new Date(milliseconds);
            }
        } else if (typeof value === 'object') {
            convertDateStringsToDates(value);
        }
    }
    return input;
};

const ddMMyyyy = function (date: Date): string {
    const MM = date.getMonth() + 1;
    const dd = date.getDate();

    return [`${dd > 9 ? '' : '0'}${dd}`, `${MM > 9 ? '' : '0'}${MM}`, date.getFullYear()].join('/');
};




export {
    obtenerPrimerDiaDelMes,
    obtenerUltimoDiaDelMes,
    obtenerFormatoPeriodo,
    obtenerPorDiaFechaHoraFin,
    obtenerPorDiaFechaHoraInicio,
    convertDateStringsToDates, ddMMyyyy
};

