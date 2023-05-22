using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PausaScript : MonoBehaviour
{
    [SerializeField] AscensoreSO_Script ascensSO;

    bool giocoInPausa = false;

    RumScript rumScr;
    RotazioneTelecamPrimaPers rotazCameraScr;
    SparoScript sparoScr;

    [SerializeField] GameObject menuPausa;

    List<AudioSource> tuttiSfxSource;
    [SerializeField] AudioMixerGroup gruppoSfx;

    [Space(15)]
    [SerializeField] Animator caricamAnim;


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
        bool pulsantePremuto = GameManager.inst.inputManager.Generali.Pausa.triggered;


        //Fa entrare o uscire dalla pausa se si preme il pulsante (e si puo' mettere in pausa)
        if (pulsantePremuto && ascensSO.LeggiPossoMettereInPausa())
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
        rumScr.enabled = !giocoInPausa;

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

    public void AvviaCaricamento()
    {
        caricamAnim.SetTrigger("Avvia caricam");
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
}
