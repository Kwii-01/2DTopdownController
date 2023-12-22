using System.Collections;
using System.Collections.Generic;

using Controller;

using UnityEngine;

public class PlayerController : Controller<Player> {
    [SerializeField] private Joystick _joystick;

    private Vector2 _direction;

    private void Start() {
        this._direction = Vector2.zero;
    }

    private void Update() {
        if (this._context.Character != null) {
            this._direction.x = this._joystick.Horizontal;
            this._direction.y = this._joystick.Vertical;
            if (this._direction != Vector2.zero) {
                this._context.Character.MoveToward(this._direction);
            } else {
                this._context.Character.Stop();
            }
        }
    }

    private void OnEnable() {
        this._joystick.gameObject.SetActive(true);
    }

    private void OnDisable() {
        if (this._joystick != null) {
            this._joystick.gameObject.SetActive(false);
        }
    }
}

