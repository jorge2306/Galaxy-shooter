using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    private int enemyNumber=0;
    [SerializeField]
    private GameObject[] powerups;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Corutina que crea un nuevo enemigo cada 5 segundos
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
        
    }
    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }
    public void DecreaseEnemyNumber()
    {
        enemyNumber--;
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (enemyNumber<11 && _gameManager.gameOver == false)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8.0f, 8.0f), 6, 0), Quaternion.identity);
            enemyNumber++;
            if (Time.time < 30)
            {
                yield return new WaitForSeconds(6.0f);
            }else if (Time.time < 60)
            {
                yield return new WaitForSeconds(4.0f);
            }
            else
            {
                yield return new WaitForSeconds(2.0f);
            }                      
        }
    }
    IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-8.0f, 8.0f), 6, 0), Quaternion.identity);
            if (Time.time < 30)
            {
                yield return new WaitForSeconds(15.0f);
            }
            else if (Time.time < 60)
            {
                yield return new WaitForSeconds(10.0f);
            }
            else
            {
                yield return new WaitForSeconds(5.0f);
            }
        }
        
    }

}
