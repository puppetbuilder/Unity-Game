using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 20f;
    public float panBoarderThickness = 10f;
    public Vector2 panLimit;

    public float scrollSpeed = 2f;
    public float minY = 20f;
    public float maxY = 120f;

	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoarderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += scroll * 100f * Time.deltaTime;

        if (pos.y < 50)
            transform.rotation.x = 45f;
        else
            Quaternion.Euler(60, 0, 0);


        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
	}
     
}
