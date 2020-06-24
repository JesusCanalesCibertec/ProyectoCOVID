package com.example.aplicacion.controlador;

import com.example.aplicacion.Entitiy.Resultado;
import com.example.aplicacion.Entitiy.Triaje;

import org.json.JSONObject;

public class ResultadoDAO {

    public static Resultado responseResultado(String json){
        Resultado res=new Resultado();
        try {
            JSONObject jsonObject=new JSONObject(json);
                res.setNombre(jsonObject.getString("nombre"));
                res.setDescripcion(jsonObject.getString("descripcion"));
                res.setRecomendacion(jsonObject.getString("recomendacion"));

            }
        catch (Exception e){
            e.printStackTrace();
        }
        return res;
    }

}
