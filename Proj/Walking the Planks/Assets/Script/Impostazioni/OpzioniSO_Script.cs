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

    public void ScenaScegliTu(int numScena)
    {
        SceneManager.LoadScene(numScena);
    }
    public void ScenaSuccessiva()
    {
        int scenaOra = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(++scenaOra);
    }
    public void ScenaPrecedente()
    {
        int scenaOra = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(--scenaOra);
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

    public Lingue_Enum LeggiLinguaScelta() => linguaScelta;

    #endregion


    #region Volume e Audio

    [Space(15)]
    [SerializeField] AudioMixer mixerGenerale;
    [Range(0, 1)]
    [SerializeField] float volumeMusica = 1f;
    [Range(0, 1)]
    [SerializeField] float volumeSuoni = 1f;
    [Range(0, 1)]
    [SerializeField] float volumeDialoghi = 1f;

    ///<summary></summary>
    /// <param name="vM"> il nuovo volume, nel range [-80; 0]</param>
    public void CambiaVolumeMusica(float vM)
    {
        //Lo mette come volume nel mixer tra [-80; 5] dB
        mixerGenerale.SetFloat("musVol", vM);
        
        volumeMusica = vM;
    }
    ///<summary></summary>
    /// <param name="vS"> il nuovo volume, nel range [-80; 0]</param>
    public void CambiaVolumeSuoni(float vS)
    {
        //Lo mette come volume nel mixer tra [-80; 5] dB
        mixerGenerale.SetFloat("sfxVol", vS);
        
        volumeSuoni = vS;
    }
    ///<summary></summary>
    /// <param name="vD"> il nuovo volume, nel range [-80; 0]</param>
    public void CambiaVolumeDialoghi(float vD)
    {
        //Lo mette come volume nel mixer tra [-80; 5] dB
        mixerGenerale.SetFloat("dialoghiVol", vD);

        volumeDialoghi = vD;
    }

    public float LeggiVolumeMusica() => volumeMusica;
    public float LeggiVolumeSuoni() => volumeSuoni;
    public float LeggiVolumeDiaoghi() => volumeDialoghi;

    #endregion


    #region Schermo intero

    public void SchermoIntero_OnOff(bool yn)
    {
        Screen.fullScreen = yn;
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