using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour {

    Rigidbody body;

    [SerializeField] private float speed;
    [SerializeField] private float firstThrust;

    [SerializeField] private float rotationSpeed;

    void Start()     {
        
        body = GetComponent<Rigidbody>();

        firstThrust = GameConstants.FIRST_THRUST;
        speed = GameConstants.BASE_SPEED;
        rotationSpeed = GameConstants.ROTATION_SPEED;
    }

    void Update() {

        ProcessInput();        
    }

    private void ProcessInput() {        
        
        ThrustProcess(); 
        RotationProcess();    
    }

    private void ThrustProcess() {

        speed = SetSpeed();
        ApplyThrust(speed);
    }

    private float SetSpeed() {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && speed < GameConstants.MAX_SPEED) {
            
            return (speed == GameConstants.BASE_SPEED) ? firstThrust : (speed * GameConstants.THRUST);

        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && speed > GameConstants.BASE_SPEED) {
            
            return (speed <= GameConstants.FIRST_THRUST) ? 0 : (speed / GameConstants.THRUST);
        }

        return 0;
    }

    private void ApplyThrust(float frameThrust) {
            
        body.AddRelativeForce(frameThrust * Time.deltaTime * Vector3.up);
    }

    private void RotationProcess() {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            
            ApplyRotation(rotationSpeed);
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float frameRotation) {
        
        body.freezeRotation = true;
        transform.Rotate(frameRotation * Time.deltaTime * Vector3.forward);
        body.freezeRotation = false;
    }
}
