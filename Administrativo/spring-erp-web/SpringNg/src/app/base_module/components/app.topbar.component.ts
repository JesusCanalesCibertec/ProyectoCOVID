import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { LayoutComponent } from './layout.component';
import { UsuarioServicio } from 'src/app/login_module/servicio/UsuarioServicio';
import { EmpleadomastServicio } from 'src/app/erp_module/shared/selectores/empleado/servicio/EmpleadomastServicio';
import { PersonaService } from 'src/app/erp_module/minedu/persona/servicio/persona.service';

@Component({
  selector: 'app-topbar',
  templateUrl: './app.topbar.component.html'
})
export class AppTopBarComponent implements OnInit {

  nombre: string = "Cargando...";

  constructor(
    public app: LayoutComponent, 
    private empleadomastServicio: EmpleadomastServicio,
    private personaService: PersonaService,
    ) {

  }

  ngOnInit() {
     this.empleadomastServicio.obtenerInformacionPorIdPersonaUsuarioActual().then(
      res => {
        this.nombre = 'BIENVENIDO:  '+ res.personaNombre;
      }
    );
  }


}
