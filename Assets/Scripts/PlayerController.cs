using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/***
 * record the button the user inputs
 */
public enum InputDirection
{
    NULL,
    LEFT,
    RIGHT,
    UP,
    DOWN
}

/***
 * record the current position of the player
 */
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
    CurrentPosition fromPosition;

    Vector3 xDirection;
    Vector3 moveDirection;

    public CharacterController controller;
    float currentTime;
    float durationTime = 0.4f;
    public AudioClip deadClip;
    public bool isRoll;
    public KeyBindManager keyBindManager;

    //public variables
    public float horizontalSpeed = 3f;

    public static PlayerController instance;
    public float jumpForce = 8f;
    public float gravity = 20f;

    //Health Attributes
    [Header("Health Attributes")]
    public int health;
    public Slider slider;
    public AudioClip hitObstacle;

    //Inventory Attributes
    [Header("Inventory Attributes")]
    public int coinAmount;
    public int diamondAmount;
    public Text coinText;
    public Text diamondText;
    public GameObject coin;
    public GameObject diamond;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(UpdateAction());
        currentPosition = CurrentPosition.MIDDLE;
        controller = GetComponent<CharacterController>();

        //assigns diamond and coin amount at start
        coinAmount = 0;
        diamondAmount = 0;

        // assigns health value to the health bar slider
        health = 5;
        slider.value = health;
        StartCoroutine(UpdateAction());

    }

    // Update is called once per frame
    void Update()
    {
        // updates slider value according to health
        slider.value = health;

        // whether can control player based on the life
        if(health <= 0)
        {
            speed = 0;
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayDead;
            AudioSource.PlayClipAtPoint(deadClip, Camera.main.transform.position-Vector3.back*5);

            StartCoroutine(LoadFailScene());
            return;
        }

        #region WebGL control
        //Move();
        #endregion

        moveDirection.z = speed;

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move((xDirection * horizontalSpeed + moveDirection) * Time.deltaTime);

    }


    // function to detect player collision and decrese player health
    private void OnTriggerEnter(Collider other)
    {
        //if player collides with an object with Tag "Obstacle" (barriers and cars)
        //and he is rolling, he doesn't lose any life points else if he is not rolling
        //or the object Tag is "Barrier", life bar is decreased by 1 and hitObstacle 
        //sound is played. "Obstacle" and "Barrier" are destroyed on collision either way
        if (other.CompareTag("Obstacle") && isRoll)
        {
            AudioSource.PlayClipAtPoint(hitObstacle, this.transform.position);
            Destroy(other.gameObject);
        } 
        else if ((other.CompareTag("Obstacle") && !isRoll) || (other.CompareTag("Barrier")))
        {
            health--;
            slider.value = health;
            AudioSource.PlayClipAtPoint(hitObstacle, this.transform.position);
            Destroy(other.gameObject);
        }

        //if player collides with an object with Tah "Enemy" (police officers)
        //he automatically loses the game.
        if (other.CompareTag("Enemy"))
        {
            health = 0;
            slider.value = health;
        }

        //Records coins amount on collision with them
        //if coinAmount == 0, then coin item is kept hidden
        //otherwise it turns active and counter keeps counting
        if (other.CompareTag("Coin") && coinAmount == 0)
        {
            coin.SetActive(true);
            coinAmount++;
            coinText.text = "x" + coinAmount;
        } 
        else if (other.CompareTag("Coin"))
        {            
            coinAmount++;
            coinText.text = "x" + coinAmount;
        }
        //Records diamonds amount on collision with them
        //if diamondAmount == 0, then diamond item is kept hidden
        //otherwise it turns active and counter keeps counting
        if (other.CompareTag("Diamond") && diamondAmount == 0)
        {
            diamond.SetActive(true);
            diamondAmount++;
            diamondText.text = "x" + diamondAmount;
        }
        else if (other.CompareTag("Diamond"))
        {            
            diamondAmount++;
            diamondText.text = "x" + diamondAmount;
        }
    }

    public void SetHealth(int newHealth)
    {
        slider.value = newHealth;
        health = newHealth;
    }

    IEnumerator UpdateAction()
    {
        while (true)
        {
            GetInputDirection();
            //PlayAnimation();
            MoveLeftRight();
            MoveForward();
            yield return 0;
        }
    }

    /// <summary>
    /// Mobile Control: get the direction of input
    /// </summary>
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
            if (vec.magnitude > 20)
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

    /// <summary>
    /// WebGL Control: move method
    /// </summary>
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

    /// <summary>
    /// move left method
    /// </summary>
    void MoveLeft()
    {

        if (currentPosition == CurrentPosition.LEFT) return;

        if (currentPosition == CurrentPosition.MIDDLE)
        {
            currentPosition = CurrentPosition.LEFT;
            fromPosition = CurrentPosition.MIDDLE;
            if (transform.position.x <= 0.5f) xDirection=Vector3.zero;
        }
        else if (currentPosition == CurrentPosition.RIGHT)
        {
            currentPosition = CurrentPosition.MIDDLE;
            fromPosition = CurrentPosition.RIGHT;
            if (transform.position.x <= 2.5f) xDirection = Vector3.zero;
        }

        AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnLeft;
        xDirection = Vector3.left;
    }

    /// <summary>
    /// move rght method
    /// </summary>
    void MoveRight()
    {
        if (currentPosition == CurrentPosition.RIGHT) return;
        
        if (currentPosition == CurrentPosition.MIDDLE)
        {
            currentPosition = CurrentPosition.RIGHT;
            fromPosition = CurrentPosition.MIDDLE;
        }
        else if (currentPosition == CurrentPosition.LEFT)
        {
            currentPosition = CurrentPosition.MIDDLE;
            fromPosition = CurrentPosition.LEFT;
        }

        AnimationManger.instance.animationHandler = AnimationManger.instance.PlayTurnRight;
        xDirection = Vector3.right;

    }

    void MoveLeftRight()
    {
        if (inputDirection == InputDirection.LEFT)
        {
            MoveLeft();
        }else if (inputDirection == InputDirection.RIGHT)
        {
            MoveRight();
        }

        if (currentPosition == CurrentPosition.LEFT)
        {
            if (transform.position.x < -3f)
            {
                xDirection=Vector3.zero;
                transform.position = new Vector3(-3f, transform.position.y, transform.position.z);
            }
        }

        if (currentPosition == CurrentPosition.RIGHT)
        {
            if (transform.position.x > 3f)
            {
                xDirection = Vector3.zero;
                transform.position = new Vector3(3f, transform.position.y, transform.position.z);
            }
        }

        if (currentPosition == CurrentPosition.MIDDLE)
        {
            if (fromPosition == CurrentPosition.LEFT && transform.position.x > 0)
                xDirection = Vector3.zero;
            if (fromPosition == CurrentPosition.RIGHT && transform.position.x < 0)
                xDirection = Vector3.zero;
        }
    }

    void MoveForward()
    {
        if (inputDirection == InputDirection.DOWN)
        {
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayRoll;
        }
        if (controller.isGrounded)
        {
            moveDirection = Vector3.zero;

            if (AnimationManger.instance.animationHandler != AnimationManger.instance.PlayJumpUp
                && AnimationManger.instance.animationHandler != AnimationManger.instance.PlayTurnLeft
                && AnimationManger.instance.animationHandler != AnimationManger.instance.PlayTurnRight
                && AnimationManger.instance.animationHandler != AnimationManger.instance.PlayDead
                && AnimationManger.instance.animationHandler != AnimationManger.instance.PlayRoll)
            {
                AnimationManger.instance.animationHandler = AnimationManger.instance.PlayRun;
            }
            if (inputDirection == InputDirection.UP)
            {
                Jump();
            }
        }
        else
        {
            if (AnimationManger.instance.animationHandler != AnimationManger.instance.PlayJumpUp)
            {
                AnimationManger.instance.animationHandler = AnimationManger.instance.PlayJumpLoop;
            }
        }
    }

    void Jump()
    {
        AnimationManger.instance.animationHandler = AnimationManger.instance.PlayJumpUp;
        moveDirection.y += jumpForce;
        if (controller.isGrounded)
            AnimationManger.instance.animationHandler = AnimationManger.instance.PlayRun;
    }

    /// <summary>
    /// roll method
    /// </summary>
    void Roll()
    {
        AnimationManger.instance.animationHandler = AnimationManger.instance.PlayRoll;
    }

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
