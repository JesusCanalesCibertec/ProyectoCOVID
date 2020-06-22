package com.example.aplicacion.Entitiy;

public class Pais {
    private int idPais;
    private String descripcion;

    @Override
    public String toString(){
        return descripcion;
    }

    public int getIdPais() {
        return idPais;
    }

    public void setIdPais(int idPais) {
        this.idPais = idPais;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }
}
