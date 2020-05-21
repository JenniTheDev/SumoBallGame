using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Enemy : MonoBehaviour {
    private Rigidbody enemyRb;
    private GameObject player;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;


    [SerializeField] private float speed = 3.0f;

    private void Awake() {
        SpawnManager.Instance.UpdateEnemyCount(1);
    }

    private void OnDestroy() {
        SpawnManager.Instance.UpdateEnemyCount(-1);
       
    }

    private void Start() {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        scoreText.text = "Score: " + score;


    }

    private void Update() {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y <= -10) {
            Destroy(gameObject);
            score++;
            scoreText.text = "Score: " + score;

        }
    }

    

}
