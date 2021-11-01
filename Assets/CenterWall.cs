using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        transform.position = new Vector3(transform.position.x, -10, transform.position.z);
        StartCoroutine(IEResetPositionAfterDelay());
    }

    private IEnumerator IEResetPositionAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        transform.position = defaultPosition;
        SpellsHandler.Instance.wallRemoved = false;
    }
}