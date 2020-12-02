using UnityEngine;

public class LaserController {

    Laser laser;
    Vector3 direction;
    LaserManager laserManager = LaserManager.Instance;
    GameManager gameManager;
    GameObject lastCollision;
    private float timeAlive = 0f;
    public bool isClone = false;
    public LaserController() {
        gameManager = GameManager.instance;
        lastCollision = null;
        timeAlive = 0f;
        GetLaser();
    }
    private void GetLaser() {
        laser = new Laser();
        laser.gameObject = GameObject.Instantiate(gameManager.LaserPrefab);
        laser.collisionScript = laser.gameObject.GetComponent<LaserCollisionScript>();
        laser.collisionScript.controller = this;

    }
    public void Shoot(Vector3 pos, Vector3 dir) {
        laser.gameObject.transform.position = pos;
        direction = dir;
        laser.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.Cross(direction, Vector3.right), direction);
        laser.gameObject.SetActive(true);
    }
    public void Update() {
        if (laser.gameObject.activeSelf) {
            Move();
        }
        timeAlive += Time.deltaTime;
        if (timeAlive >= gameManager.LaserLife) {
            DestroyInstance();
        }

    }

    public void TriggerCollision(Collision collision) {
        if (collision.gameObject != lastCollision) {
            lastCollision = collision.gameObject;
            Vector3 copyDirection = Vector3.Reflect(direction, collision.gameObject.transform.up);
            for (int i = 0; i < gameManager.CollisionCopies && laserManager.MagazineLoaded; i++) {
                float spread = gameManager.CopiesSpread;
                Vector3 randomizedDirection = Quaternion.Euler(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread)) * copyDirection;
                LaserController tempController = Clone();
                tempController.Shoot(collision.GetContact(0).point, randomizedDirection);
            }
            DestroyInstance();
        }
    }

    private void Move() {
        laser.gameObject.transform.Translate(direction * gameManager.LaserSpeed, Space.World);
    }

    //Because I use clone the life of the lasers stay the same for all copies from the same original shot
    private LaserController Clone() {
        LaserController controllerCopy = (LaserController)this.MemberwiseClone();
        //clone refrences here if needed
        controllerCopy.GetLaser();

        //Attach to LaserManager
        laserManager.AttachController(controllerCopy);
        return controllerCopy;
    }

    private void DestroyInstance() {
        GameObject.Destroy(laser.gameObject);
        laserManager.DettachController(this);
    }
}
