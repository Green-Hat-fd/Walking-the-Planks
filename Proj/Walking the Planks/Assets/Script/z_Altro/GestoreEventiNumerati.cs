using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GestoreEventiNumerati : MonoBehaviour
{
    [System.Serializable]
    public class EventiNum_Class
    {
        public int numeroDaAspettare;
        public UnityEvent OnNumeroRaggiunto;
    }

    [SerializeField] List<EventiNum_Class> eventiNumerati;

    int conta = 0;
    int indiceControllo = 0;



    /*
    private void Awake()
    {
        OrdinaLista(eventiNumerati);
    }//*/

    public void AumentaConta()
    {
        //Aumenta il conteggio interno
        conta++;

        //Controlla se il conteggio corrisponde al numero
        if(conta == eventiNumerati[indiceControllo].numeroDaAspettare)
        {
            //Se corrisponde, chiama l'evento
            eventiNumerati[indiceControllo].OnNumeroRaggiunto.Invoke();


            indiceControllo++;   //Aumenta l'indice da controllare
        }
    }
    public void ResetConta()
    {
        conta = 0;
    }

    //TODO: se hai tempo, fai un algoritmo di ordinamento (sorting algorithm)
    /*void OrdinaLista(List<EventiNum_Class> tuttiGliEventi)
    {
        List<EventiNum_Class> listaTemp = new List<EventiNum_Class>();


        for (int i = 0; i < tuttiGliEventi.Count; i++)
        {

        }


        tuttiGliEventi = listaTemp;
    }//*/
}
