using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour {

    //Sorry for all the variables, was trying to learn how to use the Editor
    //Controls settings
    #region Controls settings
    [SerializeField]
    private bool invertMouse = false;
    public bool InvertMouse { get => invertMouse; }
    [SerializeField]
    private float mouseSensitivity = 60f;
    public float MouseSensitivity { get => mouseSensitivity; }
    #endregion

    //Laser settings
    #region Laser settings
    [SerializeField]
    private float laserSpeed;
    public float LaserSpeed { get => laserSpeed; }
    [SerializeField]
    private float laserLife;
    public float LaserLife { get => laserLife; }
    [SerializeField]
    private float copiesSpread;
    public float CopiesSpread { get => copiesSpread; }
    [SerializeField]
    private int collisionCopies;
    public int CollisionCopies { get => collisionCopies; }
    [SerializeField]
    private GameObject laserPrefab;
    public GameObject LaserPrefab { get => laserPrefab; }

    [SerializeField]
    private int maxNumOfLasers = 10;
    public int MaxNumOfLasers { get => maxNumOfLasers; }
    #endregion

    //singleton
    public static GameManager instance;
    LaserManager laserManager;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        laserManager = LaserManager.Instance;
    }
    void Start() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update() {
        laserManager.Update();
        Debug.Log(laserManager.laserControllers.Count);
    }
}
//Design patterns here:
public class LaserManager {
    internal List<LaserController> laserControllers = new List<LaserController>();

    //Singleton creator
    #region Singleton
    private static LaserManager instance;
    public static LaserManager Instance {
        get {
            if (instance == null) {
                instance = new LaserManager();
                return instance;
            }
            else {
                return instance;
            }
        }
    }
    //private constructor
    private LaserManager() {
    }
    #endregion
    public void CreateLaser(Vector3 pos, Vector3 dir) {
        if (MagazineLoaded) {
            LaserController laserController = new LaserController();
            AttachController(laserController);
            laserController.Shoot(pos, dir);
        }
    }

    //Observer
    #region Observer
    public void Update() {
        foreach (LaserController controller in laserControllers.ToArray()) {
            controller.Update();
        }
    }
    public void AttachController(LaserController controller) {
        laserControllers.Add(controller);
    }
    public void DettachController(LaserController controller) {
        laserControllers.Remove(controller);
    }
    public bool MagazineLoaded { get => laserControllers.Count<GameManager.instance.MaxNumOfLasers; }
    #endregion
}
public struct Laser {
    public GameObject gameObject;
    public LaserCollisionScript collisionScript;
}


