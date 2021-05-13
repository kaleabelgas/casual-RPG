using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private Vector2 keyboardInput;
    private IMovement movementScript;

    private void Awake()
    {
        movementScript = GetComponent<IMovement>();
    }
    void Update()
    {
        keyboardInput.x = Input.GetAxisRaw("Horizontal");
        keyboardInput.y = Input.GetAxisRaw("Vertical");
        keyboardInput.Normalize();

        movementScript.SetMovement(keyboardInput);
    }
}
