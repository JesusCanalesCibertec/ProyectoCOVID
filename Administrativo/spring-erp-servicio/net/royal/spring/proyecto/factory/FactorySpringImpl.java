package net.royal.spring.factory;

public class FactorySpringImpl implements FactorySpring {
 private PmProyectoServicio pmProyectoServicio;

 @Override
 public PmProyectoServicio getPmProyectoServicio() {
     return pmProyectoServicio;
 }

 @Override
 public void setPmProyectoServicio(PmProyectoServicio pmProyectoServicio) {
     this.pmProyectoServicio = pmProyectoServicio;
 }

 private PmTareaServicio pmTareaServicio;

 @Override
 public PmTareaServicio getPmTareaServicio() {
     return pmTareaServicio;
 }

 @Override
 public void setPmTareaServicio(PmTareaServicio pmTareaServicio) {
     this.pmTareaServicio = pmTareaServicio;
 }

}
