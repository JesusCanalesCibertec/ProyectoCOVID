import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UbicacionGeograficaSelectorComponent } from 'src/app/erp_module/shared/selectores/ubicaciongeografica/ubicaciongeografica-selector.component';
import { PsEntidadPariente } from '../dominio/psentidadpariente';
import { PsEntidadEquipamiento } from '../dominio/psentidadequipamiento';
import { PsBeneficiarioIngresoCasual } from '../dominio/psbeneficiarioingresocasual';
import { PsEntidadSeguroSocial } from '../dominio/psentidadsegurosocial';
import { PsEntidadProgramaSocial } from '../dominio/psentidadprogramasocial';
import { PsEntidadDocumento } from '../dominio/psentidaddocumento';
import { PsEntidad } from '../dominio/psentidad';
import { SelectItem } from 'primeng/api';
import { PsBeneficiarioServicio } from '../service/psBeneficiarioServicio';
import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
import { ActivatedRoute, Router } from '@angular/router';
import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
import { PsBeneficiarioPk, PsBeneficiario } from '../dominio/psBeneficiario';
import { PsBeneficiarioIngreso } from '../dominio/psbeneficiarioingreso';
import { MaMiscelaneosdetalle } from 'src/app/erp_module/shared/miscelaneos/dominio/MaMiscelaneosdetalle';
import { PopUpMantenimientoMiscelaneoComponent } from 'src/app/erp_module/shared/miscelaneos/vista/miscelaneo-mantenimiento-popup.component';
import { ParametrosmastServicio } from 'src/app/erp_module/sistemas/parametrosmast/servicio/ParametrosmastServicio';
import { PsConstante } from '../../psconstantes.ts/psconstantes';
import { MessageService } from 'primeng/components/common/messageservice';

// import { Component, OnInit, ViewChild } from '@angular/core';
// import { PrincipalBaseComponent } from 'src/app/base_module/components/PrincipalBaseComponent';
// import { NoAuthorizationInterceptor } from 'src/app/base_module/interceptor/NoAuthorizationInterceptor';
// import { MessageService, SelectItem } from 'primeng/api';
// import { Router, ActivatedRoute } from '@angular/router';
// import { PsEntidad } from '../dominio/psentidad';
// import { PsEntidadPariente } from '../dominio/psentidadpariente';
// import { PsEntidadEquipamiento } from '../dominio/psentidadequipamiento';
// import { PsBeneficiarioIngresoCasual } from '../dominio/psbeneficiarioingresocasual';
// import { PsBeneficiarioIngreso } from '../dominio/psbeneficiarioingreso';
// import { PsEntidadSeguroSocial } from '../dominio/psentidadsegurosocial';
// import { PsEntidadProgramaSocial } from '../dominio/psentidadprogramasocial';
// import { PsEntidadDocumento } from '../dominio/psentidaddocumento';
// import { PsInstitucionServicio } from '../../institucion/service/PsInstitucionServicio';
// import { MaMiscelaneosdetalleServicio } from 'src/app/erp_module/shared/miscelaneos/servicio/MaMiscelaneosdetalleServicio';
// import { PsInstitucionAreaServicio } from '../../institucion_area/servicio/PsInstitucionAreaServicio';
// import { PsBeneficiarioServicio } from '../service/psBeneficiarioServicio';
// import { UbicacionGeograficaSelectorComponent } from 'src/app/erp_module/shared/selectores/ubicaciongeografica/ubicaciongeografica-selector.component';

@Component({
  templateUrl: './beneficiario-mantenimiento.component.html'
})
export class PsBeneficiariomantenimientoComponent extends PrincipalBaseComponent implements OnInit {

  @ViewChild(PopUpMantenimientoMiscelaneoComponent) popUpMantenimientoMiscelaneoComponent: PopUpMantenimientoMiscelaneoComponent;
  @ViewChild(UbicacionGeograficaSelectorComponent) ubicacionGeograficaSelectorComponent: UbicacionGeograficaSelectorComponent;

  public static BENEFICIARIO_TIPO_NNA = 1;
  public static BENEFICIARIO_TIPO_AAM = 2;
  public static BENEFICIARIO_TIPO_ALL = 3;

  BENEFICIARIO_TIPO_NNA = 1;
  BENEFICIARIO_TIPO_AAM = 2;
  BENEFICIARIO_TIPO_ALL = 3;

  titulo: string = 'Cargando...';
  esAdulto: boolean = false;
  esNino: boolean = false;

  beanPsEntidadPariente: PsEntidadPariente = new PsEntidadPariente();
  beanPsEntidadEquipamiento: PsEntidadEquipamiento = new PsEntidadEquipamiento();
  beanPsBeneficiarioIngresoCasual: PsBeneficiarioIngresoCasual = new PsBeneficiarioIngresoCasual();
  beanPsEntidadSeguroSocial: PsEntidadSeguroSocial = new PsEntidadSeguroSocial();
  beanPsEntidadProgramaSocial: PsEntidadProgramaSocial = new PsEntidadProgramaSocial();
  beanPsEntidadDocumento: PsEntidadDocumento = new PsEntidadDocumento();

  bean: PsEntidad = new PsEntidad();
  beanPsbeneficiario: PsBeneficiario = new PsBeneficiario();
  tabIndex: number = 0;
  institucionActual: string = null;
  programaActual: string = null;
  beneficiarioActual: number = null;
  tipoFUN: number;

