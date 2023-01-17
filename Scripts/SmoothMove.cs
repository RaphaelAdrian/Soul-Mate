using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SmoothMove
{
    public EaseMethod easeMethod = EaseMethod.EaseInOut;
    Vector3 lastPosition;
    Vector3 toPosition;
    // variables for animating
    float timeElapsed;
    public float duration = 1;
    GameObject moveObject;
    public void Start() {
        timeElapsed = duration;
    }
    // Start is called before the first frame update
    public void Move(GameObject moveObject, Vector3 targetPosition) {
        this.timeElapsed = 0;
        this.moveObject = moveObject;
        this.lastPosition = moveObject.transform.localPosition;
        this.toPosition = targetPosition;
    }

    public void Update(){
        if (moveObject == null)
            return;
            
        if (timeElapsed < duration) {
            float time = Ease(timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            moveObject.transform.localPosition = Vector3.Lerp(lastPosition, toPosition, time);
        } else {
            moveObject.transform.localPosition = toPosition;
        }
    }
    
    private float Ease(float v)
    {
        switch (easeMethod) {
            case EaseMethod.EaseIn:
                return Easing.Cubic.In(v);
            case EaseMethod.EaseOut:
                return Easing.Cubic.Out(v);
            default:
                return Easing.Cubic.InOut(v);   
        }
    }
}

public enum EaseMethod {
    EaseIn,
    EaseOut,
    EaseInOut
}
