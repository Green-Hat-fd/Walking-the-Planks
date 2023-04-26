using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PausaScript : MonoBehaviour
{
    bool giocoInPausa = false;

    RumScript rumScr;
    RotazioneTelecamPrimaPers rotazCameraScr;
    SparoScript sparoScr;

    [SerializeField] GameObject menuPausa;

    List<AudioSource> tuttiSfxSource;
    [SerializeField] AudioMixerGroup gruppoSfx;


    void Awake()
    {
        rumScr = GetComponentInChildren<RumScript>();
        rotazCameraScr = GetComponentInChildren<RotazioneTelecamPrimaPers>();
        sparoScr = GetComponentInChildren<SparoScript>();

        menuPausa.SetActive(false);

        tuttiSfxSource = new List<AudioSource>(FindObjectsOfType<AudioSource>());
        for (int i = 0; i < tuttiSfxSource.Count; i++)
        {
            //Rimuove tutti gli AudioSource che non fanno parte degli effetti sonori
            if (tuttiSfxSource[i].outputAudioMixerGroup != gruppoSfx)
                tuttiSfxSource.RemoveAt(i);
        }
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
        //(Dis)Abilita lo script del Rum (solo se ha raccolto il rum)
        rumScr.enabled = rumScr.RumRaccolto()  ?  !giocoInPausa  :  false;

        rotazCameraScr.CambiaMouseAlCentro(!giocoInPausa);  //Cambia il mouse dal centro
        sparoScr.enabled = !giocoInPausa;                   //(Dis)Abilita lo script dello sparo

        //Azzera/Riattiva il tempo di gioco
        Time.timeScale = giocoInPausa ? 0 : 1;

        //Per ogni AudioSource nella scena...
        foreach (AudioSource aS in tuttiSfxSource)
        {
            //(Dis)Attiva ogni effetto sonoro nella scena
            if (giocoInPausa)
                aS.Pause();
            else
                aS.UnPause();
        }


        //Attiva la schermata di pausa
        menuPausa.SetActive(giocoInPausa);
    }
}
