using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePegSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = new Color(1.0f, 0.92f, 0.016f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetXPosition(float xCoordinate)
    {
        Vector3 newPos = new Vector3(xCoordinate, gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = newPos;
    }
}
