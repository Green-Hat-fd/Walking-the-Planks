using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SparoScript : MonoBehaviour, IFeedback
{
    ObjectPoolingScript poolingScr;

    [SerializeField] float maxDistSparo = 15f;
    
    [Min(0)]
    [SerializeField] float potenzaSparo = 10f;
    
    RaycastHit hitInfo;

    [Space(10)]
    [SerializeField] float secDaAspettareSparo = 0.65f;
    float tempoTrascorso_Sparo;

    [Header("—  Feedback  —")]
    #region Audio
    [SerializeField] AudioSource sparoSfx;
    [SerializeField] AudioClip[] sparoClip;
    #endregion

    #region Particelle
    //[SerializeField] ParticleSystem proiettPart;
    [SerializeField] string sparoSparkle_part_tag;
    [SerializeField] ParticleSystem sparoFuoco_part;
    #endregion

    #region Animazioni
    [SerializeField] Animator pistolaAnim;
    #endregion

    #region Colori mirino
    
    public enum ObjColpito_Enum
    {
        Niente,
        Bersaglio,
        Scatola
    }
    
    [Space(10)]
    [SerializeField] Image mirinoImg;
    
    [SerializeField] Color colore_sparoAVuoto;
    [SerializeField] Color colore_possoColpireQualcosa;
    [SerializeField] Color colore_colpitoScatola;
    [SerializeField] Color colore_colpitoBersaglio;

    ObjColpito_Enum ultimoObjColpito = ObjColpito_Enum.Niente;
    #endregion



    private void Awake()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();
    }

    void Update()
    {
        //"Spara" un raycast che simula lo sparo della pistola
        //(non colpisce i Trigger e "~0" significa che collide con tutti i layer)
        Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistSparo, ~0, QueryTriggerInteraction.Ignore);


        if(tempoTrascorso_Sparo >= secDaAspettareSparo)
        {
            //Controlla se ho premuto/tengo premuto il pulsante dello Sparo
            if (GameManager.inst.inputManager.Giocatore.Sparo.ReadValue<float>() > 0)
            {
                Feedback();


                if (ComparaTagRaycastHitInfo())   //Controlla cosa ho colpito
                {
                    switch (hitInfo.transform.tag)
                    {
                        #region Bersaglio

                        case "Gun-Target":

                            //Attiva gli oggetti specificati nel bersaglio
                            hitInfo.transform.GetComponent<BersaglioScript>().AttivaOggetti();

                            //Cambia il colore del mirino con quello del bersaglio
                            ultimoObjColpito = ObjColpito_Enum.Bersaglio;
                            break;

                        #endregion
                        

                        #region Scatola

                        case "Gun-Box":

                            Rigidbody hit_rb = hitInfo.transform.GetComponent<Rigidbody>();

                            //Usa una forza esplosiva nel punto colpito sulla scatola
                            hit_rb.AddExplosionForce(potenzaSparo * 10f, hitInfo.point, 2.5f);

                            #region OLD_Non usato
                            //Usa la forza riflessa nel punto colpito sulla scatola
                            /*hitInfo.transform.GetComponent<Rigidbody>().
                                            AddForceAtPosition(Vector3.Reflect(transform.forward * potenzaSparo,
                                                                               hitInfo.normal),
                                                               hitInfo.point,
                                                               ForceMode.Impulse);//*/
                            #endregion

                            //Cambia il colore del mirino con quello della scatola
                            ultimoObjColpito = ObjColpito_Enum.Scatola;
                            break;

                        #endregion


                        #region Pulsanti ascensore

                        case "Elev buttons":

                            //Disattiva i pulsanti
                            hitInfo.collider.enabled = false;

                            //Prende dall'oggetto principale (root object)
                            GameObject ascensObj = hitInfo.transform.parent.parent.gameObject;
                            AscensoreScript ascScr = ascensObj.GetComponent<AscensoreScript>();

                            //Avvia l'ascensore per andare in una nuova scena
                            ascScr.AvviaAscensore();


                            //Cambia il colore del mirino con quello del bersaglio
                            ultimoObjColpito = ObjColpito_Enum.Bersaglio;
                            break;

                        #endregion
                    }
                }

                tempoTrascorso_Sparo = 0;     //Resetta il timer
            }
        }
        else
        {
            tempoTrascorso_Sparo += Time.deltaTime;   //Aumenta il conteggio del tempo trascorso


            if (tempoTrascorso_Sparo >= secDaAspettareSparo / 2)
            {
                ultimoObjColpito = ObjColpito_Enum.Niente;
            }
        }


        #region Cambio colore del mirino

        switch (ultimoObjColpito)
        {
            //Colora il mirino di default se spara il vuoto
            //oppure
            //colora il mirino di "coloreColpito" puo' sparare a qualcosa
            case ObjColpito_Enum.Niente:

                mirinoImg.color = ComparaTagRaycastHitInfo() ? colore_possoColpireQualcosa : colore_sparoAVuoto;
                break;


            //Colora il mirino del colore della scatola
            //se si colpisce la scatola
            case ObjColpito_Enum.Scatola:

                mirinoImg.color = colore_colpitoScatola;
                break;


            //Colora il mirino del colore del bersaglio
            //se si colpisce il bersaglio o i pulsanti dell'ascensore
            case ObjColpito_Enum.Bersaglio:

                mirinoImg.color = colore_colpitoBersaglio;
                break;
        }

        #endregion
    }

    #region Funzioni Get custom

    public RaycastHit LeggiSparoRaycastHitInfo() => hitInfo;

    /// <summary>
    /// Questa funzione controlla se il raycast ha colpito qualcosa
    /// <br></br>e che esso non sia il giocatore
    /// </summary>
    /// <param name="tagDaComparare">Controlla se ha colpito un oggetto con questo tag</param>
    /// <returns>prova</returns>
    public bool ComparaTagRaycastHitInfo(string tagDaComparare)
    {
        return hitInfo.transform  //Se il raycast ha colpito qualcosa
               &&
               !hitInfo.transform.CompareTag("Player")  //Se non e' il giocatore
               &&
               hitInfo.transform.CompareTag(tagDaComparare);
    }
    public bool ComparaTagRaycastHitInfo()
    {
        return hitInfo.transform  //Se il raycast ha colpito qualcosa
               &&
               !hitInfo.transform.CompareTag("Player");  //Se non e' il giocatore
    }

    #endregion


    public void Feedback()
    {
        //Riproduce un suono a caso tra quelli dati
        int iSparo = Random.Range(0, sparoClip.Length);
        sparoSfx.PlayOneShot(sparoClip[iSparo]);

        //Fa vedere la linea lasciata dal proiettile
        //proiettPart.gameObject.SetActive(true);
        //proiettPart.Play();

        //Fa vedere il fuoco della pistola
        sparoFuoco_part.Play();

        //Se ha colpito qualcosa, fa le scentille
        if(hitInfo.collider && hitInfo.distance <= maxDistSparo)
        {
            GameObject sparoSpark_part
                            = poolingScr.PrendeOggettoDallaPool(sparoSparkle_part_tag,
                                                                transform.position,
                                                                Quaternion.identity);
            sparoSpark_part.transform.position = hitInfo.point;
            sparoSpark_part.transform.forward = hitInfo.normal;
        }

        //Anima la pistola
        pistolaAnim.SetTrigger("Pistola Sparo");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;

        //Disegna una linea dalla posizione di inizio alla fine
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * maxDistSparo));

        RaycastHit rHit;
        Physics.Raycast(transform.position, transform.forward, out rHit, maxDistSparo, ~0, QueryTriggerInteraction.Ignore);


        Gizmos.color = Color.green;
        if (rHit.normal != Vector3.zero)
        {
            //Disegna una linea dal punto di inizio fin dove ha colpito
            Gizmos.DrawLine(transform.position, rHit.point);

            //Disegna un piccolo cubo dove ha colpito
            Gizmos.DrawCube(rHit.point, Vector3.one * 0.1f);

        }
    }

}
