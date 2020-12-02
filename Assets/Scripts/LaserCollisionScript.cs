using UnityEngine;

public class LaserCollisionScript : MonoBehaviour
{
    public LaserController controller;
    static readonly object lockObj = new object();
    private void OnCollisionEnter(Collision collision) {
        lock (lockObj) {
            controller.TriggerCollision(collision);
        }
    }
}
