package com.example.aplicacion.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.example.aplicacion.Entitiy.Ciudadano;
import com.example.aplicacion.Entitiy.Pais;
import com.example.aplicacion.Entitiy.Resultado;
import com.example.aplicacion.R;
import com.example.aplicacion.SessionManagement;
import com.example.aplicacion.controlador.CiudadanoDAO;
import com.example.aplicacion.controlador.PaisDAO;
import com.example.aplicacion.controlador.ResultadoDAO;
import com.example.aplicacion.taskCiudadano.ServicioTaskListCiudadano;
import com.example.aplicacion.taskPais.ServicioTaskListPais;
import com.example.aplicacion.taskResultado.ServicioTaskListResultado;
import com.example.aplicacion.ui.registrar.RegistrarActivity;

import java.util.ArrayList;

public class HomeFragment extends Fragment {

    TextView txtNombre;
    TextView txtApellido;
    TextView txtNacionalidad;
    TextView txtNumDoc;
    TextView txtEstado;
    
    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_home, container, false);

        txtNombre = (TextView) root.findViewById(R.id.lbNombreMostrado);
        txtApellido = (TextView) root.findViewById(R.id.lbApellidoMostrado);
        txtNacionalidad = (TextView) root.findViewById(R.id.lbNacionalidadMostrado);
        txtNumDoc = (TextView) root.findViewById(R.id.lbNumDocMostrado);
        txtEstado = (TextView) root.findViewById(R.id.lbEstadoMostrado);

        cargarCiudadano();
        
        return root;
    }

    private void cargarCiudadano() {
        try {
            int idCiu = getIdCiu();

            ServicioTaskListCiudadano taskListCiudadano = new ServicioTaskListCiudadano(getContext(),"http://arbchum1-001-site1.etempurl.com/api/spring/covid/Ciudadano/ObtenerPorId?pIdCiudadano="+idCiu);
            taskListCiudadano.execute();
            String c = taskListCiudadano.get();
            Ciudadano ciu = CiudadanoDAO.responseCiudadano(c);

            txtNombre.setText(ciu.getNombre());
            txtApellido.setText(ciu.getApellido());

            ServicioTaskListPais taskListPais = new ServicioTaskListPais(getContext(),"http://arbchum1-001-site1.etempurl.com/api/spring/covid/Pais/listarTodos");
            taskListPais.execute();
            String s=taskListPais.get();
            String nomPais="";
            int idPais = Integer.parseInt(ciu.getIdPais());
            ArrayList<Pais> listaPaises = PaisDAO.listaPaises(s);
            for(int i=0;i<listaPaises.size();i++){
                if(listaPaises.get(i).getIdPais() == idPais){
                    nomPais= listaPaises.get(i).getDescripcion();
                }
            }
            txtNacionalidad.setText(nomPais);

            txtNumDoc.setText(ciu.getNroDocumento());

            int idEstado = ciu.getEstado();
            ServicioTaskListResultado taskListResultado = new ServicioTaskListResultado(getContext(),"http://arbchum1-001-site1.etempurl.com/api/spring/covid/Resultado/ObtenerPorId?pIdResultado="+idEstado);
            taskListResultado.execute();
            String r = taskListResultado.get();
            Resultado res = ResultadoDAO.responseResultado(r);
            txtEstado.setText(res.getNombre());

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private int getIdCiu() {
        SessionManagement sessionManagement = new SessionManagement(getContext());
        return sessionManagement.getSession();
    }
}
