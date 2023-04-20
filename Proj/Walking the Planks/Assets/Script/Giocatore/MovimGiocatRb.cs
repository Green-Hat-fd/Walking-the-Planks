using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RumScript))]
public class MovimGiocatRb : MonoBehaviour
{
    Rigidbody rb;
    RumScript rumScr;

    [SerializeField] float velGiocat = 7.5f;
    [SerializeField] float potenzaSalto = 8.5f;
    float movimX, movimZ;
    #region OLD_Non usato
    //[Space(10)]
    //[SerializeField] float attritoTerr = 4;
    //[SerializeField] float attritoAria = 0;
    #endregion
    
    Vector3 muovi;
    float _pot_salto;

    [Space(20)]
    [SerializeField] float sogliaRilevaTerreno = 0.25f;
    float mezzaAltezzaGiocat;
    float raggioSpherecast = 0.5f;
    //Vector3 dimensBoxcast = new Vector3(0.51f, 0f, 0.51f);

    RaycastHit pendenzaHit;

    bool siTrovaATerra = false;

    bool hoSaltato = false;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rumScr = GetComponent<RumScript>();
        rb.freezeRotation = true;

        mezzaAltezzaGiocat = GetComponent<CapsuleCollider>().height / 2;
    }

    private void Update()
    {
        //Prende gli assi dall'input di movimento
        movimX = GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().x;
        movimZ = GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().y;

        muovi = transform.forward * movimZ + transform.right * movimX;      //Vettore movimento orizzontale


        #region Controlla se e' stato bevuto il Rum

        if (rumScr.SonoAttivo())
        {
            //Applica gli effetti di velocita' e salto aumentati
            muovi *= rumScr.LeggiMoltipVelGiocat();
            _pot_salto = potenzaSalto * rumScr.LeggiMoltipSaltoGiocat();
        }
        else
        {
            //Rimuove gli effetti
            muovi *= 1;
            _pot_salto = potenzaSalto;
        } 
        #endregion


        //Cambia l'attrito se si trova o a terra in aria
        //rb.drag = siTrovaATerra ? attritoTerr : attritoAria;


        //Prende l'input di salto
        hoSaltato = GameManager.inst.inputManager.Giocatore.Salto.ReadValue<float>() > 0;

    }

    RaycastHit hitBase;
    void FixedUpdate()
    {
        //Calcolo se si trova a terra
        siTrovaATerra = Physics.SphereCast(transform.position, raggioSpherecast, -transform.up, out hitBase, mezzaAltezzaGiocat + sogliaRilevaTerreno - raggioSpherecast);
        #region OLD
        //siTrovaATerra = Physics.BoxCast(transform.position, dimensBoxcast, -transform.up, out hitBase, Quaternion.identity, mezzaAltezzaGiocat + sogliaRilevaTerreno); 
        #endregion


        float moltVelAria = !siTrovaATerra ? 0.5f : 1;   //Dimezza la velocita' orizz. se si trova in aria


        //Salta se premi Spazio e si trova a terra
        if (hoSaltato && siTrovaATerra)
        {
            //Resetta la velocita' Y e applica la forza d'impulso verso l'alto
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * _pot_salto, ForceMode.Impulse);
        }


        //Movimento orizzontale (semplice) del giocatore
        rb.AddForce(muovi.normalized * velGiocat * moltVelAria * 10f, ForceMode.Force);


        #region Limitazione della velocita'

        //Prende la velocita' orizzontale del giocatore
        Vector3 velOrizz = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //Controllo se si accelera troppo, cioe' si supera la velocita'
        if (velOrizz.magnitude >= velGiocat)
        {
            //Limita la velocita' a quella prestabilita, riportandola al RigidBody
            Vector3 limitazione = velOrizz.normalized * velGiocat;
            rb.velocity = new Vector3(limitazione.x, rb.velocity.y, limitazione.z);
        }

        #endregion


        #region Controllo in pendenza

        //

        #endregion
    }

    bool InPendenza()
    {
        if(Physics.Raycast(transform.position, -transform.up, out pendenzaHit, mezzaAltezzaGiocat + sogliaRilevaTerreno))
        {
            //Prende l'angolo della pendenza
            //(tra il vett. "sotto" e la normale del terreno)
            float angolo = Vector3.Angle(transform.up, pendenzaHit.normal);
            
            return angolo != 0;
        }

        return false;   //Nel caso non colpisce nulla
    }

    private void OnDrawGizmos()
    {
        //Disegna il BoxCast per capire se e' a terra o meno (togliendo l'altezza del giocatore)
        Gizmos.color = new Color(0.85f, 0.85f, 0.85f, 1);
        Gizmos.DrawWireSphere(transform.position + (-transform.up * mezzaAltezzaGiocat)
                               + (-transform.up * sogliaRilevaTerreno)/2
                               - (-transform.up * raggioSpherecast),
                              raggioSpherecast);
        #region OLD
        //Gizmos.DrawWireCube(transform.position + (-transform.up * mezzaAltezzaGiocat) + (-transform.up * sogliaRilevaTerreno) / 2,
        //                    dimensBoxcast * 2 + (-transform.up * sogliaRilevaTerreno));
        #endregion

        //Disegna dove ha colpito se e' a terra e se ha colpito un'oggetto solido (no trigger)
        Gizmos.color = Color.green;
        if(siTrovaATerra && hitBase.collider)
        {
            Gizmos.DrawLine(hitBase.point + (transform.up*hitBase.distance), hitBase.point);
            Gizmos.DrawCube(hitBase.point, Vector3.one * 0.1f);
        }
    }
}
