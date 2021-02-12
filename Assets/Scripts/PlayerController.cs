using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum InputDirection
{
    NULL,
    LEFT,
    RIGHT,
    UP,
    DOWN
}

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
    public AudioClip deadClip;
    public bool isRoll;

    //public variables
    public float horizontalSpeed = 3f;

    public static PlayerController instance;
    //float jumpForce = 100f;
    //float gravity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //StartCoroutine(UpdateAction());
        currentPosition = CurrentPosition.MIDDLE;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameAttributes.instance.life <= 0)
        {
            speed = 0;
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayDead;
            AudioSource.PlayClipAtPoint(deadClip, Camera.main.transform.position-Vector3.back*5);

            StartCoroutine(LoadFailScene());
            return;
        }

        Move();

        moveDirection.z = speed;
        //moveDirection.y -= gravity * Time.deltaTime;

        controller.Move((xDirection * horizontalSpeed + moveDirection) * Time.deltaTime);

    }

    IEnumerator UpdateAction()
    {
        while (true)
        {
            GetInputDirection();
            //PlayAnimation();
            yield return 0;
        }
    }

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

    void Move()
    {
        inputDirection = InputDirection.NULL;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x>0 && currentTime>durationTime)
        {
            inputDirection = InputDirection.RIGHT;
            MoveRight();
            StartCoroutine(KeepStill());
            currentTime = 0;
            return;
        }else if (x < 0 && currentTime > durationTime)
        {
            inputDirection = InputDirection.LEFT;
            MoveLeft();
            StartCoroutine(KeepStill());
            currentTime = 0;
            return;
        }else if (y > 0 && currentTime > durationTime)
        {
            inputDirection = InputDirection.UP;
            currentTime = 0;
            //JumpUp();
            return;
        }
        else if (y < 0 && currentTime > durationTime)
        {
            inputDirection = InputDirection.DOWN;
            Roll();
            currentTime = 0;
            return;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

    }

    IEnumerator KeepStill()
    {
        yield return new WaitForSeconds(0.3f);
        inputDirection = InputDirection.NULL;
        xDirection = Vector3.zero;
    }


    /// <summary>
    /// animation testing
    /// </summary>
    void PlayAnimation()
    {
        if (inputDirection == InputDirection.LEFT)
        {
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnLeft;
        }
        else if (inputDirection == InputDirection.RIGHT)
        {
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnRight;
        }
        else if (inputDirection == InputDirection.UP)
        {
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayJumpUp;
        }
        else if (inputDirection == InputDirection.DOWN)
        {
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayRoll;
        }
    }

    void MoveLeft()
    {

        if (currentPosition == CurrentPosition.LEFT) return;

        if (currentPosition == CurrentPosition.MIDDLE)
        {
            currentPosition = CurrentPosition.LEFT;
            if (transform.position.x <= 0.5f) xDirection=Vector3.zero;
        }
        else if (currentPosition == CurrentPosition.RIGHT)
        {
            if (transform.position.x <= 2.5f) xDirection = Vector3.zero;
            currentPosition = CurrentPosition.MIDDLE;
        }

        AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnLeft;
        xDirection = Vector3.left;
    }

    void MoveRight()
    {
        if (currentPosition == CurrentPosition.RIGHT) return;
        
        if (currentPosition == CurrentPosition.MIDDLE)
        {
            currentPosition = CurrentPosition.RIGHT;
        }
        else if (currentPosition == CurrentPosition.LEFT)
        {
            currentPosition = CurrentPosition.MIDDLE;
        }

        AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnRight;
        xDirection = Vector3.right;

    }

    void Roll()
    {
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
    IEnumerator LoadFailScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Game_Over");
    }

}
