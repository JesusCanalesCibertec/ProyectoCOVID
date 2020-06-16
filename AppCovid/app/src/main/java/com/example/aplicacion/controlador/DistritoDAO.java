package com.example.aplicacion.controlador;

import com.example.aplicacion.Entitiy.Distrito;
import com.example.aplicacion.Entitiy.Pais;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

public class DistritoDAO {

    public static ArrayList<Distrito> listaDistrito(String json){
        ArrayList<Distrito> data=new ArrayList<Distrito>();
        try {
            JSONArray jsonArray=new JSONArray(json);
            JSONObject fila;
            for(int i=0;i<jsonArray.length();i++){
                fila=(JSONObject)jsonArray.get(i);
                Distrito distrito=new Distrito();
                distrito.setCodigopostal(fila.getInt("codigopostal"));
                distrito.setDescripcion(fila.getString("descripcion"));
                data.add(distrito);
            }
        }
        catch (Exception e){
            e.printStackTrace();
        }
        return data;
    }

}
