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
        public Direction _direction;

        public Action OnFlip;
        // use wasd to move the player on  the x and y axis

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var movement = new Vector3(horizontal, vertical, 0);
            var projectedPosition = transform.position + movement * (speed * Time.deltaTime);
            // send a raycast on layer Wall at the projected position
            var hit = Physics2D.Raycast(projectedPosition, Vector2.zero, 0, LayerMask.GetMask("Wall"));
            if (hit.collider != null)
            {
                // if the player is going to collide with a wall, set the movement to 0
                return;
            }

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

        public void IncreaseSpeed(float percentage)
        {
            speed += speed * (percentage / 100f);
        }
    }
}