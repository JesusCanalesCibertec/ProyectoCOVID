export class dtoPie {
    labels: any[] = [];
    datasets: detaPie[];
    constructor() {
        this.datasets = [];
    }
}
export class detaPie {
    label: string;
    data: any[] = [];
    backgroundColor: any[] = [];
    borderColor: any[] = [];
}

export class dtoBarra {
    labels: any[] = [];
    datasets: detaBarra[];
    constructor() {
        this.datasets = [];
    }
}
export class detaBarra {
    label: string;
    data: any[] = [];
    backgroundColor: string;
}