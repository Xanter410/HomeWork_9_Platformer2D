using UnityEngine;

public interface IEnemyBehavior
{
    public void Execute();
}

public class AIController : MonoBehaviour
{
    private IEnemyBehavior[] _behaviors;
    private int length;

    private void Awake()
    {
        _behaviors = GetComponents<IEnemyBehavior>();
    }

    void Start()
    {
        length = _behaviors.Length;
    }

    void Update()
    {
        for (int i = 0; i < length; i++)
        {
            _behaviors[i].Execute();
        }
    }
}
