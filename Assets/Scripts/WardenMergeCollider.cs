using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenMergeCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        WardenBase anyWarden;
        if (other.TryGetComponent<WardenBase>(out anyWarden))
        {
            var thisWarden = transform.parent.gameObject.GetComponent<WardenBase>();

            if (anyWarden.canMerge == false || thisWarden.canMerge == false)
                return;

            if ((thisWarden.wardenStrenght != anyWarden.wardenStrenght) || (thisWarden.wardenStrenght == 2 && anyWarden.wardenStrenght == 2))
                return;

            ZoneManager.Instance.RemoveWardenFromList(anyWarden);

            WardenRifle wardenRifle;
            WardenShotgun wardenShotgun;
            WardenBazooka wardenBazooka;

            if (thisWarden.TryGetComponent<WardenRifle>(out wardenRifle))
            {
                WardenLevelUpsHandler.Instance.LvlUpWarden(wardenRifle);
            }

            if (thisWarden.TryGetComponent<WardenBazooka>(out wardenBazooka))
            {
                WardenLevelUpsHandler.Instance.LvlUpWarden(wardenBazooka);
            }

            if (thisWarden.TryGetComponent<WardenShotgun>(out wardenShotgun))
            {
                WardenLevelUpsHandler.Instance.LvlUpWarden(wardenShotgun);
            }

            thisWarden.wardenStrenght++;
            thisWarden.expValue = 0;
            thisWarden.expBarImage.fillAmount = 0;

            thisWarden.canMerge = false;
            thisWarden.expAnimator.SetBool("IsFull", false);
            thisWarden.canMergeParticles[0].SetActive(false);
            thisWarden.canMergeParticles[1].SetActive(false);
            Destroy(other.gameObject);
        }
    }
}