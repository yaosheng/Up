using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler {

    //[System.Serializable]
    //public class ScreenEvent : UnityEvent<Vector3> { }
    //public ScreenEvent controlling;
    private Vector3 initJumpVelocity = new Vector3(0, 5.0f, 0);
    public UnityEvent tapControl;

    void Start () {
	
	}

    public void OnPointerDown(PointerEventData data)
    {
        //this.controlling.Invoke(initJumpVelocity);
        this.tapControl.Invoke( );
    }
}
