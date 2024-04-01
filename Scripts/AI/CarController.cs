using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviour
{
    NavMeshAgent agent;
    AgentRotateSmooth2d agentRotateSmooth2D;
    [Header("Car settings")]
    [SerializeField]public float driftFactor = 0.95f;
    [SerializeField]public float accelerationFactor = 10.0f;
    [SerializeField]public float turnFactor = 2.5f;
    [SerializeField]public float maxSpeed = 20;

    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp =0;

    Rigidbody2D carRigidbody2d;
    [SerializeField]private Vector2 movementVector;

    private void Awake()
    {
        carRigidbody2d = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agentRotateSmooth2D = GetComponent<AgentRotateSmooth2d>();
    
    }

    /*public void Move(Vector2 inputVector)
    {
        accelerationInput = agent.acceleration;
        steeringInput = agent.angularSpeed;
    }*/

    private void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
    }

    void ApplyEngineForce()
        {
            velocityVsUp = Vector2.Dot(transform.up, carRigidbody2d.velocity);

            if(velocityVsUp > maxSpeed && accelerationInput >0){
                return;
            }

            if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput > 0){
                return;
            }

            if(carRigidbody2d.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0){
                return;
            }

            if(accelerationInput==0)
            {
                carRigidbody2d.drag = Mathf.Lerp(carRigidbody2d.drag, 3.0f, Time.fixedDeltaTime);
            }else{
                carRigidbody2d.drag = 0;
            }

            Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

            carRigidbody2d.AddForce(engineForceVector, ForceMode2D.Force);

        }

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2d.velocity.magnitude/8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        carRigidbody2d.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2d.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2d.velocity, transform.right);

        carRigidbody2d.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector( Vector2 inputVector)
    {
        steeringInput = agentRotateSmooth2D.angularSpeed;
        accelerationInput = agent.acceleration;
    }
    

}
