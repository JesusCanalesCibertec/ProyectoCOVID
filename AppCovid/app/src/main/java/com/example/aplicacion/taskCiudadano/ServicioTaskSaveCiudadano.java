package com.example.aplicacion.taskCiudadano;

import android.content.Context;
import android.os.AsyncTask;

import com.example.aplicacion.Entitiy.Ciudadano;
import com.example.aplicacion.Entitiy.Pais;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class ServicioTaskSaveCiudadano extends AsyncTask<Void,Void,String> {
    private Context context;
    private String linkURL;
    private Ciudadano ciudadano;

    public ServicioTaskSaveCiudadano(Context context, String linkURL, Ciudadano ciudadano) {
        this.context = context;
        this.linkURL = linkURL;
        this.ciudadano = ciudadano;
    }


    @Override
    protected String doInBackground(Void... voids) {
        URL url = null;
        String datos="";
        try{
            url = new URL(linkURL);
            HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();

            JSONObject paramPost = new JSONObject();
            paramPost.put("nombre", ciudadano.getNombre());
            paramPost.put("apellido", ciudadano.getApellido());
            paramPost.put("idPais", ciudadano.getIdPais());
            paramPost.put("tipoDocumento", ciudadano.getTipoDocumento());
            paramPost.put("nroDocumento", ciudadano.getNroDocumento());
            paramPost.put("fechaNacimiento", ciudadano.getFechaNacimiento());
            paramPost.put("direccion", ciudadano.getDireccion());
            paramPost.put("idDepartamento", ciudadano.getIdDepartamento());
            paramPost.put("idProvincia", ciudadano.getIdProvincia());
            paramPost.put("idDistrito", ciudadano.getIdDistrito());


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
