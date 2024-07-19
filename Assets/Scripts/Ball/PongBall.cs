using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PongBall : NetworkBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    private float _cachedSpeed;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        InitializeBall();
    }


    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }


    [ServerCallback]
    void OnCollisionEnter(Collision col)
    {

        if(col.gameObject.layer == 6)
        {
           
            if (transform.position.x < 0) ScoreManager.OnScoreUpdate?.Invoke(FootballGoalPosition.left);
            else ScoreManager.OnScoreUpdate?.Invoke(FootballGoalPosition.right);

            //((PongNetworkManager)NetworkManager.singleton).DestroyBall();

            ((SteamPongNetworkManager)NetworkManager.singleton).ResetBallPosition();
        }
        

        if (col.transform.GetComponent<SteamPongPlayer>())
        {
            _cachedSpeed += 3f;
            // Calculate y direction via hit Factor
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            float x = col.relativeVelocity.x > 0 ? 1 : -1;

            Vector2 dir = new Vector2(x, y).normalized;

            _rigidbody.velocity = dir * _cachedSpeed;


        }
    }

    private void InitializeBall()
    {
        _cachedSpeed = _speed;

        float randomDirection = Random.Range(-1, 1);
        if (randomDirection == 0)
            randomDirection = 1;
        _rigidbody.velocity = Vector3.left * randomDirection * _speed;
    }
    
}
