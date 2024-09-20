
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class MyCharacter : MonoBehaviour
{
    private Animator _animator;

    private int _hash_ValueX = Animator.StringToHash("ValueX");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.SetBool("IsWalk", true);
        _animator.SetFloat("ValueX", 0.2f);
        _animator.SetFloat(_hash_ValueX,0.2f);
    }
}
