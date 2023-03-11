using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Rum (S.O.)", fileName = "Rum_SO")]
public class RumSO_Script : ScriptableObject
{
    [Header("—  Effetti (prima e dopo)  —")]
    [SerializeField] float moltipVelocita = 1.5f;
    [SerializeField] float moltipSalto = 1.25f;

    [Header("—  Tempistiche  —")]
    [SerializeField] float durataEffetti = 25;
    #region Tooltip()
    [Tooltip("Indica quanto tempo deve passare per avere gli effetti negativi e per poter tornare ad utilizzare di nuovo il Rum")]
    #endregion
    [SerializeField] float cooldown = 5 ;

    [Header("—  Curva dell'assuefazione  —")]
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
    bool attivo = false;



    #region Funzioni Set custom

    public void AumentaNumBevute() { numeroBevute++; }
    public void PossoBereDiNuovo() { possoBere = true; }
    public void NonPossoBere() { possoBere = false; }
    public void AttivaRum() { attivo = true; }
    public void DisattivaRum() { attivo = false; }

    #endregion

    #region Funzioni Get custom

    //Effetti positivi
    public float LeggiMoltiplVelocita() { return moltipVelocita; }
    public float LeggiMoltiplSalto() { return moltipSalto; }
    public float LeggiDurataEffetti() { return durataEffetti; }
    public float LeggiCooldown() { return cooldown; }


    //Assuefazione
    public AnimationCurve LeggiCurvaAssuefaz() { return curvaAssuefazione; }
    /// <summary>
    /// Ritorna il valore Y sulla curva dato <i><b>time</b></i>, ovvero X
    /// </summary>
    public float LeggiValoreCurvaAssuefaz(float time) { return curvaAssuefazione.Evaluate(time); }
    public int LeggiNumeroBevute() { return numeroBevute; }
    public int LeggiMaxBevuteAssuefaz() { return maxBevuteAssuefazione; }
    public float LeggiSecAssuefazEffetti() { return secAssuefazioneEffetti; }
    public float LeggiSecAssuefazCooldown() { return secAssuefazioneCooldown; }


    //Attivazioni
    public bool LeggiPossoBere() { return possoBere; }
    public bool LeggiAttivo() { return attivo; }

    #endregion
}
