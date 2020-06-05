package com.example.aplicacion.controlador;

import com.example.aplicacion.Entitiy.Pais;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

public class PaisDAO {

    public static ArrayList<Pais> listaPaises(String json){
        ArrayList<Pais> data=new ArrayList<Pais>();
        try {
            JSONArray jsonArray=new JSONArray(json);
            JSONObject fila;
            for(int i=0;i<jsonArray.length();i++){
                fila=(JSONObject)jsonArray.get(i);
                Pais pais=new Pais();
                pais.setId(fila.getInt("id"));
                pais.setTitle(fila.getString("title"));
                data.add(pais);
            }
        }
        catch (Exception e){
            e.printStackTrace();
        }
        return data;
    }

}
