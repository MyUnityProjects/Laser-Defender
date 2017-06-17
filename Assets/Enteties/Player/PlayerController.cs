using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = 1.0f; 


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

        //restrict player to game space

        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
       
    }
}
