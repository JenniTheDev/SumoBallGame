using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    private Rigidbody enemyRb;
    private GameObject player;
    private int score = 0;
    [SerializeField] public Text ScoreText;

    [SerializeField] private float speed = 3.0f;

    private void Awake() {
        SpawnManager.Instance.UpdateEnemyCount(1);
    }

    private void OnDestroy() {
        SpawnManager.Instance.UpdateEnemyCount(-1);
        SetScoreText();
    }

    private void Start() {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        
    }

    private void Update() {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if(transform.position.y <= -10) {
            Destroy(gameObject);
            score++;
            SetScoreText();
        }
    }

void SetScoreText() {
        ScoreText.text = "Score: " + score.ToString();
    }
}