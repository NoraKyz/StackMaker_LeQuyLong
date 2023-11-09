using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    private const float DISTANCE_INPUT_MIN = 5f;
    private const float ERROR_VALUE = 0.0001f;
    
    private Vector3 startPosInput;
    private Vector3 endPosInput;
    public Vector3 direction { get; private set; } = Vector3.zero;
    
    private bool enableInput = true;
    private enum Direct
    {
        Forward,
        Back,
        Left,
        Right,
        None
    }
    
    public void GetInput()
    {
        if(!enableInput)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            startPosInput = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPosInput = Input.mousePosition;
            Vector2 distanceInput = endPosInput - startPosInput;

            if (distanceInput.magnitude > DISTANCE_INPUT_MIN)
            {
                direction = GetDirectionVector();
            }
        }
    }
    private Direct GetDirectionInput()
    {
        Vector2 distanceInput = endPosInput - startPosInput;
        distanceInput.Normalize();
 
        if (Vector2.Dot(distanceInput, Vector2.up) > 0.5f)
        {
            return Direct.Forward;
        }

        if(Vector2.Dot(distanceInput, Vector2.down) > 0.5f)
        {
            return Direct.Back;
        }
                
        if(Vector2.Dot(distanceInput, Vector2.left) > 0.5f)
        {
            return Direct.Left;
        }

        if(Vector2.Dot(distanceInput, Vector2.right) > 0.5f)
        {
            return Direct.Right;
        }

        return Direct.None;
    }
    private Vector3 GetDirectionVector()
    {
        Vector3 direction = Vector3.zero;
        Direct directionInput = GetDirectionInput();
        
        switch (directionInput)
        {
            case Direct.Forward:
                direction = Vector3.forward;
                break;
            case Direct.Back:
                direction = Vector3.back;
                break;
            case Direct.Left:
                direction = Vector3.left;
                break;
            case Direct.Right:
                direction = Vector3.right;
                break;
        }

        return direction;
    }
    
    public void EnableInput()
    {
        enableInput = true;
    }
    
    public void DisableInput()
    {
        enableInput = false;
        direction = Vector3.zero;
    }
    
}
