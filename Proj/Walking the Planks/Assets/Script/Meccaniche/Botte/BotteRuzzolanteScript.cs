using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotteRuzzolanteScript : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float potenza = 15;
    [Range(0, 10)]
    [SerializeField] float maxVelocitaBotte = 5f;
    Vector3 davantiIniziale, destraIniziale;

    Vector3 inizioAngolo, fineAngolo, assePositNegatAngolo;

    bool giocatSalito;

    float movimZ;
    float angoloGiocat;

    [Space(20)]
    [SerializeField] bool voglioIlDebug;
    #region Tooltip()
    [Tooltip("(Funziona solo se \"VoglioIlDebug\" è attiva)\nVero = Il debug sarà sulla testa del giocatore \nFalso = Il debug sarà al centro del mondo")]
    #endregion
    [InspectorName("Debug sopra il giocatore")]
    [SerializeField] bool debugSopraGiocat;

    GameObject DEBUG_OBJ;


    void Start()
    {
        giocatSalito = true;  //TODO: toglimi
        DEBUG_OBJ = GameObject.FindGameObjectWithTag("Player");

        davantiIniziale = transform.forward;
        destraIniziale = transform.right;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Negativo -> per andare nella direzione opposta al giocatore
        movimZ = -GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().y;

        inizioAngolo = DEBUG_OBJ.transform.forward;  //TODO: togli questo e metti quello del giocatore

        #region Calcolo angolo giocatore-botte

        //Capovolge l'angolo e l'asse se il giocatore guarda nell'altra meta',
        //ovvero mantiene sempre l'angolo tra 90° e -90°
        if (Vector3.Angle(inizioAngolo, destraIniziale) > 90)
        {
            fineAngolo = -destraIniziale;
            assePositNegatAngolo = -davantiIniziale;  //TODO: trova il modo per avere sempre la  iniziale/non cambiata
        }
        else
        {
            fineAngolo = destraIniziale;
            assePositNegatAngolo = davantiIniziale;
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
            rb.AddForce((transform.forward  * potenza) * movimZ * angoloGiocat);
        }

        //Limita la velocita' della botte se eccede(supera) il limite di velocita'
        if (rb.velocity.z > maxVelocitaBotte)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocitaBotte);
    }

    private void OnDrawGizmos()
    {
        if (voglioIlDebug && DEBUG_OBJ)
        {
            //Seguira' il giocatore se la variabile e' vera
            Vector3 posizInizio = debugSopraGiocat
                                   ?
                                  DEBUG_OBJ.transform.position + Vector3.up * 4f
                                   :
                                  new Vector3(0, 4f);


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
