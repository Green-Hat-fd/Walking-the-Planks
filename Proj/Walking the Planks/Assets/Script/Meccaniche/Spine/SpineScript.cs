using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineScript : MonoBehaviour
{
    LevelManagerScript levelManagerScr;
    ObjectPoolingScript poolingScr;

    [Header("—  Feedback (Scatola)  —")]
    [SerializeField] ParticleSystem scatolaRotta_part;

    [Header("—  Feedback (Barile)  —")]
    [SerializeField] ParticleSystem barileRotto_part;



    private void Awake()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();
        levelManagerScr = FindObjectOfType<LevelManagerScript>();
    }

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
        switch (obj.tag)
        {
            //Fa morire il giocatore
            case "Player":
                obj.GetComponent<StatsGiocatore>().ScriviSonoMorto(true);
                break;


            //"Distrugge" le scatole e i barili
            case "Gun-Box":
                FeedbackScatola(obj);   //Feedback visivo e audio per la scatola

                //Viene resettata posizione, rotazione e tutto del Rigidbody
                levelManagerScr.ResetSingoloOggetto(obj);
                break;


            case "Rolling-Barrel":
                //FeedbackBarile();   //Feedback visivo e audio per il barile
                
                //Viene riaggiunta nella pool
                poolingScr.RiAggiungiOggetto("Barili", obj);
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
        Transform boxTransf = scatola.transform;

        //Fa vedere le particelle della scatola che si rompe
        Instantiate(scatolaRotta_part.gameObject, boxTransf.position, boxTransf.rotation);
    }

    public void Feedback__________()
    {
        //Feedback (SFX, particelle, ecc.)
        //PS: il feedback e' come quello della scatola
        #region Guida feedback
        /* Transform boxTransf = scatola.transform;
         *
         * //Prende l'AudioClip dalla scatola e riproduce il suono della scatola rotta
         * scatolaRotta_sfx = boxTransf.GetComponent<AudioSource>();
         * scatolaRotta_sfx.Play();
         * 
         * //Fa vedere le particelle della scatola che si rompe
         * Instantiate(scatolaRotta_part.gameObject, boxTransf.position, boxTransf.rotation);
         */ 
        #endregion

    }
}
