using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    #region Sensibilità Mouse

    [Space(15)]
    [SerializeField] float moltipSensibilita = 1f;

    public void CambiaSensibilita(float s)
    {
        moltipSensibilita = s;
    }

    #endregion


    #region Selezione Lingua

    [Space(15)]
    [SerializeField] Lingue_Enum linguaScelta;

    public void CambiaLingua(Lingue_Enum l)
    {
        linguaScelta = l;
        //0: English (default negli switch)
        //1: Italiano
    }

    #endregion


    #region Volume

    [Space(15)]
    [SerializeField] float volumeMusica = 1f;
    [SerializeField] float volumeSuoni = 1f;

    public void CambiaVolumeMusica(float vM)
    {
        volumeMusica = vM / 100f;
    }
    public void CambiaVolumeSuoni(float vS)
    {
        volumeSuoni = vS / 100f;
    }

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