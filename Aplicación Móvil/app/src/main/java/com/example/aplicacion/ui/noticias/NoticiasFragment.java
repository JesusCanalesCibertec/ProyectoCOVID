package com.example.aplicacion.ui.noticias;

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

public class NoticiasFragment extends Fragment {

    private NoticiasViewModel noticiasViewModel;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        noticiasViewModel =
                ViewModelProviders.of(this).get(NoticiasViewModel.class);
        View root = inflater.inflate(R.layout.fragment_noticias, container, false);
        final TextView textView = root.findViewById(R.id.text_noticias);
        noticiasViewModel.getText().observe(getViewLifecycleOwner(), new Observer<String>() {
            @Override
            public void onChanged(@Nullable String s) {
                textView.setText(s);
            }
        });
        return root;
    }
}
