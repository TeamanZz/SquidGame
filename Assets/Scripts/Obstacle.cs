using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    public float disableDelay = 5;
    public float startY = -0.4f;

    private void OnEnable()
    {
        transform.DOMoveY(0.02f, 2);
        StartCoroutine(DisableAfterDelay());
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(disableDelay);
        transform.DOMoveY(startY, 2);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}