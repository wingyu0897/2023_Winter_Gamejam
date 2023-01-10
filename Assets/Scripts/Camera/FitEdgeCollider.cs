using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitEdgeCollider : MonoBehaviour
{
    private Camera cam;
    private EdgeCollider2D edgeCol;

    private void Awake()
    {
        cam = Camera.main;
        edgeCol = GetComponent<EdgeCollider2D>();

        FitCollider();
    }

    private void FitCollider()
    {
        Vector2 start = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        Vector2 leftTop = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        Vector2 leftBottom = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 rightBottom = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
        Vector2 rightTop = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));

        List<Vector2> edgePoints = new List<Vector2>() { start, leftTop, leftBottom, rightBottom, rightTop };

        for (int i = 0; i < edgePoints.Count; i++)
        {
            edgePoints[i] -= (Vector2)transform.position;
        }

        edgeCol.SetPoints(edgePoints);
    }
}
