using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathwayManager
{
    private readonly PathwayConfig config;
    public readonly List<Pathway> pathways;
    private Vector3 lastPoint;

    public PathwayManager(PathwayConfig config)
    {
        pathways = new List<Pathway>();
        this.config = config;
    }

    public List<TileFactoryInstanceData> Tiles => ConcatenatePathways();

    public void CreatePathway(Vector3 startPosition, float length, float tileSize)
    {
        var controlPoints = GenerateRandomControlPoints(length);
        var newPoints = TranslatePathwayPoints(startPosition, controlPoints);

        var newPath = new Pathway(newPoints, tileSize, config.CustomTileAppearChance);
        pathways.Add(newPath);
    }

    public void ResolveJunctions(Vector3 tileSize)
    {
        var tilesToDelete = FindIntersections(tileSize);

        foreach (var tile in tilesToDelete)
        {
            var target = pathways.First(x => x.TilesDatas.Contains(tile));
            target.TilesDatas.Remove(tile);
        }
    }

    public List<TileFactoryInstanceData> FindIntersections(Vector3 tileSize)
    {
        var tiles = Tiles;
        var cellsToDelete = new List<TileFactoryInstanceData>();
        var toRemove = new HashSet<int>();

        for (var i = 0; i < tiles.Count; i++)
        {
            if (toRemove.Contains(i)) continue;

            var boundsA = new Bounds(tiles[i].Position, tileSize);

            for (var j = i + 1; j < tiles.Count; j++)
            {
                if (toRemove.Contains(j)) continue;

                var boundsB = new Bounds(tiles[j].Position, tileSize);

                if (boundsA.Intersects(boundsB))
                {
                    toRemove.Add(j);
                    cellsToDelete.Add(tiles[j]);
                }
            }
        }

        return cellsToDelete;
    }

    private List<Vector3> TranslatePathwayPoints(Vector3 offset, List<Vector3> points)
    {
        var newPoints = new List<Vector3>();

        foreach (var point in points)
        {
            var newPoint = point + offset;
            newPoints.Add(newPoint);
        }

        var minDist = config.MinDistanceBetweenPoints;
        var maxDist = config.MaxDistanceBetweenPoints;

        for (var i = 0; i < 4; i++)
        {
            var point = new Vector3();
            point.z = lastPoint.z + Random.Range(minDist, maxDist);
            newPoints.Add(point);

            lastPoint = point;
        }

        return newPoints;
    }

    private List<TileFactoryInstanceData> ConcatenatePathways()
    {
        var concatenatedList = new List<TileFactoryInstanceData>();

        foreach (var pathway in pathways) concatenatedList.AddRange(pathway.TilesDatas);
        return concatenatedList;
    }

    private List<Vector3> GenerateRandomControlPoints(float zLength)
    {
        var minDist = config.MinDistanceBetweenPoints;
        var maxDist = config.MaxDistanceBetweenPoints;
        var maxSpread = config.MaxSpread;

        var controlPoints = new List<Vector3>();

        lastPoint = Vector3.zero;
        controlPoints.Add(lastPoint);

        while (lastPoint.z < zLength)
        {
            var randomDistance = new Vector3(
                Random.Range(-maxSpread, maxSpread),
                0,
                lastPoint.z + Random.Range(minDist, maxDist)
            );

            var nextPoint = randomDistance;
            controlPoints.Add(nextPoint);
            lastPoint = nextPoint;
        }

        return controlPoints;
    }
}