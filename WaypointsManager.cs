using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private static WaypointManager _instance;
    public static WaypointManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public GameObject[] waypoints;
    public GameObject player;
    int current = 0;
    public float speed;
    public float rotSpeed;
    float WPradius = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position)
                < WPradius)
        {
            current = Random.Range(0, waypoints.Length);
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }

        Vector3 waypointPos = waypoints[current].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, waypointPos,
                Time.deltaTime * speed);

        Quaternion goalRot = Quaternion.LookRotation(waypointPos - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRot,
                Time.deltaTime * rotSpeed);

    }

}
