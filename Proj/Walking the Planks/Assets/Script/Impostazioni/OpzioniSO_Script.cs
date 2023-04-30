using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptable Objects/Opzioni (S.O.)", fileName = "Opzioni_SO")]
public class OpzioniSO_Script : ScriptableObject
{
    //Menu principale
    #region Cambia scena

    [SerializeField] CheckpointSO_Script checkpointSO;
    [SerializeField] RumSO_Script rumSO;
    [SerializeField] int numScenaGestore;

    public void ScenaScelta(int numScena)
    {
        SceneManager.LoadSceneAsync(numScena);
    }
    public void ScenaAggiunta(string nomeScena)
    {
        SceneManager.LoadScene(nomeScena, LoadSceneMode.Additive);
    }
    public void ScenaAggiunta(int numScena)
    {
        SceneManager.LoadScene(numScena, LoadSceneMode.Additive);
    }
    public void ScenaSuccessiva()
    {
        int scenaOra = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync(++scenaOra);
    }
    public void ScenaPrecedente()
    {
        int scenaOra = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync(--scenaOra);
    }
    public void CaricaUltimaScena()
    {
        int scenaDaCheckpoint = checkpointSO.LeggiLivello();

        //Carica l'ultima scena se si ha giocato al gioco, se no ricomincia dalla prima
        if (scenaDaCheckpoint <= 0)
        {
            ResetTutto();
            ScenaSuccessiva();
        }
        else
            ScenaScelta(scenaDaCheckpoint);
    }
    void ResetTutto()
    {
        checkpointSO.CambiaCheckpoint(1, 0, Vector3.zero);
        rumSO.ResetNumBevute();
        rumSO.PossoBereDiNuovo();
        rumSO.DisattivaPoteriRum();
        rumSO.CambiaRumRaccolto(false);
    }

    #endregion


    #region Esci

    public void EsciDalGioco()
    {
        Application.Quit();
    }

    #endregion


    //Opzioni
    #region Sensibilita' Mouse

    [Space(15)]
    [SerializeField] float moltipSensibilita = 1f;

    public void CambiaSensibilita(float s)
    {
        moltipSensibilita = s;
    }

    public float LeggiSensibilita() => moltipSensibilita;

    #endregion


    #region Selezione Lingua

    [Space(15)]
    [SerializeField] Lingue_Enum linguaScelta;

    public void CambiaLingua(Lingue_Enum l)
    {
        linguaScelta = l;
    }
    public void CambiaLingua(int i)
    {
        linguaScelta = (Lingue_Enum)i;
    }

    public Lingue_Enum LeggiLinguaScelta() => linguaScelta;

    #endregion


    #region Volume e Audio

    [Space(15)]
    [SerializeField] AudioMixer mixerGenerale;
    [SerializeField] AnimationCurve curvaAudio;
    [Range(0, 110)]
    [SerializeField] float volumeMusica = 0f;
    [Range(0, 110)]
    [SerializeField] float volumeSuoni = 0f;
    [Range(0, 110)]
    [SerializeField] float volumeDialoghi = 0f;

    ///<summary></summary>
    /// <param name="vM"> il nuovo volume, nel range [0; 1.1]</param>
    public void CambiaVolumeMusica(float vM)
    {
        //Lo mette come volume nel mixer tra [-80; 5] dB
        mixerGenerale.SetFloat("musVol", curvaAudio.Evaluate(vM));
        
        volumeMusica = vM * 100;
    }
    ///<summary></summary>
    /// <param name="vS"> il nuovo volume, nel range [0; 1.1]</param>
    public void CambiaVolumeSuoni(float vS)
    {
        //Lo mette come volume nel mixer tra [-80; 5] dB
        mixerGenerale.SetFloat("sfxVol", curvaAudio.Evaluate(vS));
        
        volumeSuoni = vS * 100;
    }
    ///<summary></summary>
    /// <param name="vD"> il nuovo volume, nel range [0; 1.1]</param>
    public void CambiaVolumeDialoghi(float vD)
    {
        //Lo mette come volume nel mixer tra [-80; 5] dB
        mixerGenerale.SetFloat("dialoghiVol", curvaAudio.Evaluate(vD));

        volumeDialoghi = vD * 100;
    }

    public AnimationCurve LeggiCurvaVolume() => curvaAudio;

    public float LeggiVolumeMusica() => curvaAudio.Evaluate(volumeMusica);
    public float LeggiVolumeMusica_Percent() => volumeMusica / 100;
    public float LeggiVolumeSuoni() => curvaAudio.Evaluate(volumeSuoni);
    public float LeggiVolumeSuoni_Percent() => volumeSuoni / 100;
    public float LeggiVolumeDiaoghi() => volumeDialoghi;

    #endregion


    #region Schermo intero

    [Space(15)]
    [SerializeField] bool schermoIntero = true;

    public void SchermoIntero_OnOff(bool yn)
    {
        Screen.fullScreen = yn;

        schermoIntero = yn;
    }

    #endregion


    //Altro
    #region Altre funzioni

    //Enum per le lingue
    public enum Lingue_Enum
    {
        Inglese,
        Italiano
    }

    #endregion
}