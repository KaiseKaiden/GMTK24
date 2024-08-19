using System.Collections.Generic;
using UnityEngine;

public class SpiralGenerator
{
    public List<Vector3> GetSpiralPoints(Vector3 center, float spiralParameter, float distanceBetweenPoints,
                                         int numberOfPoints)
    {
        List<Vector3> points = new List<Vector3>();

        // Parameters
        float b = spiralParameter;       // Spiral parameter
        float d = distanceBetweenPoints; // Desired distance between points
        int nPoints = numberOfPoints;    // Number of points to generate

        // Initialize the first point
        float theta = 0;
        float r = 0;

        // Generate points
        for (int i = 0; i < nPoints; i++)
        {
            float xPos = r * Mathf.Cos(theta);
            float zPos = r * Mathf.Sin(theta);
            Vector3 pos = new Vector3(xPos, center.y, zPos);

            points.Add(pos);

            // Find the next angle by adjusting theta to keep arc length ~ d
            theta += d / Mathf.Sqrt(r * r + b * b); // Increment angle
            r = b * theta;                          // Update radius
        }

        return points;
    }

    private void PolarToCartesian(float r, float theta, out float x, out float y)
    {
        x = (float)(r * Mathf.Cos(theta));
        y = (float)(r * Mathf.Sin(theta));
    }
}
