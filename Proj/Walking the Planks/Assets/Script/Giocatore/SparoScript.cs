using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SparoScript : MonoBehaviour
{
    [SerializeField] float maxDistSparo = 15f;
    
    RaycastHit hitInfo;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (GameManager.inst.inputManager.Giocatore.Sparo.triggered)
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxDistSparo);
    }

    void Update()
    {
        if (GameManager.inst.inputManager.Giocatore.Sparo.triggered)
        {
            Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistSparo);

            // DEBUG
            if (ComparaTagRaycastHitInfo())
                //print(hitInfo.transform.name);
                switch (hitInfo.transform.tag)
                {
                    case "Gun-Target":
                        hitInfo.transform.GetComponent<BersaglioScript>().AttivaOggetti();
                        break;
                }
        }
    }

    public RaycastHit LeggiSparoRaycastHitInfo()
    {
        return hitInfo;
    }

    public bool ComparaTagRaycastHitInfo(string tagDaComparare)
    {
        return hitInfo.transform  //Se il raycast ha colpito qualcosa
               &&
               !hitInfo.transform.CompareTag("Player")  //Se non è il giocatore
               &&
               hitInfo.transform.CompareTag(tagDaComparare);
    }
    public bool ComparaTagRaycastHitInfo()
    {
        return hitInfo.transform  //Se il raycast ha colpito qualcosa
               &&
               !hitInfo.transform.CompareTag("Player");  //Se non è il giocatore
    }
}
