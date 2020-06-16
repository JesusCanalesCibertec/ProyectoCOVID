import { DtoSolicitudes } from '../../erp_module/shared/dominio/dto/DtoSolicitudes';
export class Sid {
    constructor() {
        this.dto = new DtoSolicitudes();
    }
    token: string;
    url: string;
    dto: DtoSolicitudes;
}
