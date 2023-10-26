using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Wind : MonoBehaviour {

    //[System.Serializable]
    //public class WindEvent : UnityEvent<Vector3> { }
    //public WindEvent windEvent;

    public Vector3 windForce = new Vector3(0.25f, 0, 0);
    public float intermittentTime = 3.0f;
    public float duration = 3.0f;

    private Collider2D coll;
    private float timer1, timer2;
    private WindState windState;
    private Thing thing;

    void Awake( )
    {
        thing = GameObject.FindObjectOfType<Thing>( );
        coll = GetComponent<Collider2D>( );
    }

    void Start () {
        coll.enabled = false;
        windState = WindState.Stop;
    }

    void Update( )
    {
        SwitchWindState( );
    }

    void SwitchWindState( )
    {
        if(windState == WindState.Open) {
            coll.enabled = true;
            timer2 += Time.deltaTime;
            if(timer2 > duration) {
                windState = WindState.Stop;
                timer2 = 0;
            }
        }
        else if (windState == WindState.Stop) {
            coll.enabled = false;
            timer1 += Time.deltaTime;
            if(timer1 > intermittentTime) {
                windState = WindState.Open;
                timer1 = 0;
            }
        }
    }

    void OnTriggerStay2D( Collider2D other )
    {
        if(other.tag == "Thing") {
            //this.windEvent.Invoke(windForce);
            Debug.Log("add force to thing");
            thing.ChangeForce(windForce);
        }
    }
}
