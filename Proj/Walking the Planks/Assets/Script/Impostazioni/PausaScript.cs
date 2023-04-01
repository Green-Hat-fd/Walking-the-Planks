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

            TuttoCioDaCambiare();
        }
    }

    public void InvertiGiocoInPausa_DaMenu()
    {
        giocoInPausa = !giocoInPausa;

        TuttoCioDaCambiare();
    }


    void TuttoCioDaCambiare()
    {
        //(Dis)Abilita lo script del Rum
        rumScr.enabled = !giocoInPausa;

        rotazCameraScr.CambiaMouseAlCentro(!giocoInPausa);  //Cambia il mouse dal centro
        sparoScr.enabled = !giocoInPausa;                   //(Dis)Abilita lo script dello sparo

        //Azzera il tempo di gioco
        Time.timeScale = giocoInPausa ? 0 : 1;

        //Attiva la schermata di pausa
        menuPausa.SetActive(giocoInPausa);
    }
}
