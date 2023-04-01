using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BersaglioScript : MonoBehaviour, IFeedback
{
    [SerializeField] UnityEvent objDaAttivare;

    [SerializeField] bool distruggereDopoAttivo = true;


    public void AttivaOggetti()
    {
        print("Bersaglio colpito");  //DEBUG
        
        //Avvia l'evento
        objDaAttivare.Invoke();


        Feedback();


        //Viene tolto solo se la variabile e' vera
        if (distruggereDopoAttivo)
        {
            gameObject.SetActive(false);
        }
    }

    public void Feedback()
    {
        //TODO: feedback (particelle, SFX)
    }
}
