using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SparoScript : MonoBehaviour, IFeedback
{
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
    [SerializeField] ParticleSystem sparoSparkle_part;
    [SerializeField] ParticleSystem sparoFuoco_part; 
    #endregion



    void Update()
    {
        if(tempoTrascorso_Sparo >= secDaAspettareSparo)
        {
            //Controlla se ho premuto/tengo premuto il pulsante dello Sparo
            if (GameManager.inst.inputManager.Giocatore.Sparo.ReadValue<float>() > 0)
            {
                //"Spara" un raycast che simula lo sparo della pistola
                //(non colpisce i Trigger e "~0" significa che collide con tutti i layer)
                Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistSparo, ~0, QueryTriggerInteraction.Ignore);


                Feedback();


                if (ComparaTagRaycastHitInfo())   //Controlla cosa ho colpito
                {
                    switch (hitInfo.transform.tag)
                    {
                        #region Obiettivo

                        case "Gun-Target":

                            //Attiva gli oggetti specificati nel bersaglio
                            hitInfo.transform.GetComponent<BersaglioScript>().AttivaOggetti();
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
        }
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
            GameObject sparoSpark_part = Instantiate(sparoSparkle_part.gameObject);
            sparoSpark_part.transform.position = hitInfo.point;
            sparoSpark_part.transform.forward = hitInfo.normal;
        }
    }
}
