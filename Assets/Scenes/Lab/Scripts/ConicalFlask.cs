using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ConicalFlask : MonoBehaviour
{
    public enum FlaskNumber { A, B, none };
    public FlaskNumber flaskNumber;

    private MeshRenderer flaskLiquid;

    private Vector3 flaskStartingPosition;
    /// <summary>
    /// Position for flask to be placed in center of wood plate.
    /// </summary>
    private Vector3 flaskExperimentPosition = new Vector3(0.132f, 0.544f, 0.135f);
    private bool isShaking;

    private void Start()
    {
        //liquid is in first child of flask
        flaskLiquid = transform.GetChild(0).GetComponent<MeshRenderer>();
        flaskStartingPosition = transform.localPosition;
    }

    public void MoveFlaskToPlate()
    {
        StartCoroutine(MoveFlaskCoroutine(transform.localPosition, flaskExperimentPosition));
    }
    public void MoveFlaskToStart()
    {
        StartCoroutine(MoveFlaskCoroutine(transform.localPosition, flaskStartingPosition));
    }

    public void StartShaking()
    {
        if (!isShaking)
            StartCoroutine(ShakeFlaskCoroutine());
    }

    private IEnumerator MoveFlaskCoroutine(Vector3 from, Vector3 to)
    {
        float startTime = Time.time;

        //move the flask over a period of 1.5 seconds
        while (Time.time - startTime <= 1.5f)
        {
            transform.position = Vector3.Lerp(from, to, Time.time - startTime);
            yield return null;
        }
    }

    private IEnumerator ShakeFlaskCoroutine()
    {
        isShaking = true;
        Quaternion initialRotation = transform.rotation;

        //lift the flask to a distance of 0.2f units before shaking
        StartCoroutine(MoveFlaskCoroutine(transform.localPosition, transform.localPosition + new Vector3(0, 0.2f, 0)));

        //wait for flask to get lifted
        yield return new WaitForSeconds(1.5f);

        float startTime = Time.time;
        int direction = 1;
        float directionChangeTimer = 0;

        //shake for 1.5 seconds
        while (Time.time - startTime <= 1.5f)
        {
            if (directionChangeTimer > 0.1f)
            {
                directionChangeTimer = 0;
                direction *= -1;
            }
            transform.Rotate(Vector3.left * direction, 1f, Space.World);
            directionChangeTimer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = initialRotation;   //auto-correct any new resultant rotation
        StartCoroutine(MoveFlaskCoroutine(transform.localPosition, transform.localPosition + new Vector3(0, -0.2f, 0)));

        Color mixedColor;
        switch (flaskNumber)
        {
            case FlaskNumber.A:
                mixedColor = new Color32(145, 121, 228, 255); //purple
                break;
            case FlaskNumber.B:
                mixedColor = Color.yellow;
                break;
            default:
                mixedColor = Color.white;
                break;
        }
        Color startColor = flaskLiquid.materials[0].color;

        Material[] materials = flaskLiquid.materials;
        float currentTime = 0;
        float colorChangeDuration = 3f;

        while (currentTime <= colorChangeDuration)
        {
            currentTime += Time.deltaTime;
            foreach (Material material in materials)
            {
                material.SetColor("_Tint", Color.Lerp(startColor, mixedColor, (currentTime / colorChangeDuration)));
            }
            yield return null;
        }

        isShaking = false;
    }
}