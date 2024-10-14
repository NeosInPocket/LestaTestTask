using System.Collections.Generic;
using UnityEngine;

public class TileBuilder
{
    private readonly Mesh mesh;
    private readonly MeshFilter meshFilter;
    private readonly Vector3 size;
    private List<int> triangles;
    private int[][] trianglesData;
    private List<Vector3> vertices;
    private Vector3[] verticesData;

    public TileBuilder(MeshFilter meshFilter, Vector3 size)
    {
        mesh = new Mesh();

        this.meshFilter = meshFilter;
        this.meshFilter.mesh = mesh;

        this.size = size;

        UpdateMeshData();
        Build();
    }

    public Vector3 Size => size;

    private void Build()
    {
        triangles = new List<int>();
        vertices = new List<Vector3>();

        for (var i = 0; i < 6; i++)
        {
            var fv = new Vector3[4];
            for (var j = 0; j < fv.Length; j++) fv[j] = verticesData[trianglesData[i][j]];

            vertices.AddRange(fv);

            var vertIndex = vertices.Count - 4;

            triangles.Add(vertIndex + 0);
            triangles.Add(vertIndex + 1);
            triangles.Add(vertIndex + 2);

            triangles.Add(vertIndex + 0);
            triangles.Add(vertIndex + 2);
            triangles.Add(vertIndex + 3);
        }

        UpdateMesh();
    }

    private void UpdateMeshData()
    {
        verticesData = new Vector3[]
        {
            new(size.x / 2, size.y / 2, size.z / 2),
            new(-size.x / 2, size.y / 2, size.z / 2),
            new(-size.x / 2, -size.y / 2, size.z / 2),
            new(size.x / 2, -size.y / 2, size.z / 2),
            new(-size.x / 2, size.y / 2, -size.z / 2),
            new(size.x / 2, size.y / 2, -size.z / 2),
            new(size.x / 2, -size.y / 2, -size.z / 2),
            new(-size.x / 2, -size.y / 2, -size.z / 2)
        };

        trianglesData = new[]
        {
            new[] { 0, 1, 2, 3 },
            new[] { 5, 0, 3, 6 },
            new[] { 4, 5, 6, 7 },
            new[] { 1, 4, 7, 2 },
            new[] { 5, 4, 1, 0 },
            new[] { 3, 2, 7, 6 }
        };
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }
}