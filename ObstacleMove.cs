using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmoothMove))]
public class ObstacleMove : Obstacle
{
    public Vector2 moveToPosition = Vector2.zero;
    public bool isMovementCanStack;
    
    [Header("Animation")]
    public SmoothMove smoothMove;
    int currentMove;
    Vector3 initPosition;
    Vector3 targetPosition;
    

    void Start() {
        initPosition = transform.localPosition;
        UpdateTargetPosition();
        // set z axis to avoid bugs
    }

    public override void Activate(bool isActivate)
    {
        base.Activate(isActivate);
        if (!gameObject.activeInHierarchy)
            return;

        HandleMoveMultiplier(isActivate);
        UpdateTargetPosition(isActivate);

        smoothMove.Move(gameObject, targetPosition);
    }
    public void Update() {
        smoothMove.Update();
    }

    private void HandleMoveMultiplier(bool isActivate)
    {
        currentMove = isActivate ? currentMove + 1 : currentMove - 1;
    }

    private void UpdateTargetPosition(bool isActivate = false)
    {
        if (!isMovementCanStack) {
            if (isActivate)
                targetPosition = initPosition + (Vector3)moveToPosition;
            else 
                targetPosition = initPosition;
        } else {
                targetPosition = initPosition + ((Vector3)moveToPosition * currentMove);
        }

    }

}
