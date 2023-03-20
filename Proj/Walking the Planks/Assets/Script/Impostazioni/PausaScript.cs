using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaScript : MonoBehaviour
{
    bool giocoInPausa = false;

    RumScript rumScr;
    RotazioneTelecamPrimaPers rotazCameraScr;
    SparoScript sparoScr;

    [SerializeField] GameObject menuPausa;


    void Awake()
    {
        rumScr = GetComponentInChildren<RumScript>();
        rotazCameraScr = GetComponentInChildren<RotazioneTelecamPrimaPers>();
        sparoScr = GetComponentInChildren<SparoScript>();

        menuPausa.SetActive(false);
    }

    void Update()
    {
        //Fa entrare o uscire dalla pausa se si preme il pulsante
        if (GameManager.inst.inputManager.Generali.Pausa.triggered)
        {
            giocoInPausa = !giocoInPausa;

            #region Tutto cio' che che da cambiare

            rumScr.enabled = !giocoInPausa;                     //(Dis)Abilita lo script del Rum

            rotazCameraScr.CambiaMouseAlCentro(!giocoInPausa);  //Cambia il mouse dal centro
            sparoScr.enabled = !giocoInPausa;                   //(Dis)Abilita lo script dello sparo

            Time.timeScale = giocoInPausa ? 0 : 1;  //Azzera il tempo di gioco

            menuPausa.SetActive(giocoInPausa);      //Attiva la schermata di pausa

            #endregion
        }
    }
}
