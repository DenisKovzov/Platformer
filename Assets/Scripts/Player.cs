using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour, IDamageable, IInitializable
    {
        [SerializeField] private Transform groundDetection;
        private IPlayerInput input;
        private PlayerData config;

        private float horizontal;
        private Rigidbody2D rigidBody;
        private float currentHealthPoint;

        public bool IsRunning => input.IsRunning();
        public Vector2 Velocity => rigidBody.velocity;
        public event Action OnJump;
        public event Action OnGround;
        public event Action OnDeath;

        public void Construct(IPlayerInput input, PlayerData config)
        {
            this.input = input;
            this.config = config;
        }

        public void Initialize()
        {
            // TODO add unsubscribe
            currentHealthPoint = config.MaxHealthPoint;
            input.OnJump += Jump;
        }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            HandleInput();
            Move();
            ApplyGravity();
        }

        private void HandleInput()
        {
            horizontal = input.GetHorizontalMovement();
        }
        private void Move()
        {
            float speed = input.IsRunning() ? config.RunSpeed : config.WalkSpeed;
            rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
        }


        private void ApplyGravity()
        {
            rigidBody.velocity += Vector2.up * config.Gravity * Time.deltaTime;

            if (rigidBody.velocity.y < 0f && IsGrounded())
                OnGround?.Invoke();
        }

        private void Jump()
        {
            if (IsGrounded())
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sqrt(2 * config.JumpHeight * Mathf.Abs(config.Gravity)));
                OnJump?.Invoke();
            }
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapBox(groundDetection.position, config.size, 0f, config.GroundMask);
        }

        public void ApplyDamage(float value)
        {
            if (value < 0)
            {
                Debug.LogError($"Damage is less than zero: {value}", this);
            }

            currentHealthPoint -= value;
            if (currentHealthPoint < 0)
            {
                currentHealthPoint = 0;
                OnDeath?.Invoke();
            }

        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(groundDetection.position, config.size);
        }
    }
    
    [Serializable]
    public struct PlayerData
    {
        [Header("Movement")]
        public float WalkSpeed;
        public float RunSpeed;
        [Header("Jump")]
        public float JumpHeight;
        public float Gravity;
        public LayerMask GroundMask;
        [Header("DetectionSize")]
        public Vector2 size;
        [Header("Stats")]
        public float MaxHealthPoint;
    }
}