package com.example.aplicacion.ui.registrar;

import androidx.appcompat.app.AppCompatActivity;

import android.app.DatePickerDialog;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.Toast;

import com.example.aplicacion.Entitiy.Ciudadano;
import com.example.aplicacion.Entitiy.Departamento;
import com.example.aplicacion.Entitiy.Distrito;
import com.example.aplicacion.Entitiy.Pais;
import com.example.aplicacion.Entitiy.Provincia;
import com.example.aplicacion.Entitiy.Triaje;
import com.example.aplicacion.MainActivity;
import com.example.aplicacion.R;
import com.example.aplicacion.Session;
import com.example.aplicacion.controlador.CiudadanoDAO;
import com.example.aplicacion.controlador.DepartamentoDAO;
import com.example.aplicacion.controlador.DistritoDAO;
import com.example.aplicacion.controlador.PaisDAO;
import com.example.aplicacion.controlador.ProvinciaDAO;
import com.example.aplicacion.controlador.TriajeDAO;
import com.example.aplicacion.taskCiudadano.ServicioTaskSaveCiudadano;
import com.example.aplicacion.taskPais.ServicioTaskListPais;
import com.example.aplicacion.taskUbigeo.ServicioTaskListDepartamento;
import com.example.aplicacion.taskUbigeo.ServicioTaskListDistrito;
import com.example.aplicacion.taskUbigeo.ServicioTaskListProvincia;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

public class RegistrarActivity extends AppCompatActivity{

