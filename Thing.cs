using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Thing : MonoBehaviour {

    private Vector3 initJumpVelocity = new Vector3(0, 5.0f, 0);
    private Vector3 dropAcceleration = new Vector3(0, -9.8f, 0);
    [SerializeField]
    private Vector3 currentVelocity;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    private RaycastHit2D hit;
    [SerializeField]
    private bool isTopOfPlatform;
    private bool isPushedLeft, isPushedRight;

    private bool[ ] touchDirection = new bool[4];

    private float area;
    private float ForceLeft, ForceRight;
    private float originalRadius;
    private float originalSizeX;

    void Awake( )
    {
        rb = GetComponent<Rigidbody2D>( );
        col = GetComponent<CircleCollider2D>( );
    }

	void Start () {
        area = transform.localScale.x * transform.localScale.y;
        originalRadius = col.radius;
        originalSizeX = transform.localScale.x;

        ForceLeft = 12.0f;
        ForceRight = 12.0f;
    }
	
	void FixedUpdate () {

        if(touchDirection[1]) {
            if(this.currentVelocity.y < 0) {
                this.currentVelocity = Vector3.zero;
            }
        }
        if(this.transform.localScale.x < 1 / 3 * originalSizeX) {
            Dead( );
        }
        TransformScale( );
        RestoreScale( );
        Motion( );
    }

    public void ChangeForce(Vector3 tempVector)
    {
        this.currentVelocity += tempVector;
    }

    public void Jump( )
    {
        this.currentVelocity = initJumpVelocity;
    }

    void Motion( )
    {
        currentVelocity += dropAcceleration * Time.fixedDeltaTime;
        //transform.position += currentVelocity * Time.fixedDeltaTime;
        rb.MovePosition(rb.transform.position + currentVelocity * Time.fixedDeltaTime);
    }

    void TransformScale( )
    {
        //if(isPushedLeft && isPushedRight) {
        if(touchDirection[2] && touchDirection[3]) { 
            if(transform.localScale.x >= 0) {
                //Debug.Log("Squeeze");
                float squeezeForce = (ForceLeft + ForceRight);
                float x = transform.localScale.x - squeezeForce * Time.deltaTime;
                float y = area / x;
                transform.localScale = new Vector2(x, y);
                col.radius = originalRadius/2 * (transform.localScale.x / originalSizeX);
            }
            //else {
            //    transform.localScale = new Vector2(0, transform.localScale.y);
            //}
        }
    }

    void RestoreScale( )
    {
        //if(!isPushedLeft || !isPushedRight) {
        if(!touchDirection[2] || !touchDirection[3]) {
            if(transform.localScale.x < originalSizeX) {
                float squeezeForce = ForceLeft + ForceRight;
                float x = transform.localScale.x + squeezeForce * Time.deltaTime;
                float y = area / x;
                transform.localScale = new Vector2(x, y);
                col.radius = originalRadius;
            }
        }
    }

    public void Dead( )
    {
        this.gameObject.SetActive(false);
    }

    public void SetTouch(int tempInt, bool tempBool )
    {
        touchDirection[tempInt] = tempBool;
    }
}

//void OnCollisionStay2D( Collision2D coll )
//{
//    for(int i = 0; i < coll.contacts.Length; i++) {
//        if(this.transform.position.y > coll.contacts[i].point.y) {
//            isTopOfPlatform = true;
//        }
//    }

//    if(coll.collider.tag == "Platform" && isTopOfPlatform) {
//        //Debug.Log("stand on a platform");
//        if(this.currentVelocity.y < 0) {
//            this.currentVelocity = Vector3.zero;
//        }
//    }
//}

//public void SetPushedLeft(bool tempBool)
//{
//    this.isPushedLeft = tempBool;
//}

//public void SetPushedRight( bool tempBool )
//{
//    this.isPushedRight = tempBool;
//}

