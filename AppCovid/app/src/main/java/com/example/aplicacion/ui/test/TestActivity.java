package com.example.aplicacion.ui.test;

import androidx.appcompat.app.AppCompatActivity;

import android.app.DatePickerDialog;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.RadioGroup;
import android.widget.Toast;

import com.example.aplicacion.Entitiy.Triaje;
import com.example.aplicacion.R;
import com.example.aplicacion.SessionManagement;
import com.example.aplicacion.controlador.TriajeDAO;
import com.example.aplicacion.taskTriaje.ServicioTaskSaveTriaje;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class TestActivity extends AppCompatActivity {

    CheckBox ckDismi;
    CheckBox ckTos;
    CheckBox ckDolor;
    CheckBox ckDifi;
    CheckBox ckNasal;
    CheckBox ckFiebre;
    EditText txtFecIni;
    RadioGroup rgContacto;
    RadioGroup rgDesplazo;
    RadioGroup rgTrabaja;
    CheckBox ckObesidad;
    CheckBox ckCancer;
    CheckBox ckDiabetes;
    CheckBox ckRenal;
    CheckBox ckAsma;
    CheckBox ckCardio;
    CheckBox ckHipertención;
    CheckBox ckPulmonar;
    Button btnNext;
    String fecval;
    int estado;
    int count=0;
    Date today;
    Date thatDay;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_test);

        getSupportActionBar().setTitle("Triaje");

        ckDismi = (CheckBox) findViewById(R.id.ckDis);
        ckTos = (CheckBox) findViewById(R.id.ckTos);
        ckDolor = (CheckBox) findViewById(R.id.ckDolor);
        ckDifi = (CheckBox) findViewById(R.id.ckDifRes);
        ckNasal = (CheckBox) findViewById(R.id.ckNasal);
        ckFiebre = (CheckBox) findViewById(R.id.ckFiebre);
        txtFecIni = (EditText) findViewById(R.id.txtFecIni);
        rgContacto = (RadioGroup) findViewById(R.id.rdGContacto);
        rgDesplazo = (RadioGroup) findViewById(R.id.rdGDesplazo);
        rgTrabaja = (RadioGroup) findViewById(R.id.rdGTrabajo);
        ckObesidad = (CheckBox) findViewById(R.id.ckObesidad);
        ckCancer = (CheckBox) findViewById(R.id.ckCancer);
        ckDiabetes = (CheckBox) findViewById(R.id.ckDiabetes);
        ckRenal = (CheckBox) findViewById(R.id.ckRenal);
        ckAsma = (CheckBox) findViewById(R.id.ckAsma);
        ckCardio = (CheckBox) findViewById(R.id.ckCardio);
        ckHipertención = (CheckBox) findViewById(R.id.ckHipertención);
        ckPulmonar = (CheckBox) findViewById(R.id.ckFiebre);
        btnNext = (Button) findViewById(R.id.btnTestEnviar);


        ckDismi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                check(ckDismi);
            }
        });

        ckTos.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                check(ckTos);
            }
        });

        ckDolor.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                check(ckDolor);
            }
        });

        ckDifi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                check(ckDifi);
            }
        });

        ckNasal.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                check(ckNasal);
            }
        });

        ckFiebre.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                check(ckFiebre);
            }
        });

        //Mostrar Fecha
        txtFecIni.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showDateDailog(txtFecIni);
            }
        });

        //Enviar
        btnNext.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                validar();

            }
        });

    }


    private void showDateDailog(EditText fecha) {
        Calendar calendario = Calendar.getInstance();
        DatePickerDialog.OnDateSetListener dateSetListener = new DatePickerDialog.OnDateSetListener() {
            @Override
            public void onDateSet(DatePicker datePicker, int year, int month, int day) {
                calendario.set(Calendar.HOUR_OF_DAY, 0);
                calendario.set(Calendar.MINUTE, 0);
                calendario.set(Calendar.SECOND, 0);
                calendario.set(Calendar.MILLISECOND, 0);

                today = calendario.getTime();

                calendario.set(Calendar.YEAR,year);
                calendario.set(Calendar.MONTH,month);
                calendario.set(Calendar.DAY_OF_MONTH,day);
                SimpleDateFormat oSimpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");
                SimpleDateFormat oSdf = new SimpleDateFormat("yyyy/MM/dd");

                fecha.setText(oSimpleDateFormat.format(calendario.getTime()));
                fecval = oSdf.format(calendario.getTime());

                thatDay = calendario.getTime();
            }
        };
        new DatePickerDialog(TestActivity.this,dateSetListener,
                calendario.get(Calendar.YEAR),calendario.get(Calendar.MONTH), calendario.get(Calendar.DAY_OF_MONTH)).show();
    }


    private String SioNoCk(CheckBox ck){
        if(ck.isChecked())
            return "SI";

        return "NO";
    }

    private void registrar(){
        try{
            Triaje t = new Triaje();
            int idCiu = getIdCiu();

            //Setear Ciudadano
            t.setIdCiudadano(idCiu);
            //Pregunta 1
            t.setDisgus(SioNoCk(ckDismi));
            t.setTos(SioNoCk(ckTos));
            t.setDolor(SioNoCk(ckDolor));
            t.setDifi(SioNoCk(ckDifi));
            t.setNasal(SioNoCk(ckNasal));
            t.setFiebre(SioNoCk(ckFiebre));
            t.setFechainicio(fecval);
            //Situaciones
            switch (rgContacto.getCheckedRadioButtonId()){
                case R.id.rdbtnContactoSi:
                    t.setSituacion1("SI");
                    break;
                case R.id.rdbtnContactoNo:
                    t.setSituacion1("NO");
                default:
                    break;
            }
            switch (rgDesplazo.getCheckedRadioButtonId()){
                case R.id.rdbtnDesplaSi:
                    t.setSituacion2("SI");
                    break;
                case R.id.rdbtnDesplaNo:
                    t.setSituacion2("NO");
                default:
                    break;
            }
            switch (rgTrabaja.getCheckedRadioButtonId()){
                case R.id.rdbtnTrabajoSi:
                    t.setSituacion3("SI");
                    break;
                case R.id.rdbtnTrabajoNo:
                    t.setSituacion3("NO");
                default:
                    break;
            }
            t.setObesidad(SioNoCk(ckObesidad));
            t.setCancer(SioNoCk(ckCancer));
            t.setDiabetes(SioNoCk(ckDiabetes));
            t.setRenal(SioNoCk(ckRenal));
            t.setAsma(SioNoCk(ckAsma));
            t.setCardio(SioNoCk(ckCardio));
            t.setHipertension(SioNoCk(ckHipertención));
            t.setPulmonar(SioNoCk(ckPulmonar));

            ServicioTaskSaveTriaje saveTriaje = new ServicioTaskSaveTriaje(this,"http://arbchum1-001-site1.etempurl.com/api/spring/covid/Triaje/registrar",t);
            saveTriaje.execute();
            String c = saveTriaje.get();
            Triaje tri = TriajeDAO.responseTriaje(c);
            estado=tri.getEstado();

        }catch (Exception e){
            e.printStackTrace();
        }


    }

    private int getIdCiu() {
        SessionManagement sessionManagement = new SessionManagement(TestActivity.this);
        return sessionManagement.getSession();
    }

    private void check(CheckBox chk){
        if(chk.isChecked())
            count++;
        else{
            count--;
        }

        if(count>0){
            txtFecIni.setEnabled(true);
        }else{
            txtFecIni.setText("");
            txtFecIni.setEnabled(false);
        }
    }

    private void validar(){
        String textfec = txtFecIni.getText().toString();


        if(textfec.equals("")&&count>0){
            Toast.makeText(getApplicationContext(), "Ingrese los datos faltantes", Toast.LENGTH_SHORT).show();
        }else if(!textfec.equals("")){
            long resta = today.getTime() - thatDay.getTime();
            long treinta = 3000000000l;
            int comp = Long.compare(resta,treinta);
            if(thatDay.after(today)||thatDay.equals(today)){
                Toast.makeText(getApplicationContext(), "Ingrese una fecha anterior a hoy", Toast.LENGTH_SHORT).show();
            }
            else if(comp>0){
                Toast.makeText(getApplicationContext(), "Ingrese una fecha no mayor a 30 días", Toast.LENGTH_SHORT).show();
            }else{
                Toast.makeText(getApplicationContext(), "El triaje ha sido registrado con éxito", Toast.LENGTH_SHORT).show();
                resultadoExitoso();
            }
        }else{
            Toast.makeText(getApplicationContext(), "El triaje ha sido registrado con éxito", Toast.LENGTH_SHORT).show();
            resultadoExitoso();
        }
    }

    private void resultadoExitoso() {
        registrar();
        Intent intent = new Intent(TestActivity.this, ResultadoActivity.class);
        intent.putExtra("estado",estado);
        startActivity(intent);
        finish();
    }

}
