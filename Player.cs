using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uImanager;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private GameObject _ExplosionPrefab;

    //la cadencia de disparo es 0.5f
    [SerializeField]
    public float _fireRate = 0.5f;
    private float _canFire = 0;
    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private bool shieldEnabled = false;

    public bool canTripleShot = false;
    public int vidas = 3;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject[] _engines;
    private int _hitCount = 0;

    private void Start()
    {
        //Posicion inicial
        transform.position = new Vector3(0, -3, 0);
        _speed = 8.0f;
        _hitCount = 0;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uImanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uImanager != null)
        {
            _uImanager.UpdateLives(vidas);
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Movement();
        ShootLaser();


    }
    private void ShootLaser()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Time.time > _canFire)
            {
                _audioSource.Play();
                if (canTripleShot == false)
                {
                    //Spawn del laser
                    Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.94f, 0), Quaternion.identity);
                }
                else
                {
                    GameObject tripleshot = (GameObject)Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
                    Destroy(tripleshot, 2.0f);

                }
                _canFire = Time.time + _fireRate;
            }

        }
    }


    private void Movement()
    {
        //Controles
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * _speed * horizontalinput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalinput * Time.deltaTime);

        //Límites del Player
        if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        if (transform.position.x > 8)
        {
            transform.position = new Vector3(8, transform.position.y, 0);
        }
        if (transform.position.x < -8)
        {
            transform.position = new Vector3(-8, transform.position.y, 0);
        }
    }
    public void Damage()
    {
        
        if (shieldEnabled == false)
        {
            _hitCount++;
            vidas--;
            if (_hitCount == 1)
            {
                _engines[0].SetActive(true);
            }
            else if (_hitCount==2)
            {
                _engines[1].SetActive(true);
            }
            if (vidas < 1)
            {
                Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
                _gameManager.gameOver = true;
                _uImanager.ShowTitleScreen();
                Destroy(this.gameObject);

            }
        }
        else
        {
            _shieldGameObject.SetActive(false);
            shieldEnabled = false;
        }
        
        _uImanager.UpdateLives(vidas);
        
    }
    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
    public void SpeedPowerupOn()
    {
        _speed = 15.0f;
        StartCoroutine(SpeedPowerDownRoutine());
    }
    public IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = 8.0f; ;
    }
    public void EnableShields()
    {
        _shieldGameObject.SetActive(true);
        shieldEnabled = true;
    }
   
}