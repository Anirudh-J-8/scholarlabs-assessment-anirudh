using UnityEngine;

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

    private void Start()
    {
        instance = this;
        userInputRay = new();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            userInputRay.ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            userInputRay.rayHit = Physics.Raycast(userInputRay.ray, out userInputRay.hit, 100);
        }
        else
        {
            userInputRay.rayHit = false;
        }
    }
}