package com.example.aplicacion;

import android.content.Context;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;

public class Session {

    private SharedPreferences prefs;

    public Session(Context context){
        prefs = PreferenceManager.getDefaultSharedPreferences(context);
    }

    public void setIdCiudadano(int idCiu){
        prefs.edit().putInt("idCiu",idCiu).commit();
    }

    public int getIdCiudadano(){
        int idCiu = prefs.getInt("idCiu",0);
        return idCiu;
    }

}
