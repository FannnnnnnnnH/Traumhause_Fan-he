using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBody : MonoBehaviour
{
    GravityAttractor planet;
    private void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
    // Start is called before the first frame update
    
    // Update is called once per frame
    void FixedUpdate()
    {
        planet.Attract(transform);
    }
}
