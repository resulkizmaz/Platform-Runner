using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimManager : MonoBehaviour
{
    public enum States { idle, run, victory }

    Animator _anim;

    int _idle;
    int _run;
    int _victory;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        _idle = Animator.StringToHash("idle");
        _run = Animator.StringToHash("run");
        _victory = Animator.StringToHash("victory");
    }

    public void AnimStates(AnimManager.States state)
    {
        switch (state)
        {
            case AnimManager.States.idle:
                _anim.SetBool(_idle, true);
                _anim.SetBool(_run, false);
                _anim.SetBool(_victory, false);
                break;
            case AnimManager.States.run:
                _anim.SetBool(_idle, false);
                _anim.SetBool(_run, true);
                _anim.SetBool(_victory, false);
                break;
            case AnimManager.States.victory:
                _anim.SetBool(_idle, false);
                _anim.SetBool(_run, false);
                _anim.SetBool(_victory, true);
                break;

        }
    }
}
