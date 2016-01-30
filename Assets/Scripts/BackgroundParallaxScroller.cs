using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class BackgroundLayer
{
    [Range(0.0f, 1.0f)]
    public float ScrollSpeed;

    public Transform Layer;
}

[RequireComponent(typeof(Camera))]
public class BackgroundParallaxScroller : MonoBehaviour
{
    public BackgroundLayer[] BackgroundLayers;

    private Vector3 _previousCameraPosition;
    private Camera _camera;

    protected void Awake()
    {
        _camera = GetComponent<Camera>();
        _previousCameraPosition = _camera.transform.position;
    }

    protected void Update()
    {
        Vector3 deltaPos = _previousCameraPosition - _camera.transform.position;
        deltaPos.z = 0.0f;
        foreach (BackgroundLayer layer in BackgroundLayers)
        {
            Vector3 layerpos = layer.Layer.position;
            layerpos += deltaPos * layer.ScrollSpeed;
            layer.Layer.position = layerpos;
        }
        _previousCameraPosition = _camera.transform.position;
    }
}
