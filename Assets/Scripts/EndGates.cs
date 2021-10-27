using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGates : MonoBehaviour
{
    Escaper escaper;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Escaper>(out escaper))
        {
            ScreensHandler.Instance.ShowLoseScreen();
        }
    }
}
