using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //radius around the player where the camera should be centered
    [SerializeField] private float mouseDeadzone = 10.2f;
    //distance the camera should move when looking at the screen edges
    [SerializeField] private Vector2 mouseLookDistance = new Vector2(2.0f, 3.0f);
    //force camera is moved with when being panned
    [SerializeField] private Vector2 mouseLookSpeed = new Vector2(0.02f, 0.02f);

    //how much to zoom the camera out as it moves away from the player (0 = no zooming)
    [SerializeField] private float mouseViewScaling = 0.07f;

    //how much to scale the intensity of incoming camera shakes by
    [SerializeField] private float cameraShakeIntensityMultiplier = 1.0f;
    //how much to scale the duration of incoming camera shake by
    [SerializeField] private float cameraShakeDurationMultiplier = 1.0f;

    //mouse tracking
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 targetMouseDelta;

    //camera shake
    //how many more seconds to shake the camera
    private float cameraShakeDuration = 0.0f;
    //the intensity of the current shake
    private float cameraShakeIntensity = 0.0f;
    private Vector2 cameraShakeDelta = Vector2.zero;


    //camera zoom
    private float baseCameraSize;

    private Camera camera;

    // Start is called before the first frame update
    private void Start()
    {
        camera = GetComponent<Camera>();
        baseCameraSize = camera.fieldOfView;
    }

    private void TickShake()
    {
        if(cameraShakeDuration <= 0)
        {
            cameraShakeDuration = 0;
        }
        else
        {
            cameraShakeDuration -= Time.deltaTime;

            //undo shake from last frame (this prevents camera from drunk walking away from the action)
            transform.Translate(-1 * cameraShakeDelta);
            cameraShakeDelta = Random.insideUnitCircle * cameraShakeIntensity;
            //apply new shake
            transform.Translate(cameraShakeDelta);
        }
    }

    private void FollowMouse()
    {
        //vector pointing to mouse
        //this is not portable but good enough for us
        Vector3 mouseVec = MouseUtils.GetWorldMousePos(new Vector3(0, 0, 0)) - transform.position;
        
        if(mouseVec.magnitude < mouseDeadzone)
        {
            targetMouseDelta = Vector2.zero;
        }
        else
        {
            targetMouseDelta = mouseLookDistance * new Vector2(mouseVec.x, mouseVec.y).normalized;
        }

        Vector2 moveVec = (targetMouseDelta - currentMouseDelta) * mouseLookSpeed;
        currentMouseDelta += moveVec;
        transform.Translate(moveVec.x, moveVec.y, 0);

        //update camera FOV
        //TODO: consider supporting relative camera size adjustment instead of forced size
        camera.fieldOfView = baseCameraSize * (1 + currentMouseDelta.magnitude * mouseViewScaling);
    }

    // Update is called once per frame
    private void Update()
    {
        FollowMouse();
        TickShake();
    }
}
