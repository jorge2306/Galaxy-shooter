using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
       
{
    [SerializeField]
    private int powerupID; //0-Triple shot, 1- Speed boost, 2-Shield
    [SerializeField]
    private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupID == 0)
        {
            transform.Translate(new Vector3(0, -1.5f, 0) * Time.deltaTime);
        }else if (powerupID == 1)
        {
            transform.Translate(new Vector3(0, -1.5f, 0) * Time.deltaTime);
        }
        else if (powerupID == 2)
        {
            transform.Translate(new Vector3(0, -1.5f, 0) * Time.deltaTime);
        }

        
        if (transform.position.x > 10 || transform.position.y < -6 ||transform.position.x<-10)
        {
            //Si sale de la pantalla destruimos el powerup
            Destroy(this.gameObject);
        }
    }
    //Detector de colisiones
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player nave = other.GetComponent<Player>();
            //Activamos el powerup
            
            if (powerupID == 0)
            {
                nave.TripleShotPowerupOn();
            }else if (powerupID == 1)
            {
                nave.SpeedPowerupOn();
            }
            else if (powerupID == 2)
            {
                nave.EnableShields();
            }
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            //Destruimos el powerup
            Destroy(this.gameObject);
        }
        
    }
}
