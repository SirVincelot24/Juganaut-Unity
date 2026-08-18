using System;
using System.Collections;
using space;
using Unity.VisualScripting;
using UnityEngine;

namespace logic
{
    public class BombController : MonoBehaviour
    {
        public int countdown;
        public bool active = true;
        public bool falling;
        public Coord coord;
        
        private GameManager _gameManager;
        private SoundManager _soundManager;
        private BoxCollider2D _collider;
        private BoxCollider2D _explosionCollider;
        private SpriteRenderer _spriteRenderer;
        private Sprite _bombActive;

        private void Awake()
        {
            countdown = 3;
            active = true;
            _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            _soundManager = _gameManager.GetComponent<SoundManager>();
            _collider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _bombActive = Resources.Load<Sprite>("Textures/bombe-active");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player") || !active) return;
            _spriteRenderer.sprite = _bombActive;
            Tick();
        }

        private IEnumerator CountDown()
        {
            while (countdown > 0)
            {
                countdown--;
                yield return new WaitForSeconds(0.5f);
            }
            Explode();
        }

        private void Tick()
        {
            StartCoroutine(CountDown());
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (active) return;
            switch (other.tag)
            {
                case "Bomb":
                    other.gameObject.GetComponent<BombController>().Tick();
                    break;
                case "Player":
                    _gameManager.GameOver(new ExplosionReason());
                    Destroy(other.gameObject);
                    break;
                default:
                    Destroy(other.gameObject);
                    break;
            }
            // countdown--;
        }

        private void Explode()
        {
            active = false;
            // Coordinate approach
            // var fieldsToExplode = coord.NeighborsWithDiagonal();
            // fieldsToExplode.Add(coord);
            // foreach (var field in fieldsToExplode)
            // {
            //     if (_gameManager.World.IsValid(field))
            //     {
            //         
            //     }
            // }
            // Collider approach
            _explosionCollider = this.AddComponent<BoxCollider2D>();
            _explosionCollider.size = new Vector2(15f, 15f);
            _explosionCollider.isTrigger = true;
            _collider.enabled = false;
            _soundManager.PlaySfx(SfxType.BombExplode);
        }

        public void Update()
        {
            if (!_explosionCollider)
                return;
            if (countdown < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}