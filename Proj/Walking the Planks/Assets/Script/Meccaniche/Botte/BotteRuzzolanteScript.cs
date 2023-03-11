using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotteRuzzolanteScript : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float potenza = 15;
    [Range(0, 10)]
    [SerializeField] float maxVelocitaBotte = 5f;
    Vector3 sopraIniziale, destraIniziale, davantiIniziale;

    Vector3 inizioAngolo, fineAngolo, assePositNegatAngolo;

    GameObject giocat_Obj;
    bool giocatSalito;

    float movimZ;
    float angoloGiocat;

    [Space(20)]
    [SerializeField] bool voglioIlDebug;
    #region Tooltip()
    [Tooltip("(Funziona solo se \"VoglioIlDebug\" è attiva)\nVero = Il debug sarà sulla testa del giocatore \nFalso = Il debug sarà al centro del mondo")]
    #endregion
    [SerializeField] bool debugSopraGiocat;


    void Start()
    {
        sopraIniziale = transform.up;
        davantiIniziale = transform.forward;
        destraIniziale = transform.right;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Negativo -> per andare nella direzione opposta al giocatore
        movimZ = -GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().y;

        if (giocat_Obj)
            inizioAngolo = giocat_Obj.transform.forward;


        #region Calcolo angolo giocatore-botte

        //Capovolge l'angolo e l'asse se il giocatore guarda nell'altra meta',
        //ovvero mantiene sempre l'angolo tra 90° e -90°
        if (Vector3.Angle(inizioAngolo, destraIniziale) > 90)
        {
            fineAngolo = -destraIniziale;
            assePositNegatAngolo = -sopraIniziale;
        }
        else
        {
            fineAngolo = destraIniziale;
            assePositNegatAngolo = sopraIniziale;
        }

        //Calcolo della velocita' rispetto all'angolazione del giocatore
        //(piu' guarda davanti, piu' veloce va;
        // piu' guarda di lato, meno veloce va)
        angoloGiocat = Vector3.SignedAngle(inizioAngolo, fineAngolo, assePositNegatAngolo);

        //Rende il numero tra 0 e 1
        angoloGiocat /= 90;

        #endregion
    }

    private void FixedUpdate()
    {
        if(giocatSalito && movimZ != 0)
        {
            rb.AddForce((davantiIniziale  * potenza) * movimZ * angoloGiocat);  //Movimento della botte rispetto al giocat.
        }

        //Limita la velocita' della botte se eccede(supera) la velocita' max
        if (rb.velocity.z > maxVelocitaBotte || rb.velocity.z < maxVelocitaBotte)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocitaBotte);
    }

    #region Funzioni Set custom

    public void ScriviGiocat_Obj(GameObject obj)
    {
        giocat_Obj = obj;
    }
    public GameObject LeggiGiocat_Obj()
    {
        return giocat_Obj;
    }
    public void ScriviGiocatSalito(bool b)
    {
        giocatSalito = b;
    }

    #endregion

    private void OnDrawGizmos()
    {
        if (voglioIlDebug && giocat_Obj)
        {
            //Seguira' il giocatore se la variabile e' vera
            Vector3 posizInizio = debugSopraGiocat
                                   ?
                                  giocat_Obj.transform.position + Vector3.up * 2f
                                   :
                                  transform.position + new Vector3(0, 4f);


            //Direzione da dove prendere l'angolo
            Gizmos.color = Color.red;
            Gizmos.DrawLine(posizInizio, posizInizio + fineAngolo*2);
            
            //Asse per SignedAngle()
            Gizmos.color = Color.gray;
            
            //Gizmos.DrawWireCube(posizInizio, new Vector3(Mathf.Abs(axis.x), Mathf.Abs(axis.y), Mathf.Abs(axis.z))*2);
            Gizmos.DrawLine(posizInizio, posizInizio + assePositNegatAngolo);
            
            //Davanti del giocat.
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(posizInizio, posizInizio + inizioAngolo*2);
            
            //Segna l'angolo
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(posizInizio + fineAngolo*.5f, posizInizio + inizioAngolo*.5f);
        }
    }
}