    Button btnRegistrar;
    Spinner spnNac;
    Spinner spnDep;
    Spinner spnProv;
    Spinner spnDis;
    Spinner spnTipoDoc;
    EditText txtNombre;
    EditText txtApellido;
    EditText txtDoc;
    EditText txtViv;
    EditText txtFecha;
    ArrayList<Pais> dataPais;
    ArrayList<Departamento> dataDep;
    ArrayList<Provincia> dataProv;
    ArrayList<Distrito> dataDis;
    int posPais;
    int posDep;
    int posProv;
    int posDis;
    String fecval;
    String edad;
    public  int idCiu;
    private Session session;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registrar);

        getSupportActionBar().setTitle("Registro");

        btnRegistrar = (Button) findViewById(R.id.btnRegistrar);
        spnNac = (Spinner) findViewById(R.id.spnNacionalidad);
        spnDep = (Spinner) findViewById(R.id.spnDepartamento);
        spnProv = (Spinner) findViewById(R.id.spnProvincia);
        spnDis = (Spinner) findViewById(R.id.spnDistrito);
        spnTipoDoc = (Spinner) findViewById(R.id.spnDocumento);
        txtFecha = (EditText) findViewById(R.id.txtFecNac);
        txtNombre = (EditText) findViewById(R.id.txtNombre);
        txtApellido = (EditText) findViewById(R.id.txtApellido);
        txtDoc = (EditText) findViewById(R.id.txtNumDoc);
        txtViv = (EditText) findViewById(R.id.txtVivienda);
        session = new Session(RegistrarActivity.this);
        cargarPais();

        //Cargar Spinner Departamento
        spnNac.setOnItemSelectedListener(new OnItemSelectedListener() {
                @Override
                public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                       try{
                           posPais = dataPais.get(position).getIdPais();
                           ServicioTaskListDepartamento servicio=new ServicioTaskListDepartamento(RegistrarActivity.this,"http://arbchum1-001-site1.etempurl.com/api/spring/core/Departamento/listarTodos");
                           servicio.execute();
                           String c = servicio.get();
                           dataDep= DepartamentoDAO.listaDepartamento(c);
                           ArrayAdapter<Departamento> adapter=new ArrayAdapter<Departamento>(RegistrarActivity.this,android.R.layout.simple_spinner_item,dataDep);
                           spnDep.setAdapter(adapter);

                       }
                       catch (Exception e){
                           e.printStackTrace();
                       }
                }

                @Override
                public void onNothingSelected(AdapterView<?> parent) {

                }
        });

        //Cargar spinner Provincia
        spnDep.setOnItemSelectedListener(new OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                    try{
                        posDep=dataDep.get(position).getDepartamento();
                        ServicioTaskListProvincia servicio;
                        if(posDep<10){
                            servicio=new ServicioTaskListProvincia(RegistrarActivity.this,"http://arbchum1-001-site1.etempurl.com/api/spring/core/Provincia/listarActivosPorDepartamento?idDepartamento=0"+ posDep);
                        }
                        else{
                            servicio=new ServicioTaskListProvincia(RegistrarActivity.this,"http://arbchum1-001-site1.etempurl.com/api/spring/core/Provincia/listarActivosPorDepartamento?idDepartamento="+ posDep);
                        }
                        servicio.execute();
                        String c=servicio.get();
                        dataProv= ProvinciaDAO.listaProvincia(c);
                        ArrayAdapter<Provincia> adapter=new ArrayAdapter<Provincia>(RegistrarActivity.this,android.R.layout.simple_spinner_item,dataProv);
                        spnProv.setAdapter(adapter);

                    }catch (Exception e){
                        e.printStackTrace();
                    }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        //Cargar Spinner Distrito
        spnProv.setOnItemSelectedListener(new OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

                    try{
                        posProv=dataProv.get(position).getProvincia();
                        ServicioTaskListDistrito servicio;
                        if(posDep<10 && posProv<10){
                                servicio=new ServicioTaskListDistrito(RegistrarActivity.this,"http://arbchum1-001-site1.etempurl.com/api/spring/core/Zonapostal/listarActivosPorProvincia?idDepartamento=0"+posDep+"&idProvincia=0"+posProv);
                        }
                        else if(posDep<10 && posProv>=10){
                            servicio=new ServicioTaskListDistrito(RegistrarActivity.this,"http://arbchum1-001-site1.etempurl.com/api/spring/core/Zonapostal/listarActivosPorProvincia?idDepartamento=0"+posDep+"&idProvincia="+posProv);
                        }
                        else if(posDep>=10 && posProv<10){
                            servicio=new ServicioTaskListDistrito(RegistrarActivity.this,"http://arbchum1-001-site1.etempurl.com/api/spring/core/Zonapostal/listarActivosPorProvincia?idDepartamento="+posDep+"&idProvincia=0"+posProv);
                        }
                        else{
                            servicio=new ServicioTaskListDistrito(RegistrarActivity.this,"http://arbchum1-001-site1.etempurl.com/api/spring/core/Zonapostal/listarActivosPorProvincia?idDepartamento="+posDep+"&idProvincia="+posProv);
                        }

                        servicio.execute();
                        String c=servicio.get();
                        dataDis= DistritoDAO.listaDistrito(c);
                        ArrayAdapter<Distrito> adapter=new ArrayAdapter<Distrito>(RegistrarActivity.this,android.R.layout.simple_spinner_item,dataDis);
                        spnDis.setAdapter(adapter);
                    }catch (Exception e){
                        e.printStackTrace();
                    }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        //Capturar Distrito
        spnDis.setOnItemSelectedListener(new OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
               posDis=dataDis.get(position).getCodigopostal();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        //Mostrar Fecha
        txtFecha.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showDateDailog(txtFecha);
            }
        });


        btnRegistrar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //validar();
                resultadoExitoso();
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
                SimpleDateFormat oSdf = new SimpleDateFormat("yyyy/MM/dd");

                fecha.setText(oSimpleDateFormat.format(calendario.getTime()));
                fecval = oSdf.format(calendario.getTime());
                edad = getAge(calendario);
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
            ServicioTaskListPais servicio=new ServicioTaskListPais(this,"http://arbchum1-001-site1.etempurl.com/api/spring/covid/Pais/listarTodos");
            servicio.execute();
            String c=servicio.get();
            dataPais= PaisDAO.listaPaises(c);
            ArrayAdapter<Pais> adapter=new ArrayAdapter<Pais>(this,android.R.layout.simple_spinner_item,dataPais);
            spnNac.setAdapter(adapter);
        }
        catch (Exception e){
            e.printStackTrace();
        }

    }

    public void registrar(){
        try{
            Ciudadano c = new Ciudadano();
            c.setNombre(txtNombre.getText().toString());
            c.setApellido(txtApellido.getText().toString());
            c.setIdPais("00"+posPais);
            int tipodoc = spnTipoDoc.getSelectedItemPosition() + 1;
            c.setTipoDocumento(tipodoc);
            c.setNroDocumento(txtDoc.getText().toString());
            c.setFechaNacimiento(fecval);
            c.setDireccion(txtViv.getText().toString());
            if(posDep<10){
                String idde="0"+posDep;
                c.setIdDepartamento(idde);
            }else{
                c.setIdDepartamento(posDep+"");
            }
            if(posProv<10){
                String idpr="0"+posProv;
                c.setIdProvincia(idpr);
            }else{
                c.setIdProvincia(posProv+"");
            }
            if(posDis<10){
                String iddi="0"+posDis;
                c.setIdDistrito(iddi);
            }else{
                c.setIdDistrito(posDis+"");
            }

            ServicioTaskSaveCiudadano servicio= new ServicioTaskSaveCiudadano(this,"http://arbchum1-001-site1.etempurl.com/api/spring/covid/Ciudadano/registrar",c);
            servicio.execute();
            String s = servicio.get();
            Ciudadano ciudadano = CiudadanoDAO.responseCiudadano(s);
            idCiu=ciudadano.getIdCiudadano();

        }catch (Exception e){
            e.printStackTrace();
        }
    }

    public void validar(){
        String nom = txtNombre.getText().toString();//
        String ape = txtApellido.getText().toString();//
        String doc = txtDoc.getText().toString();//
        String tipDoc = spnTipoDoc.getSelectedItem().toString();//
        String fecNac = txtFecha.getText().toString();
        String viv = txtViv.getText().toString();//



        if(nom.equals("")||ape.equals("")||doc.equals("")||fecNac.equals("")||viv.equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese los datos faltantes", Toast.LENGTH_SHORT).show();
        }else{
            int edadInt = Integer.parseInt(edad);
            if(edadInt < 14){
                Toast.makeText(getApplicationContext(), "Debes tener de 14 años a más para usar la aplicación", Toast.LENGTH_SHORT).show();
            }
            else if(tipDoc.equals("DNI")) {
                if (doc.length() < 8 || doc.length() > 8) {
                    Toast.makeText(getApplicationContext(), "DNI debe ser de 8 digitos", Toast.LENGTH_SHORT).show();
                }else {
                    Toast.makeText(getApplicationContext(), "exito", Toast.LENGTH_SHORT).show();
                    resultadoExitoso();
                }
            }
            else if (tipDoc.equals("Carne EXT.")) {
                if (doc.length() < 12) {
                    Toast.makeText(getApplicationContext(), "Carnet de extranjeria debe ser de 12 digitos", Toast.LENGTH_SHORT).show();
                }else {
                    Toast.makeText(getApplicationContext(), "exitoso", Toast.LENGTH_SHORT).show();
                    resultadoExitoso();
                }
            }
        }


    }

    private void resultadoExitoso() {
        //registrar();
        session.setIdCiudadano(idCiu);
        Intent intent = new Intent(RegistrarActivity.this, MainActivity.class);
        startActivity(intent);
    }

    private String getAge(Calendar date){
        Calendar dob = Calendar.getInstance();
        Calendar today = Calendar.getInstance();

        int year = date.get(Calendar.YEAR);
        int month = date.get(Calendar.MONTH);
        int day = date.get(Calendar.DAY_OF_MONTH);

        dob.set(year, month, day);

        int age = today.get(Calendar.YEAR) - dob.get(Calendar.YEAR);

        if (today.get(Calendar.DAY_OF_YEAR) < dob.get(Calendar.DAY_OF_YEAR)){
            age--;
        }

        Integer ageInt = new Integer(age);
        String ageS = ageInt.toString();

        return ageS;
    }


}

