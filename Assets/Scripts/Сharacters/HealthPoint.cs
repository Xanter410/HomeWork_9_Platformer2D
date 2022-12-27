using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class HealthPoint : MonoBehaviour
{
    [SerializeField] private int _maxValue = 1;
    private Animator _animator;
    private int _currentValue;
    private static readonly int _deadedAnimator = Animator.StringToHash("deaded");
    private static readonly int _takeDamageAnimator = Animator.StringToHash("takeDamage");


    public int MaxValue => _maxValue;
    public int CurrentValue => _currentValue;
    public bool IsAlive => _currentValue > 0;

    private event UnityAction _onValueChanged;
    public event UnityAction OnValueChanged
    {
        add { _onValueChanged += value; }
        remove { _onValueChanged -= value; }
    }
    private event UnityAction _onDeaded;
    public event UnityAction OnDeaded
    {
        add { _onDeaded += value; }
        remove { _onDeaded -= value; }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        ChangeValue(_maxValue);
    }
    public void Increment(int modifier)
    {
        ChangeValue(_currentValue + Mathf.Abs(modifier));
    }

    public void Decrement(int modifier)
    {
        ChangeValue(_currentValue - Mathf.Abs(modifier));
        CheckIsDeaded();
    }

    public void Dead()
    {
        ChangeValue(0);
    }

    private void ChangeValue(int newValue)
    {
        _currentValue = Mathf.Clamp(newValue, 0, _maxValue);
        _onValueChanged?.Invoke();
    }

    private void CheckIsDeaded()
    {
        if (_currentValue <= 0) 
        {
            _animator.SetBool(_deadedAnimator, true);
            _onDeaded?.Invoke();
        }
        else
        {
            _animator.SetBool(_takeDamageAnimator, true);
        }
    }
}
