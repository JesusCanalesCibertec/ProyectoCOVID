package com.example.aplicacion.ui.test;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.example.aplicacion.R;


public class Test2Activity extends AppCompatActivity {

    Button btnNext;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_test2);

        getSupportActionBar().setTitle("Triaje");
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);


        btnNext = (Button) findViewById(R.id.btnTest2Next);
        btnNext.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(Test2Activity.this, Test3Activity.class);
                startActivity(intent);
    }
});
    }
}
