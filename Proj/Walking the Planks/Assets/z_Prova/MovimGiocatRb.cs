using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimGiocatRb : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float velGiocat = 10f;
    [SerializeField] float potenzaSalto = 7.5f;
    [Space(10)]
    [SerializeField] float attritoTerr = 5f;
    [SerializeField] float attritoAria = 1f;
    float movimX, movimZ;
    
    Vector3 muovi;

    [Space(20)]
    [SerializeField] float sogliaRilevaTerreno = 0.25f;
    float mezzaAltezzaGiocat;
    Vector3 dimensBoxcast = new Vector3(0.65f, 0f, 0.65f);
    
    bool siTrovaATerra = false;

    bool saltoAvvenuto = false;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        mezzaAltezzaGiocat = GetComponent<CapsuleCollider>().height / 2;
    }

    private void Update()
    {
        //Prende gli assi dall'input di movimento
        movimX = GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().x;
        movimZ = GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().y;

        muovi = transform.forward * movimZ + transform.right * movimX;      //Vettore movimento orizzontale


        //Cambia l'attrito se si trova in aria
        rb.drag = siTrovaATerra ? attritoTerr : attritoAria;


        //Prende l'input di salto
        saltoAvvenuto = GameManager.inst.inputManager.Giocatore.Salto.triggered;
    }


    void FixedUpdate()
    {
        //Calcolo se si trova a terra
        siTrovaATerra = Physics.BoxCast(transform.position, dimensBoxcast, -transform.up, Quaternion.identity, mezzaAltezzaGiocat + sogliaRilevaTerreno);


        //Salta se premi Spazio e si trova a terra
        if (saltoAvvenuto && siTrovaATerra)
        {
            //Resetta la velocita' Y e applica la forza d'impulso verso l'alto
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * potenzaSalto, ForceMode.Impulse);
        }


        //Movimento orizzontale del giocatore
        if(!saltoAvvenuto)
            rb.AddForce(muovi.normalized * velGiocat * 10f, ForceMode.Force);


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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position+(-transform.up * mezzaAltezzaGiocat) + (-transform.up * sogliaRilevaTerreno), dimensBoxcast + (-transform.up * mezzaAltezzaGiocat) + (-transform.up * sogliaRilevaTerreno));
    }
}
