package com.example.aplicacion.ui.test;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.example.aplicacion.R;
import com.example.aplicacion.TriajeActivity;

public class Test1Activity extends AppCompatActivity {

    Button btnNext;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_test1);

        getSupportActionBar().setTitle("Triaje");


        btnNext = (Button) findViewById(R.id.btnTest1Next);
        btnNext.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(Test1Activity.this, Test2Activity.class);
                startActivity(intent);
            }
        });
    }
}
