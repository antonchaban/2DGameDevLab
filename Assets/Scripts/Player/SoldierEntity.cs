using System;
using Core.Enums;
using Core.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SoldierEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")] 
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private Direction _direction;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;
        private bool _isJumping;
        private float _startJumpVerticalPosition;

        [SerializeField] private DirectionalCameraPair _cameras;
        
        private Rigidbody2D _soldierRigidbody2D;


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

        public void MoveHorizontally(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _soldierRigidbody2D.velocity;
            velocity.x = direction * _horizontalSpeed;
            _soldierRigidbody2D.velocity = velocity;
        }

        private void SetDirection(float direction)
        {
            if (_direction == Direction.Right  && direction < 0 || _direction == Direction.Left && direction > 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            transform.Rotate(0f, 180f, 0f);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            foreach (var cameraPair in _cameras.DirectionalCameras)
            {
                cameraPair.Value.enabled = cameraPair.Key == _direction;
            }
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
    }
}