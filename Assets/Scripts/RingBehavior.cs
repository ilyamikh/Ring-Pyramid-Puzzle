using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBehavior : MonoBehaviour
{
    [SerializeField] int inputGauge;
    public Ring ring;
    private Ghost ghostRing;
    public GameObject currentPeg;
    private float ringThickness = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        ring = new Ring(inputGauge);
        ghostRing = new Ghost(ring.Gauge());
        ring.peg = currentPeg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Ring
    {
        protected readonly int gauge;
        public GameObject peg { set; get; }

        public Ring(int size)
        {
            gauge = size;
        }

        public int Gauge()
        {
            return gauge;
        }
        public bool CanStack(Ring other)
        {
            return other.gauge < gauge;
        }
    }

    public class Ghost : Ring
    {
        public GameObject ghostRing;
        public Ghost(int size) : base(size)
        {
            ghostRing = Instantiate(GameObject.Find("Selection Manager").GetComponent<SelectionManager>().ringPrefabs[base.gauge-1]);
            ghostRing.SetActive(false);
        }

        public void Show(GameObject peg)
        {
            Stack<GameObject> rings = peg.GetComponent<PegBehavior>().rings;
            if(rings.Count == 0)
            {
                ColorValid();
                SetPosition(peg.transform.position);
            }
            else
            {
                SetValid(rings.Peek().GetComponent<RingBehavior>().ring);
                SetPosition(peg);
            }
            ghostRing.SetActive(true);
        }

        public void Hide()
        {
            ghostRing.SetActive(false);
        }

        private void SetValid(Ring other)
        {
            if (base.CanStack(other))
            {
                ColorValid();
            }
            else
            {
                ColorInvalid();
            }
        }

        private void ColorValid()
        {
            ghostRing.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f);
        }

        private void ColorInvalid()
        {
            ghostRing.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
        }
        
        private void SetPosition(Vector3 pegPosition)
        {
            ghostRing.transform.position = new Vector3(
                pegPosition.x,
                0.5f,
                0.0f
                );
        }

        private void SetPosition(GameObject peg)
        {
            ghostRing.transform.position = new Vector3(
                peg.transform.position.x, 
                (peg.GetComponent<PegBehavior>().rings.Count + 1) * 0.5f,
                0
                );
        }
    }

    public void ActivateGhost(GameObject peg)
    {
        ghostRing.Show(peg);
    }
    public void DeactivateGhost()
    {
        ghostRing.Hide();
    }

    public void StackToPeg(GameObject peg)
    {
        currentPeg = peg;
        currentPeg.GetComponent<PegBehavior>().rings.Push(gameObject);
        gameObject.transform.position = new Vector3(currentPeg.transform.position.x,
            (currentPeg.GetComponent<PegBehavior>().rings.Count) * ringThickness, 0);
    }
}
