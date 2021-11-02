using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CenterWall : MonoBehaviour
{
    public float resetDelay = 5;
    private Vector3 defaultPosition;

    private void Awake()
    {
        defaultPosition = transform.position;
    }

    public void RemoveWall()
    {
        // DOTween.To(() => transform.position.x, x => transform.position.x = x, newValue, 0.5f).SetUpdate(true);
        transform.DOMoveY(-0.35f, 2);
        //  position. = new Vector3(transform.position.x, -10, transform.position.z);
        StartCoroutine(IEResetPositionAfterDelay());
    }

    private IEnumerator IEResetPositionAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        transform.DOMoveY(defaultPosition.y, 2);
        // transform.position = defaultPosition;
        SpellsHandler.Instance.wallRemoved = false;
    }
}