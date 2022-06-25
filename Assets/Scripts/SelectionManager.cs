using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public List<GameObject> pegs;
    public GameObject[] ringPrefabs = new GameObject[9];
    private int currentSelection = 0;
    GameObject selectedPeg;
    public GameObject activeRing;
    [SerializeField] TextMeshProUGUI moveCountText;
    private int moves = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetCurrentSelection(true);
        LoadRings();
        LogState();
        moveCountText.text = "Moves: " + moves;
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
                else
                {
                    if (IsLowerGauge(selectedPeg.GetComponent<PegBehavior>().rings.Peek()))
                    {
                        MoveActiveRing();
                    }
                }
            }
            else
            {
                if (activeRing)
                {
                    MoveActiveRing();
                }
            }
        }
    }
    private bool IsLowerGauge(GameObject otherRing)
    {
        return activeRing.GetComponent<RingBehavior>().ring.CanStack(otherRing.GetComponent<RingBehavior>().ring);
    }
    private void MoveActiveRing()
    {
        //ABSTRACTION
        moves++;
        moveCountText.text = "Moves: " + moves;
        GameObject fromPeg = activeRing.GetComponent<RingBehavior>().currentPeg;
        GameObject movingRing = fromPeg.GetComponent<PegBehavior>().rings.Pop();
        movingRing.GetComponent<RingBehavior>().StackToPeg(selectedPeg);
        activeRing.GetComponent<RingBehavior>().DeactivateGhost();
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
        ShowGhost();

    }
    private void ShowGhost()
    {
        if (activeRing)
        {
            activeRing.GetComponent<RingBehavior>().ActivateGhost(selectedPeg);
            
            if(activeRing.GetComponent<RingBehavior>().currentPeg == selectedPeg)
            {
             activeRing.GetComponent<RingBehavior>().DeactivateGhost();
            }
        }


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
        activeRing.GetComponent<RingBehavior>().DeactivateGhost();
        activeRing = null;
    }

    private void LogState()
    {
        string peg = selectedPeg.GetComponent<PegBehavior>().pegPosition.ToString();
        int rings = selectedPeg.GetComponent<PegBehavior>().rings.Count;
        Debug.Log(peg + " peg, " + rings + " rings.");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
}
