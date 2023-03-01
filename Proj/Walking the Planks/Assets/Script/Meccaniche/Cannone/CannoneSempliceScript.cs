using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannoneSempliceScript : MonoBehaviour
{
    [SerializeField] bool sonoAttivo;

    [Space(10)]
    [SerializeField] float potenzaImpulso = 10;
    [SerializeField] float secDaAspettare = 10;
    float tempoTrascorso;

    [Space(15)]
    [SerializeField] Transform puntoOrigineProiet;
    [SerializeField] GameObject proiettile;


    void Update()
    {
        //Se e' passato abbastanza tempo...
        if (tempoTrascorso >= secDaAspettare)
        {
            SparaPallaDiCannone();  //Spara la palla di cannone
            tempoTrascorso = 0;     //Resetta il timer
        }
        else
        {
            if(sonoAttivo)
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

    public void AttivaCannoneSemplice()
    {
        sonoAttivo = true;
    }
    public void DisattivaCannoneSemplice()
    {
        sonoAttivo = false;
    }
}
