package com.example.aplicacion.Entitiy;

public class Pais {
    private int pais;
    private String nacionalidad;

    @Override
    public String toString(){
        return nacionalidad;
    }

    public int getPais() {
        return pais;
    }

    public void setPais(int pais) {
        this.pais = pais;
    }

    public String getNacionalidad() {
        return nacionalidad;
    }

    public void setNacionalidad(String nacionalidad) {
        this.nacionalidad = nacionalidad;
    }
}
