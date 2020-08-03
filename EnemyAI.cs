using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
    {
    [SerializeField]
    private float _speed=3;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    private UIManager _uImanager;
    [SerializeField]
    private AudioClip _clip;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 6, 0);
        _uImanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 6, 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        
        if (other.CompareTag("Player"))
        {
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            _spawnManager.DecreaseEnemyNumber();
            Destroy(this.gameObject);
            player.Damage();

        }
        //Comentario
        else if (other.tag == "Laser")
        {
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (_uImanager != null)
            {
                _uImanager.UpdateScore();
            }
            _spawnManager.DecreaseEnemyNumber();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
