using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    [SerializeField] float playerRadius = .7f;
    [SerializeField] float playerHeight = 2f;

    private bool isWalking;
    private Vector3 lastInteractDir;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hitInfo, interactDistance,counterLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight, playerRadius,moveDir,moveDistance);
        if (!canMove)
        {
            //cant move towards moveDir
            
            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight, playerRadius,moveDirX,moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0,moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight, playerRadius,moveDirZ,moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                    
                }
                else
                {
                    //cant move in any direction
                }
            }
            
        }
        
        
        
        if(canMove)
            transform.position += moveDir*moveDistance;

        isWalking = moveDir != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotateSpeed);
    }

    private void HandleInteraction()
    { 
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hitInfo, interactDistance,counterLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //clearCounter.Interact();
            }
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
