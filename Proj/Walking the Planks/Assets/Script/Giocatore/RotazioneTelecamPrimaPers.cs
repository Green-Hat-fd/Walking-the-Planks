using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotazioneTelecamPrimaPers : MonoBehaviour
{
    [SerializeField] OpzioniSO_Script opzioni_SO;

    [Space(15)]
    [SerializeField] float velRotazione = 10;

    [SerializeField] Transform corpoGiocat;

    float xRotaz = 0f;

    bool mouseAlCentro = true;


    void Update()
    {
        //Blocca (o no) il mouse al centro
        Cursor.lockState = mouseAlCentro
                           ?
                          CursorLockMode.Locked
                           :
                          CursorLockMode.None ;

        //Prende la rotazione
        InputAction inputRotazione = GameManager.inst.inputManager.Giocatore.RotazioneVista;


        //Prende le (X,Y) del mouse
        float mouseX = inputRotazione.ReadValue<Vector2>().x * velRotazione * Time.deltaTime;
        float mouseY = inputRotazione.ReadValue<Vector2>().y * velRotazione * Time.deltaTime;


        //Movimento mouse * sensibilita' (dalle impost.)
        mouseX *= opzioni_SO.LeggiSensibilita();
        mouseY *= opzioni_SO.LeggiSensibilita();


        #region Gamepad

        if(Gamepad.all.Count > 0)   //Se c'e' almeno un Gamepad
        {
            string nomeInput = inputRotazione.activeControl.name;
            string nomeGamepadStickDx = Gamepad.current.rightStick.name;

            //Aumenta la sensibilita' se si usa un controller/gamepad
            if (nomeInput == nomeGamepadStickDx)
            {
                mouseX *= 10;
                mouseY *= 10;
            }
        }
        #endregion


        xRotaz -= mouseY;
        xRotaz = Mathf.Clamp(xRotaz, -90f, 90f);        //Restrige la rotazione tra su (-90°) e giu' (90°)

        transform.localRotation = Quaternion.Euler(xRotaz, 0f, 0f);         //La Y la porta come rotazione X della camera...
        corpoGiocat.Rotate(Vector3.up * mouseX);           //...e la X come rotazione Y del giocatore
    }

    public void CambiaMouseAlCentro(bool value)
    {
        mouseAlCentro = value;
    }
}
