using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    //player movement fields
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    //camera related fields
    [SerializeField] private Camera playerCamera;
    [SerializeField, Range(1, 20)] private float mouseSensX;
    [SerializeField, Range(1, 20)] private float mouseSensY;
    [SerializeField, Range(-90, 0)] private float minViewAngle;
    [SerializeField, Range(0, 90)] private float maxViewAngle;
    [SerializeField] private Transform lookAtPoint;

    private Rigidbody rb;
    private Vector2 currentRotation;

    private Vector3 targetVelocity;
    Vector3 movementVector;

    private float xVelocity;
    private float yVelocity;
    private float zVelocity;

    // animation fields 
    [SerializeField] private Animator playerAnimator = new Animator();
    private bool isWalking = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardDirection = transform.forward.normalized;
        Vector3 rightDirection = transform.right.normalized;

        Vector3 relativeMovement = (forwardDirection * movementVector.z + rightDirection * movementVector.x).normalized;

        targetVelocity = relativeMovement * speed;
        xVelocity = Mathf.Lerp(rb.velocity.x, targetVelocity.x, acceleration * Time.deltaTime);
        zVelocity = Mathf.Lerp(rb.velocity.z, targetVelocity.z, acceleration * Time.deltaTime);
        rb.velocity = new Vector3(xVelocity, rb.velocity.y, zVelocity);

        //change animation states 
        if (isWalking == false)
        {
            playerAnimator.SetBool("idle", true);
            playerAnimator.SetBool("walking", false);
        } 
        else if (isWalking == true) 
        {
            playerAnimator.SetBool("idle", false);
            playerAnimator.SetBool("walking", true);
        }
    }

    private void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector3>();
        
        if (movementVector == Vector3.zero)
        {
            isWalking = false;
        } 
        else
        {
            isWalking = true;
        }
    }

    private void OnLook(InputValue lookValue)
    {
        //controls rotation angles
        currentRotation.x += lookValue.Get<Vector2>().x * Time.deltaTime * mouseSensX;
        currentRotation.y += lookValue.Get<Vector2>().y * Time.deltaTime * -mouseSensY;

        //rotates left & right 
        transform.rotation = Quaternion.AngleAxis(currentRotation.x, Vector3.up);

        //clamp rotation angles 
        currentRotation.y = Mathf.Clamp(currentRotation.y, minViewAngle, maxViewAngle);

        //rotate up and down
        lookAtPoint.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.right);
    }
}
