using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RumScript : MonoBehaviour
{
    [SerializeField] RumSO_Script rum_SO;
    [SerializeField] StatsGiocatore statsScript;

    [Space(15)]
    [SerializeField] GameObject[] oggettiDaMostrare;

    //---- Effetti positivi ----
    float _moltipVelGiocat,
          _moltipSaltoGiocat;

    [Space(10f)]
    [SerializeField] GameObject effettoVisivo;  //L'effetto negativo mostrato quando finiscono quelli positivi

    //---- Tempistiche ----
    float tempoTrascorso_Effetti = 0,     //tempoTrascorso servono per tenere traccia del tempo nei diversi timer
          tempoTrascorso_Cooldown = 0,
          _secMaxEffetti,                 //_secMax servono per controllare quanto deve durare ogni timer
          _sexMaxCooldown;


    //---- Feedback ----
    [Header("Feedback")]
    [SerializeField] AudioSource rumBevuto_sfx;
    
    [Space(10)]
    [SerializeField] Slider sliderRum;
    [SerializeField] GameObject modelloRum;
    [SerializeField] Animator modelloRumAnim;



    private void Start()
    {
        _secMaxEffetti = rum_SO.LeggiDurataEffetti();
        _sexMaxCooldown = rum_SO.LeggiCooldown();

        effettoVisivo.SetActive(false);

        rum_SO.DisattivaPoteriRum();
        rum_SO.PossoBereDiNuovo();

        //Attiva o meno il modello se ha raccolto il rum o no
        modelloRum.SetActive(rum_SO.LeggiRaccolto());
    }

    void Update()
    {
        bool hoBevuto = GameManager.inst.inputManager.Giocatore.UsoRum.triggered;
        
        
        #region Cambio delle durate dal numero di bevute

        float puntoX_dellaCurva = rum_SO.LeggiNumeroBevute() / rum_SO.LeggiMaxBevuteAssuefaz();   //Rende il numero tra 0 e 1
        float puntoSullaCurva = rum_SO.LeggiValoreCurvaAssuefaz(puntoX_dellaCurva);               //Prende la Y sulla curva rispetto alla X

        //Diminuisce la durata degli effetti rispetto alle bevute ()
        _secMaxEffetti = rum_SO.LeggiDurataEffetti() - (puntoSullaCurva * rum_SO.LeggiSecAssuefazEffetti());

        //Aumenta la durata del cooldown rispetto alle bevute
        _sexMaxCooldown = rum_SO.LeggiCooldown() + (puntoSullaCurva * rum_SO.LeggiSecAssuefazCooldown());

            #region Spiegazione sull'operazione (puntoSullaCurva * secAssuefazione)
            /* secAssuefazione (entrambe le variabili)
             * servono per capire quanto bisogna
             * togliere o aggiungere alle durate iniziali
             */
            #endregion


        #endregion


        if (hoBevuto && rum_SO.LeggiPossoBere() && rum_SO.LeggiRaccolto())
        {
            rum_SO.AttivaPoteriRum();

            Feedback();
        }

        //Se i poteri attivi e NON sono morto
        if (rum_SO.LeggiPoteriAttivo() && !statsScript.LeggiSonoMorto())
        {
            //Se la durata degli effetti e' finita...
            if (tempoTrascorso_Effetti >= _secMaxEffetti)
            {
                //Controlla se e' finito il cooldown...
                if (tempoTrascorso_Cooldown >= _sexMaxCooldown)
                {
                    #region Fine degli effetti negativi
                
                    //Ripristina la visibilita'
                    effettoVisivo.SetActive(false);
                
                    #endregion
              

                    rum_SO.DisattivaPoteriRum();
                    rum_SO.PossoBereDiNuovo();   //Il giocatore puo' utilizzare di nuovo il Rum

                    rum_SO.AumentaNumBevute();    //Aumenta il conteggio delle bevute


                    //Reset di entrambi i timer (effetti e cooldown)
                    tempoTrascorso_Cooldown = 0;
                    tempoTrascorso_Effetti = 0;


                    #region Feedback

                    //Toglie lo slider del rum
                    sliderRum.gameObject.SetActive(false);

                    #endregion
                }
                else
                {
                    #region Effetti negativi

                    //Riduce la visibilita' del giocatore
                    effettoVisivo.SetActive(true);

                    #endregion


                    tempoTrascorso_Cooldown += Time.deltaTime;   //Aumenta il tempo trascorso per il cooldown

                    #region Feedback

                    //Aumenta lo slider rispetto al cooldown
                    sliderRum.value = tempoTrascorso_Cooldown / _sexMaxCooldown;

                    #endregion
                }


                #region Fine degli effetti positivi

                //Nasconde ogni oggetto nell'array
                foreach (GameObject obj in oggettiDaMostrare)
                    obj.SetActive(false);

                //Ripristina la velocita' e il salto del giocatore
                _moltipVelGiocat  =  1;
                _moltipSaltoGiocat = 1;

                #endregion
            }
            else
            {
                #region Effetti positivi

                //Mostra ogni oggetto nell'array
                foreach (GameObject obj in oggettiDaMostrare)
                    obj.SetActive(true);

                //Aumenta velocita' e salto del giocatore
                _moltipVelGiocat  =  rum_SO.LeggiMoltiplVelocita();
                _moltipSaltoGiocat = rum_SO.LeggiMoltiplSalto();

                //Rimuove la possibilita' di bere un'altra volta
                rum_SO.NonPossoBere();

                #endregion


                tempoTrascorso_Effetti += Time.deltaTime;  //Aumenta il conteggio del tempo trascorso

                #region Feedback

                //Mostra lo slider del rum
                sliderRum.gameObject.SetActive(true);

                //Diminuisce lo slider rispetto a quanto tempo manca
                sliderRum.value = (_secMaxEffetti - tempoTrascorso_Effetti) / _secMaxEffetti;

                #endregion
            }
        }
    }


    #region Funzioni Get custom

    public float LeggiMoltipVelGiocat() => _moltipVelGiocat;
    public float LeggiMoltipSaltoGiocat() => _moltipSaltoGiocat;
    public bool SonoAttivoConPoteri() => rum_SO.LeggiPoteriAttivo();
    public bool RumRaccolto() => rum_SO.LeggiRaccolto();

    #endregion

    void Feedback()
    {
        modelloRumAnim.SetTrigger("Bevuto");

        rumBevuto_sfx.Play();
    }

    public void AnimazioneRaccolto()
    {
        modelloRum.SetActive(true);
        modelloRumAnim.SetTrigger("Raccolto");
    }
}
