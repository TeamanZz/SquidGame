using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideChecker : MonoBehaviour
{
    public Warden warden;
    public GameZone gameZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Warden>(out warden))
        {
            warden.gameZone = gameZone;
            warden.SetTarget();
            warden.canShoot = true;
        }
    }
}
