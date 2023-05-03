using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AscensoreScript : MonoBehaviour
{
    [SerializeField] OpzioniSO_Script opzSO;
    [SerializeField] AscensoreSO_Script ascensSO;

    [Space(15)]
    [SerializeField] bool ascensoreDiArrivo;

    [Space(15)]
    [SerializeField] AudioMixer mixerGenerale;

    bool giocatoreEntrato = false;
    bool cambiaMusica = false;
    float tempoPassato_VolumeMus;

    const float SEC_TOGLIERE_MUSICA_ASCENS = 0.5f;
    const float SEC_ASPETTARE_DENTRO = 10f;
    const float SEC_ASPETTARE_INIZIO_SCENA = 2f;


    [Header("—  Feedback  —")]
    #region Audio
    [SerializeField] AudioSource musicaAscens;

    [Space(10)]
    [SerializeField] AudioSource ding_sfx;
    [SerializeField] AudioSource porte_sfx;
    #endregion

    #region Animazioni
    [Space(10)]
    [SerializeField] Animator ascensAnim;
    #endregion

    #region Altro
    [Space(10)]
    [SerializeField] GameObject vanoPulsNormale;
    [SerializeField] GameObject vanoPulsRotto;
    #endregion




    private void Awake()
    {
        if (ascensoreDiArrivo)
        {
            StartCoroutine(AspettaInizioScena());
        }

        RompiVanoPulsanti(ascensoreDiArrivo);
    }


    void Update()
    {
        //Se posso cambiare la musica...
        if (cambiaMusica)
        {
            //Fa svanire la musica di sfondo
            mixerGenerale.SetFloat("musVol", opzSO.LeggiCurvaVolume().Evaluate(tempoPassato_VolumeMus));

            tempoPassato_VolumeMus -= Time.deltaTime / 5;
        }
        else
        {
            //Prende il volume (in percentuale) della musica
            tempoPassato_VolumeMus = opzSO.LeggiVolumeMusica_Percent();
        }
    }


    #region OnTrigger()

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            giocatoreEntrato = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            giocatoreEntrato = false;
    }

    #endregion


    #region Funzioni Ascensore

    public void ApriPorteAscensore()
    {
        ding_sfx.Play();
        porte_sfx.Play();
        ascensAnim.SetBool("Porte aperte", true);
    }
    public void ChiudiPorteAscensore()
    {
        porte_sfx.Play();
        ascensAnim.SetBool("Porte aperte", false);
    }

    public void AvviaAscensore()
    {
        if (giocatoreEntrato && !ascensoreDiArrivo)
        {
            //Feedback
            ding_sfx.Play();
            RompiVanoPulsanti(true);

            ChiudiPorteAscensore();


            //Toglie la possibilita' di mettere in pausa
            //e cambia il tipo di cambio scena
            ascensSO.ScriviPossoMettereInPausa(false);
            ascensSO.ScriviDaDoveCambioScena(CambioScena_Enum.DaAscensore);

            //Aspetta fino alla chiusura delle porte
            StartCoroutine(AspettaFinePorteCaricamentoScena());

            cambiaMusica = true;
        }
    }

    void RompiVanoPulsanti(bool rotto)
    {
        vanoPulsNormale.SetActive(!rotto);
        vanoPulsRotto.SetActive(rotto);
    }

    #endregion


    #region Coroutines

    IEnumerator AspettaFinePorteCaricamentoScena()
    {
        //Aspetta che finisce il rumore delle porte
        yield return new WaitForSeconds(porte_sfx.clip.length - SEC_TOGLIERE_MUSICA_ASCENS);

        //Riproduce la musica d'ascensore
        musicaAscens.Play();

        yield return new WaitForSeconds(SEC_ASPETTARE_DENTRO);

        //Carica la scena successiva dopo aver aspettato alcuni secondi
        opzSO.ScenaSuccessiva();
    }

    IEnumerator AspettaInizioScena()
    {
        yield return new WaitForSeconds(SEC_ASPETTARE_INIZIO_SCENA);

        ApriPorteAscensore();

        yield return new WaitForSeconds(ding_sfx.clip.length);

        //Fa tornare normale le impostazioni
        ascensSO.ScriviPossoMettereInPausa(true);
        opzSO.CambiaVolumeMusica(opzSO.LeggiVolumeMusica_Percent());

        StopAllCoroutines();
    }

    #endregion



    #region Funzioni Get custom

    public bool LeggiGiocatoreEntrato() => giocatoreEntrato;

    #endregion
}
