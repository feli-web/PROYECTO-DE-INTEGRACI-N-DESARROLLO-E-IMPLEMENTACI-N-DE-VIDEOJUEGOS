using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedWidthCamera : MonoBehaviour
{
    public float referenceOrthographicSize = 10f;
    public float targetAspect = 9f / 16f;

    private void Start()
    {
        Camera cam = GetComponent<Camera>();

        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect < targetAspect)
        {
            cam.orthographicSize =
                referenceOrthographicSize * (targetAspect / currentAspect);
        }
        else
        {
            cam.orthographicSize = referenceOrthographicSize;
        }
    }
}