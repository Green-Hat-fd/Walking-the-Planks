using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannoneSpecialeScript : MonoBehaviour
{
    [SerializeField] float potenzaImpulso = 10;
    [SerializeField] Vector2 rangeSecDaAspettare = new Vector2(3, 5);
    float secDaAspettare,
          tempoTrascorso;

    [Space(15)]
    [SerializeField] Transform puntoOrigineProiet;
    [SerializeField] GameObject proiettile;


    void Start()
    {
        Vector2 rangeTmp;

        //Rende il minimo nella X e il massimo nella Y del Vector2
        rangeTmp.x = Mathf.Min(rangeSecDaAspettare.x, rangeSecDaAspettare.y);
        rangeTmp.y = Mathf.Max(rangeSecDaAspettare.x, rangeSecDaAspettare.y);

        rangeSecDaAspettare = rangeTmp;

        //...e genera un nuovo tempo casuale
        GeneraNuovoTempoRandom();
    }

    void Update()
    {
        //Se e' passato abbastanza tempo...
        if (tempoTrascorso >= secDaAspettare)
        {
            SparaPallaDiCannone();     //Spara la palla di cannone
            tempoTrascorso = 0;        //Resetta il timer
            GeneraNuovoTempoRandom();  //Genera un nuovo tempo da aspettare
        }
        else
        {
            tempoTrascorso += Time.deltaTime;  //Aumenta il conteggio del tempo trascorso
        }
    }

    void SparaPallaDiCannone()
    {
        //Crea e salva la palla di cannone
        GameObject proiet = Instantiate(proiettile, puntoOrigineProiet.position, Quaternion.identity);

        //Propelle la palla di cannone e la fa rotolare
        proiet.GetComponent<Rigidbody>().AddForce(puntoOrigineProiet.up * potenzaImpulso, ForceMode.Impulse);
    }

    void GeneraNuovoTempoRandom()
    {
        //Prende un nuovo tempo a caso tra il range scelto nella variabile
        secDaAspettare = Random.Range(rangeSecDaAspettare.x, rangeSecDaAspettare.y);
    }
}
