using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDeadEnd : MonoBehaviour
{
    public GameObject BottomDeadEndPrefab;
    void Start()
    {
        Instantiate(BottomDeadEndPrefab, transform.position, transform.rotation);
    }

}
