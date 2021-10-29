using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenMergeCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        WardenBase warden;
        if (other.TryGetComponent<WardenBase>(out warden))
        {
            var thisWarden = transform.parent.gameObject.GetComponent<WardenBase>();
            var otherWarden = other.gameObject.GetComponent<WardenBase>();

            if (warden.canMerge == false)
                return;

            if ((thisWarden.wardenStrenght != otherWarden.wardenStrenght) || (thisWarden.wardenStrenght == 2 && otherWarden.wardenStrenght == 2))
                return;

            ZoneManager.Instance.RemoveWardenFromList(thisWarden);
            ZoneManager.Instance.RemoveWardenFromList(otherWarden);
            WardenSpawner.Instance.SpawnWardenOnMerge(transform.position, thisWarden.wardenStrenght + 1);
            warden.canMerge = false;
            warden.canMergeParticles[0].SetActive(false);
            warden.canMergeParticles[1].SetActive(false);
            Destroy(other.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}