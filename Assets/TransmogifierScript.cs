using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmogifierScript : MonoBehaviour
{
    public float multAm = 1.0f;

    public void Increase()
    {
        multAm += 0.1f;
    }

    public void Decrease()
    {
        multAm -= 0.1f;
    }

    void OnTriggerEnter(Collider col)
    {
        Transform obb = col.gameObject.transform;
        if(col.gameObject.tag == "Object")
        {
            obb.localScale = new Vector3(obb.localScale.x * multAm, obb.localScale.y * multAm, obb.localScale.z * multAm);
        }
    }
}
