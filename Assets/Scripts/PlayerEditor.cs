using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerEditor : MonoBehaviour
{
    private const float DISTANCE_INPUT_MIN = 5f;
    private const float ERROR_VALUE = 0.0001f;
    
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject unBrickPrefab;
    [SerializeField] private GameObject wallPrefab;
    
    [Header("==============================================")]
    [SerializeField] private Transform brickParent;
    [SerializeField] private Transform unBrickParent;
    [SerializeField] private Transform wallParent;
    
    [Header("==============================================")]
    [SerializeField] private float speed = 20f;

    private Vector3 startPos;
    
    private Vector3 targetPosition;
    
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        
        Move();
    }

    #region Movement Functions

    private enum Direct
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
    private void GetInput()
    {
        if (isMoving)
        {
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            isMoving = true;
            targetPosition = GetTargetPosition(Direct.Up);
        }
        
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            isMoving = true;
            targetPosition = GetTargetPosition(Direct.Down);
        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isMoving = true;
            targetPosition = GetTargetPosition(Direct.Left);
        }
        
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            isMoving = true;
            targetPosition = GetTargetPosition(Direct.Right);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            
            SpawnPivot(brickPrefab, PivotType.Brick);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SpawnPivot(unBrickPrefab, PivotType.UnBrick);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad3))  
        {
            SpawnPivot(wallPrefab, PivotType.Wall);
        }
    }
 
    private Vector3 GetTargetPosition(Direct direction = Direct.None)
    {
        Vector3 target = transform.position;
        
        switch (direction)
        {
            case Direct.Up:
                target += Vector3.forward;
                break;
            case Direct.Down:
                target += Vector3.back;
                break;
            case Direct.Left:
                target += Vector3.left;
                break;
            case Direct.Right:
                target += Vector3.right;
                break;
        }

        return target;
    }
    private bool IsAtTargetPosition()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        return distance < ERROR_VALUE;
    }
    private void Move()
    {
        if (isMoving == false)
        {
            return;
        }

        isMoving = !IsAtTargetPosition();
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    #endregion
    
    private enum PivotType
    {
        Brick,
        UnBrick,
        Wall
    }
    private void SpawnPivot(GameObject prefab, PivotType pivotType)
    {
        
        GameObject pivot = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        pivot.transform.position = transform.position;

        if (pivotType == PivotType.Brick)
        {
            pivot.transform.SetParent(brickParent);
        }
        
        if (pivotType == PivotType.UnBrick)
        {
            pivot.transform.SetParent(unBrickParent);
        }
        
        if (pivotType == PivotType.Wall)
        {
            pivot.transform.SetParent(wallParent);
        }
    }
    
    public void RemoveBrick()
    {
        
    }
    
    public void AddBrick()
    {
        
    }
}