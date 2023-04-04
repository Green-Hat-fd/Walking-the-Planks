using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGiocatore : MonoBehaviour
{
    [SerializeField] float secDiAttesa = 1.5f;
    float tempoTrascorso;

    [Space(15)]
    [SerializeField]
    GameObject cameraTerzaPers;  //La telecamera visibile quando il giocat. muore
    [SerializeField]
    GameObject cameraGiocat;  //La telecamera in prima persona (default nel gioco)
    
    MovimGiocatRb movimGiocatScr;
    
    [SerializeField] float velCamTerzaPers = 10f;

    bool sonoMorto;

    [Space(15)]
    [SerializeField] CheckpointSO_Script checkpoint;
    LevelManagerScript levelManagerScr;



    private void Start()
    {
        movimGiocatScr = GetComponent<MovimGiocatRb>();
        levelManagerScr = FindObjectOfType<LevelManagerScript>();
    }

    void Update()
    {
        if (sonoMorto)
        {
            GetComponent<Rigidbody>().drag = 0;

            cameraGiocat.SetActive(false);
            cameraTerzaPers.SetActive(true);
            movimGiocatScr.enabled = false;

            //Ruota la camera in terza persona attorno al giocatore
            cameraTerzaPers.transform.Rotate(0, velCamTerzaPers * Time.deltaTime, 0);


            if (tempoTrascorso >= secDiAttesa)
            {
                //Ritorna al checkpoint
                transform.position = checkpoint.LeggiPosizioneCheckpoint();

                //Resetta tutto il livello
                levelManagerScr.ResetCompleto();


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
            movimGiocatScr.enabled = true;

            if(tempoTrascorso != 0)
                tempoTrascorso = 0;   //Reset -- misura di sicurezza
        }

        //Controlla se il giocatore si trova in uno spazio
        //negativo fuori dalla mappa, e lo fa morire
        if (transform.position.y <= -700)
            sonoMorto = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(transform.position.x, -700, transform.position.z), new Vector3(100f, 0.1f, 100f));
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
