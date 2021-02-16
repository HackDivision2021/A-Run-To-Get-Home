using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */

//display of directions
public enum InputDirection
{
    NULL,
    LEFT,
    RIGHT,
    UP,
    DOWN
}

//possible positions
public enum CurrentPosition
{
    LEFT,
    MIDDLE,
    RIGHT
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    InputDirection inputDirection;
    Vector3 currentMousePos;
    bool activeInput;
    CurrentPosition currentPosition;
    Vector3 xDirection;
    Vector3 moveDirection;
    CharacterController controller;
    float currentTime;
    float durationTime = 0.4f;
    

    //public variables
    public float horizontalSpeed = 3f;
    public AudioClip deadClip;
    public bool isRoll;

    public static PlayerController instance;
    //float jumpForce = 100f;
    //float gravity = 10f;


    void Start()
    {
        instance = this; //set player controller
        //StartCoroutine(UpdateAction());

        //default position is: middle
        currentPosition = CurrentPosition.MIDDLE;
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        //if player life is less than 0
        if (GameAttributes.instance.life <= 0)
        {
            //stop player
            speed = 0;
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayDead; //play death animation
            AudioSource.PlayClipAtPoint(deadClip, Camera.main.transform.position-Vector3.back*5); //death audio clip

            StartCoroutine(LoadFailScene());//game over menu
            return;
        }

        Move();

        moveDirection.z = speed;
        //moveDirection.y -= gravity * Time.deltaTime;

        controller.Move((xDirection * horizontalSpeed + moveDirection) * Time.deltaTime);

    }

    IEnumerator UpdateAction()
    {
        //update actions contantly
        while (true)
        {
            GetInputDirection();
            //PlayAnimation();
            yield return 0;
        }
    }

    //Input manager
    void GetInputDirection()
    {
        inputDirection = InputDirection.NULL;
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            currentMousePos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0) && activeInput)
        {
            Vector3 vec = Input.mousePosition - currentMousePos;
            if (vec.magnitude > 40)
            {
                float angleY = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.up)) * Mathf.Rad2Deg;
                float angleX = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.right)) * Mathf.Rad2Deg;
                
                activeInput = false;

                if (angleY <= 45)
                {
                    inputDirection = InputDirection.UP;
                }else if (angleY >= 135)
                {
                    inputDirection = InputDirection.DOWN;
                }

                if (angleX <= 45)
                {
                    inputDirection = InputDirection.RIGHT;
                }else if (angleX >= 135)
                {
                    inputDirection = InputDirection.LEFT;
                }
            }
        }
    }

    //Logic for moving player
    void Move()
    {
        inputDirection = InputDirection.NULL;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        //moving right
        if (x>0 && currentTime>durationTime)
        {
            inputDirection = InputDirection.RIGHT;
            MoveRight(); //execute move right method
            StartCoroutine(KeepStill()); //keep still method for 0.3s
            currentTime = 0;
            return;
        }else if (x < 0 && currentTime > durationTime) //moving left
        {
            inputDirection = InputDirection.LEFT;
            MoveLeft();//execute move left method
            StartCoroutine(KeepStill());//keep still method for 0.3s
            currentTime = 0;
            return;
        }else if (y > 0 && currentTime > durationTime)
        {
            //moving up or jump (currently not functional)
            inputDirection = InputDirection.UP; 
            currentTime = 0;
            //JumpUp();
            return;
        }
        else if (y < 0 && currentTime > durationTime) //rolling 
        {
            inputDirection = InputDirection.DOWN;
            Roll(); //exectute roll method
            currentTime = 0;
            return;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

    }

    //Keep still logic after actions 
    IEnumerator KeepStill()
    {
        yield return new WaitForSeconds(0.3f);//wait 0.3 seconds
        inputDirection = InputDirection.NULL; //no input registered
        xDirection = Vector3.zero;
    }


    /// <summary>
    /// animation testing
    /// </summary>
    /// 
    //animation manager
    void PlayAnimation()
    {
        //animation left
        if (inputDirection == InputDirection.LEFT)
        {
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnLeft;
        }
        //animation right
        else if (inputDirection == InputDirection.RIGHT)
        {
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnRight;
        }
        //jump (not functional)
        else if (inputDirection == InputDirection.UP)
        {
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayJumpUp;
        }
        //roll
        else if (inputDirection == InputDirection.DOWN)
        {
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayRoll;
        }
    }


    // Moving left logic
    void MoveLeft()
    {

        if (currentPosition == CurrentPosition.LEFT) return; //if player left dont let player move left

        //go left
        if (currentPosition == CurrentPosition.MIDDLE)
        {
            currentPosition = CurrentPosition.LEFT;
            if (transform.position.x <= 0.5f) xDirection=Vector3.zero;
        }
        //go middle
        else if (currentPosition == CurrentPosition.RIGHT)
        {
            if (transform.position.x <= 2.5f) xDirection = Vector3.zero;
            currentPosition = CurrentPosition.MIDDLE;
        }

        AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnLeft;
        xDirection = Vector3.left;
    }

    //Moving right logic
    void MoveRight()
    {
        if (currentPosition == CurrentPosition.RIGHT) return; //if player right dont let player move right

        //go right
        if (currentPosition == CurrentPosition.MIDDLE)
        {
            currentPosition = CurrentPosition.RIGHT;
        }
        //go middle
        else if (currentPosition == CurrentPosition.LEFT)
        {
            currentPosition = CurrentPosition.MIDDLE;
        }

        AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnRight;
        xDirection = Vector3.right;

    }

    //Rolling logic
    void Roll()
    {
        //roll animation
        AnimationManger.instance.animationHandler = AnimationManger.instance.PlayRoll;
    }

    //void JumpUp()
    //{
    //    if (controller.isGrounded && currentTime > durationTime)
    //    {
    //        if (AnimationManger.instance.animationHandler == AnimationManger.instance.PlayRun)
    //        {
    //            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayJumpUp;
    //            moveDirection.y += jumpForce;
    //            currentTime = 0;
    //        }else
    //        {
    //            currentTime += Time.deltaTime;
    //        }
    //    }
    //}

    /// <summary>
    /// load failure scene
    /// </summary>
    /// <returns></returns>
    /// 

    //display game over
    IEnumerator LoadFailScene()
    {
        //wait for 3 seconds, then display the game over
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Game_Over");
    }

}
