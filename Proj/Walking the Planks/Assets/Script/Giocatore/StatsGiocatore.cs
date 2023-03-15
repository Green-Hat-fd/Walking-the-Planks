using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGiocatore : MonoBehaviour
{
    [SerializeField] float secDiAttesa = 1.5f;
    float tempoTrascorso;

    [Space(15)]
    [SerializeField]
    GameObject cameraTerzaPers;
    [SerializeField]
    GameObject cameraGiocat;  //La telecamera in prima persona (default nel gioco)
    MovimGiocatRb movimGiocatScript;

    bool sonoMorto;

    [Space(15)]
    [SerializeField] CheckpointSO_Script checkpoint;



    private void Start()
    {
        movimGiocatScript = GetComponent<MovimGiocatRb>();
    }

    void Update()
    {
        if (sonoMorto)
        {
            cameraGiocat.SetActive(false);
            cameraTerzaPers.SetActive(true);
            movimGiocatScript.enabled = false;

            if (tempoTrascorso >= secDiAttesa)
            {
                //Ritorna al checkpoint
                transform.position = checkpoint.LeggiPosizioneCheckpoint();

                //Resetta tutto il livello
                FindObjectOfType<LevelManagerScript>().ResetCompleto();


                sonoMorto = false;
                tempoTrascorso = 0;
            }
            else
            {
                tempoTrascorso += Time.deltaTime;
            }
        }
        else
        {
            cameraGiocat.SetActive(true);
            cameraTerzaPers.SetActive(false);
            movimGiocatScript.enabled = true;

            if(tempoTrascorso != 0)
                tempoTrascorso = 0;   //Reset -- misura di sicurezza
        }
    }

    #region Funzioni Get custom

    public bool LeggiSonoMorto() => sonoMorto;

    //void Funzione() { }

    #endregion

    #region Funzioni Set custom

    public void ScriviSonoMorto(bool nuovoValore)
    {
        sonoMorto = nuovoValore;
    }

    #endregion
}
