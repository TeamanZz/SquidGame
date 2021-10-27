using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenMergeCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Warden warden;
        if (other.TryGetComponent<Warden>(out warden))
        {
            var thisWarden = transform.parent.gameObject.GetComponent<Warden>();
            var otherWarden = other.gameObject.GetComponent<Warden>();

            if (warden.canMerge == false)
                return;

            if ((thisWarden.wardenStrenght != otherWarden.wardenStrenght) || (thisWarden.wardenStrenght == 2 && otherWarden.wardenStrenght == 2))
                return;

            ZoneManager.Instance.RemoveWardenFromList(thisWarden);
            ZoneManager.Instance.RemoveWardenFromList(otherWarden);
            WardenSpawner.Instance.SpawnWardenOnMerge(transform.position, thisWarden.wardenStrenght + 1);
            warden.canMerge = false;
            warden.canMergeParticles.SetActive(false);
            Destroy(other.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}