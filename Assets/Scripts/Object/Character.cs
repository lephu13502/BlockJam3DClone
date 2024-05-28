using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [HideInInspector] public Node node;

    bool isMoveable;
    bool inBarrier;
    bool isStacking;
    public List<Node> path;

    Color color;
    float speed;
    public bool Moveable
    {
        get
        {
            return isMoveable;
        }
        set
        {
            isMoveable = value;
            if (isMoveable) transform.localScale += transform.localScale / 2;
        }
    }

    public bool InBarrier
    {
        get
        {
            return inBarrier;
        }
        set
        {
            inBarrier = value;
            if (inBarrier)
            {
                Transform ninjaChild = gameObject.transform.Find("Ninja");
                if (ninjaChild != null)
                {
                    SkinnedMeshRenderer meshRenderer = ninjaChild.GetComponent<SkinnedMeshRenderer>();
                    if (meshRenderer != null)
                    {
                        meshRenderer.material.SetColor("_Color", Color.cyan);
                    }
                }
            }
            else
            {
                Transform ninjaChild = gameObject.transform.Find("Ninja");
                if (ninjaChild != null)
                {
                    SkinnedMeshRenderer meshRenderer = ninjaChild.GetComponent<SkinnedMeshRenderer>();
                    if (meshRenderer != null)
                    {
                        meshRenderer.material.SetColor("_Color", color);
                    }
                }
            }
        }
    }

    public Color Color
    {
        set
        {
            color = value;
        }
        get
        {
            return color;
        }
    }

    public bool IsStacking
    {
        get
        {
            return isStacking;
        }
        set
        {
            isStacking = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking", false);
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (path == null)
        {
            return;
        }

        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, path[0].worldPosition) > stoppingDistance)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        // Move towards the next node on the path
        Vector3 moveDirection = (path[0].worldPosition - transform.position).normalized;
        transform.forward = moveDirection;
        transform.position = Vector3.MoveTowards(transform.position, path[0].worldPosition, speed * Time.deltaTime);
        if (transform.position == path[0].worldPosition && !isStacking)
        {
            if (path[0].parent == node)
            {
                node.walkable = true;
                ObjectManager.Instance.CheckObjects(LevelManager.GetCurrentGrid());
            }
            if (path[0].gridY == 0)
            {
                path.Clear();
                StackManager.Instance.OrderStackNode(this);
                isStacking = true;
                speed *= 2;
            }
            else path.RemoveAt(0);
        }
        else if (transform.position == path[0].worldPosition)
        {
            path.Clear();
            StackManager.Instance.ControlStack();
        }

        if (path != null && path.Count == 0)
        {
            path = null;
        }
    }

}
