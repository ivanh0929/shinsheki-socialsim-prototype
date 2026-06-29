using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;
using Yarn.Utility;

public class Player : MonoBehaviour
{
    /* TODO
     * Add social stats
     * Add Yollars
     * Prevent inputs from being registered when in dialogue scene
     * Add Interacts
     * Add Attacks (Way Later)
     */

    [Header("References")]
    CharacterController charaController;

    [Header("Input Package System References")]
    InputAction movement;
    InputAction interact;
    InputAction attack;
    

    [Header("Inputs")]
    Vector2 movementInput;
    // interact bool
    // attack bool

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed;

    [Header("Misc")]
    bool pauseControl = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charaController = GetComponent<CharacterController>();
        movement = InputSystem.actions.FindAction("Move");
        //interact = InputSystem.actions.FindAction("Interact");
        //attack = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForControlPause();
        GetInputs();
    }

    private void FixedUpdate()
    {
        if(!pauseControl)
        {
            MovementUpdate();
        }
    }

    void GetInputs()
    {
        movementInput = movement.ReadValue<Vector2>();
        
    }

    void MovementUpdate()
    {
        Movement();
    }


    void Movement()
    {
        // Movement
        Vector3 fullMovement = new Vector3(movementInput.x, 0, movementInput.y);
        fullMovement = fullMovement * walkSpeed;
        charaController.Move(fullMovement);

        // Turning

        if (fullMovement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(fullMovement.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

    }
    
    void CheckForControlPause()
    {
        //pauseControl = Yarn.Unity.DialogueRunner.IsDialogueRunning;
    }
}
