using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPausaScript : MonoBehaviour
{
    [SerializeField] OpzioniSO_Script opz_SO;

    [Header("Gli elementi della UI")]
    [SerializeField] Slider sl_volMusica;
    [SerializeField] Slider sl_volSuoni;
    [SerializeField] Slider sl_sensibilitaMouse;
    [SerializeField] Dropdown dr_lingua;
    [SerializeField] Toggle tg_schermoIntero;


    private void OnEnable()
    {
        //Cambia ogni elemento grafico della UI alle impostazioni scelte
        
        sl_volMusica.value = opz_SO.LeggiVolumeMusica();
        sl_volSuoni.value = opz_SO.LeggiVolumeSuoni();
        sl_sensibilitaMouse.value = opz_SO.LeggiSensibilita();

        dr_lingua.value = (int)opz_SO.LeggiLinguaScelta();

        tg_schermoIntero.isOn = Screen.fullScreen;
    }
}
