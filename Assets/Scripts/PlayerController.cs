using UnityEngine;
using Pause;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    public Inventory inventory => _inventory;
    [SerializeField] private QuizScriptable _restrict;
    public QuizScriptable restrict => _restrict;
    [SerializeField] private Canvas _keyCanvas; 
    public Canvas keyCanvas => _keyCanvas;
    [SerializeField] private float _speed = 5;
    public float speed {get => _speed; set{_speed = value;}}

    private new Rigidbody2D rigidbody;
    private Animator animator;
    private Vector3 _direction;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();  
    }

    private void OnEnable() => SceneAnchor.OnSceneTransition += InitialisePosition; // When player is in scene and actie
    private void OnDisable() => SceneAnchor.OnSceneTransition -= InitialisePosition; // When player goes to another scene will deactive 

    private void InitialisePosition(Vector2 position) => transform.position = position; // To get player last stop position

    private void Update() 
    {
        if (PauseMovement.IsActive()) 
        {
            keyCanvas.GetComponent<Canvas>().enabled = false;
            return;
        }
        else
        {
            keyCanvas.GetComponent<Canvas>().enabled = true;
        }
        

        if (Input.GetKeyDown(KeyCode.I)) InventoryUI.RequestInventory(inventory);
        else if (Input.GetKeyDown(KeyCode.Escape)) PauseMenu.RequestMenu();
        
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");

        UpdateAnimator();
    }

    private void FixedUpdate() 
    {
        if (PauseMovement.IsActive())
        {
            keyCanvas.GetComponent<Canvas>().enabled = false;
            
        }
        else
        {
            keyCanvas.GetComponent<Canvas>().enabled = true;
        }
        
        
        if (_direction != Vector3.zero)
        {
            var delta = _direction.normalized * speed * Time.deltaTime;
            rigidbody.MovePosition(transform.position + delta);
        }
        else
        {
            rigidbody.velocity = Vector2.zero; // Velocity to zero when no direction input
        }
    }

    private void UpdateAnimator()
    {
        if (_direction != Vector3.zero)
        {
            animator.SetFloat("moveX", _direction.x);
            animator.SetFloat("moveY", _direction.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
}
