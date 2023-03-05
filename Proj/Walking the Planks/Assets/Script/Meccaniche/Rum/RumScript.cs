using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumScript : MonoBehaviour
{
    [SerializeField] GameObject[] oggettiDaMostrare;

    //TODO: Inserisci le stats che modifica quando c'e' l'effetto
    [Header("—  Effetti (prima e dopo)  —")]
    [SerializeField] float moltipVelocita = 1.5f; 
    [SerializeField] float moltipSalto = 1.25f;
    float _moltipVelGiocat,
          _moltipSaltoGiocat;

    [Space(7.5f)]
    [SerializeField] GameObject effettoVisivo;  //L'effetto negativo mostrato quando finiscono quelli positivi

    [Header("—  Tempistiche  —")]
    [SerializeField] float durataEffetti = 25;
    #region Tooltip()
    [Tooltip("Indica quanto tempo deve passare per avere gli effetti negativi\ne per poter tornare ad utilizzare di nuovo il Rum")]
    #endregion
    [SerializeField] float cooldown = 5;
    float tempoTrascorso_Effetti = 0,
          tempoTrascorso_Cooldown = 0,
          _secMaxEffetti,
          _sexMaxCooldown;

    [Header("—  Curva dell'assuefazione  —")]
    [SerializeField] AnimationCurve curvaAssuefazione;   //Serve per cambiare la durata ogni volta che si beve il Rum
    #region Tooltip()
    [Tooltip("I secondi da togliere agli effetti")]
    #endregion
    [SerializeField] float secAssuefazioneEffetti = 22.5f;
    #region Tooltip()
    [Tooltip("I secondi da aggiungere al cooldown")]
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



    private void Start()
    {
        _secMaxEffetti = durataEffetti;
        _sexMaxCooldown = cooldown;

        effettoVisivo.SetActive(false);
    }

    void Update()
    {
        bool hoBevuto = GameManager.inst.inputManager.Giocatore.UsoRum.triggered;

        
        #region Cambio delle durate dal numero di bevute

        float puntoX_dellaCurva = numeroBevute / maxBevuteAssuefazione;         //Rende il numero tra 0 e 1
        float puntoSullaCurva = curvaAssuefazione.Evaluate(puntoX_dellaCurva);  //Prende la Y sulla curva rispetto alla X


        //Diminuisce la durata degli effetti rispetto alle bevute
        _secMaxEffetti = durataEffetti - (puntoSullaCurva * secAssuefazioneEffetti);

        //Aumenta la durata del cooldown rispetto alle bevute
        _sexMaxCooldown = cooldown + (puntoSullaCurva * secAssuefazioneCooldown);

        #endregion


        if (hoBevuto && possoBere)
            attivo = true;


        if (attivo)
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
              

                    attivo = false;
                    possoBere = true;   //Il giocatore puo' utilizzare di nuovo il Rum

                    numeroBevute++;    //Aumenta il conteggio delle bevute


                    //Reset di entrambi i timer (effetti e cooldown)
                    tempoTrascorso_Cooldown = 0;
                    tempoTrascorso_Effetti = 0;
                }
                else
                {
                    #region Effetti negativi

                    //Riduce la visibilita' del giocatore
                    effettoVisivo.SetActive(true);

                    #endregion


                    tempoTrascorso_Cooldown += Time.deltaTime;   //Aumenta il tempo trascorso per il cooldown
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
                _moltipVelGiocat  =  moltipVelocita;
                _moltipSaltoGiocat = moltipSalto;

                //Rimuove la possibilita' di bere un'altra volta
                possoBere = false;

                #endregion


                tempoTrascorso_Effetti += Time.deltaTime;  //Aumenta il conteggio del tempo trascorso
            }
        }
    }


    #region Funzioni Get custom

    public float LeggiMoltipVelGiocat()
    {
        return _moltipVelGiocat;
    }
    public float LeggiMoltipSaltoGiocat()
    {
        return _moltipSaltoGiocat;
    }
    public bool LeggiAttivo()
    {
        return attivo;
    }

    #endregion
}
