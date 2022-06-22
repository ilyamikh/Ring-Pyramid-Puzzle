using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public List<GameObject> pegs;
    [SerializeField] GameObject[] ringPrefabs = new GameObject[9];
    private int currentSelection = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetCurrentSelection(true);
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
    private void SelectNext()
    {
        SetCurrentSelection(false);
        currentSelection++;
        if (currentSelection >= pegs.Count)
            currentSelection = 0;

        SetCurrentSelection(true);
    }
    private void SelectPrevious()
    {
        SetCurrentSelection(false);
        currentSelection--;
        if (currentSelection < 0)
            currentSelection = pegs.Count - 1;

        SetCurrentSelection(true);
    }

    private void SetCurrentSelection(bool selection)
    {
        pegs[currentSelection].GetComponent<PegBehavior>().SetSelected(selection);
    }
}
