using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManger : MonoBehaviour
{
    public delegate void AnimationHandler();
    public AnimationHandler animationHandler;

    private Animation animation;
    public AnimationClip
        dead,
        jumpDown,
        jumpLoop,
        jumpUp,
        roll,
        run,
        turnLeft,
        turnRight;

    public static AnimationManger instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        animation = GetComponent<Animation>();
        animationHandler = PlayRun;
    }

    // Update is called once per frame
    void Update()
    {
        //animation.Play(run.name);
        if (animationHandler != null)
        {
            animationHandler.Invoke();
        }

    }

    public void PlayDead() => animation.Play(dead.name);
    public void PlayJumpDown() => animation.Play(jumpDown.name);
    public void PlayJumpLoop() => animation.Play(jumpLoop.name);
    public void PlayJumpUp() => animation.Play(jumpUp.name);

    public void PlayTurnRight()
    {
        animation.Play(turnRight.name);
        if(animation[turnRight.name].normalizedTime >= 0.95f)
        {
            animationHandler = PlayRun;
        }
    }
    public void PlayTurnLeft() {
        animation.Play(turnLeft.name);
        if (animation[turnLeft.name].normalizedTime >= 0.95f)
        {
            animationHandler = PlayRun;
        }
    }
    public void PlayRoll() {
        animation.Play(roll.name);
        if (animation[roll.name].normalizedTime >= 0.95f)
        {
            animationHandler = PlayRun;
        }
    }

    public void PlayRun() => animation.Play(run.name);
}
