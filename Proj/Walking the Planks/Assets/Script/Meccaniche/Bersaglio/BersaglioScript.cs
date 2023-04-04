using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BersaglioScript : MonoBehaviour, IFeedback
{
    [SerializeField] UnityEvent objDaAttivare;

    [SerializeField] bool distruggereDopoAttivo = true;
    bool attivato = false;

    [Header("—  Feedback  —")]
    #region Audio
    [SerializeField] AudioSource bers_sfx;
    [SerializeField] Vector2 rangePitch = new Vector2(0.85f, 1.5f); 
    #endregion


    public void AttivaOggetti()
    {
        print("Bersaglio colpito");  //DEBUG
        
        //Avvia l'evento
        objDaAttivare.Invoke();


        Feedback();


        //Viene tolto solo se la variabile e' vera
        if (distruggereDopoAttivo)
        {
            attivato = true;
        }
    }

    private void Update()
    {
        if (attivato)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    public void Feedback()
    {
        //Riproduce il suono di quando viene colpito (con un pitch casuale)
        bers_sfx.pitch = Random.Range(rangePitch.x, rangePitch.y);
        bers_sfx.Play();

        //TODO: feedback (particelle)
    }
}
