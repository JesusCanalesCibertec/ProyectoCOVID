package com.example.aplicacion;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.example.aplicacion.ui.test.TestActivity;

public class TriajeActivity extends AppCompatActivity {

    Button btnIniciar;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_triaje);


        getSupportActionBar().setTitle("Triaje");
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);



        btnIniciar = (Button) findViewById(R.id.btnIniciarTest);
        btnIniciar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(TriajeActivity.this, TestActivity.class);
                startActivity(intent);
            }
        });


    }
}
