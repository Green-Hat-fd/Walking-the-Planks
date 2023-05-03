using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarileScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ControlloCollisioni(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        ControlloCollisioni(other.gameObject);
    }


    void ControlloCollisioni(GameObject obj)
    {
        //Fa morire il giocatore se tocca il Barile
        if (obj.CompareTag("Player"))
            obj.GetComponent<StatsGiocatore>().ScriviSonoMorto(true);
    }
}
