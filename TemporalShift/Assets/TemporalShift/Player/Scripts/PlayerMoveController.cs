using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private LayerMask collisionLayer;
    private InputController inputController;
    private CapsuleCollider collider;
    private Bounds capsuleBounds;
    private Vector3 boxCheckSize;
    private Rigidbody rb;

    private Vector3 targetVelocity;
    public bool hitGround = true;
    public bool hitCeiling = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<CapsuleCollider>();
        inputController = GetComponent<InputController>();
    }

    private void FixedUpdate()
    {
        CheckCollision();
        HandleHorizontal();
        HandleVertical();
        HandleGravity();
        Move();
    }

    /// <summary>
    /// Check for player collision with a ground or with a ceiling
    /// </summary>
    private void CheckCollision()
    {
        capsuleBounds = collider.bounds;
        boxCheckSize = capsuleBounds.extents * 0.9f;
        hitGround = Physics.BoxCast(capsuleBounds.center, boxCheckSize, Vector3.down,
            transform.rotation, stats.GroundCheckDistance, collisionLayer);

        hitCeiling = Physics.BoxCast(capsuleBounds.center, boxCheckSize, Vector3.up,
            transform.rotation, stats.GroundCheckDistance, collisionLayer);


        ExtDebug.DrawBoxCastBox(capsuleBounds.center, boxCheckSize, transform.rotation, Vector3.down,
            stats.GroundCheckDistance, Color.red);
        ExtDebug.DrawBoxCastBox(capsuleBounds.center, boxCheckSize, transform.rotation, Vector3.up,
            stats.GroundCheckDistance, Color.red);
    }

    /// <summary>
    /// Calculate horizontal velocity of player
    /// </summary>
    private void HandleHorizontal()
    {
        if (inputController.Move.x == 0)
        {
            targetVelocity.x = Mathf.MoveTowards(targetVelocity.x, 0, stats.Deceleration * Time.fixedDeltaTime);
        }
        else
        {
            targetVelocity.x = Mathf.MoveTowards(targetVelocity.x, stats.MaxSpeed * inputController.Move.x,
                stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Calculate vertical velocity of player
    /// </summary>
    private void HandleVertical()
    {
        if (inputController.JumpBuffer > 0 && hitGround)
        {
            hitGround = false;
            targetVelocity.y = stats.JumpForce;
        }

        if (hitCeiling)
        {
            targetVelocity.y = 0;
        }
    }

    /// <summary>
    /// Calculate gravity.
    /// Also apply on ground to ensure the player stick the ground
    /// </summary>
    private void HandleGravity()
    {
        if (!hitGround)
        {
            targetVelocity.y = Mathf.MoveTowards(targetVelocity.y, stats.FallMaxSpeed,
                stats.FallAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            targetVelocity.y = -1.5f;
        }
    }

    private void Move()
    {
        rb.velocity = targetVelocity;
    }
}