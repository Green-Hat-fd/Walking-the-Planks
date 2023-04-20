using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuPausaScript : MonoBehaviour
{
    [SerializeField] OpzioniSO_Script opz_SO;

    [Header("Gli elementi della UI")]
    [SerializeField] Slider sl_volMusica;
    [SerializeField] Slider sl_volSuoni;
    [SerializeField] Slider sl_sensibilitaMouse;
    [SerializeField] TMP_Dropdown dr_lingua;
    [SerializeField] Toggle tg_schermoIntero;



    ///<summary>
    ///Cambia ogni elemento grafico della UI seguendo le impostazioni scelte
    ///</summary>
    public void CambiaElementi()
    {
        sl_volMusica.value = opz_SO.LeggiVolumeMusica_Percent();
        sl_volSuoni.value = opz_SO.LeggiVolumeSuoni_Percent();
        //sl_volMusica.onValueChanged.Invoke(1);
        //sl_volSuoni.onValueChanged.Invoke(1);
        sl_sensibilitaMouse.value = opz_SO.LeggiSensibilita();

        dr_lingua.value = (int)opz_SO.LeggiLinguaScelta();

        tg_schermoIntero.isOn = Screen.fullScreen;
    }
}
