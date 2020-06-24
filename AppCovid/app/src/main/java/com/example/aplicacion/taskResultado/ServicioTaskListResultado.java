package com.example.aplicacion.taskResultado;

import android.content.Context;
import android.os.AsyncTask;

import com.example.aplicacion.Libreria;

public class ServicioTaskListResultado extends AsyncTask<Void,Void,String> {

    private Context context;
    private String urlLink;

    public ServicioTaskListResultado(Context context, String urlLink) {
        this.context = context;
        this.urlLink = urlLink;
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
    }

    @Override
    protected  String doInBackground(Void... voids){
        String contenido="";
        try{
            contenido = Libreria.leerPagina(urlLink);
        }catch(Exception e){
            e.printStackTrace();
        }
        return contenido;
    }
    @Override
    protected void onPostExecute(String s) {
        super.onPostExecute(s);

    }
}
