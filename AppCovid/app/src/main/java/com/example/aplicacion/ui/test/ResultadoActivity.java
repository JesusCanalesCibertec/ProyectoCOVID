package com.example.aplicacion.ui.test;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.example.aplicacion.Entitiy.Resultado;
import com.example.aplicacion.MainActivity;
import com.example.aplicacion.R;
import com.example.aplicacion.controlador.ResultadoDAO;
import com.example.aplicacion.taskResultado.ServicioTaskListResultado;

import org.w3c.dom.Text;


public class ResultadoActivity extends AppCompatActivity {

    Bundle bundle;
    TextView lbResultadoNom;
    TextView lbResultadoDes;
    TextView lbResultadoRec;
    Button btnNext;
    int estado;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_resultado);

        getSupportActionBar().setTitle("Resultado");
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        lbResultadoNom = (TextView) findViewById(R.id.lbResultadoNom);
        lbResultadoDes = (TextView) findViewById(R.id.lbResultadoDes);
        lbResultadoRec = (TextView) findViewById(R.id.lbResultadoRec);
        btnNext = (Button) findViewById(R.id.btnFinalizar);

       bundle = getIntent().getExtras();
        if(bundle!=null){
            estado = getIntent().getExtras().getInt("estado");
        }
        llenarResultado();


        btnNext.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(ResultadoActivity.this, MainActivity.class);

                startActivity(intent);
                    }
                    });
    }

    void llenarResultado(){
        try {
            ServicioTaskListResultado servicio = new ServicioTaskListResultado(ResultadoActivity.this,"http://arbchum1-001-site1.etempurl.com/api/spring/covid/Resultado/ObtenerPorId?pIdResultado="+estado);
            servicio.execute();
            String c = servicio.get();
            Resultado res = ResultadoDAO.responseResultado(c);

            lbResultadoNom.setText(res.getNombre());
            lbResultadoDes.setText(res.getDescripcion());
            lbResultadoRec.setText(res.getRecomendacion());
        }catch (Exception e){
            e.printStackTrace();
        }
    }

}
