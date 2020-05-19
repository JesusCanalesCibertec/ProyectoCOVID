package com.example.aplicacion.ui.test;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.example.aplicacion.R;
import com.example.aplicacion.TriajeActivity;

public class PreTestActivity extends AppCompatActivity {

    Button btnNext;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pre_test);
        getSupportActionBar().setTitle("Triaje");
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);


        btnNext = (Button) findViewById(R.id.btnPreNext);
        btnNext.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(PreTestActivity.this, Test1Activity.class);
                startActivity(intent);
            }
        });
    }
}