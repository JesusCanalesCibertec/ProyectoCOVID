export class CompanyownerrecursoPk {
	companyowner: string;
	tiporecurso: string;
	periodo: string;
}

export class Companyownerrecurso extends CompanyownerrecursoPk {
	nombrerecurso: string;
	ultimousuario: string;
	ultimafechamodif: Date;
	auxString: string;
}


