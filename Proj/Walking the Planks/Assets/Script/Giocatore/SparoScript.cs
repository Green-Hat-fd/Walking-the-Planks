using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SparoScript : MonoBehaviour
{
    [SerializeField] float maxDistSparo = 15f;
    
    [Space(10)]
    [Min(0)]
    [SerializeField] float potenzaSparo = 10f;
    
    RaycastHit hitInfo;



    void Update()
    {
        if (GameManager.inst.inputManager.Giocatore.Sparo.triggered)
        {
            //"Spara" un raycast che simula lo sparo della pistola
            Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistSparo);
            
            if (ComparaTagRaycastHitInfo())
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
        }
    }

    #region Funzioni Get custom

    public RaycastHit LeggiSparoRaycastHitInfo()
    {
        return hitInfo;
    }

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
}
