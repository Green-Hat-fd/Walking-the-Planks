using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BersaglioScript : MonoBehaviour
{
    [SerializeField] UnityEvent objDaAttivare;

    [SerializeField] bool distruggereDopoAttivo = true;


    public void AttivaOggetti()
    {
        print("Bersaglio colpito");  //DEBUG
        
        //Avvia l'evento
        objDaAttivare.Invoke();


        #region Polishing

        //TODO: polishing (particelle, SFX)

        #endregion


        //Viene tolto solo se la variabile e' vera
        if (distruggereDopoAttivo)
        {
            gameObject.SetActive(false);
        }
    }
}
