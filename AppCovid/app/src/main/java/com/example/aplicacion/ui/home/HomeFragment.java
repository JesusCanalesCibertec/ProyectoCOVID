package com.example.aplicacion.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import com.example.aplicacion.R;
import com.example.aplicacion.Session;

public class HomeFragment extends Fragment {

    TextView txtNombre;
    TextView txtApellido;
    TextView txtNacionalidad;
    TextView txtNumDoc;
    TextView txtEstado;
    private Session session;
    int idCiu;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_home, container, false);

        txtNombre = (TextView) root.findViewById(R.id.lbNombreMostrado);
        txtApellido = (TextView) root.findViewById(R.id.lbApellidoMostrado);
        txtNacionalidad = (TextView) root.findViewById(R.id.lbNacionalidadMostrado);
        txtNumDoc = (TextView) root.findViewById(R.id.lbNumDocMostrado);
        txtEstado = (TextView) root.findViewById(R.id.lbEstadoMostrado);
        idCiu = session.getIdCiudadano();

        return root;
    }
}
