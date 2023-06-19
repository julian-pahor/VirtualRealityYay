using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisiblityDetector : MonoBehaviour
{
    public Camera cam;
    public Collider target;

    public bool IsVisible()
    {
        return GetIsVisible(cam, target);
    }

    bool GetIsVisible(Camera c, Collider target)
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(c);
        Vector3 point = target.transform.position;

        //get min max extents, then dist

        //1/2 distance between min/max extents

        foreach (Plane p in planes)
        {
            if (p.GetDistanceToPoint(point) < -target.bounds.extents.x)
            {
                return false;
            }
        }


        return true;

    }
}
