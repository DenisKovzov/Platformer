using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Player player;
        [Header("AnimationStates")]
        [SerializeField] private string idleAnimationName;
        [SerializeField] private string walkAnimationName;
        [SerializeField] private string runAnimationName;
        [SerializeField] private string jumpAnimationName;
        private Animator animator;

        private bool isJumping = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            // TODO add unsubscribe
            player.OnJump += Player_OnJump;
            player.OnGround += Player_OnGround;
        }

        private void Update()
        {
            HandleFacing();
            HandleMovement();
        }

        private void Player_OnJump()
        {
            isJumping = true;
            DisableStates();
            animator.SetTrigger(jumpAnimationName);
        }

        private void Player_OnGround()
        {
            isJumping = false;
        }

        private void HandleFacing()
        {
            bool isFacingRight = transform.localScale.x > 0;
            float horizontal = player.Velocity.x;

            if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;

                transform.localScale = localScale;
            }
        }



        private void HandleMovement()
        {
            if (isJumping)
                return;


            float horizontal = player.Velocity.x;

            if (horizontal == 0f)
            {
                SetState(PlayerState.Idle);
            }
            else if (player.IsRunning)
            {
                SetState(PlayerState.Running);
            }
            else
            {
                SetState(PlayerState.Walking);
            }
        }

        public enum PlayerState
        {
            Idle,
            Walking,
            Running,
        }

        private void SetState(PlayerState state)
        {
            DisableStates();
            switch (state)
            {
                case PlayerState.Idle:
                    animator.SetBool(idleAnimationName, true);
                    break;
                case PlayerState.Walking:
                    animator.SetBool(walkAnimationName, true);
                    break;
                case PlayerState.Running:
                    animator.SetBool(runAnimationName, true);
                    break;
            }
        }

        private void DisableStates()
        {
            animator.SetBool(idleAnimationName, false);
            animator.SetBool(walkAnimationName, false);
            animator.SetBool(runAnimationName, false);
        }
    }
}