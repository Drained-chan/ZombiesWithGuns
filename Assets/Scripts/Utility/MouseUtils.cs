using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//global class for dealing with the mouse
public static class MouseUtils
{
    //cache the camera for faster access
    private static Camera camera = null;

    /// <summary>
    /// Displays an error in the console and halts the game if in editor. If not in editor, does nothing.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void CrashAndBurn(object message)
    {
#if UNITY_EDITOR
        Debug.LogError(message);
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    /// <summary>
    /// This function attempts to set the cached camera to a given object.
    /// If none is specified, attempts to fall back to the object tagged with MainCamera.
    /// If multiple MainCameras exist, errors to the console and halts the editor.
    /// </summary>
    /// <param name="cameraObj">Object to replace the camera with. If none is specified, falls back to default camera.</param>
    private static void SetCamera(GameObject cameraObj)
    {
        //only apply override if one was specified
        if (cameraObj)
        {
            Camera cam = cameraObj.GetComponent<Camera>();
            if (cam)
            {
                camera = cam;
            }
            else
            {
                Debug.LogWarning("Camera object override specified for GetWorldMousePos that didn't actually have a camera! (Object name: "
                    + cameraObj.name + ")");
            }
        }

        //ensure we have a camera

        if (!camera)
        {
            //fallback to searching for camera by the tag
            GameObject[] fallbackCameraCandidates = GameObject.FindGameObjectsWithTag("MainCamera");
            GameObject fallbackCamera = null;

            if (fallbackCameraCandidates.Length > 1)
            {
                Debug.LogWarning("Multiple cameras tagged with MainCamera found in scene! There may be unexpected behavior. All cameras found listed below:");
                foreach (GameObject camCandidate in fallbackCameraCandidates)
                {
                    Debug.LogWarning(camCandidate.name);
                }
            }
            else if (fallbackCameraCandidates.Length == 0)
            {
                CrashAndBurn("No camera override was specified, and no camera in scene tagged with MainCamera could be found!");
            }
            else
            {
                fallbackCamera = fallbackCameraCandidates[0];
            }

            Camera cam = fallbackCamera.GetComponent<Camera>();
            if(!cam)
            {
                CrashAndBurn("No camera was attached to the main camera object! Camera object: " + fallbackCamera.name);
            }
            camera = cam;
        }

    }

    /// <summary>
    /// Gets the mouse position as if it were projected into world space on the given plane.
    /// </summary>
    /// <param name="planeOrigin">An object along the plane that the camera should be able to hit. It is assumed the target plane is perpindicular to the camera.</param>
    /// <param name="cameraOverride"></param>
    /// <returns></returns>
    public static Vector3 GetWorldMousePos(Vector3 planeOrigin, GameObject cameraOverride = null)
    {
        SetCamera(null);

        Camera localCam = camera;

        if(cameraOverride)
        {
            Camera cam = cameraOverride.GetComponent<Camera>();
            if(cam)
            {
                localCam = cam;
            }
            else
            {
                Debug.LogWarning("Object " + cameraOverride.name + " was passed in as a camera override, but it isn't a camera.");
            }
        }

        Vector3 cameraDirection = MathUtils.Vector.FromRotation(camera.transform.rotation);
        Plane projectionSurface = new Plane(cameraDirection, planeOrigin);

        return camera.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x, 
            Input.mousePosition.y,
            -projectionSurface.GetDistanceToPoint(camera.transform.position)));
    }

    /// <summary>
    /// Get the main scene camera if one exists.
    /// </summary>
    /// <returns></returns>
    public static Camera GetMainCamera()
    {
        SetCamera(null);
        return camera;
    }
}
