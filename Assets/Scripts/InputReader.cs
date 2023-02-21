using Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputReader : MonoBehaviour
    {
        [SerializeField] private SoldierEntity _soldierEntity;
        private float _horizontalDirection;


        private void Update()
        {
            _horizontalDirection = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                _soldierEntity.Jump();
            }
        }

        private void FixedUpdate()
        {
            _soldierEntity.MoveHorizontally(_horizontalDirection);
        }
    }
}