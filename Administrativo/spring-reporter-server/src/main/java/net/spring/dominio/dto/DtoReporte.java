package net.spring.dominio.dto;

public class DtoReporte {

	public String excepcion;
	public byte[] datos;

	public DtoReporte() {

	}

	public DtoReporte(String excepcion, byte[] datos) {
		this.excepcion = excepcion;
		this.datos = datos;
	}

	public String getExcepcion() {
		return excepcion;
	}

	public void setExcepcion(String excepcion) {
		this.excepcion = excepcion;
	}

	public byte[] getDatos() {
		return datos;
	}

	public void setDatos(byte[] datos) {
		this.datos = datos;
	}

}
