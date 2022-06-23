using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public List<GameObject> pegs;
    [SerializeField] GameObject[] ringPrefabs = new GameObject[9];
    private int currentSelection = 0;
    GameObject selectedPeg;
    public GameObject activeRing;
    // Start is called before the first frame update
    void Start()
    {
        SetCurrentSelection(true);
        LoadRings();
        LogState();
    }

    // Update is called once per frame
    void Update()
    {
        GetAction();

    }
    private void GetAction()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectNext();
            LogState();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectPrevious();
            LogState();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(selectedPeg.GetComponent<PegBehavior>().rings.Count > 0)
            {
                if (!activeRing)
                {
                    ActivateRing();
                }
                else if(activeRing == selectedPeg.GetComponent<PegBehavior>().rings.Peek())
                {
                    DeactivateRing();
                }
            }
            else
            {
                Debug.Log("Move Triggered.");
                if (activeRing)
                {
                    MoveActiveRing();
                }
            }
        }
    }
    private void MoveActiveRing()
    {

        GameObject fromPeg = activeRing.GetComponent<RingBehavior>().currentPeg;
        GameObject movingRing = fromPeg.GetComponent<PegBehavior>().rings.Pop();
        movingRing.GetComponent<RingBehavior>().StackToPeg(selectedPeg);
        activeRing = null;
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
        selectedPeg = pegs[currentSelection];
    }

    private void LoadRings()
    {
        for (int i = 0; i < ringPrefabs.Length; i++)
        {
            Instantiate(ringPrefabs[i]).GetComponent<RingBehavior>().StackToPeg(selectedPeg);      
        }
    }
    private void ActivateRing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activeRing = selectedPeg.GetComponent<PegBehavior>().rings.Peek();
            activeRing.transform.Translate(Vector3.up * 0.50f);
        }
    }

    private void DeactivateRing()
    {
        activeRing.transform.Translate(Vector3.down * 0.50f);
        activeRing = null;
    }

    private void LogState()
    {
        string peg = selectedPeg.GetComponent<PegBehavior>().pegPosition.ToString();
        int rings = selectedPeg.GetComponent<PegBehavior>().rings.Count;
        Debug.Log(peg + " peg, " + rings + " rings.");
    }
}
