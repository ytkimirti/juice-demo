using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Ball : MonoBehaviour
{

    public bool isCenterBall;
    public int hp;
    [Space]
    public Color[] colorList;
    public SpriteRenderer sprite;
    public TrailRenderer trail;
    public Flasher flasher;
    public SpriteRenderer[] eyeSprites;
    public Face face;
    public GameObject happyMouth;
    [Space]
    public float minImpactForce;

    public static Ball centerBall;
    Rigidbody2D rb;

    private void Awake()
    {
        if (isCenterBall)
        {
            centerBall = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!isCenterBall)
            ParticleManager.main.play(4, transform.position);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleManager.main.play(2, collision.contacts[0].point);

        if (isCenterBall)
            return;

        float impactForce = collision.GetImpactForce();

        if (collision.gameObject.tag == "Center")
        {
            Ball.centerBall.GetDamage(impactForce, rb);
        }
        else
        {
            if (impactForce > minImpactForce / 2)
            {
                GetDamage(impactForce, collision.otherRigidbody);
            }
        }

    }

    //sender is the rigidbody you collided with
    public void GetDamage(float impactForce, Rigidbody2D sender)
    {
        //Everytime any contact happens
        CameraShaker.Instance.ShakeOnce(2, 5, 0, 0.2f);



        if (impactForce > minImpactForce)
        {
            //This means game over, the center ball dies :(
            if (isCenterBall)
            {
                //There is no gameover in debug mode
                if (GameManager.main.DEBUG_MODE)
                    return;

                //Knockback
                rb.isKinematic = false;
                rb.velocity = 13 * -sender.transform.position.normalized;
                rb.angularVelocity = Random.Range(200f, -200f);

                //-flash
                flasher.Flash(2, 0.02f);

                //Death particle for center ball
                ParticleManager.main.play(3, transform.position);

                //Wery important thing
                face.parts[0].localScale = Vector3.one * 0.156f;

                //Call the game manager and loose the game.
                GameManager.main.Loose();
            }
            //Everytime another ball gets hit
            else
            {
                //Loose hp
                hp--;

                //-flash
                flasher.Flash(1, 0.08f);
                CameraShaker.Instance.ShakeOnce(2, 7, 0, 0.3f);

                //-freeze
                SlowMo.main.Freeze(5);

                ParticleManager.main.play(0, transform.position, colorList[hp + 1]);

                //Change color
                sprite.color = colorList[hp];

                foreach (SpriteRenderer sprite in eyeSprites)
                {
                    sprite.color = colorList[hp];
                }

                if (hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    public void Die()
    {
        if (trail)
        {
            //trail.autodestruct = true;
            trail.transform.parent = null;
        }

        //If this is an enemy
        if (!isCenterBall)
        {
            //Tell the game manager an enemy died
            GameManager.main.OnEnemyDies();

            //Death particle for enemies
            ParticleManager.main.play(0, transform.position, colorList[hp + 1]);
        }

        Destroy(gameObject);
    }
}
