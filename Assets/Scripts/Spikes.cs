using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spikes : MonoBehaviour
{
    public float spikeDamage = 1;
    public float disableDelay = 5;
    public float startY = -3f;

    public List<EscaperBase> escapersOnSpikes = new List<EscaperBase>();

    private void OnDisable()
    {
        escapersOnSpikes.Clear();
    }

    private void OnEnable()
    {
        transform.DOMoveY(-2.73f, 2);
        StartCoroutine(DamageEscapers());
        StartCoroutine(DisableAfterDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        EscaperBase newEscaper;
        if (other.TryGetComponent<EscaperBase>(out newEscaper))
        {
            escapersOnSpikes.Add(newEscaper);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EscaperBase newEscaper;
        if (other.TryGetComponent<EscaperBase>(out newEscaper))
        {
            escapersOnSpikes.Remove(newEscaper);
        }
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(disableDelay);
        transform.DOMoveY(startY, 2);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    private IEnumerator DamageEscapers()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < escapersOnSpikes.Count; i++)
            {
                escapersOnSpikes[i].DescreaseHealth(spikeDamage);
            }
        }
    }
}