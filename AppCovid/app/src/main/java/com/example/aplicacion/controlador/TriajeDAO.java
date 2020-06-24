package com.example.aplicacion.controlador;

import com.example.aplicacion.Entitiy.Pais;
import com.example.aplicacion.Entitiy.Triaje;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

public class TriajeDAO {

    public static Triaje responseTriaje(String json){
        Triaje tri=new Triaje();
        try {
            JSONObject jsonObject=new JSONObject(json);
                tri.setEstado(jsonObject.getInt("estado"));
            }
        catch (Exception e){
            e.printStackTrace();
        }
        return tri;
    }

}
