using System.Collections;
using logic;
using space;
using UnityEngine;
using UnityEngine.InputSystem;
using WorldItems;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private bool isRepeatedMovement;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float moveDuration = 0.1f;

    public InputActionReference movementKeys;
    private GameManager _gameManager;
    private SoundManager _soundManager;
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Diamond":
                Destroy(collision.gameObject);
                _gameManager.World.SetField(Coord, WorldItemType.Empty);
                CollectDiamond();
                break;
            case "Monster":
                Destroy(gameObject);
                _gameManager.GameOver(new PlayerWalksIntoMonsterReason());
                break;
            case "Dirt":
                Destroy(collision.gameObject);
                _soundManager.PlaySfx(SfxType.Crisp);
                break;
        }
    }

    private void Awake()
    {
        var gameManagerObj = GameObject.FindWithTag("GameManager");
        _gameManager = gameManagerObj.GetComponent<GameManager>();
        _soundManager = gameManagerObj.GetComponent<SoundManager>();
        Coord = _gameManager.World.Find(type => type == WorldItemType.Player);
    }

    private void MovePlayer(Coord source, Coord destination)
    {
        _gameManager.World.SetField(source, WorldItemType.Empty);
        _gameManager.World.SetField(destination, WorldItemType.Player);
    }
    
    private void CollectDiamond()
    {
        _gameManager.diamondCount.Value++;
        if (_gameManager.diamondCount.Value >= _gameManager.diamondsInGame.Value)
        {
            _gameManager.Win(new AllDiamondsCollected(_gameManager.diamondCount.Value));
        }
        _soundManager.PlaySfx(SfxType.CollectDiamond);
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
