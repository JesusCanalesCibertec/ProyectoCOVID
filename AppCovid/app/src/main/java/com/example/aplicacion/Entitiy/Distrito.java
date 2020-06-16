package com.example.aplicacion.Entitiy;

public class Distrito {
    private int departamento;
    private int provincia;
    private int codigopostal;
    private String descripcion;

    @Override
    public String toString(){
        return descripcion;
    }

    public int getDepartamento() {
        return departamento;
    }

    public void setDepartamento(int departamento) {
        this.departamento = departamento;
    }

    public int getProvincia() {
        return provincia;
    }

    public void setProvincia(int provincia) {
        this.provincia = provincia;
    }

    public int getCodigopostal() {
        return codigopostal;
    }

    public void setCodigopostal(int codigopostal) {
        this.codigopostal = codigopostal;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }
}
