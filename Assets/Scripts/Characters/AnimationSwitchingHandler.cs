using UnityEngine;

public class AnimationSwitchingHandler
{
    private Animator _animator;
    private string _currentAnimationName;

    public AnimationSwitchingHandler(Animator animator)
    {
        _animator = animator;
    }

    public void ChangeAnimation(string animationName)
    {
        if(animationName == _currentAnimationName)
            return;

        _animator.Play(animationName);
        _currentAnimationName = animationName;
    }
}
