using System;
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

        private void Awake()
        {
            active = true;
            _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            _soundManager = _gameManager.GetComponent<SoundManager>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && active)
            {
                Explode();
            }
            
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (active) return;
            switch (other.tag)
            {
                case "Player":
                    _gameManager.GameOver(new ExplosionReason());
                    break;
                case "Bomb":
                    other.gameObject.GetComponent<BombController>().Explode();
                    break;
            }
            Destroy(other.gameObject);
            countdown--;
        }

        public void Explode()
        {
            active = false;
            // Coordinate approach
            var fieldsToExplode = coord.NeighborsWithDiagonal();
            fieldsToExplode.Add(coord);
            foreach (var field in fieldsToExplode)
            {
                if (_gameManager.World.IsValid(field))
                {
                    
                }
            }
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