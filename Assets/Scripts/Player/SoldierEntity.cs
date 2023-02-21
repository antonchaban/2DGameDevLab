using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SoldierEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")] 
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;

        [Header("Jump")] 
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;

        private Rigidbody2D _soldierRigidbody2D;

        private bool _isJumping;
        private float _startJumpVerticalPosition;

        // Start is called before the first frame update
        private void Start()
        {
            _soldierRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_isJumping)
            {
                UpdateJump();
            }
        }

        public void Jump()
        {
            if (_isJumping)
                return;

            _isJumping = true;
            _soldierRigidbody2D.AddForce(Vector2.up * _jumpForce);
            _soldierRigidbody2D.gravityScale = _gravityScale;
            _startJumpVerticalPosition = transform.position.y;
        }

        private void UpdateJump()
        {
            if (_soldierRigidbody2D.velocity.y < 0 && _soldierRigidbody2D.position.y <= _startJumpVerticalPosition)
            {
                ResetJump();
            }
        }

        private void ResetJump()
        {
            _isJumping = false;
            _soldierRigidbody2D.position = new Vector2(_soldierRigidbody2D.position.x, _soldierRigidbody2D.position.y);
            _soldierRigidbody2D.gravityScale = 0;
        }


        public void MoveHorizontally(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _soldierRigidbody2D.velocity;
            velocity.x = direction * _horizontalSpeed;
            _soldierRigidbody2D.velocity = velocity;
        }

        private void SetDirection(float direction)
        {
            if (_faceRight && direction < 0 || !_faceRight && direction > 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            transform.Rotate(0f, 180f, 0f);
            _faceRight = !_faceRight;
        }
    }
}