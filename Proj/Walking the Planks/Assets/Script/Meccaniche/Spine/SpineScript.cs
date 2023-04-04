using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineScript : MonoBehaviour
{
    LevelManagerScript levelManagerScr;
    ObjectPoolingScript poolingScr;

    [Header("—  Feedback (Scatola)  —")]
    [SerializeField] ParticleSystem scatolaRotta_part;
    AudioSource scatolaRotta_sfx;

    [Header("—  Feedback (Barile)  —")]
    [SerializeField] ParticleSystem barileRotto_part;
    AudioSource barileRotto_sfx;



    private void Awake()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();
        levelManagerScr = FindObjectOfType<LevelManagerScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            //Fa morire il giocatore
            case "Player":
                collision.gameObject.GetComponent<StatsGiocatore>().ScriviSonoMorto(true);
                break;


            //"Distrugge" le scatole e i barili
            case "Gun-Box":
                FeedbackScatola(collision.gameObject);   //Feedback visivo e audio per la scatola

                //Viene resettata posizione, rotazione e tutto del Rigidbody
                levelManagerScr.ResetSingoloOggetto(collision.gameObject);
                break;


            case "Rolling-Barrel":
                //FeedbackBarile();   //Feedback visivo e audio per il barile
                
                //Viene riaggiunta nella pool
                poolingScr.RiAggiungiOggetto("Barili", collision.gameObject);
                break;


            #region Istruzioni per aggiungere altro 
            case "Altro da aggiungere":
                //Feedback__________();   //Feedback visivo e audio per il/la __________

                //Viene riaggiunta nella pool
                //poolingScr.RiAggiungiOggetto(/*tag della pool (come variabile)*/, collision.gameObject);
                //Viene resettata posizione, rotazione e tutto del Rigidbody
                //levelManagerScr.ResetSingoloOggetto(collision.gameObject);

                break; 
                #endregion
        }
    }

    public void FeedbackScatola(GameObject scatola)
    {
        //Prende l'AudioClip dalla scatola e riproduce il suono della scatola rotta
        Transform boxTransf = scatola.transform;
        scatolaRotta_sfx = boxTransf.GetComponent<AudioSource>();
        scatolaRotta_sfx.Play();

        //Fa vedere le particelle della scatola che si rompe
        Instantiate(scatolaRotta_part.gameObject, boxTransf.position, boxTransf.rotation);
    }

    public void Feedback__________()
    {
        //Feedback (SFX, particelle, ecc.)
        //PS: il feedback è come quello della scatola
    }
}
