using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGates : MonoBehaviour
{
    EscaperBase escaper;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EscaperBase>(out escaper))
        {
            ScreensHandler.Instance.ShowLoseScreen();
        }
    }
}
