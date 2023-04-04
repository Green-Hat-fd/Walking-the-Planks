using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CambiaTestoScript : MonoBehaviour
{
    #region Tooltip()
    [Tooltip("Il tag da cercare negli Scriptable Obj delle lingue \n(quelli sotto)")]
    #endregion
    [SerializeField] string tagDaCercare;

    [Space(10)]
    [SerializeField] OpzioniSO_Script opzioniSO;

    [Header("Lingue (Scriptable Obj.)")]
    [SerializeField] LinguaSO_Script inglese;
    [SerializeField] LinguaSO_Script italiano;

    /// <summary>
    /// La componente Testo di TMPro da modificare
    /// </summary>
    TMP_Text testoComp;



    private void Awake()
    {
        testoComp = GetComponent<TMP_Text>();

        //Sistema, per ogni lingua, dalla lista al dictionary
        inglese.SpostaTestiNelDictionary();
        italiano.SpostaTestiNelDictionary();
    }

    private void Update()
    {
        CambiaTesto();  //TODO: trova un modo per chiamare la funzione SOLO quando cambi la lingua
    }

    public void CambiaTesto()
    {
        LinguaSO_Script linguaTestiSelezSO = default;

        //Prende lo Scriptable Obj. rispetto alla lingua scelta
        switch (opzioniSO.LeggiLinguaScelta())
        {
            #region Inglese

            case OpzioniSO_Script.Lingue_Enum.Inglese:
                linguaTestiSelezSO = inglese;
                break;
            #endregion

            #region Italiano

            case OpzioniSO_Script.Lingue_Enum.Italiano:
                linguaTestiSelezSO = italiano;
                break;
            #endregion
        }

        //Prende il testo dalla lingua scelta e lo cambia nella componente TMPro
        testoComp.text = linguaTestiSelezSO.LeggiTesti(tagDaCercare);
    }
}