//void OnCollisionExit2D( Collision2D coll )
//{
//    //Debug.Log("exit collider");
//    //for(int i = 0; i < coll.contacts.Length; i++) {
//    //    if(this.transform.position.y > coll.contacts[i].point.y) {
//    //        isTopOfPlatform = false;
//    //    }
//    //    if(this.transform.position.x >= coll.contacts[i].point.x) {
//    //        isPushedLeft = false;
//    //        //Debug.Log("touch x :" + coll.contacts[i].point.x);
//    //        //Debug.Log("reset left push = false");
//    //    }
//    //    else {
//    //        isPushedRight = false;
//    //        //Debug.Log("reset right push = false");
//    //    }
//    //}
//}

//if(this.transform.position.x > coll.contacts[i].point.x) {
//    if(col.bounds.center.x - coll.contacts[i].point.x <= col.bounds.center.x - col.bounds.min.x) {
//        //Debug.Log("Squeeze left");
//        isPushedLeft = true;
//        ForceLeft = 5.0f;
//    }
//}

//if(this.transform.position.x < coll.contacts[i].point.x) {
//    if(coll.contacts[i].point.x - col.bounds.center.x <= col.bounds.max.x - col.bounds.center.x ) {
//        //Debug.Log("Squeeze right");
//        isPushedRight = true;
//        ForceRight = 5.0f;
//    }
//}

//void CheckSqueeze(bool isLeft, bool isRight)
//{
//    if(!isLeft || !isRight) {
//        return;
//    }
//    else {
//        Debug.Log("Squeeze");
//        float squeezeForce = ForceLeft + ForceRight;
//        float x = transform.localScale.x - squeezeForce * Time.deltaTime;
//        float y = area / x;
//        transform.localScale = new Vector2(x, y);
//    }
//}

//if(isPushedLeft && isPushedRight) {
//    float squeezeForce = ForceLeft + ForceRight;
//    float x = transform.localScale.x - squeezeForce * Time.deltaTime;
//    float y = area / x;
//    transform.localScale = new Vector2(x, y);
//}

//void OnCollisionExit2D( Collision2D coll )
//{
//    isTopOfPlatform = false;
//    isPushedLeft = false;
//    isPushedRight = false;
//}

//Vector2 diff = coll.contacts[i].point * (1 - coll.contacts[i].normal.magnitude);
//if(diff.x <= 0) {
//    isPushedLeft = true;
//    ForceLeft = 0.1f;
//}

//Check all points of contacts occuring on this object
//for(int i = 0; i < coll.contacts.Length; i++) {

//    //Is the collision occuring to the left of box?
//    //if(this.transform.position.x > coll.contacts[i].point.x) {
//    //    //Is it pushing in? or pulling out?
//    //    Vector2 diff = coll.contacts[i].point * (1 - coll.contacts[i].normal.magnitude) ;
//    //    if(diff.x <= 0) {
//    //        isPushedLeft = true;
//    //        ForceLeft = 0.1f;
//    //    }
//    //}
//    //collision occuring to the right of box
//    else if(this.transform.position.x < coll.contacts[i].point.x) {
//        //Is it pushing in? or pulling out?
//        Vector2 diff = coll.contacts[i].point - coll.contacts[i].normal;
//        if(diff.x <= 0) {
//            isPushedRight = true;
//            ForceRight = 0.1f;
//        }
//    }
//}

//void HitCollider( )
//{
//    hit = Physics2D.Raycast(transform.position + Vector3.down * 0.5f, Vector3.down);
//    if(hit.collider != null) {
//        if(hit.collider.tag == "Platform") {
//            isHitPlatform = true;
//        }
//        else {
//            isHitPlatform = false;
//        }
//    }
//}

//public void Jump( )
//{
//    this.currentVelocity = initJumpVelocity;
//}

//void Drop( )
//{
//    transform.position += dropVelocity * Time.deltaTime;
//}

//IEnumerator jumpCorotine( )
//{
//    Vector3 acceleration = Vector3.up * 10f;
//    jumpVelocity += acceleration * Time.deltaTime;
//    transform.position += jumpVelocity * Time.deltaTime;
//    Debug.Log("jump");
//    yield return new WaitForSeconds(0.1f);
//    isJump = false;
//}