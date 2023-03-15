using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ObjectPoolingScript poolingScr = FindObjectOfType<ObjectPoolingScript>();

        switch (collision.gameObject.tag)
        {
            //Fa morire il giocatore
            case "Player":
                collision.gameObject.GetComponent<StatsGiocatore>().ScriviSonoMorto(true);
                break;

            //"Distrugge" le scatole e i barili
            case "Gun-Box":
                poolingScr.RiAggiungiOggetto("Scatole", collision.gameObject);
                break;

            case "Rolling-Barrel":
                poolingScr.RiAggiungiOggetto("Barili", collision.gameObject);
                break;
        }
    }
}