  //COMBOS
  listaGradoInstruccion: SelectItem[] = [];
  listaProgramaBean: SelectItem[] = [];
  listaParentesco: SelectItem[] = [];
  listaEquipamiento: SelectItem[] = [];
  listaCausal: SelectItem[] = [];
  listaSeguro: SelectItem[] = [];
  listaPrograma: SelectItem[] = [];
  listaTipoDoc: SelectItem[] = [];
  listaInstituciones: SelectItem[] = [];
  listaSexo: SelectItem[] = [];
  listaOcupacion: SelectItem[] = [];
  listaDesague: SelectItem[] = [];
  listaAgua: SelectItem[] = [];
  listaElectricidad: SelectItem[] = [];
  listaTenencia: SelectItem[] = [];
  listaEstadoCivil: SelectItem[] = [];
  listaMaterialContruccion: SelectItem[] = [];
  listaGrupoSanguineo: SelectItem[] = [];
  listaDeriva: SelectItem[] = [];
  listaLegal: SelectItem[] = [];
  listaPabellones: SelectItem[] = [];
  listaFactor: SelectItem[] = [];
  listaConadis: SelectItem[] = [];
  listaEstado: SelectItem[] = [];

  nom: String;
  apepa: String;
  apema: String;
  estadoBeneficiario: string;



  //COMBOS

  constructor(
    private parametrosmastServicio: ParametrosmastServicio,
    private psBeneficiarioServicio: PsBeneficiarioServicio,
    private psInstitucionAreaServicio: PsInstitucionAreaServicio,
    private maMiscelaneosdetalleServicio: MaMiscelaneosdetalleServicio,
    private psInstitucionServicio: PsInstitucionServicio,
    private route: ActivatedRoute,
    private router: Router,
    noAuthorizationInterceptor: NoAuthorizationInterceptor,
    messageService: MessageService) {
    super(noAuthorizationInterceptor, messageService);

  }

  handleChange(e) {
    this.tabIndex = e.index;
  }



  prevTab() {
    this.tabIndex = this.tabIndex - 1;
  }

