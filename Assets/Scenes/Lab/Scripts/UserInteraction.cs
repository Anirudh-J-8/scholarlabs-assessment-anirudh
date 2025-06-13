using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class UserInputRay
{
    public Ray ray = new Ray();
    public RaycastHit hit;
    public bool rayHit { get; set; }
}

public class UserInteraction : MonoBehaviour
{
    //singleton
    public static UserInteraction instance;

    //ray to be used for mouse point, click, touch
    public UserInputRay userInputRay;

    private void Awake()
    {
        InputActions inputActions = new InputActions();
        inputActions.ActionMap.Enable();
        inputActions.ActionMap.Select.performed += AttemptObjectSelect;
    }

    private void Start()
    {
        instance = this;
        userInputRay = new();
    }

    private void AttemptObjectSelect(InputAction.CallbackContext context)
    {
        userInputRay.ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        userInputRay.rayHit = Physics.Raycast(userInputRay.ray, out userInputRay.hit, 100);

        if (userInputRay.rayHit)
        {
            //currently only interactable objects have colliders
            Debug.Log($"Tapped on {userInputRay.hit.transform.gameObject.name}");
        }
    }
}