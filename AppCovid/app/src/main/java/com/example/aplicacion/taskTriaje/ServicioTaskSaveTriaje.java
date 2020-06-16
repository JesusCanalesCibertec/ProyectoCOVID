package com.example.aplicacion.taskTriaje;

import android.content.Context;
import android.os.AsyncTask;

import com.example.aplicacion.Entitiy.Ciudadano;
import com.example.aplicacion.Entitiy.Pais;
import com.example.aplicacion.Entitiy.Triaje;

import org.json.JSONObject;

import java.io.DataOutputStream;
import java.net.HttpURLConnection;
import java.net.URL;

public class ServicioTaskSaveTriaje extends AsyncTask<Void,Void,Void> {
    private Context context;
    private String linkURL;
    private Triaje triaje;

    public ServicioTaskSaveTriaje(Context context, String linkURL, Pais pais) {
        this.context = context;
        this.linkURL = linkURL;
        this.triaje = triaje;
    }


    @Override
    protected Void doInBackground(Void... voids) {
        URL url = null;
        try{
            url = new URL(linkURL);
            HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();

            JSONObject paramPost = new JSONObject();
            //paramPost.put("id",pais.getId());


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
                //server_reponse = readStream(urlConnection.getInputStream());
            }

        }catch(Exception e){
            e.printStackTrace();
        }
        return null;
    }
}
