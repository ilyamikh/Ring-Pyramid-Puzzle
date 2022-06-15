using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public List<GameObject> pegs;
    private int currentSelection = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetSelection();
    }

    // Update is called once per frame
    void Update()
    {
        SelectPeg();
    }
    private void SelectPeg()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectNext();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectPrevious();
        }


    }
    private void SetSelection()
    {
        foreach(GameObject peg in pegs)
        {
            if (peg == pegs[currentSelection])
            {
                peg.GetComponent<PegBehavior>().SetSelected(true);
            }
            else
            {
                peg.GetComponent<PegBehavior>().SetSelected(false);
            }
        }
    }
    private void SelectNext()
    {
        currentSelection++;
        if (currentSelection >= pegs.Count)
            currentSelection = 0;

        SetSelection();
    }
    private void SelectPrevious()
    {
        currentSelection--;
        if (currentSelection < 0)
            currentSelection = pegs.Count - 1;

        SetSelection();
    }
}
