using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private void Update()
    {
        if (UserInteraction.instance.userInputRay.rayHit)
        {

            Debug.Log($"Tapped on {UserInteraction.instance.userInputRay.hit.transform.gameObject.name}");
        }
    }
}
