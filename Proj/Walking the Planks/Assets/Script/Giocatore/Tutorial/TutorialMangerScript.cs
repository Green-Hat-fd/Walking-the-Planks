using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialMangerScript : MonoBehaviour
{
    [SerializeField] Animator tutorialAnim;

    [SerializeField] Image icona;
    [SerializeField] TMP_Text testoComp;
    Sprite sprite_Tut;
    string tagDaCercare_Tut = " ";
    Tutorial_Enum cosaAspettare_Tut;
    float secDaAspettare_Tut;

    [Space(20)]
    [SerializeField] OpzioniSO_Script opzioniSO;

    [Header("Lingue (Scriptable Obj.)")]
    [SerializeField] LinguaSO_Script inglese;
    [SerializeField] LinguaSO_Script italiano;

    LinguaSO_Script linguaTestiSelezSO = default;




    private void Awake()
    {
        italiano.SpostaTestiNelDictionary();
        inglese.SpostaTestiNelDictionary();
    }

    void Update()
    {
        CambiaLinguaScelta();

        //Prende il testo dalla lingua scelta (se la variabile NON e' vuota)
        //e lo cambia continuamente nella componente TMPro
        testoComp.text = linguaTestiSelezSO.LeggiTesti(tagDaCercare_Tut);

        //Cambia continuamente l'immagine
        icona.sprite = sprite_Tut;
    }

    void CambiaLinguaScelta()
    {
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
    }


    public void AvviaTutorial()
    {
        StopAllCoroutines();
        StartCoroutine(ApriEChiudiTutorial());
    }

    //Mostra il tutorial,
    //e dopo tot secondi, lo nasconde
    IEnumerator ApriEChiudiTutorial()
    {
        MostraTutorial();

        //Prende il trigger del Giocatore
        InputManager.GiocatoreActions giocatAct = GameManager.inst.inputManager.Giocatore;
        
        //Sceglie cosa aspettare in base a quello scelto nell'area Tutorial
        switch (cosaAspettare_Tut)
        {
            #region Aspetta il salto

            case Tutorial_Enum.AspettaSalto:
                yield return new WaitUntil(() => giocatAct.Salto.triggered);
                yield return new WaitForSeconds(1f);
                break;
            #endregion

            #region Aspetta che il giocatore spari

            case Tutorial_Enum.AspettaSparo:
                yield return new WaitUntil(() => giocatAct.Sparo.triggered);
                yield return new WaitForSeconds(1f);
                break;
            #endregion

            #region Aspetta che il Rum venga bevuto

            case Tutorial_Enum.AspettaRumBevuto:
                yield return new WaitUntil(() => giocatAct.UsoRum.triggered);
                yield return new WaitForSeconds(1f);
                break;
            #endregion

            #region Aspetta tot secondi
            
            default:
            case Tutorial_Enum.AspettaSecondi:
                yield return new WaitForSeconds(secDaAspettare_Tut);
                break;
            #endregion

        }

        NascondiTutorial();
        StopAllCoroutines();
    }


    void MostraTutorial()
    {
        //Lo fa rimbalzare solo se era gia' attivo
        if(tutorialAnim.GetBool("Aperto"))
            tutorialAnim.SetTrigger("Rimbalzo");
        
        tutorialAnim.SetBool("Aperto", true);
    }
    void NascondiTutorial()
    {
        tutorialAnim.SetBool("Aperto", false);
    }


    public void PassaImmagine(Sprite img)
    {
        sprite_Tut = img;
    }
    public void PassaTagTesto(string tag_testo)
    {
        tagDaCercare_Tut = tag_testo;
    }
    public void PassaCosaAspettare(Tutorial_Enum cosaAspett)
    {
        cosaAspettare_Tut = cosaAspett;
    }
    public void PassaSecondiDaAspettare(float sec)
    {
        secDaAspettare_Tut = sec;
    }
}
