using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [Tooltip("Player max velocity on horizontal axis")] [SerializeField]
    private float maxSpeed;

    public float MaxSpeed => maxSpeed;

    [Tooltip("Value at which player attains max horizontal velocity")] [SerializeField]
    private float acceleration;

    public float Acceleration => acceleration;

    [Tooltip("Speed at which player loose velocity")] [SerializeField]
    private float deceleration;

    public float Deceleration => deceleration;


    [Tooltip("Player instant jump force")] [SerializeField]
    private float jumpForce;

    public float JumpForce => jumpForce;

    [Tooltip("Distance to check if ground")] [SerializeField]
    private float groundCheckDistance;

    public float GroundCheckDistance => groundCheckDistance;
    
    [Tooltip("Player Max Speed when falling")] [SerializeField]
    private float fallMaxSpeed;

    public float FallMaxSpeed => fallMaxSpeed;
    
    [Tooltip("Speed at which player attains max speed when falling")] [SerializeField]
    private float fallAcceleration;

    public float FallAcceleration => fallAcceleration;
    
   
}