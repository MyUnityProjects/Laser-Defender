using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10.0f;
    public float height = 5.0f;

    private bool movingRight = false;
    public float speed = 5.0f;
    private float xMin;
    private float xMax;
    public float padding = 0.3f;

    // Use this for initialization
    void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint (new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

        xMax = rightBoundry.x - padding;
        xMin = leftBoundry.x + padding;

        foreach (Transform child in transform) // loop to spawn an enemy in every position
        {   //chil.transform.position refers to the location of the enemy spawn which is the location of the 'position we created in the prefabs'

            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject; // this function spawns what ever i want at the begining, it takes 3 args:
                                                                                                                      // 1. the object to spawn (made it public at the top hance drag and drop in the editor, a vector3, and a Quaternion which is the roatation)
            enemy.transform.parent = child; //this maket the enemy spawn within the enemy formation game object.
            //the after equal transform refers to this script which is under enemy foramation
        }

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }


    // Update is called once per frame
    void Update () {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime; //exactly the same as the lower one just other syntax
        } else {
            transform.position += new Vector3(-speed * Time.deltaTime, 0);
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xMin)
        {
            movingRight = true;
        } else if (rightEdgeOfFormation > xMax)
        {
            movingRight = false;
        }
    }
}
