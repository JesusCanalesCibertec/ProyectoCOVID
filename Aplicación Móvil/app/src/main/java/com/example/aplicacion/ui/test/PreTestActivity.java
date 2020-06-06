package com.example.aplicacion.ui.test;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.RadioButton;
import android.widget.RadioGroup;

import com.example.aplicacion.R;
import com.example.aplicacion.TriajeActivity;
import com.example.aplicacion.ui.registrar.RegistrarPreTestActivity;

public class PreTestActivity extends AppCompatActivity {

    Button btnNext;
    RadioButton rdbSelf;
    RadioButton rdbFamiliar;
    RadioButton rdbFam1;
    RadioButton rdbFam2;
    RadioButton rdbFam3;
    RadioButton rdbFam4;
    RadioButton rdbFam5;
    boolean bol;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pre_test);
        getSupportActionBar().setTitle("Triaje");
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);


        btnNext = (Button) findViewById(R.id.btnPreNext);
        rdbFamiliar = (RadioButton) findViewById(R.id.rdbtnFamiliar);
        rdbSelf = (RadioButton) findViewById(R.id.rdbtnYo);
        rdbFam1 = (RadioButton) findViewById(R.id.rdbtnfam1);
        rdbFam2 = (RadioButton) findViewById(R.id.rdbtnfam2);
        rdbFam3 = (RadioButton) findViewById(R.id.rdbtnfam3);
        rdbFam4 = (RadioButton) findViewById(R.id.rdbtnfam4);
        rdbFam5 = (RadioButton) findViewById(R.id.rdbtnfam5);

        rdbFamiliar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                rdbFam1.setEnabled(true);
                rdbFam2.setEnabled(true);
                rdbFam3.setEnabled(true);
                rdbFam4.setEnabled(true);
                rdbFam5.setEnabled(true);
                bol = true;
            }
        });

        rdbSelf.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                rdbFam1.setEnabled(false);
                rdbFam2.setEnabled(false);
                rdbFam3.setEnabled(false);
                rdbFam4.setEnabled(false);
                rdbFam5.setEnabled(false);
                bol = false;
            }
        });


        btnNext.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Intent intent;

                if(bol == true){
                    intent = new Intent(PreTestActivity.this, RegistrarPreTestActivity.class);
                }
                else{
                    intent = new Intent(PreTestActivity.this, Test1Activity.class);
                }

                startActivity(intent);

            }
        });
    }
}