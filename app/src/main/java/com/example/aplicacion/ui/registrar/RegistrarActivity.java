package com.example.aplicacion.ui.registrar;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;

import com.example.aplicacion.Entitiy.Pais;
import com.example.aplicacion.MainActivity;
import com.example.aplicacion.R;
import com.example.aplicacion.controlador.PaisDAO;
import com.example.aplicacion.taskPais.ServicioTaskListPais;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class RegistrarActivity extends AppCompatActivity {

    Button btnRegistrar;
    Spinner spnNac;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registrar);

        btnRegistrar = (Button) findViewById(R.id.btnRegistrar);
        spnNac = (Spinner) findViewById(R.id.spnNacionalidad);


        cargarPais();
        btnRegistrar.setText("Registrar");
        getSupportActionBar().setTitle("Registro");

        btnRegistrar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                registrar();
                Intent intent = new Intent(RegistrarActivity.this, MainActivity.class);
                startActivity(intent);
            }
        });

    }

    public void cargarPais(){
        try{
            ServicioTaskListPais servicio=new ServicioTaskListPais(this,"https://my-json-server.typicode.com/typicode/demo/posts/");
            servicio.execute();
            String c=servicio.get();
            ArrayList<Pais> data= PaisDAO.listaPaises(c);
            ArrayAdapter<Pais> adapter=new ArrayAdapter<Pais>(this,android.R.layout.simple_list_item_1,data);
            spnNac.setAdapter(adapter);
        }
        catch (Exception e){
            e.printStackTrace();
        }
    }


    public void registrar(){


    }




    }

