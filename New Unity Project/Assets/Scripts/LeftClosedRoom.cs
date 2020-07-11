using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftClosedRoom : MonoBehaviour
{
    public GameObject LeftClosedRoom1;
    void Start()
    {
        Instantiate(LeftClosedRoom1, transform.position, transform.rotation);
    }

}
