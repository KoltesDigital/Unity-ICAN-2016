using UnityEngine;
using System.Collections.Generic;

public class PlayerBehavior : MonoBehaviour
{
    public GraphManager graphManager;
    public Waypoint currentWaypoint;
    public float lerpRatio = 5.0f;
    public float reachThreshold = 0.1f;
    List<Waypoint> path;

    void Start()
    {
        transform.position = currentWaypoint.transform.position;
    }

	void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Waypoint waypoint = hit.collider.GetComponent<Waypoint>();
                if (waypoint != null)
                {
                    path = graphManager.GetPath(currentWaypoint, waypoint);
                }
            }
        }

        if (path != null &&
            path.Count > 0 &&
            Vector3.Distance(transform.position, currentWaypoint.transform.position) < reachThreshold)
        {
            currentWaypoint = path[0];
            path.RemoveAt(0);
        }

        transform.position = Vector3.Lerp(transform.position, currentWaypoint.transform.position, lerpRatio * Time.deltaTime);
	}
}
