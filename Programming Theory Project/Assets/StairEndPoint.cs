using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairEndPoint : MonoBehaviour
{
    public bool isOccupied = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Platform")
        {
            isOccupied = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Platform")
        {
            isOccupied = true;
        }
    }
}
