using UnityEngine;
using System.Collections;

public class Sharp : MonoBehaviour {

    private float rotateSpeed = 1000.0f;
    private Thing thing;

    void Awake( )
    {
        thing = GameObject.FindObjectOfType<Thing>( );
    }
	
	void Update () {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
	}

    void OnCollisionEnter2D( Collision2D coll )
    {
        if(coll.collider.tag == "Thing") {
            thing.Dead( );
        }
    }
}
