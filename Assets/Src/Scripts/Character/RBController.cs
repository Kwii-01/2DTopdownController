using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Controller {
    [RequireComponent(typeof(Rigidbody2D))]
    public class RBController : MonoBehaviour {
        public Transform inputSpace = default;
        [SerializeField] private Rigidbody2D _rigidbody;

        public Rigidbody2D Rigidbody => this._rigidbody;

        private Vector2 _velocity;
        private Vector2 _desiredVelocity;
        private Vector2 _rightAxis;
        private Vector2 _forwardAxis;
        private float _maxSpeed;

        private void Reset() {
            this._rigidbody = this.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            this._velocity = this._rigidbody.velocity;
            this.AdjustVelocity();
            this._rigidbody.velocity = this._velocity;
        }

        public void Move(Vector2 motion, float maxSpeed) {
            if (this.inputSpace) {
                this._rightAxis = Vector2.Perpendicular(this.inputSpace.up);
                this._forwardAxis = Vector2.Perpendicular(this.inputSpace.right);
                this._desiredVelocity = this._forwardAxis * motion.y + this._rightAxis * motion.x;
            } else {
                this._rightAxis = Vector2.right;
                this._forwardAxis = Vector2.up;
                this._desiredVelocity = motion;
            }
            this._maxSpeed = maxSpeed;
        }

        public void Stop() {
            this._desiredVelocity = Vector2.zero;
            this._velocity = Vector2.zero;
            this._rigidbody.velocity = this._velocity;
        }

        private void AdjustVelocity() {
            float currentX = Vector2.Dot(this._velocity, this._rightAxis);
            float currentY = Vector2.Dot(this._velocity, this._forwardAxis);
            float maxSpeedChange = this._maxSpeed * Time.deltaTime;

            float newX = Mathf.MoveTowards(currentX, this._desiredVelocity.x, maxSpeedChange);
            float newY = Mathf.MoveTowards(currentY, this._desiredVelocity.y, maxSpeedChange);
            this._velocity += this._rightAxis * (newX - currentX) + this._forwardAxis * (newY - currentY);
        }


    }
}
