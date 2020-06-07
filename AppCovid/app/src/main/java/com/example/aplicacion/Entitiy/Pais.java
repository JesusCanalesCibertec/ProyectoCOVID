package com.example.aplicacion.Entitiy;

public class Pais {
    private int id;
    private String title;

    @Override
    public String toString(){
        return title;
    }


    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }
}
