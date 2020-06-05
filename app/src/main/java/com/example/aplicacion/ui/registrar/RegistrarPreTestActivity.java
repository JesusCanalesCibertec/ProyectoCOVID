package com.example.aplicacion.ui.registrar;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.example.aplicacion.R;
import com.example.aplicacion.ui.test.PreTestActivity;
import com.example.aplicacion.ui.test.Test1Activity;


public class RegistrarPreTestActivity extends AppCompatActivity {


    Button btnNext;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registrar);

        btnNext = (Button) findViewById(R.id.btnRegistrar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        btnNext.setText("Continuar");


        getSupportActionBar().setTitle("Registro de Familiar");

        btnNext.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(RegistrarPreTestActivity.this, Test1Activity.class);
                startActivity(intent);
            }
        });


    }
}
