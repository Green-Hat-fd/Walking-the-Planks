using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tutorial_Enum
{
    AspettaSalto,
    AspettaSparo,
    AspettaRumBevuto,
    AspettaSecondi
}

public class AreaTutorialScript : MonoBehaviour
{
    [SerializeField] Sprite immagine;
    #region Tooltip()
    [Tooltip("Il tag da cercare negli Scriptable Obj delle lingue \n(quelli nel TutorialManagerScript)")]
    #endregion
    [SerializeField] string tagDaCercare;

    [Space(15)]
    [SerializeField] Tutorial_Enum cosaDevoAspettare = Tutorial_Enum.AspettaSecondi;
    [Min(1)]
    [SerializeField] float secDaAspettare = 7.5f;
    
    Collider coll;



    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Prende lo script del Tutorial dal giocatore
            TutorialMangerScript tutScr = other.GetComponent<TutorialMangerScript>();

            //Passa l'icona, il testo da mostrare e quanti secondi deve aspettare
            tutScr.PassaImmagine(immagine);
            tutScr.PassaTagTesto(tagDaCercare);
            tutScr.PassaCosaAspettare(cosaDevoAspettare);
            tutScr.PassaSecondiDaAspettare(secDaAspettare);

            //Avvia il tutorial
            tutScr.AvviaTutorial();

            //Disattiva il trigger
            coll.enabled = false;
        }
    }
}
