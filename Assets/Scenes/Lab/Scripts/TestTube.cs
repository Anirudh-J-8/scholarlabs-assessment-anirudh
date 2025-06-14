using System.Collections;
using UnityEngine;

public class TestTube : MonoBehaviour
{
    private Vector3 testTubeStartingPosition;
    /// <summary>
    /// Position to place test tube when about to pour.
    /// </summary>
    private Vector3 pourPosition = new Vector3(0.0779f, 0.0604f, 0.0149f);
    private bool isPouring = false;

    private void Start()
    {
        testTubeStartingPosition = transform.localPosition;
    }

    /// <summary>
    /// Move to pour position when selected.
    /// </summary>
    public void MoveTestTubeToPlate()
    {
        StartCoroutine(MoveTestTubeCoroutine(transform.localPosition, pourPosition));
    }
    public void MoveTestTubeToStart()
    {
        StartCoroutine(MoveTestTubeCoroutine(transform.localPosition, testTubeStartingPosition));
    }

    public void StartPouring()
    {
        if (!isPouring)
            StartCoroutine(PourCoroutine());
    }

    private IEnumerator MoveTestTubeCoroutine(Vector3 from, Vector3 to)
    {
        float startTime = Time.time;

        //move the flask over a period of 1.5 seconds
        while (Time.time - startTime <= 1.5f)
        {
            transform.localPosition = Vector3.Lerp(from, to, Time.time - startTime);
            yield return null;
        }
    }
    private IEnumerator PourCoroutine()
    {
        float maxRotateAngle = 75f;
        float currentRotation = 0;
        isPouring = true;

        while (currentRotation < maxRotateAngle)
        {
            transform.Rotate(Vector3.left, 0.5f, Space.World);
            currentRotation += 0.5f;
            yield return null;
        }

        yield return new WaitForSeconds(1);

        while (currentRotation > 0)
        {
            transform.Rotate(Vector3.left, -0.5f, Space.World);
            currentRotation -= 0.5f;
            yield return null;
        }

        isPouring = false;
        UserInteraction.instance.pourComplete = true;

        MoveTestTubeToStart();
    }
}
