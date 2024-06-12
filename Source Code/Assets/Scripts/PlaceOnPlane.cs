using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    private ARRaycastManager _arRaycastManager;
    private GameObject _spawnedObject;
    private bool _heartPlaced = false; // Flag to track if heart is placed
    public GameObject heartPrefab;
    public float moveAmount = 0.1f; // Amount to move the heart up or down

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (!_heartPlaced && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (_arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = hits[0].pose;

                    if (_spawnedObject == null)
                    {
                        _spawnedObject = Instantiate(heartPrefab, hitPose.position, hitPose.rotation);
                        _heartPlaced = true; // Set the flag to true after placing heart
                    }
                }
            }
        }
    }

    public GameObject GetSpawnedObject()
    {
        return _spawnedObject;
    }

    public void ResetHeartPosition()
    {
        if (_spawnedObject != null)
        {
            Destroy(_spawnedObject); // Destroy the current heart object
            _spawnedObject = null; // Reset the spawned object reference
            _heartPlaced = false; // Reset the heart placed flag
        }
    }

    public void MoveHeartUp()
    {
        if (_spawnedObject != null)
        {
            _spawnedObject.transform.position += Vector3.up * moveAmount;
        }
    }

    public void MoveHeartDown()
    {
        if (_spawnedObject != null)
        {
            _spawnedObject.transform.position -= Vector3.up * moveAmount;
        }
    }
}
