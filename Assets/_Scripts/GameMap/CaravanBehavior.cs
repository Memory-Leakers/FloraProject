using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanBehavior : MonoBehaviour
{

    private Vector2 dir;
    private Vector3 destination;
    public bool isMoving = false;
    public float movingSpeed = 1.0f;

    private GameManager _gameManager;

    public Camera camera;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        if (!isMoving)
            return;

        dir = destination - transform.position; // We update Dir, in case a frame rate problem happens, and the caravan goes further from the destination than predicted...
        dir.Normalize();

        if (Vector3.Distance(transform.position, destination) <= 0.1f) // ...which would cause this statement to return false.
        {
            isMoving = false;
            _gameManager.CaravanArrived();
            return;
        }

        transform.position += (Vector3)dir * Time.deltaTime * movingSpeed;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (camera.orthographicSize < 9)
                camera.orthographicSize++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (camera.orthographicSize > 4)
                camera.orthographicSize--;
        }
    }

    public void GoTo(Vector2 pos)
    {
        isMoving = true;
        destination = pos;
    }
}
