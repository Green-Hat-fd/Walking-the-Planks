using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PallaCannoneScript : MonoBehaviour, IFeedback
{
    ObjectPoolingScript poolingScr;

    [Header("�  Feedback  �")]
    [SerializeField] ParticleSystem pallaRotta_part;



    private void Awake()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();
        //rottura_sfx = GetComponent<AudioSource>();
    }

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
            poolingScr.RiAggiungiOggetto("Palle di cannone", gameObject);
        }
    }

    public void Feedback()
    {
        //Fa vedere le particelle della palla che si rompe
        //(e riproduce automaticamente il suono nel prefab)
        Instantiate(pallaRotta_part.gameObject, transform.position, Quaternion.identity);
    }
}
