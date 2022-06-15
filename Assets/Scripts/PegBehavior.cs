using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegBehavior : MonoBehaviour
{
    public enum Position {Starting, Middle, Ending};
    [SerializeField] Position pegPosition;
    private bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSelected(bool selection)
    {
        isSelected = selection;
        if(isSelected)
            Debug.Log(pegPosition + " peg selected.");
    }
}
