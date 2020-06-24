package com.example.aplicacion.taskTriaje;

import android.content.Context;
import android.os.AsyncTask;

import com.example.aplicacion.Entitiy.Ciudadano;
import com.example.aplicacion.Entitiy.Pais;
import com.example.aplicacion.Entitiy.Triaje;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class ServicioTaskSaveTriaje extends AsyncTask<Void,Void,String> {
    private Context context;
    private String linkURL;
    private Triaje triaje;

    public ServicioTaskSaveTriaje(Context context, String linkURL, Triaje triaje) {
        this.context = context;
        this.linkURL = linkURL;
        this.triaje = triaje;
    }


    @Override
    //void
    protected String doInBackground(Void... voids) {
        URL url = null;
        String datos="";
        try{
            url = new URL(linkURL);
            HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();

            JSONObject paramPost = new JSONObject();
            paramPost.put("idCiudadano",triaje.getIdCiudadano());
            paramPost.put("disgus",triaje.getDisgus());
            paramPost.put("tos",triaje.getTos());
            paramPost.put("dolor",triaje.getDolor());
            paramPost.put("difi",triaje.getDifi());
            paramPost.put("nasal",triaje.getNasal());
            paramPost.put("fiebre",triaje.getFiebre());
            paramPost.put("fechainicio",triaje.getFechainicio());
            paramPost.put("situacion1",triaje.getSituacion1());
            paramPost.put("situacion2",triaje.getSituacion2());
            paramPost.put("situacion3",triaje.getSituacion3());
            paramPost.put("obesidad",triaje.getObesidad());
            paramPost.put("pulmonar",triaje.getPulmonar());
            paramPost.put("asma",triaje.getAsma());
            paramPost.put("diabetes",triaje.getDiabetes());
            paramPost.put("hipertension",triaje.getHipertension());
            paramPost.put("cardio",triaje.getCardio());
            paramPost.put("renal",triaje.getRenal());
            paramPost.put("cancer",triaje.getCancer());



            //Parametros de conexion
            urlConnection.setReadTimeout(15000);
            urlConnection.setConnectTimeout(15000);
            urlConnection.setRequestMethod("POST");
            urlConnection.setRequestProperty("Content-Type","application/json");
            urlConnection.setDoOutput(true);
            DataOutputStream wr = new DataOutputStream(urlConnection.getOutputStream());
            wr.writeBytes(paramPost.toString());
            wr.flush();
            wr.close();
            urlConnection.connect();
            int responseCode = urlConnection.getResponseCode();
            if(responseCode == HttpURLConnection.HTTP_OK){
                InputStream is = urlConnection.getInputStream();
                BufferedReader br = new BufferedReader(new InputStreamReader(is));
                String data="";
                while((data=br.readLine())!=null){
                    datos+=data;
                }
            }

        }catch(Exception e){
            e.printStackTrace();
        }
        return datos;
    }
}
