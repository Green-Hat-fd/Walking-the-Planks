using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class provaInput : MonoBehaviour
{
    void Update()
    {
        if (GameManager.inst.inputManager.Giocatore.Salto.triggered)
            print(GameManager.inst.inputManager.Giocatore.Sparo.ReadValueAsObject());

        if (GameManager.inst.inputManager.Giocatore.Sparo.triggered)
            print("Pew!");

        if (GameManager.inst.inputManager.Generali.Pausa.triggered)
            print("Mette in pausa!");
    }
}
