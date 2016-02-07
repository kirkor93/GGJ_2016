using System;
using UnityEngine;

[Serializable]
public struct CameraBounds
{
    public float XMin;
    public float XMax;
    public float YMin;
    public float YMax;
}

public class CameraFollow : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public Transform BottomLeftLevelBound;
    public Transform UpperRightLevelBound;
    public float MaxCameraSize = 1030.0f;
    public float MinPlayerDistance;
    public float MaxPlayerDistance;

    [Range(0.0f, 1.0f)]
    public float PositionUpdateSpeed;

    private Camera _camera;
    private float _startCameraSize;

    protected void Awake()
    {
        _camera = GetComponent<Camera>();
        _startCameraSize = _camera.orthographicSize;
    }

    protected void LateUpdate()
    {
        //moving camera
        Vector3 targetPos = (Player1.transform.position + Player2.transform.position)/2.0f;
        targetPos.x = Mathf.Clamp(targetPos.x,
            BottomLeftLevelBound.transform.position.x + _camera.orthographicSize*_camera.aspect,
            UpperRightLevelBound.transform.position.x - _camera.orthographicSize*_camera.aspect);
        targetPos.y = Mathf.Clamp(targetPos.y,
            BottomLeftLevelBound.transform.position.y + _camera.orthographicSize,
            UpperRightLevelBound.transform.position.y - _camera.orthographicSize);
        targetPos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, targetPos, PositionUpdateSpeed);

        //scaling camera
        float lerpFactor = Mathf.InverseLerp(MinPlayerDistance, MaxPlayerDistance,
            (Player1.transform.position - Player2.transform.position).magnitude);
        _camera.orthographicSize = Mathf.Lerp(_startCameraSize, MaxCameraSize, lerpFactor);
    }
}
