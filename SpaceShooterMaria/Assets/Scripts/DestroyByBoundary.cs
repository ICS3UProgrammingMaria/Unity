using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}


/*public class DestroyByBoundary : MonoBehaviour
{
    public GameObject explosion;
public GameObject playerExplosion;

void OnTriggerExit(Collider other)
{
    if (other.tag == "Boundary")
    {
        return;
    }
    Instantiate(explosion, transform.position, transform.rotation);
    Destroy(other.gameObject);
    Destroy(gameObject);
}
}*/
