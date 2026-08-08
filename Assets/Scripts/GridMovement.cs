using System.Collections;
using space;
using UnityEngine;
using UnityEngine.InputSystem;
using WorldItems;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private bool isRepeatedMovement = false;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float moveDuration = 0.1f;

    public InputActionReference movementKeys;
    private GameManager _gameManager;
    public Coord Coord;

    private bool _isMoving = false;
    private void Update()
    {
        if (!_gameManager.IsRunning) return;
        if (_isMoving) return;
        if ((!movementKeys.action.triggered || isRepeatedMovement) &&
            (!movementKeys.action.IsPressed() || !isRepeatedMovement)) return;
        var movementDirection = DirectionExtensions.DirectionFromVector(movementKeys.action.ReadValue<Vector2>());
        var destination = Coord.Move(movementDirection);
        if (!_gameManager.World.IsValid(destination)) return;
        StartCoroutine(Move(movementDirection));
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        switch (collision.gameObject.name)
        {
            case "Diamond":
                Destroy(collision.gameObject);
                _gameManager.World.SetField(Coord, WorldItemType.Empty);
                CollectDiamond();
                break;
        }
    }

    private void Awake()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Coord = _gameManager.World.Find(type => type == WorldItemType.Player);
    }

    private void MovePlayer(Coord source, Coord destination)
    {
        _gameManager.World.SetField(source, WorldItemType.Empty);
        _gameManager.World.SetField(destination, WorldItemType.Player);
    }

    private IEnumerator Move(Direction direction)
    {
        _isMoving = true;
        
        Vector2 startPos = transform.position;
        var endPos = startPos + DirectionExtensions.DirectionToVector(direction) * gridSize;
        
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
        _gameManager.World.SetField(Coord, WorldItemType.Empty);
        Coord = Coord.Move(direction);
        _gameManager.World.SetField(Coord, WorldItemType.Player);
        _isMoving = false;
    }
}