using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairEndPoint : MonoBehaviour
{
    private void Update()
    {
        RaycastHit r;
        Physics.SphereCast(transform.position, 1f, Vector3.zero, out r);
        Collider[] hcs = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hc in hcs)
        {
            if (hc.CompareTag("Platform") && !GetComponent<Collider>())
            {
                Debug.Log("Detected");
                Destroy(gameObject);
                break;
            }
        }
    }
}
