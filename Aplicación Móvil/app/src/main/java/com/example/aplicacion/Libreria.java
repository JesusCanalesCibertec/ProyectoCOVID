package com.example.aplicacion;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class Libreria {

    //método estatico
    public static String leerPagina(String urlLink){
        String datos="";
        //objeto para abrir y cerrar la conexion de un URL
        HttpURLConnection cn=null;
        try {
            //objeto para establecer direccion URL
            URL url=new URL(urlLink);
            //abrir conexion
            cn=(HttpURLConnection)url.openConnection();
            //tiempo para leer la página
            cn.setReadTimeout(10000);
            //tiempo para indicar la apertuar de la conexion
            cn.setConnectTimeout(10000);
            //acceso al tipo de anotacion GET "SELECT"
            cn.setRequestMethod("GET");
            //entrada de datos
            cn.setDoInput(true);
            //conectar a la direccion URL
            cn.connect();
            //validar el estado del objeto "cn"
            if(HttpURLConnection.HTTP_OK==cn.getResponseCode()){
                //objeto para almacenar el contendio de "cn", el
                //objeto "cn" contiene bytes
                InputStream is=cn.getInputStream();
                //
                BufferedReader br=new
                        BufferedReader(new InputStreamReader(is));
                //
                String fila;
                //bucle
                while((fila=br.readLine())!=null){
                    datos+=fila;
                }
            }
        }
        catch (Exception e){
            e.printStackTrace();
        }
        finally {
            cn.disconnect();
        }
        return datos;
    }


}
