using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    private UIManager _uImanager;

    public GameObject player;
    private void Start()
    {
        _uImanager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, new Vector3(0,0,0), Quaternion.identity);
                gameOver = false;
                _uImanager.HideTitleScreen();
            }
        }
    }

}
