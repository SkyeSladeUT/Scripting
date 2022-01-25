using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraManager : MonoBehaviour
{
    public Transform CameraPivot;

    private TestInputManager _manager;
    private Transform CameraTransform;
    private Transform targetTransform;

    public LayerMask CollisionLayers;
    public float CameraCollisionRadius = .2f, CollisionOffset = .2f, MinimumCollisionOffset = .2f;
    public float MinPivotAngle = -35, MaxPivotAngle = 35;
    public float CameraFollowSpeed = .2f;
    public float CameraLookSpeed = 2, CameraPivotSpeed = 2;

    private float LookAngle, PivotAngle;
    private Vector3 camFollowVelocity = Vector3.zero, _cameraVectorPosition, rotation;
    private Quaternion targetRotation;
    private float defaultPosition;

    private void Awake()
    {
        targetTransform = FindObjectOfType<TestPlayerManager>().transform;
        _manager = FindObjectOfType<TestInputManager>();
        CameraTransform = Camera.main.transform;
        defaultPosition = CameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref camFollowVelocity, CameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        LookAngle = LookAngle + (_manager.camInputX * CameraLookSpeed);
        PivotAngle = PivotAngle - (_manager.camInputY * CameraPivotSpeed);
        PivotAngle = Mathf.Clamp(PivotAngle, MinPivotAngle, MaxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = LookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = PivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        CameraPivot.localRotation = targetRotation;

    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = CameraTransform.position - CameraPivot.position;
        direction.Normalize();
        if(Physics.SphereCast(CameraPivot.position, CameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), CollisionLayers))
        {
            float distance = Vector3.Distance(CameraPivot.position, hit.point);
            targetPosition =- (distance - CollisionOffset);
        }

        if(Mathf.Abs(targetPosition) < MinimumCollisionOffset)
        {
            targetPosition = targetPosition - MinimumCollisionOffset;
        }

        _cameraVectorPosition.z = Mathf.Lerp(CameraTransform.localPosition.z, targetPosition, .2f);
        CameraTransform.localPosition = _cameraVectorPosition;
    }


}
