using UnityEngine;

public class TestTube : MonoBehaviour
{
    private Vector3 testTubeStartingPosition;
    /// <summary>
    /// Position to place test tube when about to pour.
    /// </summary>
    private Vector3 pourPosition = new Vector3(0.0779f, 0.0604f, 0.0149f);

    private void Start()
    {
        testTubeStartingPosition = transform.position;
    }

    /// <summary>
    /// Move to pour position when selected.
    /// </summary>
    public void Select()
    {
        transform.localPosition = pourPosition;
    }
    public void Deselect()
    {
        transform.localPosition = testTubeStartingPosition;
    }
}
