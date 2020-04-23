
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody playerRb;
    private GameObject focalPoint;

    [SerializeField]
    private float powerupStrength = 15.0f;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private bool hasPowerup = false;

    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start(){

        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");

    }

    // Update is called once per frame
    void Update() {

        float moveForward = Input.GetAxis("Vertical");
        //rigid bodies don't need time.detla time, they have own 
        playerRb.AddForce(focalPoint.transform.forward * moveForward * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Powerup")) {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }

    }

    // Co routine yeild waits 7 seconds and does whatever is after it
    IEnumerator PowerupCountdownRoutine() {

        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);

    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.CompareTag("Enemy") && hasPowerup) {

            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Player has collided with: " + collision.gameObject.name );

        }
    }
}
