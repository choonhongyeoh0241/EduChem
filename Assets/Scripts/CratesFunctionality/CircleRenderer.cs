using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{
    [SerializeField] private Vector2 innerSize = Vector2.one;
    [SerializeField][Range(0, 1986)] private int vertices = 50;
    [SerializeField][Range(0, 10)] private int overlap = 2;

    [SerializeField, HideInInspector] private LineRenderer lineRenderer;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Generate();
    }

    private void Reset() => OnValidate();
    #endif

    private void Generate()
    {
        lineRenderer.positionCount = vertices;
        lineRenderer.useWorldSpace = false;

        float x;
        float y;

        float theta = 0;
        float delta = (2 * Mathf.PI) / (vertices - overlap);

        for (int i = 0; i < vertices; i++)
        {
            x = Mathf.Cos(theta) * innerSize.x;
            y = Mathf.Sin(theta) * innerSize.y;

            lineRenderer.SetPosition(i, new Vector2(x, y));

            theta += delta;
        }
    }
}
