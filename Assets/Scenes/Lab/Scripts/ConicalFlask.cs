using UnityEngine;

public class ConicalFlask : MonoBehaviour
{
    public enum FlaskNumber { A, B, none };
    public FlaskNumber flaskNumber;

    private Vector3 flaskStartingPosition;
    /// <summary>
    /// Position for flask to be placed in center of wood plate.
    /// </summary>
    private Vector3 flaskExperimentPosition = new Vector3(0.132f, 0.544f, 0.135f);

    private void Start()
    {
        flaskStartingPosition = transform.position;
    }

    public void Select()
    {
        transform.position = flaskExperimentPosition;
    }
    public void Deselect()
    {
        transform.position = flaskStartingPosition;
    }
}
