using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI : MonoBehaviour, ILaserSelectReceiver
{
    public void LaserSelectReceiver()
    {
        Debug.Log("test");
    }
}
