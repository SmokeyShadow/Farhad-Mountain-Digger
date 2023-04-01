using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    #region STATIC METHODS
    public static bool almostEqual(Vector3 a, Vector3 b, float dist)
    {
        bool equal = true;

        if (Mathf.Abs(b.x - a.x) > dist)
            equal = false;
        if (Mathf.Abs(b.y - a.y) > dist)
            equal = false;
        if (Mathf.Abs(b.z - a.z) > dist)
            equal = false;

        return equal;
    }

    public static bool almostEqual(float a, float b, float dist)
    {
        bool equal = true;

        if (Mathf.Abs(b - a) > dist)
            equal = false;

        return equal;
    }
    #endregion
}
