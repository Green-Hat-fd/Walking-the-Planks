using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CambiaTestoNumericoScript : MonoBehaviour
{
    [SerializeField] OpzioniSO_Script opzioniSO;

    TMP_Text testoDaCamb;

    [Space(10)]
    [SerializeField] string daAggiungereDopo;


    private void Awake()
    {
        testoDaCamb = GetComponent<TMP_Text>();
    }


    #region Funzioni che cambiano il testo

    /// <summary>
    /// Cambia il testo con il valore assegnato,
    /// <br></br> aggiungendo la variabile <i><b>daAggiungereDopo</b></i> dopo il numero
    /// </summary>
    /// <param name="t">Il testo da cambiare</param>
    public void CambiaTesto_ConStringa(string t)
    {
        testoDaCamb.text = t + daAggiungereDopo;
    }


    /// <summary>
    /// Rende il <i>float</i> in un <i>int</i> e lo scrive sottoforma di testo
    /// </summary>
    /// <param name="t">Il testo da cambiare</param>
    public void CambiaTestoNum_daFloatAInt(float t)
    {
        testoDaCamb.text = Mathf.RoundToInt(t).ToString();
    }
    /// <summary>
    /// Rende il <i>float</i> in un <i>int</i> e lo scrive sottoforma di testo,
    /// <br></br> aggiungendo la variabile <i><b>daAggiungereDopo</b></i> dopo il numero
    /// </summary>
    public void CambiaTestoNum_daFloatAInt_ConStringa(float t)
    {
        testoDaCamb.text = Mathf.RoundToInt(t) + daAggiungereDopo;
    }


    /// <summary>
    /// Approssima il <i>float</i> a 2 cifre decimali e lo scrive sottoforma di testo,
    /// </summary>
    /// <param name="t">Il testo da cambiare</param>
    public void CambiaTestoNum_Appross(float t)
    {
        testoDaCamb.text = ((float)(Mathf.RoundToInt(t*100f) / 100f)).ToString();
    }
    /// <summary>
    /// Approssima il <i>float</i> a 2 cifre decimali e lo scrive sottoforma di testo,
    /// <br></br> aggiungendo la variabile <i><b>daAggiungereDopo</b></i> dopo il numero
    /// </summary>
    public void CambiaTestoNum_Appross_ConStringa(float t)
    {
        testoDaCamb.text = (float)(Mathf.RoundToInt(t * 100f) / 100f) + daAggiungereDopo;
    }


    /// <summary>
    /// Prende un numero nello stile dell'AudioMixerGroup (nel range <b>[-80; 5]</b>) e lo rende una percentuale <i>(da 0 a 100)</i>
    /// </summary>
    public void CambiaTestoVolume(float t)
    {
        testoDaCamb.text = DaVolumeAPercent(t).ToString();
    }
    /// <summary>
    /// Prende un numero nello stile dell'AudioMixerGroup (nel range <b>[-80; 5]</b>) e lo rende una percentuale <i>(da 0 a 100)</i>,
    /// <br></br> aggiungendo la variabile <i><b>daAggiungereDopo</b></i> dopo il numero
    /// </summary>
    public void CambiaTestoVolume_ConStringa(float t)
    {
        testoDaCamb.text = DaVolumeAPercent(t) + daAggiungereDopo;
    }

    #endregion


    #region Funz. che converte da [-80; 5] a [0; 100]

    /// <summary>
    /// Prende il numero da AudioMixerGroup e lo converte in percentuale
    /// </summary>
    /// <param name="num">Numero (range <b>[-80; 5]</b>) da convertire in percentuale</param>
    /// <returns>ritorna il misfatto</returns>
    float DaVolumeAPercent(float num)
    {
        float ans = opzioniSO.LeggiCurvaVolume().Evaluate(num);

        return Mathf.RoundToInt(ans * 100);
    }

    #endregion
}
