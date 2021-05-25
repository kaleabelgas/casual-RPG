using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private Vector2 keyboardInput;
    private IMovement movementScript;

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

        movementScript.SetMovement(keyboardInput);
    }
}
