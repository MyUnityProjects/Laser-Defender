using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = 1.0f;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 250.0f;


    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z; //this claculates the distance between the game space and the camera - the z axis
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftMost.x + padding; // calculates the game space borders using the camera view port
        xMax = rightMost.x - padding; // added padding so that the player will not reach half way out of bounds
    }

    float xMin;
    float xMax;


    void Fire()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

	// Update is called once per frame
	void Update () {
        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0); // this makes the speed independent from the frame rate rendering time
            //long render time = greater spped and the other way around
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime; // this is the same as the the code to move left only a bit more readable
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("Fire", 0.00001f,firingRate); //this calls a function written as a string in order to controll the fire rate
        }
        if (Input.GetKeyUp(KeyCode.Space)) // this cancels the invokation of the fire method on key up
        {
            CancelInvoke("Fire");
        }

            //restrict player to game space

            float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
       
    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
