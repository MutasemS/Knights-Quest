using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Patrol : MonoBehaviour
{

    public Transform[] waypoints;
    public float detectionRange = 10f;
    private bool isChasing = false;

    public Transform target;
    int cur = 0;
    public float speed = 0.001f;
    private SpriteRenderer enemySprite;
    private bool isFacingRight = false;
    Seeker seeker;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public float nextWaypointDistance = 3f;

    Rigidbody2D rb;

    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        Debug.Log("Path found. Error: " + p.error);
        if (!p.error)
        {
            path = p;

            currentWaypoint = 0;
        }
    }


    void Update()
    {
        {
            if (Vector2.Distance(transform.position, target.position) < detectionRange)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }

            if (isChasing)
            {
                chase();
            }
            else
            {
                onPatrol();
            }
        }
    }

    void chase()
    {
        if (path == null)
            return;
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

        Vector2 force = direction * 700 * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemySprite.flipX = false;
        }
        else if (force.x <= -0.01f)
        {
            enemySprite.flipX = true;
        }
    }

    void onPatrol()
    {
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else
        {
            cur = (cur + 1) % waypoints.Length;
        }

        Vector2 dir = waypoints[cur].position - transform.position;

        if (dir.x <= -0.01f && isFacingRight)
        {
            FlipSpriteX();
        }
        if (dir.x >= 0.01f && !isFacingRight)
        {
            FlipSpriteX();
        }

    }

    private void FlipSpriteX()
    {
        enemySprite.flipX = !enemySprite.flipX;
        isFacingRight = !isFacingRight;
    }


}