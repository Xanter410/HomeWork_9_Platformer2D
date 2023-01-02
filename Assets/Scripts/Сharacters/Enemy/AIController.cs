using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private Behavior[] _behaviors;
    private int length;

    void Start()
    {
        length = _behaviors.Length;
    }

    void Update()
    {
        for (int i = 0; i < length; i++)
        {
            if (_behaviors[i].Evaluate() == true)
            {
                _behaviors[i].Execute();
                break;
            }
        }
    }
}
