using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed;
    public float lifeTime;
    // public GameObject deathParticle;
    void Start() {
        Destroy(gameObject, lifeTime);
    }

    void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col) {
        Destroy(gameObject);
        // Destroy(Instantiate(deathParticle, transform.position, Quaternion.identity), 0.3f);
    }
}
