using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private Vector2 keyboardInput;
    private IMovement movementScript;
    [SerializeField] private Animator animator;

    private bool _inputEnabled = true;

    public void EnableInput()
    {
        _inputEnabled = true;
    }

    public void DisableInput()
    {
        _inputEnabled = false;
        movementScript.SetMovement(Vector2.zero);
    }

    private void Awake()
    {
        movementScript = GetComponent<IMovement>();
    }
    void Update()
    {
        if (!_inputEnabled) { return; }
        keyboardInput.x = Input.GetAxisRaw("Horizontal");
        keyboardInput.y = Input.GetAxisRaw("Vertical");
        keyboardInput.Normalize();

        if(Mathf.Abs(keyboardInput.x) > 0 || Mathf.Abs (keyboardInput.y) > 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        movementScript.SetMovement(keyboardInput);
    }
}
