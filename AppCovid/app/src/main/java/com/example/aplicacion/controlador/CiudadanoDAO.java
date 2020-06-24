package com.example.aplicacion.controlador;

import com.example.aplicacion.Entitiy.Ciudadano;
import com.example.aplicacion.Entitiy.Triaje;

import org.json.JSONObject;

public class CiudadanoDAO {

    public static Ciudadano responseCiudadano(String json){
        Ciudadano c=new Ciudadano();
        try {
            JSONObject jsonObject=new JSONObject(json);
                c.setIdCiudadano(jsonObject.getInt("idCiudadano"));
                c.setNombre(jsonObject.getString(""));
                c.setApellido(jsonObject.getString(""));
                c.setIdPais(jsonObject.getString(""));
                c.setNroDocumento(jsonObject.getString(""));
                c.setEstado(jsonObject.getInt(""));
            }
        catch (Exception e){
            e.printStackTrace();
        }
        return c;
    }

}
