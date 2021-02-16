using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class AnimationManger : MonoBehaviour
{
    //Public variables
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

    //functions to play animations by getting the parameters
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
            PlayerController.instance.isRoll = false;
        }
        else
        {
            PlayerController.instance.isRoll = true;
        }
    }

    public void PlayRun() => animation.Play(run.name);
}
