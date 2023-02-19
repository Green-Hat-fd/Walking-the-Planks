using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoGiocat : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] float velGiocat;
    [SerializeField] float altezzaSalto;

    //Controllo terreno
    [SerializeField] Transform controllatoreTerr;
    [SerializeField] float minDistanzaTerr = .4f;
    [SerializeField] LayerMask mascheraLivello;
    
    bool siTrovaATerra = false;
    Vector3 velCaduta;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controllatoreTerr.position - Vector3.up * (minDistanzaTerr / 2), new Vector3(.75f, minDistanzaTerr, .75f));
    }

    void Update()
    {
        #region Controlla se si trova a terra (con un cubo)

        //La CheckBox()
        siTrovaATerra = 
                   Physics.CheckBox(controllatoreTerr.position - Vector3.up * (minDistanzaTerr/2),   //Posizione del Box
                                    new Vector3(2/3, minDistanzaTerr, 2/3),      //Dimensioni del Box
                                    Quaternion.identity,
                                    mascheraLivello,
                                    QueryTriggerInteraction.Ignore);    //Ignorando tutti i trigger
            
        #endregion


        #region Resetta la gravità accumulata

        if (siTrovaATerra && velCaduta.y < 0)
        {
            velCaduta.y = -2f;
        }

        #endregion


        //Prende gli assi dall'input di movimento
        float movimX = GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().x;
        float movimZ = GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().y;



        Vector3 muovi = transform.right * movimX + transform.forward * movimZ;        //Vettore movimento orizzontale


        #region Salta se premi Spazio e si trova a terra
        
        if (siTrovaATerra && GameManager.inst.inputManager.Giocatore.Salto.triggered)
        {
            velCaduta.y = Mathf.Sqrt(altezzaSalto * -2f * Physics.gravity.y);  //Equazione salto: radq(h*-2*g)
        }
        #endregion

        //Controllo gravità
        velCaduta.y += Physics.gravity.y * Time.deltaTime;


        #region Movimento in sè

        controller.Move(muovi * velGiocat * Time.deltaTime);
        controller.Move(velCaduta * Time.deltaTime);

        #endregion
    }
}
