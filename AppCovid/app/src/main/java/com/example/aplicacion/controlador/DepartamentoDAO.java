package com.example.aplicacion.controlador;

import com.example.aplicacion.Entitiy.Departamento;
import com.example.aplicacion.Entitiy.Pais;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

public class DepartamentoDAO {

    public static ArrayList<Departamento> listaDepartamento(String json){
        ArrayList<Departamento> data=new ArrayList<Departamento>();
        try {
            JSONArray jsonArray=new JSONArray(json);
            JSONObject fila;
            for(int i=0;i<jsonArray.length();i++){
                fila=(JSONObject)jsonArray.get(i);
                Departamento departamento=new Departamento();
                departamento.setDepartamento(fila.getInt("departamento"));
                departamento.setDescripcion(fila.getString("descripcion"));
                data.add(departamento);
            }
        }
        catch (Exception e){
            e.printStackTrace();
        }
        return data;
    }

}
