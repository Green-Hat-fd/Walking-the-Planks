using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BersaglioScript : MonoBehaviour, IFeedback
{
    [SerializeField] GameObject modello;
    Collider collComp;

    [SerializeField] UnityEvent objDaAttivare;

    [SerializeField] bool distruggereDopoAttivo = true;

    [Header("—  Feedback  —")]
    #region Audio
    [SerializeField] AudioSource bers_sfx;
    [SerializeField] Vector2 rangePitch = new Vector2(0.85f, 1.5f);
    #endregion



    private void Awake()
    {
        modello = transform.GetChild(0).gameObject;
        collComp = GetComponent<Collider>();
    }

    public void AttivaOggetti()
    {
        print("Bersaglio colpito");  //DEBUG
        
        //Avvia l'evento
        objDaAttivare.Invoke();


        Feedback();


        //Viene tolto solo se la variabile e' vera
        if (distruggereDopoAttivo)
        {
            MostraONascondiBersaglio(false);
        }
    }

    void MostraONascondiBersaglio(bool attivo)
    {
        //Nasconde e disattiva/Mostra e attiva il bersagio
        modello.SetActive(attivo);
        collComp.enabled = attivo;
    }

    public void Feedback()
    {
        //Riproduce il suono di quando viene colpito (con un pitch casuale)
        bers_sfx.pitch = Random.Range(rangePitch.x, rangePitch.y);
        bers_sfx.Play();

        //TODO: feedback (particelle)
    }
}
