using UnityEngine;

public abstract class Behavior : MonoBehaviour
{
    public abstract bool Evaluate();

    public abstract void Execute();
}
