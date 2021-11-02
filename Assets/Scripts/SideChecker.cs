using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideChecker : MonoBehaviour
{
    public WardenBase warden;
    public GameZone gameZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WardenBase>(out warden))
        {
            warden.gameZone = gameZone;
            warden.SetTarget();
        }
    }
}