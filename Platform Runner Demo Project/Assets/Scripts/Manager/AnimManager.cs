using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public enum AnimationStates { Idle, Run, Win };

    private Animator _animator;

    //Animator Variables
    private int _isIdle;
    private int _isRun;
    private int _isWin;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        //Animator parameter reference
        _isRun = Animator.StringToHash("run");
        _isWin = Animator.StringToHash("win");
        _isIdle = Animator.StringToHash("idle");
    }


    public void SetAnimationState(AnimManager.AnimationStates state)
    {
        switch (state)
        {
            case AnimManager.AnimationStates.Idle:
                _animator.SetBool(_isRun, false);
                _animator.SetBool(_isIdle, true);
                _animator.SetBool(_isWin, false);
                break;

            case AnimManager.AnimationStates.Run:
                _animator.SetBool(_isRun, true);
                _animator.SetBool(_isIdle, false);
                _animator.SetBool(_isWin, false);
                break;

            case AnimManager.AnimationStates.Win:
                _animator.SetBool(_isWin, true);
                _animator.SetBool(_isRun, false);
                _animator.SetBool(_isIdle, false);
                break;

            default:
                break;
        }
    }
}
