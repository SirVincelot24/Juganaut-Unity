using System.Collections;
using space;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private bool isRepeatedMovement = false;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float moveDuration = 0.1f;

    public InputActionReference movementKeys;
    public GameManager gameManager;
    public Coord Coord = new(10, 10);

    private bool _isMoving = false;
    private void Update()
    {
        if (_isMoving) return;
        if ((!movementKeys.action.triggered || isRepeatedMovement) &&
            (!movementKeys.action.IsPressed() || !isRepeatedMovement)) return;
        var movementDirection = DirectionExtensions.DirectionFromVector(movementKeys.action.ReadValue<Vector2>());
        var destination = Coord.Move(movementDirection);
        if (!gameManager.World.IsValid(destination)) return;
        StartCoroutine(Move(movementDirection));
    }
    
    

    private IEnumerator Move(Direction direction)
    {
        _isMoving = true;
        
        Vector2 startPos = transform.position;
        var endPos = startPos + (DirectionExtensions.DirectionToVector(direction) * gridSize);
        
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            var p = elapsedTime / moveDuration;
            p = p * p * (3f - 2f * p);
            transform.position = Vector3.Lerp(startPos, endPos, p);
            yield return null;
        }
        transform.position = endPos;
        Coord = Coord.Move(direction);
        _isMoving = false;
    }
}