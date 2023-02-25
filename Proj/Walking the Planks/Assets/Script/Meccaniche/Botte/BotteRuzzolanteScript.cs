using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotteRuzzolanteScript : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float potenza = 15;
    [Range(0, 10)]
    [SerializeField] float maxVelocitaBotte = 5f;

    bool giocatSalito;

    float movimZ;
    [SerializeField, Range(-1, 1)] float angoloGiocat;

    GameObject DEBUG_OBJ;

    void Start()
    {
        giocatSalito = true;  //TODO: toglimi
        DEBUG_OBJ = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Negativo -> per andare nella direzione opposta al giocatore
        movimZ = -GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().y;


        //Calcolo della velocita' rispetto all'angolazione del giocatore
        //(piu' guarda davanti, piu' veloce va;
        // piu' guarda di lato, meno veloce va)

        from = DEBUG_OBJ.transform.forward;
        axis = transform.forward;

        //if (Vector3.Angle(from, transform.right) > 90)
        //{
        //    to = -transform.right;
        //}
        //else
        {
            to = transform.right;
        }
        //TODO: sistemare l'angolo di modo che -> se guarda in avanti = 1,
        //                                        se guarda indietro = -1
        //                                        e se guarda di lato (dx o sx) = 0

        angoloGiocat = Vector3.SignedAngle(from, to, axis);

        angoloGiocat /= 90;  //Rende il numero tra 0 e 1
    }

    Vector3 Vect3_Abs(Vector3 v3)
    {
        return new Vector3(Mathf.Abs(v3.x),
                           v3.y,
                           Mathf.Abs(v3.z));;
    }

    private void FixedUpdate()
    {
        if(giocatSalito && movimZ != 0)
        {
            rb.AddForce((transform.forward  * potenza) * movimZ /** angoloGiocat*/);
        }

        //Limita la velocita' della botte se eccede(supera) il limite di velocita'
        if (rb.velocity.z > maxVelocitaBotte)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocitaBotte);
    }

    Vector3 from, to, axis;

    private void OnDrawGizmos()
    {
                //TODO: togliere tutta questa funzione
        Vector3 posizInizio = new Vector3(0, 4f);


        //Direzione da dove prendere l'angolo
        Gizmos.color = Color.white;
        Gizmos.DrawLine(posizInizio, posizInizio + to*2);
        //Asse per SignedAngle()
        Gizmos.color = new Color(0.75f, 0.75f, 0.75f);
        Gizmos.DrawWireCube(posizInizio, axis*2);
        //Davanti del giocat.
        Gizmos.color = Color.red;
        Gizmos.DrawLine(posizInizio, posizInizio + from*2);
        //Segna l'angolo
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(posizInizio + to*.5f, posizInizio + from*.5f);
    }
}
