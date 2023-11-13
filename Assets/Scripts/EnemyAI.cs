using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float wanderRadius = 3f;
    public float wanderTimer = 5f;
    public float maxX = 6f;
    public float maxY = 6f;
    public float nextWaypointDistance = 3f;

    private float timer;
    private bool isChasing = false;
    private Vector3 wanderTarget;
    private Seeker seeker;
    private Rigidbody2D rb;
    public Transform enemyGFX;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        timer = wanderTimer;
        GetNewWanderTarget();
    }

    void FixedUpdate()
    {
        LimitPosition();
        if (isChasing)
        {
            Chase();
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {

                isChasing = false;
                timer = wanderTimer;
                GetNewWanderTarget();
            }
        }
        else
        {
            Wander();
            timer = wanderTimer;
        }
    }

    void Wander()
    {
        if (Vector3.Distance(transform.position, target.position) < 5f)
        {
            isChasing = true;
            return;
        }


        if (Vector3.Distance(transform.position, wanderTarget) < 0.1f)
        {
            GetNewWanderTarget();
        }
        Vector3 direction = (wanderTarget - transform.position).normalized;
        Vector2 force = new Vector2(direction.x, direction.y) * speed * Time.deltaTime;
        rb.AddForce(force);


        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void GetNewWanderTarget()
    {
        wanderTarget = new Vector3(
            Mathf.Clamp(transform.position.x + Random.Range(-wanderRadius, wanderRadius), -maxX, maxX),
            Mathf.Clamp(transform.position.y + Random.Range(-wanderRadius, wanderRadius), -maxY, maxY),
            transform.position.z
        );
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            Debug.Log("Path generated successfully!");
        }
        else
        {
            Debug.LogError("Path generation failed: " + p.errorLog);
        }
    }

    void Chase()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;


        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);


        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }




    void LimitPosition()
    {
        // Limit the enemy's position to the specified bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -maxX, maxX),
            Mathf.Clamp(transform.position.y, -maxY, maxY),
            transform.position.z
        );
    }
}
