using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPreview : MonoBehaviour
{
    [Header("Trajectory Settings")]
    public int pointsCount = 80;
    public float timeStep = 0.025f;
    public float maxSimulationTime = 5f;
    public int maxBounces = 3;                    // ? How many wall bounces to predict

    [Header("Visuals")]
    public float lineWidth = 0.12f;
    public Color lineColor = new Color(1f, 1f, 1f, 0.75f);

    private LineRenderer lineRenderer;
    private Camera cam;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cam = Camera.main;

        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.textureMode = LineTextureMode.Tile;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    public void ShowTrajectory(Vector2 startPosition, Vector2 direction)
    {
        lineRenderer.positionCount = pointsCount;

        Vector2 position = startPosition;
        Vector2 velocity = direction * 15f;   // Match your bullet speed

        int currentBounce = 0;
        int pointIndex = 0;

        while (pointIndex < pointsCount && currentBounce <= maxBounces)
        {
            float remainingTime = maxSimulationTime - (pointIndex * timeStep);
            if (remainingTime <= 0) break;

            Vector2 newPosition = position + velocity * timeStep
                                + Physics2D.gravity * 0.5f * timeStep * timeStep;

            // Check for collision
            RaycastHit2D hit = Physics2D.Linecast(position, newPosition);

            if (hit.collider != null && hit.collider.isTrigger == false)
            {
                // Hit a wall ? draw up to hit point
                if (pointIndex < pointsCount)
                    lineRenderer.SetPosition(pointIndex, hit.point);
                pointIndex++;

                // Reflect velocity
                velocity = Vector2.Reflect(velocity, hit.normal);
                position = hit.point + hit.normal * 0.01f; // Small offset to avoid sticking

                currentBounce++;

                // Optional: Stop after max bounces
                if (currentBounce > maxBounces)
                    break;
            }
            else
            {
                // No hit ? normal movement
                if (pointIndex < pointsCount)
                    lineRenderer.SetPosition(pointIndex, newPosition);

                position = newPosition;
                velocity += Physics2D.gravity * timeStep;
                pointIndex++;
            }
        }

        // Trim unused points
        lineRenderer.positionCount = Mathf.Min(pointIndex, pointsCount);
    }

    public void HideTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}