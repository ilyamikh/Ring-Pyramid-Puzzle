using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBehavior : MonoBehaviour
{
    [SerializeField] int inputGauge;
    public Ring ring;
    public GameObject currentPeg;
    private float ringThickness = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        ring = new Ring(inputGauge);
        ring.peg = currentPeg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Ring
    {
        readonly int gauge;
        public GameObject peg { set; get; }

        public Ring(int size)
        {
            gauge = size;
        }

        public bool CanStack(Ring other)
        {
            return other.gauge > gauge;
        }
    }

    public void StackToPeg(GameObject peg)
    {        
        currentPeg = peg;
        currentPeg.GetComponent<PegBehavior>().rings.Push(gameObject);
        gameObject.transform.position = new Vector3(currentPeg.transform.position.x,
            (currentPeg.GetComponent<PegBehavior>().rings.Count) * ringThickness, 0);
    }
}
