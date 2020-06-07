package com.example.aplicacion.ui.registrar;

import androidx.appcompat.app.AppCompatActivity;

import android.app.DatePickerDialog;
import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Toast;

import com.example.aplicacion.R;

import java.text.SimpleDateFormat;
import java.util.Calendar;

public class registroPersona extends AppCompatActivity {

    EditText fecha;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registro_persona);
        fecha = findViewById(R.id.edtFechanacimiento);

        fecha.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
               showDateDailog(fecha);
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
        new DatePickerDialog(registroPersona.this,dateSetListener,
                calendario.get(Calendar.YEAR),calendario.get(Calendar.MONTH), calendario.get(Calendar.DAY_OF_MONTH)).show();
    }

    private void hideKeyboard(){
        View oView = this.getCurrentFocus();
        if(oView != null){
            InputMethodManager oInputMethodManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
            oInputMethodManager.hideSoftInputFromWindow(oView.getWindowToken(),0);
        }
    }
}
