using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pathway
{
    private readonly List<Vector3> controlPoints;
    private readonly float customTileAppearChance;
    private readonly float tileSize;

    public Pathway(List<Vector3> controlPoints, float tileSize, float customTileAppearChance)
    {
        this.controlPoints = controlPoints;
        TilesDatas = new List<TileFactoryInstanceData>();
        this.tileSize = tileSize;
        this.customTileAppearChance = customTileAppearChance;

        GenerateBezierPath();
    }

    public List<TileFactoryInstanceData> TilesDatas { get; }

    private void GenerateBezierPath()
    {
        var tileSpacing = tileSize / 2f;
        var tileCount = (int)((controlPoints[^1].z - controlPoints[0].z) / (tileSize + tileSpacing));

        for (var i = 0; i < tileCount; i++)
        {
            var t = i / (float)(tileCount - 1);
            var tile = new TileFactoryInstanceData();

            tile.Position = CalculateBezierPoint(t, controlPoints);
            tile.Rotation = CalculateRotationOnCurve(t, controlPoints);
            TilesDatas.Add(tile);
        }

        InsertCustomTiles();
    }

    private void InsertCustomTiles()
    {
        var tileTypes = new List<TileType>();
        foreach (TileType type in Enum.GetValues(typeof(TileType))) tileTypes.Add(type);

        foreach (var tileData in TilesDatas)
            if (Random.Range(0, 1f) < customTileAppearChance)
                tileData.Type = tileTypes[Random.Range(0, tileTypes.Count)];
    }

    private Vector3 CalculateBezierPoint(float t, List<Vector3> controlPoints)
    {
        var n = controlPoints.Count - 1;
        var point = Vector3.zero;

        for (var i = 0; i <= n; i++)
        {
            var binomialCoefficient = BinomialCoefficient(n, i);
            var term = binomialCoefficient * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i);
            point += term * controlPoints[i];
        }

        return point;
    }

    private Quaternion CalculateRotationOnCurve(float t, List<Vector3> controlPoints)
    {
        var tangent = CalculateBezierTangent(t, controlPoints);
        return Quaternion.LookRotation(tangent);
    }

    private Vector3 CalculateBezierTangent(float t, List<Vector3> controlPoints)
    {
        var tangent = Vector3.zero;
        var n = controlPoints.Count - 1;

        for (var i = 0; i < n; i++)
        {
            var binomialCoefficient = BinomialCoefficient(n - 1, i);
            var term = binomialCoefficient * Mathf.Pow(1 - t, n - 1 - i) * Mathf.Pow(t, i);
            tangent += term * (controlPoints[i + 1] - controlPoints[i]);
        }

        return tangent.normalized;
    }

    private float BinomialCoefficient(int n, int k)
    {
        return Factorial(n) / (Factorial(k) * Factorial(n - k));
    }

    private float Factorial(int n)
    {
        if (n == 0 || n == 1)
            return 1;
        return n * Factorial(n - 1);
    }

    public void DrawGizmos()
    {
        var resolution = 100;
        var previousPoint = CalculateBezierPoint(0f, controlPoints);

        for (var i = 1; i <= resolution; i++)
        {
            var t = i / (float)resolution;
            var currentPoint = CalculateBezierPoint(t, controlPoints);
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }
}