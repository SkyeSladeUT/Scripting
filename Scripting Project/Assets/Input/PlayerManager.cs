using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInput
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerManager _instance;
        public PlayerManager Instance
        {
            get { return _instance; }
        }

        private PlayerMovementManager _playerMovement;
        public PlayerMovementManager PlayerMovement
        {
            get
            {
                if (_playerMovement == null) 
                {
                    _playerMovement = GetComponent<PlayerMovementManager>();
                    if (_playerMovement == null)
                        gameObject.AddComponent<PlayerMovementManager>();
                }
                return _playerMovement;
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        }

        private void Update()
        {
            if (InputVariables.Jump)
            {
                InputVariables.Jump = false;
                PlayerMovement.HandleJumping();
            }
        }

        private void FixedUpdate()
        {
            PlayerMovement.HandleAllMovement();
        }

    }
}
