using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class GraphManager : MonoBehaviour
{
    public float distanceMax = 5.0f;

    public GameObject linkPrefab;
    Waypoint[] waypoints;
    
    void Awake()
    {
        UpdateLinks();
    }

    void Update()
    {
        if (!Application.isPlaying)
            UpdateLinks();
    }

    void OnDestroy()
    {
        RemoveLinks();
    }

    void RemoveLinks()
    {
        while (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
    }

    public void UpdateLinks()
    {
        RemoveLinks();

        waypoints = FindObjectsOfType<Waypoint>();
        
        foreach (Waypoint waypoint in waypoints)
        {
            waypoint.neighbors.Clear();
        }

        for (int i = 0; i < waypoints.Length; ++i)
        {
            Waypoint aWaypoint = waypoints[i];
            
            for (int j = i + 1; j < waypoints.Length; ++j)
            {
                Waypoint bWaypoint = waypoints[j];

                float distance = Vector3.Distance(aWaypoint.transform.position, bWaypoint.transform.position);

                if (distance <= distanceMax)
                {
                    aWaypoint.neighbors.Add(bWaypoint);
                    bWaypoint.neighbors.Add(aWaypoint);

                    GameObject linkInstance = Instantiate<GameObject>(linkPrefab);
                    linkInstance.transform.parent = transform;

                    Link linkComponent = linkInstance.GetComponent<Link>();
                    linkComponent.Setup(aWaypoint.transform.position, bWaypoint.transform.position);
                }
            }
        }
	}

    float CostHeuristic(Waypoint a, Waypoint b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }

    // https://en.wikipedia.org/wiki/A*_search_algorithm
    public List<Waypoint> GetPath(Waypoint start, Waypoint target)
    {
        HashSet<Waypoint> visitedWaypoints = new HashSet<Waypoint>();
        HashSet<Waypoint> discoveredWaypoints = new HashSet<Waypoint>();
        Dictionary<Waypoint, Waypoint> originWaypoints = new Dictionary<Waypoint, Waypoint>();
        Dictionary<Waypoint, float> pastCosts = new Dictionary<Waypoint, float>();
        Dictionary<Waypoint, float> totalCosts = new Dictionary<Waypoint, float>();

        discoveredWaypoints.Add(start);
        pastCosts.Add(start, 0.0f);
        totalCosts.Add(start, CostHeuristic(start, target));

        while (discoveredWaypoints.Count > 0)
        {
            Waypoint current = discoveredWaypoints.OrderBy(waypoint => totalCosts[waypoint]).First();
            if (current == target)
            {
                List<Waypoint> path = new List<Waypoint>();
                path.Add(current);
                while (originWaypoints.ContainsKey(current))
                {
                    current = originWaypoints[current];
                    path.Insert(0, current);
                }
                return path;
            }

            discoveredWaypoints.Remove(current);
            visitedWaypoints.Add(current);

            foreach (Waypoint neighbor in current.neighbors)
            {
                if (visitedWaypoints.Contains(neighbor))
                    continue;

                float pastCost = pastCosts[current] + CostHeuristic(current, neighbor);
                if (!discoveredWaypoints.Contains(neighbor))
                    discoveredWaypoints.Add(neighbor);
                else if (pastCost >= pastCosts[neighbor])
                    continue;

                originWaypoints[neighbor] = current;
                pastCosts[neighbor] = pastCost;
                totalCosts[neighbor] = pastCost + CostHeuristic(neighbor, target);
            }
        }

        return null;
    }
}
