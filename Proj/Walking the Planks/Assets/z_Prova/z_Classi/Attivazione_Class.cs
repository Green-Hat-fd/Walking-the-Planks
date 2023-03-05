using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attivazione_Class : MonoBehaviour
{
    bool isAttivo;

    public bool GetAttivo()
    {
        return isAttivo;
    }
    public void SetAttivo(bool valore)
    {
        isAttivo = valore;
    }
    public void InvertiAttivo()
    {
        isAttivo = !isAttivo;
    }
}
