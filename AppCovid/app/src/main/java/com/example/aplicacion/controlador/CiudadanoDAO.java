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
                c.setNombre(jsonObject.getString("nombre"));
                c.setApellido(jsonObject.getString("apellido"));
                c.setIdPais(jsonObject.getString("idPais"));
                c.setNroDocumento(jsonObject.getString("nroDocumento"));
                c.setEstado(jsonObject.getInt("estado"));
            }
        catch (Exception e){
            e.printStackTrace();
        }
        return c;
    }

}
