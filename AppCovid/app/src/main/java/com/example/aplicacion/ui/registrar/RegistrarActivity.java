package com.example.aplicacion.ui.registrar;

import androidx.appcompat.app.AppCompatActivity;

import android.app.DatePickerDialog;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Spinner;

import com.example.aplicacion.Entitiy.Pais;
import com.example.aplicacion.Interface.Api;
import com.example.aplicacion.MainActivity;
import com.example.aplicacion.R;
import com.example.aplicacion.controlador.PaisDAO;
import com.example.aplicacion.taskPais.ServicioTaskListPais;
import com.example.aplicacion.taskPais.ServicioTaskSaveTest;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class RegistrarActivity extends AppCompatActivity {

    Button btnRegistrar;
    Spinner spnNac;
    EditText txtNombre;
    EditText txtFecha;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registrar);

        btnRegistrar = (Button) findViewById(R.id.btnRegistrar);
        spnNac = (Spinner) findViewById(R.id.spnNacionalidad);
        txtFecha = (EditText) findViewById(R.id.txtFecNac);
        txtNombre = (EditText) findViewById(R.id.txtNombre);


        cargarPais();
        getSupportActionBar().setTitle("Registro");

        txtFecha.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showDateDailog(txtFecha);
            }
        });


        btnRegistrar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                registrar();
                Intent intent = new Intent(RegistrarActivity.this, MainActivity.class);
                startActivity(intent);
            }
        });

    }

    private void showDateDailog(EditText fecha) {
        Calendar calendario = Calendar.getInstance();
        DatePickerDialog.OnDateSetListener dateSetListener = new DatePickerDialog.OnDateSetListener() {
            @Override
            public void onDateSet(DatePicker datePicker, int year, int month, int day) {
                calendario.set(Calendar.YEAR,year);
                calendario.set(Calendar.MONTH,month);
                calendario.set(Calendar.DAY_OF_MONTH,day);
                SimpleDateFormat oSimpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");

                fecha.setText(oSimpleDateFormat.format(calendario.getTime()));
            }
        };
        new DatePickerDialog(RegistrarActivity.this,dateSetListener,
                calendario.get(Calendar.YEAR),calendario.get(Calendar.MONTH), calendario.get(Calendar.DAY_OF_MONTH)).show();
    }

    private void hideKeyboard(){
        View oView = this.getCurrentFocus();
        if(oView != null){
            InputMethodManager oInputMethodManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
            oInputMethodManager.hideSoftInputFromWindow(oView.getWindowToken(),0);
        }
    }


    public void cargarPais(){
        try{
            ServicioTaskListPais servicio=new ServicioTaskListPais(this,"http://10.0.2.2:8081/SpringConcurso/libro/list");
            servicio.execute();
            String c=servicio.get();
            ArrayList<Pais> data= PaisDAO.listaPaises(c);
            ArrayAdapter<Pais> adapter=new ArrayAdapter<Pais>(this,android.R.layout.simple_list_item_1,data);
            spnNac.setAdapter(adapter);
        }
        catch (Exception e){
            e.printStackTrace();
        }/*
        Retrofit retrofit = new Retrofit.Builder()
                            .baseUrl("http://localhost:8081/SpringConcurso/libro/")
                            .addConverterFactory(GsonConverterFactory.create()).build();

        Api api = retrofit.create(Api.class);
        Call<List<Pais>> call = api.getPais();
        call.enqueue(new Callback<List<Pais>>() {
            @Override
            public void onResponse(Call<List<Pais>> call, Response<List<Pais>> response) {
                if(!response.isSuccessful()){
                    //response.code();
                    List<String> data = new ArrayList<String>();
                    List<Pais> paisList = response.body();
                    for(Pais pais: paisList){
                        data.add(pais.getTitle());
                    }
                    ArrayAdapter<String> adapter=new ArrayAdapter<String>(getBaseContext(),android.R.layout.simple_list_item_1,data);
                    adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    spnNac.setAdapter(adapter);


                }
            }

            @Override
            public void onFailure(Call<List<Pais>> call, Throwable t) {
                    //t.getMessage();
            }
        });*/

    }


    public void registrar(){
        try{
            Pais pais = new Pais();
            int idpais = 0;
            //Nacionalidad nac= new Nacionalidad();

            /*idpais = 6;
            pais.setId(idpais);*/
            pais.setTitle(txtNombre.getText().toString());
            //nac= (Nacionalidad) spNacion.getItemAtPosition(spNacion.getSelectedItemPosition());
            //per.setCodigoNac(nac.getIdnacionalidad());
            ServicioTaskSaveTest servicio= new ServicioTaskSaveTest(this,"http://localhost:8081/SpringConcurso/libro/save/",pais);
            servicio.execute();

        }catch (Exception e){
            e.printStackTrace();
        }

    }




    }

