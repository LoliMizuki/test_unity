using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        transform.Rotate( 0, 10*Time.deltaTime, 0 );
    }
}
