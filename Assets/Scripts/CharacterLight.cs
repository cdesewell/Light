using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLight : MonoBehaviour
{
    private Rigidbody2D characterRigidBody;
    GameObject torch;

    public float rotateSpeed;
    public float upperViewLimit;
    public float lowerViewLimit;

    public float rotation;

    // Start is called before the first frame update
    void Start()
    {
        characterRigidBody = GetComponent<Rigidbody2D>();
        torch = GameObject.Find("CharacterLight");

        torch.transform.eulerAngles = new Vector3
        {
            x = 0,
            y = 0,
            z = rotation
        };

    }

    // Update is called once per frame
    void Update()
    {
        FlipTorch();
        PositionTorch();
        RotateTorch();
        RestrictTorch();
    }

    private void PositionTorch()
    {
        torch.transform.position = new Vector3
        {
            x = characterRigidBody.position.x,
            y = characterRigidBody.position.y,
            z = 0f
        };
    }

    private void FlipTorch()
    {
        var desairedDirection = Input.GetAxis("Horizontal");

        var currentDirection = Mathf.Abs(torch.transform.rotation.eulerAngles.z);

        if (desairedDirection < 0 && (currentDirection < 360 && currentDirection > 180))
        {
            var diff = rotation * 2;
            rotation -= diff;
        }

        if (desairedDirection > 0 && (currentDirection > 0 && currentDirection < 180))
        {
            var diff = rotation * -2;
            rotation += diff;
        }
    }

    private void RotateTorch()
    {
        var verticalRotation = Input.GetAxis("Vertical");

        if(rotation < 0)
        {
            if (verticalRotation > 0)//Look Down
            {
                rotation -= rotateSpeed;
            }
            else if (verticalRotation < 0) //Look Up
            {
                rotation += rotateSpeed;
            }
        }
        else
        {
            if (verticalRotation > 0)//Look Up
            {
                rotation += rotateSpeed;
            }
            else if (verticalRotation < 0) //Look Down
            {
                rotation -= rotateSpeed;
            }
        }

        torch.transform.eulerAngles = new Vector3
        {
            x = 0,
            y = 0,
            z = rotation
        };

    }

    private void RestrictTorch()
    {
        if (rotation < -(upperViewLimit) && rotation < 0)
        {
            rotation = -(upperViewLimit);
        }
        else if (rotation > -(lowerViewLimit) && rotation < 0)
        {
            rotation = -(lowerViewLimit);
        }
        else if (rotation < (lowerViewLimit) && rotation > 0)
        {
            rotation = (lowerViewLimit);
        }
        else if (rotation > (upperViewLimit) && rotation > 0)
        {
            rotation = (upperViewLimit);
        }

        torch.transform.eulerAngles = new Vector3
        {
            x = rotateSpeed * 0,
            y = rotateSpeed * 0,
            z = rotation
        };
    }
}

public enum CharacterDirectin
{
    Left,
    Right
}