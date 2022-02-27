using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    Rigidbody2D RB;

    public float speed = 1f;

    public GameObject deathScreen;

    public float minPosX;
    public float maxPosX;
    public float minPosY;
    public float maxPosY;

    public Vector3 spawnPosition;
    public GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {      
        // if player decides to be smart and tries to move the virus outside the boundry destroy the virus and add a new one :)
        if ((transform.position.x <= minPosX || transform.position.x >= maxPosX) || (transform.position.y <= minPosY || transform.position.y >= maxPosY)){
            Instantiate(enemy, spawnPosition, transform.rotation);
            Destroy(gameObject);
        }
    }

    // player moves enemy
    void OnMouseDrag(){
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 playerPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = playerPos;
    }

    // enemy random movement
    void FixedUpdate(){
        Vector2 Movement = new Vector2 (Random.Range(-1, 1), Random.Range(-1, 1));
        RB.AddForce(Movement * speed);  
    }

    // collistions 
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag != "Man"){
            speed *= -1;
        }else{
            Time.timeScale = 0;
            deathScreen.SetActive(true);
        }
    }
}
