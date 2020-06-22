package com.example.aplicacion.controlador;

import com.example.aplicacion.Entitiy.Pais;
import com.example.aplicacion.Entitiy.Provincia;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

public class ProvinciaDAO {

    public static ArrayList<Provincia> listaProvincia(String json){
        ArrayList<Provincia> data=new ArrayList<Provincia>();
        try {
            JSONArray jsonArray=new JSONArray(json);
            JSONObject fila;
            for(int i=0;i<jsonArray.length();i++){
                fila=(JSONObject)jsonArray.get(i);
                Provincia provincia=new Provincia();
                provincia.setProvincia(fila.getInt("provincia"));
                provincia.setDescripcion(fila.getString("descripcion"));
                data.add(provincia);
            }
        }
        catch (Exception e){
            e.printStackTrace();
        }
        return data;
    }

}
