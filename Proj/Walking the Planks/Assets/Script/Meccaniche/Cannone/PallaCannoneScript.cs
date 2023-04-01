using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PallaCannoneScript : MonoBehaviour, IFeedback
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("CannonBall"))
        {
            Feedback();


            #region Controllo per il giocatore

            //Controlla se ha preso il giocatore
            if (collision.gameObject.CompareTag("Player"))
                collision.gameObject.GetComponent<StatsGiocatore>().ScriviSonoMorto(true);

            #endregion


            //Vengono riaggunte nella pool
            FindObjectOfType<ObjectPoolingScript>().RiAggiungiOggetto("Palle di cannone", gameObject);
        }
    }

    public void Feedback()
    {
        //TODO: feedback (particelle, SFX)
    }
}
