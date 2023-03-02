using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumScript : MonoBehaviour
{
    [SerializeField] GameObject[] oggettiDaMostrare;

    //TODO: Inserisci le stats che modifica quando c'e' l'effetto
    [Header("—  Effetti (prima e dopo)  —")]
    [SerializeField] float moltipVelocita; 
    [SerializeField] float moltipSalto;

    [Space(10)]
    [SerializeField] GameObject effettoVisivo;  //L'effetto che viene mostrato dopo che viene 

    [Header("—  Tempistiche  —")]
    [SerializeField] float durataEffetto;
    [SerializeField] float cooldownRiutilizzare;
    float tempoPassato_Durata = 0,
          tempoPassato_Cooldown = 0;

    [Space(10)]
    [SerializeField] AnimationCurve curvaAssuefazione;  //Serve per cambiare la durata e il cooldown ogni volta che si beve il Rum
    int numeroBevute = 0;



    void Update()
    {
        
    }
}
