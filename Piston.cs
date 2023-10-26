using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Piston : MonoBehaviour {

    public Vector3 swingVelocity = new Vector3(3.0f, 0, 0);
    public float velocity = 5.0f;
    public CollisionState css;

    private Thing thing;
    private BoxCollider2D bc;

    void Awake( )
    {
        thing = GameObject.FindObjectOfType<Thing>( );
        bc = GetComponent<BoxCollider2D>( );
    }

    void Start( )
    {
        css = CollisionState.none;
    }

    void Update () {
        if(Vector3.Dot(transform.position, Vector3.right) >= 0) {
            Vector3 acceleration = Vector3.left * velocity;
            swingVelocity += acceleration * Time.deltaTime;
            transform.position += swingVelocity * Time.deltaTime;
        }
        else {
            Vector3 acceleration = Vector3.right * velocity;
            swingVelocity += acceleration * Time.deltaTime;
            transform.position += swingVelocity * Time.deltaTime;
        }
    }

    void OnCollisionStay2D( Collision2D coll )
    {
        if(coll.collider.tag == "Thing") {
            if(thing.transform.position.y >= bc.bounds.min.y && thing.transform.position.y <= bc.bounds.max.y) {
                if(thing.transform.position.x < bc.bounds.min.x) {
                    Debug.Log("Thing is touch and right");
                    css = CollisionState.right;
                    thing.SetTouch(3, true);
                }
                else if(thing.transform.position.x > bc.bounds.max.x) {
                    Debug.Log("Thing is touch and left");
                    css = CollisionState.left;
                    thing.SetTouch(2, true);
                }
            }
            else if(thing.transform.position.x >= bc.bounds.min.x && thing.transform.position.x <= bc.bounds.max.x) {
                if(thing.transform.position.y < bc.bounds.min.y) {
                    Debug.Log("Thing is touch and up");
                    css = CollisionState.up;
                    thing.SetTouch(0, true);
                }else if(thing.transform.position.y > bc.bounds.max.y) {
                    Debug.Log("Thing is touch and down");
                    css = CollisionState.down;
                    thing.SetTouch(1, true);
                }
            }
        }
    }

    void OnCollisionExit2D( Collision2D coll )
    {
        switch(css) {
            case CollisionState.up:
            thing.SetTouch(0, false);
            css = CollisionState.none;
            break;
            case CollisionState.down:
            thing.SetTouch(1, false);
            css = CollisionState.none;
            break;
            case CollisionState.left:
            thing.SetTouch(2, false);
            css = CollisionState.none;
            break;
            case CollisionState.right:
            thing.SetTouch(3, false);
            css = CollisionState.none;
            break;
            case CollisionState.none:
            break;
        }
    }
}

////Debug.Log("exit");
//if(coll.collider.tag == "Thing") {
//    //Debug.Log("exit Thing");
//    Debug.Log("1 : " + thing.transform.position.x);
//    Debug.Log("2 : " + bc.bounds.min.x);

//    if(thing.transform.position.x < bc.bounds.min.x) {
//        Debug.Log("Thing is exit and right");
//    }
//    else if(thing.transform.position.x > bc.bounds.max.x) {
//        Debug.Log("Thing is exit and left");
//    }
//}