using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    bool turned = false;

    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (!turned)
        {
            StartCoroutine(RandomRotate());
            Debug.Log("turned" + turned);
        }
        //else if (turned)
        //{
        //    EnemyRun();
        //    Debug.Log("turned" + turned);
        //}
    }

    private void EnemyRun()
    {
        if (turned)
        {
            Debug.Log("Enemy Run");
            _rigidbody2D.velocity = new Vector2(moveSpeed * Time.deltaTime, _rigidbody2D.velocity.y);
            turned = false;
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }
    }

    private IEnumerator RandomRotate()
    {
        float randomNumber = Random.Range(2, 7);
        Debug.Log(randomNumber);
        if (randomNumber < 5 && !turned)
        {
            yield return new WaitForSeconds(randomNumber);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            turned = true;
            Debug.Log("turned" + turned);
        }
        else if (randomNumber >= 5 && !turned)
        {
            yield return new WaitForSeconds(randomNumber);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            turned = true;
            Debug.Log("turned" + turned);
        }
    }
}
