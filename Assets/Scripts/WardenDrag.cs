using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WardenDrag : MonoBehaviour
{
    public float yOffsetForDraggedObject = 1;
    Plane plane;
    float distance;

    WardenBase warden;

    private void Start()
    {
        plane = new Plane(Vector3.up, new Vector3(0, yOffsetForDraggedObject, 0));
        transform.DOMoveY(0, 0.2f);
        warden = GetComponent<WardenBase>();
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            float newX = Mathf.Clamp(ray.GetPoint(distance).x, -2.2f, 2.2f);
            float newZ = Mathf.Clamp(ray.GetPoint(distance).z, -3.5f, 3);
            transform.position = Vector3.Lerp(transform.position, new Vector3(newX, ray.GetPoint(distance).y, newZ), 0.2f);
        }
    }

    private void OnMouseDown()
    {
        warden.canShoot = false;
        warden.Target = null;
        GetComponent<Animator>().SetBool("IsFlying", true);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnMouseUp()
    {
        warden.canShoot = true;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.DOMoveY(0, 0.2f);
        GetComponent<Animator>().SetBool("IsFlying", false);
    }
}