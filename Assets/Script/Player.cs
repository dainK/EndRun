using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndRun
{

    public class Player : MonoBehaviour
    {
        public Action onHurt;

        private bool isJump;
        private bool isTop;
        private Vector2 startPostion = Vector2.zero;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            isJump = false;
            isTop = false;
        }

        void Start()
        {
            startPostion = transform.position;
            animator.Play("run");
        }

        void Update()
        {
            if( GameSettings.gameData.isEnd )
                return;
            
            if (Input.GetMouseButtonDown(0) && !isJump)
            {
                AudioManager.Instance.PlayTouch();
                isJump = true;
            }

            if (isJump)
            {
                if (transform.position.y < GameSettings.playerData.jumpHight && !isTop)
                {
                    transform.position += Vector3.up * GameSettings.playerData.jumpSpeed * Time.deltaTime;
                }
                else
                {
                    isTop = true;
                }

                if (isTop)
                {
                    if (transform.position.y > startPostion.y)
                    {
                        transform.position += Vector3.down * GameSettings.playerData.jumpSpeed * Time.deltaTime;
                    }
                    else
                    {
                        transform.position = startPostion;
                        isJump = false;
                        isTop = false;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            animator.Play("hurt");
            onHurt?.Invoke();
        }
    }
}