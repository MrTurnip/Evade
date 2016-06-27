using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Movement animation
    public Animation Walk;
    // Idle animation
    public Animation Idle;
    // Destination point
    public Vector3 endPoint;

    // Flag to check if the user clicked / tapped
    private bool flag = false;
    
    // Default z axis Player position
    private float defZ;

    // Duration to reach the destination
    public float speed = 50.0f;

    // Use this for initialization
    void Start () {
        // Set the constant z axis position
        defZ = gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        // Check if a location on the screen has been tapped / clicked
        if ((Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved)) || Input.GetMouseButton(0))
        {
            // Declare a var of RaycastHit struct
            RaycastHit2D hit;
            // For Unity Editor
#if UNITY_EDITOR
            Vector3 clickPos = Input.mousePosition;
            // For Mobile Devices
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
            Vector3 clickPos = Input.GetTouch(0).position;
#endif
            // Detect on screen click position
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(clickPos);

            // Perform Raycast
            hit = Physics2D.Raycast(screenPos, Vector2.zero);

            // Check if Raycast has hit anything
            if (hit)
            {
                // Set the bolean to true to indicate movement
                flag = true;
                // Save the the click / tap position
                endPoint = hit.point;
                // We keep the same z axis as we don't want the Player to run off the screen
                endPoint.z = defZ;
            }
        }

        // Check if the flag for movement is true and the current Player position is not same as the clicked / tapped position
        if (flag && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            
            // Move the Player
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1 / (speed * (Vector3.Distance(gameObject.transform.position, endPoint))));
        }
        // Checking if the Player position doesn't equal to endPoint's position
        else if (flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            
            // Set the flag for movement to false
            flag = false;
        }

    }
}
