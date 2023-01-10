using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitEdgeCollider : MonoBehaviour
{
    private Camera camera;
    private EdgeCollider2D edgeCol;

    private void Awake()
    {
        camera = Camera.main;
        edgeCol = GetComponent<EdgeCollider2D>();

        FitCollider();
    }

    private void FitCollider()
    {
        Vector2 start = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, camera.nearClipPlane));
        Vector2 leftTop = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane));
        Vector2 leftBottom = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector2 rightBottom = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, camera.nearClipPlane));
        Vector2 rightTop = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, camera.nearClipPlane));

        List<Vector2> edgePoints = new List<Vector2>() { start, leftTop, leftBottom, rightBottom, rightTop };

        for (int i = 0; i < edgePoints.Count; i++)
        {
            edgePoints[i] -= (Vector2)transform.position;
        }

        edgeCol.SetPoints(edgePoints);
    }
}
