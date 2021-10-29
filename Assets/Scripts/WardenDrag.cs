using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WardenDrag : MonoBehaviour
{
    public float yOffsetForDraggedObject = 1;
    Plane plane;
    float distance;

    private void Start()
    {
        plane = new Plane(Vector3.up, new Vector3(0, yOffsetForDraggedObject, 0));
        transform.DOMoveY(0, 0.2f);
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            float newX = Mathf.Clamp(ray.GetPoint(distance).x, -2, 2);
            float newZ = Mathf.Clamp(ray.GetPoint(distance).z, -3, 4);
            // transform.position = new Vector3(newX, ray.GetPoint(distance).y, newZ);
            transform.position = Vector3.Lerp(transform.position, new Vector3(newX, ray.GetPoint(distance).y, newZ), 0.2f);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().SetBool("IsFlying", true);
        GetComponent<WardenBase>().Target = null;
        GetComponent<WardenBase>().canShoot = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnMouseUp()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.DOMoveY(0, 0.2f);
        // transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
        GetComponent<Animator>().SetBool("IsFlying", false);
    }
}