package com.example.aplicacion.Entitiy;

public class Departamento {
    private int departamento;
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

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }
}
