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
        try{
            Pais pais = new Pais();
            //Nacionalidad nac= new Nacionalidad();

            pais.setId(4);
            pais.setTitle(txtNombre.getText().toString());
            //nac= (Nacionalidad) spNacion.getItemAtPosition(spNacion.getSelectedItemPosition());
            //per.setCodigoNac(nac.getIdnacionalidad());
            ServicioTaskSaveTest servicio= new ServicioTaskSaveTest(this,"https://my-json-server.typicode.com/typicode/demo/posts",pais);
            servicio.execute();

        }catch (Exception e){
            e.printStackTrace();
        }

    }




    }

