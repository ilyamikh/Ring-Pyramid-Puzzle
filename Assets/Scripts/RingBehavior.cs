using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBehavior : MonoBehaviour
{
    [SerializeField] int inputGauge;
    public Ring ring;
    // Start is called before the first frame update
    void Start()
    {
        ring = new Ring(inputGauge);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Ring
    {
        readonly int gauge;

        public Ring(int size)
        {
            gauge = size;
        }

        public bool CanStack(Ring other)
        {
            return other.gauge > gauge;
        }
    }
}