  nextTab() {
    this.tabIndex = this.tabIndex + 1;
  }
  conBeneficiarios = false;
  hacerCambios: boolean = true;
  ngOnInit() {

    super.ngOnInit();
    this.bloquearPagina();


    this.tipoFUN = this.route.snapshot.params['TIPO'] as number;
    this.institucionActual = this.route.snapshot.params['INSTITUCION'] as string;
    this.beneficiarioActual = this.route.snapshot.params['BENEFICIARIO'] as number;
    this.hacerCambios = (this.route.snapshot.params['EDITAR'] as number) == 1 ? true : false;
    //cargar combos
    this.listaProgramaBean.push({ label: '-- Seleccione --', value: null });
    this.listaParentesco.push({ label: ' -- Seleccione -- ', value: null });
    this.listaInstituciones.push({ label: '-- Seleccione --', value: null });
    this.listaSexo.push({ label: '-- Seleccione --', value: null });
    this.listaOcupacion.push({ label: '-- Seleccione --', value: null });
    this.listaDesague.push({ label: '-- Seleccione --', value: null });
    this.listaAgua.push({ label: '-- Seleccione --', value: null });
    this.listaMaterialContruccion.push({ label: '-- Seleccione --', value: null });
    this.listaElectricidad.push({ label: '-- Seleccione --', value: null });
    this.listaTenencia.push({ label: '-- Seleccione --', value: null });
    this.listaEstadoCivil.push({ label: '-- Seleccione --', value: null });
    this.listaGrupoSanguineo.push({ label: '-- Seleccione --', value: null });
    this.listaDeriva.push({ label: '-- Seleccione --', value: null });
    this.listaLegal.push({ label: '-- Seleccione --', value: null });
    this.listaTipoDoc.push({ label: '-- Seleccione --', value: null });
    this.listaPabellones.push({ label: '-- Seleccione --', value: null });
    this.listaCausal.push({ label: '-- Seleccione --', value: null });
    this.listaSeguro.push({ label: '-- Seleccione --', value: null });
    this.listaPrograma.push({ label: '-- Seleccione --', value: null });
    this.listaConadis.push({ label: '-- Seleccione --', value: null });
    this.listaFactor.push({ label: '-- Seleccione --', value: null });
    this.listaEquipamiento.push({ label: '-- Seleccione --', value: null });

    this.listaGradoInstruccion.push({ label: '-- Seleccione --', value: null });

    this.listaSexo.push({ label: 'Masculino', value: 'M' });
    this.listaSexo.push({ label: 'Femenino', value: 'F' });

    this.listaProgramaBean.push({ label: 'Programa Niñas, Niños y Adolescentes', value: 'NNA' });
    this.listaProgramaBean.push({ label: 'Programa Adultos Mayores', value: 'AAM' });

    var p1 = this.psInstitucionServicio.listarInstituciones()
      .then(td => { td.forEach(obj => this.listaInstituciones.push({ label: obj.nombre, value: obj.idInstitucion.trim() })); });

    var p2 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'PARENFUN')
      .then(r => { r.forEach(rr => { this.listaParentesco.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p3 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'OCUPAFUN')
      .then(r => { r.forEach(rr => { this.listaOcupacion.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p4 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SERDESAGU')
      .then(r => { r.forEach(rr => { this.listaDesague.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p5 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SERAGUAPO')
      .then(r => { r.forEach(rr => { this.listaAgua.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p6 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'MATECONST')
      .then(r => { r.forEach(rr => { this.listaMaterialContruccion.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p7 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'EQPBASICO')
      .then(r => { r.forEach(rr => { this.listaEquipamiento.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p8 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SERELECTR')
      .then(r => { r.forEach(rr => { this.listaElectricidad.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p9 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'TENENCINNA')
      .then(r => { r.forEach(rr => { this.listaTenencia.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p10 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'GRUPOSANGR')
      .then(r => { r.forEach(rr => { this.listaGrupoSanguineo.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p11 = this.maMiscelaneosdetalleServicio.listarActivos('HR', 'ESTCIVIL')
      .then(r => { r.forEach(rr => { this.listaEstadoCivil.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p12 = this.maMiscelaneosdetalleServicio.listarActivos('PS', this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_NNA ? 'INSDAAM' : 'INSDERI')
      .then(r => { r.forEach(rr => { this.listaDeriva.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p13 = null;
    var p14 = null;

    if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_NNA) {
      p13 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SITLEGNIN')
        .then(r => { r.forEach(rr => { this.listaLegal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
      p14 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SITLEGADU')
        .then(r => { r.forEach(rr => { }); });
    }
    else if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_AAM) {
      p13 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SITLEGADU')
        .then(r => { r.forEach(rr => { this.listaLegal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
      p14 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SITLEGNIN')
        .then(r => { r.forEach(rr => { }); });
    } else if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_ALL) {
      p13 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SITLEGNIN')
        .then(r => { r.forEach(rr => { this.listaLegal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
      p14 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SITLEGADU')
        .then(r => { r.forEach(rr => { this.listaLegal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
    }

    var p15 = this.maMiscelaneosdetalleServicio.listarActivos('PS', this.tipoFUN == this.BENEFICIARIO_TIPO_AAM ? "TIPDCAAM" : "TIPDCNNA")
      .then(r => { r.forEach(rr => { this.listaTipoDoc.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });




    var p16 = this.psInstitucionAreaServicio.listado(this.institucionActual)
      .then(r => {
        r.forEach(rr => {
          console.log(rr.idPrograma);
          console.log(this.bean.auxPrograma);
          if (rr.idPrograma == "NNA" && this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_NNA) {
            this.listaPabellones.push({ label: rr.nombre, value: rr.idArea });
          }
          if (rr.idPrograma == "AAM" && this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_AAM) {
            this.listaPabellones.push({ label: rr.nombre, value: rr.idArea });
          }
        });
      });

    var p17 = this.maMiscelaneosdetalleServicio.listarActivos('PS', this.tipoFUN == this.BENEFICIARIO_TIPO_AAM ? 'PROGSOAAM' : 'PROGSOCIA')
      .then(r => { r.forEach(rr => { this.listaPrograma.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p18 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'CARCONA')
      .then(r => { r.forEach(rr => { this.listaConadis.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
    var p19 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'FACTORRH')
      .then(r => { r.forEach(rr => { this.listaFactor.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
    var p20 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'SEGSAL')
      .then(r => { r.forEach(rr => { this.listaSeguro.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    var p21 = null;
    var p22 = null;


    if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_NNA) {
      p21 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'CINGNNA')
        .then(r => { r.forEach(rr => { this.listaCausal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
      p22 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'FAKE')
        .then(r => { r.forEach(rr => { }); });
    }

    else if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_AAM) {
      p21 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'CINGAAM')
        .then(r => { r.forEach(rr => { this.listaCausal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
      p14 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'FAKE')
        .then(r => { r.forEach(rr => { }); });

    } else if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_ALL) {
      p21 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'CINGAAM')
        .then(r => { r.forEach(rr => { this.listaCausal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
      p22 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'CINGNNA')
        .then(r => { r.forEach(rr => { this.listaCausal.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });
    }


    var p23 = this.parametrosmastServicio.obtenerValorTipoDatoFlag("BENEPAR", "PS", "999999").then(
      r => {
        this.conBeneficiarios = r == 'S' ? true : false;
      }
    );

    var p24 = this.maMiscelaneosdetalleServicio.listarActivos('PS', 'GRADINST')
      .then(r => { r.forEach(rr => { this.listaGradoInstruccion.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); }); });

    Promise.all([p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24]).then(
      r => {
        if (this.beneficiarioActual != 0) {
          this.accion = this.ACCIONES.EDITAR;
          var pk = new PsBeneficiarioPk();
          pk.idBeneficiario = this.beneficiarioActual;
          pk.idInstitucion = this.institucionActual;
          this.psBeneficiarioServicio.obtenerCompleto(pk).then(
            b => {
              this.bean = b;
              this.estadoBeneficiario = b.estadoBeneficiario;
              var diff = Math.abs(new Date().getTime() - this.bean.ingreso.fechaIngreso.getTime());
              var diffDays = Math.ceil(diff / (1000 * 3600 * 24));

              this.bean.ingreso.diasPermanencia = diffDays;

              this.desbloquearPagina();
            }
          );
        }
        else {
          this.accion = this.ACCIONES.NUEVO;
          this.bean = new PsEntidad();
          this.bean.ingreso = new PsBeneficiarioIngreso();
          this.bean.auxInstitucion = this.institucionActual;
          this.bean.creacionFecha = new Date();
          if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_NNA) {
            this.bean.auxPrograma = 'NNA';
            this.programaActual = 'NNA';
            this.bean.idEstadoCivil = 'S';
          }
          if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_AAM) {
            this.bean.auxPrograma = 'AAM';
            this.programaActual = 'AAM';
          }
          if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_ALL) {
            this.bean.auxPrograma = 'AAM';
            this.programaActual = 'AAM';

          }

          this.bean.ingreso.diasPermanencia = 0;



          this.desbloquearPagina();


        }

        this.cargarEstados();

        if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_NNA) {
          this.titulo = 'FUN Niños(as) y Adolescentes';
          this.esNino = true;
        }
        if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_AAM) {
          this.titulo = 'FUN Adultos Mayores';
          this.esAdulto = true;
        }
        if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_ALL) {
          this.titulo = 'FUN';
          this.esAdulto = true;
          this.esNino = true;
        }


      }
    );
  }

  agregarMiscelaneosSituacion() {
    const maMiscelaneosdetalle: MaMiscelaneosdetalle = new MaMiscelaneosdetalle();
    maMiscelaneosdetalle.aplicacioncodigo = PsConstante.APLICACION_CODIGO;
    if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_NNA) {
      maMiscelaneosdetalle.codigotabla = 'SITLEGNIN';
    } else {
      maMiscelaneosdetalle.codigotabla = 'SITLEGADU';
    }

    maMiscelaneosdetalle.compania = '999999';
    maMiscelaneosdetalle.estado = 'A';
    this.tag = 'SITUA';

    this.popUpMantenimientoMiscelaneoComponent.mostrarVentana(maMiscelaneosdetalle, null);
  }





  cargarEstados() {
    this.bean.estadoBeneficiario = null;
    this.maMiscelaneosdetalleServicio.listarActivos('PS', 'BENEESTA').then(
      r => {
        this.bean.estado = null;
        r.forEach(
          rr => {
            this.listaEstado.push({ label: rr.descripcionlocal, value: rr.codigoelemento });
          }
        );
        this.bean.estadoBeneficiario = this.estadoBeneficiario;
      });
  }

  guardar() {
    if (!this.validarGeneral()) {
      return;
    }

    this.bean.nombrecompleto = this.bean.apellidoPaterno + ' ' + this.bean.apellidoMaterno + ', ' + this.bean.nombres;
    this.bean.idTipoDocumento = 'D';
    if (this.accion == this.ACCIONES.NUEVO) {
      this.psBeneficiarioServicio.registrarCompleto(this.bean).then(
        r => {
          this.desbloquearPagina();
          if (r != null) {
            this.mostrarMensajeExito(this.getMensajeGrabadoBeneficiario(this.bean.nombrecompleto));
            this.router.navigate(['spring/beneficiario-listado']);
          }
        }
      )
    }
    else if (this.accion == this.ACCIONES.EDITAR) {
      this.psBeneficiarioServicio.actualizarCompleto(this.bean).then(
        r => {
          this.desbloquearPagina();
          if (r != null) {
            this.mostrarMensajeExito(this.getMensajeActualizadoEmpty());
            this.router.navigate(['spring/beneficiario-listado']);
          }
        }
      )
    }

  }

  validarGeneral() {
    var valida = true;
    if (this.estaVacio(this.bean.idSexo)) {
      this.mostrarMensajeAdvertencia('Seleccione el sexo del beneficiario');
      valida = false;
    }
    if (this.estaVacio(this.bean.apellidoPaterno)) {
      this.mostrarMensajeAdvertencia('El apellido paterno es requerido');
      valida = false;
    }
    if (this.estaVacio(this.bean.idDiscapacidad)) {
      this.mostrarMensajeAdvertencia('Seleccione si es discapacitado');
      valida = false;
    }

    if (this.bean.fechaNacimiento == null) {
      this.mostrarMensajeAdvertencia('La fecha de nacimiento es requerida');
      valida = false;
    }
    if (this.estaVacio(this.bean.nombres)) {
      this.mostrarMensajeAdvertencia('Los nombres son requeridos');
      valida = false;
    }

    /*
    if (false && this.estaVacio(this.bean.idGrupoSanguineo)) {
      this.mostrarMensajeAdvertencia('El grupo sanguineo es requerido');
      valida = false;
    }
    */


    /*
    if (false && this.estaVacio(this.bean.idFactorRh)) {
      this.mostrarMensajeAdvertencia('El factor Rh es requerido');
      valida = false;
    }
    */

    if (!this.estaVacio(this.bean.documento)) {
      var numberFormat = /^\d{8}$/;
      if (!numberFormat.test(this.bean.documento)) {
        this.mostrarMensajeAdvertencia('El DNI deben ser 8 números');
        valida = false;
      }
    }

    if (this.estaVacio(this.bean.idCarnetConadis) && this.bean.idDiscapacidad == 'S') {
      this.mostrarMensajeAdvertencia('El carnet de CONADIS es requerido');
      valida = false;
    }
    if (this.bean.ingreso.fechaIngreso == null) {
      this.mostrarMensajeAdvertencia('La fecha de ingreso es requerida');
      valida = false;
    }




    if (!this.estaVacio(this.bean.telefono1)) {
      var numberFormat = /^\d{7,11}$/;
      if (!numberFormat.test(this.bean.telefono1)) {
        this.mostrarMensajeAdvertencia('El teléfono debe ser de 7 a 11 números');
        valida = false;
      }
    }

    if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_AAM) {
      if (this.estaVacio(this.bean.flgPensionista)) {
        this.mostrarMensajeAdvertencia('Seleccione si es pensionista');
        valida = false;
      }
      else {
        if (this.bean.flgPensionista == "S") {
          /*if (this.bean.ingresoMensual == null) {
            this.mostrarMensajeAdvertencia('En caso sea pensionista el monto de ingreso es requerido');
            valida = false;
          }*/
        }
      }
      if (this.estaVacio(this.bean.flgFamiliares)) {
        this.mostrarMensajeAdvertencia('Seleccione si tiene familiares');
        valida = false;
      }
    }
    if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_NNA) {
      if (!this.estaVacio(this.bean.padreNroDocumento)) {
        var numberFormat = /^\d{8}$/;
        if (!numberFormat.test(this.bean.padreNroDocumento)) {
          this.mostrarMensajeAdvertencia('El DNI del padre deben ser 8 números');
          valida = false;
        }
      }
      if (!this.estaVacio(this.bean.madreNroDocumento)) {
        var numberFormat = /^\d{8}$/;
        if (!numberFormat.test(this.bean.madreNroDocumento)) {
          this.mostrarMensajeAdvertencia('El DNI de la madre deben ser 8 números');
          valida = false;
        }
      }
    }

    if (!this.estaVacio(this.bean.apoderadoNroDocumento)) {
      var numberFormat = /^\d{8}$/;
      if (!numberFormat.test(this.bean.apoderadoNroDocumento)) {
        this.mostrarMensajeAdvertencia('El DNI del apoderado(a) deben ser 8 números');
        valida = false;
      }
    }



    if (this.tipoFUN == PsBeneficiariomantenimientoComponent.BENEFICIARIO_TIPO_AAM) {
      if (!this.estaVacio(this.bean.telefono2)) {
        var numberFormat = /^\d{7,11}$/;
        if (!numberFormat.test(this.bean.telefono2)) {
          this.mostrarMensajeAdvertencia('El teléfono del apoderado debe ser de 7 a 11 números');
          valida = false;
        }
      }
      if (!this.estaVacio(this.bean.apoderadoEsposoaNroDocumento)) {
        var numberFormat = /^\d{8}$/;
        if (!numberFormat.test(this.bean.apoderadoNroDocumento)) {
          this.mostrarMensajeAdvertencia('El DNI del esposo(a) deben ser 8 números');
          valida = false;
        }
      }
    }

    return valida;
  }

  salir() {
    this.router.navigate(['spring/beneficiario-listado']);
  }

  agregarParienteBoton: string = 'Agregar';

  agregarPariente() {
    if (!this.validarPariente()) {
      return;
    }
    const lst = [...this.bean.listaPariente];

    if (this.beanPsEntidadPariente.idPariente == null) {
      var bb = new PsEntidadPariente();
      bb.idPariente = this.generarSecuenciaPariente();
      bb.idParentesco = this.beanPsEntidadPariente.idParentesco;
      bb.pariente = this.beanPsEntidadPariente.pariente;
      bb.auxParentesco = this.obtenerLabelPorCombo(this.listaParentesco, bb.idParentesco);
      lst.push(bb);
      this.bean.listaPariente = lst;
      this.beanPsEntidadPariente = new PsEntidadPariente();
      this.mostrarMensajeExito(this.getMensajeAgregado(bb.idPariente));
    }
    else {
      var bb = new PsEntidadPariente();
      bb.idPariente = this.beanPsEntidadPariente.idPariente;
      bb.idParentesco = this.beanPsEntidadPariente.idParentesco;
      bb.pariente = this.beanPsEntidadPariente.pariente;
      bb.auxParentesco = this.obtenerLabelPorCombo(this.listaParentesco, bb.idParentesco);

      const aux = this.bean.listaPariente.find(obj => obj.idPariente === bb.idPariente);
      const index = this.bean.listaPariente.indexOf(aux);
      lst[index] = bb;

      this.bean.listaPariente = lst;
      this.beanPsEntidadPariente = new PsEntidadPariente();
      this.mostrarMensajeExito(this.getMensajeActualizado(bb.idPariente));
      this.agregarParienteBoton = 'Agregar';
    }
  }
  generarSecuenciaPariente() {
    if (this.bean.listaPariente.length === 0) {
      return 1;
    }
    let max = this.bean.listaPariente[0].idPariente;
    this.bean.listaPariente.forEach(reg => {
      if (reg.idPariente > max) {
        max = reg.idPariente;
      }
    });

    return max + 1;
  }
  eliminarPariente(pariente) {
    let lst = [...this.bean.listaPariente];
    lst = lst.filter(x => x.idPariente != pariente);
    this.bean.listaPariente = lst;
    this.mostrarMensajeInfo(this.getMensajeEliminado(pariente));
  }
  editarPariente(bean: PsEntidadPariente) {
    this.beanPsEntidadPariente = new PsEntidadPariente();
    this.beanPsEntidadPariente.idParentesco = bean.idParentesco;
    this.beanPsEntidadPariente.idPariente = bean.idPariente;
    this.beanPsEntidadPariente.pariente = bean.pariente;
    this.agregarParienteBoton = 'Actualizar';
  }

  validarPariente() {
    var valido = true;
    if (this.estaVacio(this.beanPsEntidadPariente.idParentesco)) {
      this.mostrarMensajeAdvertencia('El parentesco es requerido');
      valido = false;
    }
    if (this.estaVacio(this.beanPsEntidadPariente.pariente)) {
      this.mostrarMensajeAdvertencia('El nombre del pariente es requerido');
      valido = false;
    }
    return valido;
  }

  agregarEquipamiento() {
    if (!this.validarEquipamiento()) {
      return;
    }
    const lst = [...this.bean.listaEquipamiento];

    var bb = new PsEntidadEquipamiento();
    bb.idEquipamiento = this.beanPsEntidadEquipamiento.idEquipamiento;
    bb.auxEquipamiento = this.obtenerLabelPorCombo(this.listaEquipamiento, bb.idEquipamiento);
    lst.push(bb);
    this.bean.listaEquipamiento = lst;
    this.beanPsEntidadEquipamiento = new PsEntidadEquipamiento();
    this.mostrarMensajeExito(this.getMensajeAgregado(bb.idEquipamiento));
  }
  eliminarEquipamiento(equipamiento) {
    let lst = [...this.bean.listaEquipamiento];
    lst = lst.filter(x => x.idEquipamiento != equipamiento);
    this.bean.listaEquipamiento = lst;
    this.mostrarMensajeInfo(this.getMensajeEliminado2(equipamiento));
  }
  validarEquipamiento() {
    var valido = true;
    if (this.estaVacio(this.beanPsEntidadEquipamiento.idEquipamiento)) {
      this.mostrarMensajeAdvertencia('El equipamiento es requerido');
      valido = false;
    }
    if (this.bean.listaEquipamiento.find(x => x.idEquipamiento == this.beanPsEntidadEquipamiento.idEquipamiento) != null) {
      this.mostrarMensajeAdvertencia('El equipamiento ya ha sido ingresado');
      valido = false;
    }
    return valido;
  }
  validarCausal() {
    var valido = true;
    if (this.estaVacio(this.beanPsBeneficiarioIngresoCasual.idCausal)) {
      this.mostrarMensajeAdvertencia('El causal es requerido');
      valido = false;
    }
    if (this.bean.ingreso.listaCausal.find(x => x.idCausal == this.beanPsBeneficiarioIngresoCasual.idCausal) != null) {
      this.mostrarMensajeAdvertencia('El causal ya ha sido ingresado');
      valido = false;
    }
    return valido;
  }
  agregarCausal() {
    if (!this.validarCausal()) {
      return;
    }
    const lst = [...this.bean.ingreso.listaCausal];

    var bb = new PsBeneficiarioIngresoCasual();
    bb.idCausal = this.beanPsBeneficiarioIngresoCasual.idCausal;
    bb.auxCausal = this.obtenerLabelPorCombo(this.listaCausal, bb.idCausal);
    lst.push(bb);
    this.bean.ingreso.listaCausal = lst;
    this.beanPsBeneficiarioIngresoCasual = new PsBeneficiarioIngresoCasual();
    this.mostrarMensajeExito(this.getMensajeAgregado(bb.idCausal));
  }
  eliminarCausal(causal) {
    let lst = [...this.bean.ingreso.listaCausal];
    lst = lst.filter(x => x.idCausal != causal);
    this.bean.ingreso.listaCausal = lst;
    this.mostrarMensajeInfo(this.getMensajeEliminado2(causal));
  }
  validarSeguro() {
    var valido = true;
    if (this.estaVacio(this.beanPsEntidadSeguroSocial.idSegurosocial)) {
      this.mostrarMensajeAdvertencia('El seguro salud es requerido');
      valido = false;
    }
    /*if (this.beanPsEntidadSeguroSocial.idSegurosocial != 'PRIV' && this.beanPsEntidadSeguroSocial.idSegurosocial != 'X' && this.estaVacio(this.beanPsEntidadSeguroSocial.seguroSocial)) {
      this.mostrarMensajeAdvertencia('El número de seguro es requerido');
      valido = false;
    }*/
    if (this.bean.listaSeguro.find(x => x.idSegurosocial == this.beanPsEntidadSeguroSocial.idSegurosocial) != null) {
      this.mostrarMensajeAdvertencia('El seguro de salud ya ha sido ingresado');
      valido = false;
    }
    return valido;
  }
  agregarSeguro() {
    if (!this.validarSeguro()) {
      return;
    }
    const lst = [...this.bean.listaSeguro];

    var bb = new PsEntidadSeguroSocial();
    bb.idSegurosocial = this.beanPsEntidadSeguroSocial.idSegurosocial;
    bb.seguroSocial = this.beanPsEntidadSeguroSocial.seguroSocial;
    bb.auxSeguro = this.obtenerLabelPorCombo(this.listaSeguro, bb.idSegurosocial);
    lst.push(bb);
    this.bean.listaSeguro = lst;
    this.beanPsEntidadSeguroSocial = new PsEntidadSeguroSocial();
    this.mostrarMensajeExito(this.getMensajeAgregadoEmpty());
  }
  eliminarSeguro(id) {
    let lst = [...this.bean.listaSeguro];
    lst = lst.filter(x => x.idSegurosocial != id);
    this.bean.listaSeguro = lst;
    this.mostrarMensajeInfo(this.getMensajeEliminado2(id));
  }
  validarPrograma() {
    var valido = true;
    if (this.estaVacio(this.beanPsEntidadProgramaSocial.idProgramaSocial)) {
      this.mostrarMensajeAdvertencia('El programa social es requerido');
      valido = false;
    }
    if (this.bean.listaPrograma.find(x => x.idProgramaSocial == this.beanPsEntidadProgramaSocial.idProgramaSocial) != null) {
      this.mostrarMensajeAdvertencia('El programa social ya ha sido ingresado');
      valido = false;
    }
    return valido;
  }
  agregarPrograma() {
    if (!this.validarPrograma()) {
      return;
    }
    const lst = [...this.bean.listaPrograma];

    var bb = new PsEntidadProgramaSocial();
    bb.idProgramaSocial = this.beanPsEntidadProgramaSocial.idProgramaSocial;
    bb.auxPrograma = this.obtenerLabelPorCombo(this.listaPrograma, bb.idProgramaSocial);
    lst.push(bb);
    this.bean.listaPrograma = lst;
    this.beanPsEntidadProgramaSocial = new PsEntidadProgramaSocial();
    this.mostrarMensajeExito(this.getMensajeAgregado(bb.idProgramaSocial));
  }
  eliminarPrograma(id) {
    let lst = [...this.bean.listaPrograma];
    lst = lst.filter(x => x.idProgramaSocial != id);
    this.bean.listaPrograma = lst;
    this.mostrarMensajeInfo(this.getMensajeEliminado2(id));
  }

  validarDocumento() {
    var valido = true;
    if (this.estaVacio(this.beanPsEntidadDocumento.idTipoDocumento)) {
      this.mostrarMensajeAdvertencia('El tipo documento es requerido');
      valido = false;
    }
    if (this.bean.listaDocumento.find(x => x.idTipoDocumento == this.beanPsEntidadDocumento.idTipoDocumento) != null) {
      this.mostrarMensajeAdvertencia('El tipo documento ya ha sido ingresado');
      valido = false;
    }
    return valido;
  }
  agregarDocumento() {
    if (!this.validarDocumento()) {
      return;
    }
    const lst = [...this.bean.listaDocumento];

    var bb = new PsEntidadDocumento();
    bb.idTipoDocumento = this.beanPsEntidadDocumento.idTipoDocumento;
    bb.auxTipo = this.obtenerLabelPorCombo(this.listaTipoDoc, bb.idTipoDocumento);
    lst.push(bb);
    this.bean.listaDocumento = lst;
    this.beanPsEntidadDocumento = new PsEntidadDocumento();
    this.mostrarMensajeExito(this.getMensajeAgregado(bb.idTipoDocumento));
  }
  eliminarDocumento(id) {
    let lst = [...this.bean.listaDocumento];
    lst = lst.filter(x => x.idTipoDocumento != id);
    this.bean.listaDocumento = lst;
    this.mostrarMensajeInfo(this.getMensajeEliminado2(id));
  }

  calcularEdad() {
    if (this.bean.fechaNacimiento == null) {
      return;
    }
    this.bean.edad = this.transform(this.bean.fechaNacimiento);
  }

  calcularPerm() {
    /*if (this.bean.ingreso.fechaIngreso == null) {
      return;
    }
   
    var diff = Math.abs(new Date().getTime() - this.bean.ingreso.fechaIngreso.getTime());
    var diffDays = Math.ceil(diff / (1000 * 3600 * 24));
   
    this.bean.ingreso.diasPermanencia = diffDays;*/
  }

  cargarUbigeo(data) {
    if (data.tag == 'CO') {
      this.bean.domicilioIdUbigeo = data.data.codigoElemento.trim();
      this.bean.auxUbigeoConocido = data.data.descripcion;
    }
    else if (data.tag == 'NA') {
      this.bean.idNacimientoPais = '001';
      this.bean.idNacimientoUbigeo = data.data.codigoElemento.trim();
      this.bean.auxUbigeo = data.data.descripcion;
    }
  }
  seleccionarNacimiento(tag) {
    this.ubicacionGeograficaSelectorComponent.tag = tag;
    this.ubicacionGeograficaSelectorComponent.iniciarComponente();
  }

  agregarMiscelaneos(codigo, app, tag) {
    const maMiscelaneosdetalle: MaMiscelaneosdetalle = new MaMiscelaneosdetalle();
    maMiscelaneosdetalle.aplicacioncodigo = app;
    maMiscelaneosdetalle.codigotabla = codigo;
    maMiscelaneosdetalle.compania = '999999';
    maMiscelaneosdetalle.estado = 'A';
    this.tag = tag;
    this.popUpMantenimientoMiscelaneoComponent.mostrarVentana(maMiscelaneosdetalle, null);
  }


  listaActualizar: SelectItem[] = [];
  tag: string = "";
  buscarLista(data) {
    var d = data as MaMiscelaneosdetalle;
    this.bloquearPagina();
    this.listaActualizar = [];
    this.listaActualizar.push({ label: '-- Seleccione --', value: null });
    this.maMiscelaneosdetalleServicio.listarActivos(d.aplicacioncodigo, d.codigotabla)
      .then(r => {
        r.forEach(rr => { this.listaActualizar.push({ label: rr.descripcionlocal, value: rr.codigoelemento }); });

        switch (this.tag) {
          case "causal": {
            this.beanPsBeneficiarioIngresoCasual.idCausal = null;
            this.listaCausal = this.listaActualizar;
            this.beanPsBeneficiarioIngresoCasual.idCausal = d.codigoelemento;
            this.agregarCausal();
            break;
          }
          case "TIPOD": {
            this.beanPsEntidadDocumento.idTipoDocumento = null;
            this.listaTipoDoc = this.listaActualizar;
            this.beanPsEntidadDocumento.idTipoDocumento = d.codigoelemento;
            this.agregarDocumento();
            break;
          }
          case "TENEN": {
            this.bean.idTenencia = null;
            this.listaTenencia = this.listaActualizar;
            this.bean.idTenencia = d.codigoelemento;
            break;
          }
          case "ELEC": {
            this.bean.idServicioElectricidad = null;
            this.listaElectricidad = this.listaActualizar;
            this.bean.idServicioElectricidad = d.codigoelemento;
            break;
          }
          case "MATCO": {
            this.bean.idMaterialConstruccion = null;
            this.listaMaterialContruccion = this.listaActualizar;
            this.bean.idMaterialConstruccion = d.codigoelemento;
            break;
          }
          case "EQUIP": {
            this.beanPsEntidadEquipamiento.idEquipamiento = null;
            this.listaEquipamiento = this.listaActualizar;
            this.beanPsEntidadEquipamiento.idEquipamiento = d.codigoelemento;
            this.agregarEquipamiento();
            break;
          }
          case "AGUA": {
            this.bean.idServicioAgua = null;
            this.listaAgua = this.listaActualizar;
            this.bean.idServicioAgua = d.codigoelemento;
            break;
          }
          case "DESA": {
            this.bean.idServicioDesague = null;
            this.listaDesague = this.listaActualizar;
            this.bean.idServicioDesague = d.codigoelemento;
            break;
          }
          case "DERIVA": {
            this.bean.ingreso.idInstitucionDeriva = null;
            this.listaDeriva = this.listaActualizar;
            this.bean.ingreso.idInstitucionDeriva = d.codigoelemento;
            break;
          }
          case "SITUA": {
            this.bean.ingreso.idSituacionLegal = null;
            this.listaLegal = this.listaActualizar;
            this.bean.ingreso.idSituacionLegal = d.codigoelemento;
            break;
          }
          default:
            break;
        }


        this.desbloquearPagina();
      });
  }

  buscarBeneficiario() {
    this.bloquearPagina();
    var numero = this.bean.documento;
    //var apepa = this.bean.apellidoPaterno;
    //var nom = this.bean.nombres;
    var programa = this.programaActual;


    this.psBeneficiarioServicio.obtenerDatosBasicos(this.bean).then(
      b => {

        if (b != null) {



          this.psBeneficiarioServicio.obtenerPorId(b.auxInstitucion, b.idEntidad).then(
            res => {

              this.beanPsbeneficiario = res;


              if (b != null && b.auxPrograma == programa && this.beanPsbeneficiario.estado == 'EGR') {


                this.bean = b;
                this.bean.auxInstitucion = this.institucionActual;
                this.bean.auxPrograma = this.programaActual;

              }


              if (this.beanPsbeneficiario.idInstitucion == this.institucionActual) {
                //b = new PsEntidad();
                b.auxInstitucion = this.institucionActual;
                b.auxPrograma = this.programaActual;
                //b.documento = numero;
                //b.apellidoPaterno = apepa;
                //b.nombres = nom;
                this.bean = b;
                this.mostrarMensajeAdvertencia("El beneficiario pertenece a la institución");
                this.desbloquearPagina();
                return;

              }
              else {
                this.bean.auxInstitucionOrigen = this.beanPsbeneficiario.idInstitucion;
              }

              if (this.beanPsbeneficiario.estado != 'EGR') {
                //b = new PsEntidad();
                b.auxInstitucion = this.institucionActual;
                b.auxPrograma = this.programaActual;
                //b.documento = numero;
                //b.apellidoPaterno = apepa;
                //b.nombres = nom;
                this.bean = b;
                this.mostrarMensajeAdvertencia("El beneficiario no se encuentra egresado");
              }

            }
          );





          if (b != null && b.auxPrograma != programa) {
            //b = new PsEntidad();
            b.auxInstitucion = this.institucionActual;
            b.auxPrograma = this.programaActual;
            //b.documento = numero;
            //b.apellidoPaterno = apepa;
            //b.nombres = nom;
            this.bean = b;
            this.mostrarMensajeAdvertencia("El beneficiario pertenece a otro programa");

          }
        }
        else {
          // b = new PsEntidad();
          //b.auxInstitucion = this.institucionActual;
          //b.auxPrograma = this.programaActual;
          // b.documento = numero;
          //b.apellidoPaterno = apepa;
          //b.nombres = nom;
          //this.bean = b;


          this.mostrarMensajeAdvertencia("No se encuentra al beneficiario");
        }



        this.desbloquearPagina();

      }
    );



  }



}
