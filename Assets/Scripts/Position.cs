using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1); // this pust a wire circle around an object that will be instantiated on play
    }

}
