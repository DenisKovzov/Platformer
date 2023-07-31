using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour, IDamageable, IInitializable, IResetable
    {
        [SerializeField] private Transform groundDetection;
        private IPlayerInput input;
        private PlayerData config;

        private float horizontalMovement;
        private Rigidbody2D rigidBody;
        private float currentHealthPoint;

        public bool IsRunning => input.IsRunning();
        public Vector2 Velocity => rigidBody.velocity;
        public float HorizontalMovement => horizontalMovement;

        public float CurrentHealth
        {
            get
            {
                return currentHealthPoint;
            }

            private set
            {
                currentHealthPoint = value;
                OnHealthChanged?.Invoke();
            }
        }

        public float MaxHealth => config.MaxHealthPoint;

        public event Action OnJump;
        public event Action OnGround;
        public event Action OnDeath;
        public event Action OnHealthChanged;

        public void Construct(IPlayerInput input, PlayerData config)
        {
            this.input = input;
            this.config = config;
        }

        public void Initialize()
        {
            // TODO add unsubscribe
            CurrentHealth = config.MaxHealthPoint;
            input.OnJump += Jump;

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
            horizontalMovement = input.GetHorizontalMovement();
        }
        private void Move()
        {
            float speed = input.IsRunning() ? config.RunSpeed : config.WalkSpeed;
            rigidBody.velocity = new Vector2(horizontalMovement * speed, rigidBody.velocity.y);
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

            CurrentHealth = Mathf.Max(CurrentHealth - value, 0);

            if (CurrentHealth <= 0)
            {
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

        public void Reset()
        {
            CurrentHealth = MaxHealth;
            rigidBody.velocity = Vector2.zero;
            horizontalMovement = 0;
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