using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CambioScena_Enum
{
    DaMenuPrincipale,
    DaAscensore
}

[CreateAssetMenu(menuName = "Scriptable Objects/Ascensore (S.O.)", fileName = "Ascensore_SO")]
public class AscensoreSO_Script : ScriptableObject
{
    [SerializeField] bool possoMettereInPausa;

    [Space(10)]
    [SerializeField] CambioScena_Enum daDoveCambioScena;


    #region Funzioni Set custom

    public void ScriviPossoMettereInPausa(bool vf) { possoMettereInPausa = vf; }
    public void ScriviDaDoveCambioScena(CambioScena_Enum nuovoValore) { daDoveCambioScena = nuovoValore; }

    #endregion

    #region Funzioni Get custom

    public bool LeggiPossoMettereInPausa() => possoMettereInPausa;
    public CambioScena_Enum LeggiDaDoveCambioScena() => daDoveCambioScena;

    #endregion
}
