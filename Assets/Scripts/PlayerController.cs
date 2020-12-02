using UnityEngine;

public class PlayerController : MonoBehaviour {
    GameManager gameManager;
    LaserManager laserManager = LaserManager.Instance;
    Vector2 mouseMovement;
    // Start is called before the first frame update
    void Start() {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update() {
        mouseMovement = gameManager.MouseSensitivity * Time.deltaTime * new Vector2((gameManager.InvertMouse ? 1 : -1) * Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        transform.Rotate(Vector3.up, mouseMovement.y, Space.World);
        transform.Rotate(Vector3.right, mouseMovement.x, Space.Self);

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            laserManager.CreateLaser(transform.position - Vector3.up * 0.5f, transform.forward);
            Debug.Log("Shot laser!");
        }

    }
}
