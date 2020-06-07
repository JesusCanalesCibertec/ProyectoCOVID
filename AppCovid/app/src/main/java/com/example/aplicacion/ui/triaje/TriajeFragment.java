package com.example.aplicacion.ui.triaje;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import com.example.aplicacion.R;
import com.example.aplicacion.TriajeActivity;
import com.example.aplicacion.ui.mapa.MapaViewModel;

public class TriajeFragment extends Fragment {

    Button btnComenzar;
    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_triaje, container, false);

        btnComenzar = (Button) root.findViewById(R.id.btnComenzarTriaje);

        btnComenzar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getActivity(), TriajeActivity.class);
                startActivity(intent);
            }
        });

        return root;
    }
}
