using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovGiocRb : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float velGiocat;
    [SerializeField] float altezzaSalto;

    bool siTrovaATerra = false;
    Vector3 velCaduta;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        //Prende gli assi dall'input di movimento
        float movimX = GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().x;
        float movimZ = GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().y;


        Vector3 muovi = transform.right * movimX + transform.forward * movimZ;        //Vettore movimento orizzontale





        //Salta se premi Spazio e si trova a terra
        if (siTrovaATerra && GameManager.inst.inputManager.Giocatore.Salto.triggered)
        {
            rb.AddForce(Vector3.up * altezzaSalto, ForceMode.Impulse);
        }


        //Movimento in sè
        rb.AddForce(muovi * velGiocat, ForceMode.Force);
    }
}
