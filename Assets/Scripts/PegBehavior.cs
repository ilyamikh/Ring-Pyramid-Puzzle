using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegBehavior : MonoBehaviour
{
    public enum Position {Starting, Middle, Ending};
    public Position pegPosition;
    [SerializeField] GameObject selectedIndicator;
    public Stack<GameObject> rings = new Stack<GameObject>();
    private bool isSelected = false;
    SelectionManager selectionManager;
    // Start is called before the first frame update
    void Start()
    {
        selectionManager = GameObject.Find("Selection Manager").GetComponent<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSelected(bool selection)
    {
        isSelected = selection;
        if (isSelected)
        {
            selectedIndicator.GetComponent<MovePegSelector>().SetXPosition(gameObject.transform.position.x);
        }
    }
}
