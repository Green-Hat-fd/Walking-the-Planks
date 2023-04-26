using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Rum (S.O.)", fileName = "Rum_SO")]
public class RumSO_Script : ScriptableObject
{
    [Header("�  Effetti (prima e dopo)  �")]
    [SerializeField] float moltipVelocita = 1.5f;
    [SerializeField] float moltipSalto = 1.25f;

    [Header("�  Tempistiche  �")]
    [SerializeField] float durataEffetti = 25;
    #region Tooltip()
    [Tooltip("Indica quanto tempo deve passare per avere gli effetti negativi e per poter tornare ad utilizzare di nuovo il Rum")]
    #endregion
    [SerializeField] float cooldown = 5 ;

    [Header("�  Curva dell'assuefazione  �")]
    [SerializeField] AnimationCurve curvaAssuefazione;   //Serve per cambiare la durata ogni volta che si beve il Rum
    #region Tooltip()
    [Tooltip("Il massimo dei secondi da togliere agli effetti")]
    #endregion
    [SerializeField] float secAssuefazioneEffetti = 22.5f;
    #region Tooltip()
    [Tooltip("Il massimo dei secondi da aggiungere al cooldown")]
    #endregion
    [SerializeField] float secAssuefazioneCooldown = 25f;
    #region Tooltip()
    [Tooltip("Le volte che bisogna bere \nper arrivare al max della curva")]
    #endregion
    [Space(7.5f)]
    [SerializeField] int maxBevuteAssuefazione = 75;
    int numeroBevute = 0;

    bool possoBere = true;   //Usato per non poter usare il Rum se e' gia' attivo
    bool poteriAttivi = false;
    bool raccolto = false;



    #region Funzioni Set custom

    public void AumentaNumBevute() { numeroBevute++; }
    public void ResetNumBevute() { numeroBevute = 0; }
    public void PossoBereDiNuovo() { possoBere = true; }
    public void NonPossoBere() { possoBere = false; }
    public void AttivaPoteriRum() { poteriAttivi = true; }
    public void DisattivaPoteriRum() { poteriAttivi = false; }
    public void CambiaRumRaccolto(bool valore) { raccolto = valore; }

    #endregion

    #region Funzioni Get custom

    //Effetti positivi
    public float LeggiMoltiplVelocita() => moltipVelocita;
    public float LeggiMoltiplSalto() => moltipSalto;
    public float LeggiDurataEffetti() => durataEffetti;
    public float LeggiCooldown() => cooldown;


    //Assuefazione
    public AnimationCurve LeggiCurvaAssuefaz() => curvaAssuefazione;
    /// <summary>
    /// Ritorna il valore Y sulla curva dato <i><b>time</b></i>, ovvero X
    /// </summary>
    public float LeggiValoreCurvaAssuefaz(float time) => curvaAssuefazione.Evaluate(time);
    public int LeggiNumeroBevute() => numeroBevute;
    public int LeggiMaxBevuteAssuefaz() => maxBevuteAssuefazione;
    public float LeggiSecAssuefazEffetti() => secAssuefazioneEffetti;
    public float LeggiSecAssuefazCooldown() => secAssuefazioneCooldown;


    //Attivazioni
    public bool LeggiPossoBere() => possoBere;
    public bool LeggiPoteriAttivo() => poteriAttivi;
    public bool LeggiRaccolto() => raccolto;

    #endregion
}
