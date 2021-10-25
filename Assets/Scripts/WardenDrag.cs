using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenDrag : MonoBehaviour
{
    public float yOffsetForDraggedObject = 1;
    Plane plane;
    float distance;

    private void Start()
    {
        plane = new Plane(Vector3.up, new Vector3(0, yOffsetForDraggedObject, 0));
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            transform.position = ray.GetPoint(distance);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().SetBool("IsFlying", true);
        GetComponent<Warden>().Target = null;
        GetComponent<Warden>().canShoot = false;
    }

    private void OnMouseUp()
    {
        transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
        GetComponent<Animator>().SetBool("IsFlying", false);

    }
}