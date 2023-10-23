using UnityEngine;

/// <summary>
/// Class responsible for handling camera movement and zooming.
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 startRotation;

    const float minHeight = 3.5f;
    const float maxHeight = 16f;
    const float radius = 5f;
    const float movementSpeed = 0.5f;
    const float distanceToSpeedMultiplier = 0.01f;
    const float scrollToZoomMultiplier = 0.8f;

    float lastTime;
    float oneFrameStopTime = 0;
    bool directMovementControl = false;
    bool dampingEnabled = true;
    Vector3 lastPosition;
    Vector3 swipeStartPos;
    Vector3 sphereCoord;
    Vector3 speed;

    void Awake()
    {
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
        sphereCoord = CoordinatesConverter.GetSphericalCoordinates(transform.position);
        var cart = CoordinatesConverter.GetCartesianCoordinates(sphereCoord);
        Input.simulateMouseWithTouches = false;
    }

    void Update()
    {
        if (Time.time == oneFrameStopTime)
            return;

        CameraMovement();

        HandlePlayerInput();

        // update the last mouse position
        lastPosition = ScreenToWorldPosition(Input.mousePosition);
    }

    Vector3 ScreenToWorldPosition(Vector3 screenPosition)
    {
        return new Vector3((screenPosition.x / Camera.main.pixelWidth) * 1920,
                            (screenPosition.y / Camera.main.pixelHeight) * 1080,
                            0);
    }

    void HandleZooming()
    {
        float deltaDistance = Input.mouseScrollDelta.y * scrollToZoomMultiplier;
        sphereCoord.x = Mathf.Clamp(sphereCoord.x + deltaDistance, minHeight, maxHeight);
    }

    void HandlePlayerInput()
    {
        /* Whenever the left mouse button is pressed, the mouse cursor's position
         and current time is remembered */
        if (Input.GetMouseButtonDown(0))
        {
            lastTime = Time.time;
            swipeStartPos = lastPosition = ScreenToWorldPosition(Input.mousePosition);
            directMovementControl = true;
        }
        // while the left mouse button is pressed, we manually calculate the camera's speed
        if (directMovementControl)
        {
            speed = (lastPosition - ScreenToWorldPosition(Input.mousePosition)) / Time.deltaTime * movementSpeed * distanceToSpeedMultiplier;
        }
        // when user releases left mouse button, gesture is ended
        if (Input.GetMouseButtonUp(0))
        {
            directMovementControl = false;
            // calculate time from gesture start to now
            float time = Time.time - lastTime;
            // and remember current time in lastTime variable (used for damping effect)
            lastTime = Time.time;
            /* our speed is a vector from start to end cursor's position
             divided by the gesture duration (vector length is proportional to speed) */
            speed = (swipeStartPos - ScreenToWorldPosition(Input.mousePosition)) / time * movementSpeed * distanceToSpeedMultiplier;
        }
    }

    void CameraMovement()
    {
        HandleZooming();
        if (speed.magnitude > 0)
        {
            // get the deltas that describe how much the mouse cursor got moved between frames
            float dx = speed.x;
            float dy = speed.y;
            float dampingFactor = 1f;

            if (dampingEnabled)
            {
                dampingFactor = DampingMultiplier(Time.time - lastTime);
            }
            if (!directMovementControl)
            {
                // use DampingMultiplier to smoothly deaccelerate camera movement
                dx *= dampingFactor;
                dy *= dampingFactor;
            }

            // update camera's posiiton
            if (dx != 0f || dy != 0f)
            {
                // rotate the camera
                sphereCoord.y -= dx * Time.deltaTime;
                // and prevent it from turning upside down (1.5f = approx. Pi / 2)
                sphereCoord.z = Mathf.Clamp(sphereCoord.z - dy * Time.deltaTime, 0.01f, 1.5f);

            }
        }
        // calculate the cartesian coordinates for Unity
        transform.position = CoordinatesConverter.GetCartesianCoordinates(sphereCoord) + target.transform.position;

        // make the camera look at the target
        transform.LookAt(target.transform.position);
    }

    /* This function calculates damping multiplier as logarithmic function (1/x)
     which is modified in such a way that its value in 0 is ~1 and value above 1 is 0 */
    static float DampingMultiplier(float t)
    {
        float Val = 1 / (t * 10 + 1) - 1 / 11.0f;
        Val = Mathf.Clamp(Val, 0, 1);
        return Val;
    }

    /// <summary>
    /// Class responsible for converting between Cartesian and spherical coordinates.
    /// </summary>
    class CoordinatesConverter : MonoBehaviour
    {
        /* A point in Cartesian space can be represented in spherical space,
        it is done by describing it using a radius of the sphere and two angles. */
        public static Vector3 GetSphericalCoordinates(Vector3 cartesian)
        {
            float x = cartesian.x;
            float y = cartesian.y;
            float z = cartesian.z;

            float radius = cartesian.magnitude;

            float phi = Mathf.Atan2(x, z);
            float theta = Mathf.Acos(y / radius);

            return new Vector3(radius, phi, theta);
        }

        public static Vector3 GetCartesianCoordinates(Vector3 spherical)
        {
            float radius = spherical.x;
            float phi = spherical.y;
            float theta = spherical.z;

            Vector3 result;
            var sinPhiRadius = Mathf.Sin(theta) * radius;

            result.x = sinPhiRadius * Mathf.Sin(phi);
            result.y = Mathf.Cos(theta) * radius;
            result.z = sinPhiRadius * Mathf.Cos(phi);

            return result;
        }
    }
}
