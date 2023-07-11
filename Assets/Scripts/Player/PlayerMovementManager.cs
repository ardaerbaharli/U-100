using System;
using UnityEngine;

namespace Player
{
    public enum Direction
    {
        Left,
        Right
    }

    public class PlayerMovementManager : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Direction _direction;

        public Action OnFlip;
        // use wasd to move the player on  the x and y axis

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var movement = new Vector3(horizontal, vertical, 0);
            transform.position += movement * (speed * Time.deltaTime);
            if (horizontal > 0 && _direction == Direction.Left)
            {
                _direction = Direction.Right;
                OnFlip?.Invoke();
            }
            else if (horizontal < 0 && _direction == Direction.Right)
            {
                _direction = Direction.Left;
                OnFlip?.Invoke();
            }
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
        }
    }
}