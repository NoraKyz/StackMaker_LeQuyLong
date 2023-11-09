using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private const float DISTANCE_INPUT_MIN = 5f;
    private const float ERROR_VALUE = 0.0001f;
    private const float BRICK_HEIGHT = 0.3f;
    
    [SerializeField] private GameObject playerBrickPrefab;
    [SerializeField] private Transform playerModel;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private LayerMask layerMask;
    
    [Header("==============================================")]
    [SerializeField] private float speed = 5f;
    
    private Stack<PlayerBrick> bricks = new Stack<PlayerBrick>();

    private Vector3 startPos;
    
    private Vector2 startPosInput;
    private Vector2 endPosInput;
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
                targetPosition = GetTargetPosition();

                if (!IsAtTargetPosition())
                {
                    isMoving = true;
                }
            }
        }
    }
    private Direct GetDirectionInput()
    {
        Vector2 distanceInput = endPosInput - startPosInput;
        distanceInput.Normalize();
 
        if (Vector2.Dot(distanceInput, Vector2.up) > 0.5f)
        {
            return Direct.Up;
        }

        if(Vector2.Dot(distanceInput, Vector2.down) > 0.5f)
        {
            return Direct.Down;
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
    private Vector3 GetDirectionVector(Direct directionInput)
    {
        Vector3 direction = Vector3.zero;
        
        switch (directionInput)
        {
            case Direct.Up:
                direction = Vector3.forward;
                break;
            case Direct.Down:
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
    private Vector3 GetTargetPosition()
    {
        Vector3 target = transform.position;
        
        Vector3 direction = GetDirectionVector(GetDirectionInput());

        while (Physics.Raycast(target, direction, 1f, layerMask))
        {
            target += direction;
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

        if (IsAtTargetPosition())
        {
            isMoving = false;
            ChangeAnim("jump");
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    #endregion
    
    public void AddBrick()
    {
        if(Vector3.Distance(transform.position, startPos) > ERROR_VALUE)
        {
            playerModel.position += Vector3.up * BRICK_HEIGHT;
        }
        
        GameObject playerBrick = Instantiate(playerBrickPrefab, transform);
        playerBrick.transform.position += Vector3.up * 2.5f;
        playerBrick.transform.SetParent(playerModel);

        PlayerBrick p = playerBrick.GetComponent<PlayerBrick>();
        bricks.Push(p);
    }
    public void RemoveBrick()
    {
        playerModel.position -= Vector3.up * BRICK_HEIGHT;
        
        GameObject playerBrick = bricks.Pop().gameObject;
        Destroy(playerBrick);
    }
    private void ChangeAnim(string animName)
    {
        playerAnimator.SetTrigger(animName);
    }
}