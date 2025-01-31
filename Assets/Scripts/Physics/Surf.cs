using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Surf : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    [Header("Ref")]
    [SerializeField] public Transform backgroundParent;

    [Header("Prefab Ref")]
    private GameObject backgroundPrefab;

    [Header("State")]
    public bool isGameOver = false;

    [Header("Other Values")]
    private float nextXPosBG;
    [SerializeField] public float backgroundUnit;
    // private float paddingX;

    [Header("Physics")]
    [SerializeField] public float horizontalSurfStartForce;
    [SerializeField] public float verticalSurfStartForce;

    [SerializeField] public float horizontalSurfHigherForce;
    [SerializeField] public float verticalSurfHigherForce;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        backgroundPrefab = Resources.Load<GameObject>("Prefabs/Background/Background");
        nextXPosBG = backgroundUnit;
    }
    
    private void FixedUpdate()
    {
        if(IsStopped())
        {
            GameOver();
        }

        // 이동할 때마다 배경 동적 생성
        while(transform.position.x > nextXPosBG)
        {
            GameObject bg = Instantiate(backgroundPrefab, new Vector3(nextXPosBG + backgroundUnit, 0f, 1f), Quaternion.identity);
            bg.transform.parent = backgroundParent;
            nextXPosBG += backgroundUnit;
        }
    }

    public void SurfHigher()
    {
        Vector2 forceDirection = new Vector2(horizontalSurfHigherForce, verticalSurfHigherForce);
        rigidbody.AddForce(forceDirection, ForceMode2D.Impulse);
    }

    public void StartGame()
    {
        Vector2 forceDirection = new Vector2(horizontalSurfStartForce, verticalSurfStartForce);
        rigidbody.AddForce(forceDirection, ForceMode2D.Impulse);
    }

    private bool IsStopped()
    {
        if(rigidbody.velocity.x <= 0)
            return true;
        else
            return false;
    }

    public void GameOver()
    {
        if(!isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over");
        }
    }
}