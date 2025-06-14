using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class UserInputRay
{
    public Ray ray = new Ray();
    public RaycastHit hit;
    public bool didRayHit { get; set; }
}

public class UserInteraction : MonoBehaviour
{
    //singleton
    public static UserInteraction instance;

    //ray to be used for mouse point, click, touch
    public UserInputRay userInputRay;

    private bool testTubeReadyForPour = false;
    private bool flaskReadyForPour = false;
    [HideInInspector] public ConicalFlask.FlaskNumber selectedFlask = ConicalFlask.FlaskNumber.none;
    [HideInInspector] public bool pourComplete = false;
    [HideInInspector] public bool shakeComplete = false;

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

    /// <summary>
    /// Perform ScreenPointToRay at a point according to user input,
    /// and select an object there if it exists.
    /// </summary>
    /// <param name="context"></param>
    private void AttemptObjectSelect(InputAction.CallbackContext context)
    {
        userInputRay.ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        userInputRay.didRayHit = Physics.Raycast(userInputRay.ray, out userInputRay.hit, 100);

        if (!userInputRay.didRayHit)
            return;

        TestTubeClickFunction();
        FlaskClickFunction();
    }
    private void TestTubeClickFunction()
    {
        if (userInputRay.hit.transform.GetComponent<TestTube>() == null)
            return;

        if (testTubeReadyForPour)
        {
            userInputRay.hit.transform.GetComponent<TestTube>().StartPouring();
        }
        else
        {
            if (flaskReadyForPour)
                testTubeReadyForPour = true;
            userInputRay.hit.transform.GetComponent<TestTube>().MoveTestTubeToPlate();
        }
    }
    private void FlaskClickFunction()
    {
        if (userInputRay.hit.transform.GetComponent<ConicalFlask>() == null)
            return;

        if (!flaskReadyForPour)
        {
            selectedFlask = userInputRay.hit.transform.GetComponent<ConicalFlask>().flaskNumber;
            flaskReadyForPour = true;
            userInputRay.hit.transform.GetComponent<ConicalFlask>().MoveFlaskToPlate();
        }
        else
        if (pourComplete)
        {
            userInputRay.hit.transform.GetComponent<ConicalFlask>().StartShaking();
        }
    }
}