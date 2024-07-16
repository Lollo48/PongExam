using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PongBall : NetworkBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    public override void OnStartServer()
    {
        base.OnStartServer();
        _rigidbody.velocity = Vector2.right * _speed;
    }

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    // only call this on server
    [ServerCallback]
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.GetComponent<PongPlayer>())
        {
            // Calculate y direction via hit Factor
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            float x = col.relativeVelocity.x > 0 ? 1 : -1;

            Vector2 dir = new Vector2(x, y).normalized;

            _rigidbody.velocity = dir * _speed;
        }
    }


    
}
