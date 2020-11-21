using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

//攝影機跟隨玩家
public class CameraFollow : MonoBehaviour
{
    //玩家物件
    public GameObject playerObj;
    public Vector2 speed = new Vector2(1, 0.2f);
    public Vector2 offset;
    public float yOffset; //報廢
    public float xOffset;//報廢
    public Vector2 maxRange;
    public Vector2 minRange;
    bool isShaking = false;

    private static event Action<float, float, float> Shake_event;
    private void Start()
    {
        playerObj = GameObject.FindObjectOfType<PlayerControl>().gameObject;

        //打怪物晃動:???
        Shake_event += CameraShake;


    }
    private void OnDestroy()
    {
        Shake_event -= CameraShake;
    }

    private void Update()
    {
        if (isShaking) { return; }

        Vector3 pos = new Vector3(Mathf.Lerp(transform.position.x, playerObj.transform.position.x + offset.x, speed.x * Time.deltaTime),
            Mathf.Lerp(transform.position.y, playerObj.transform.position.y + offset.y, speed.y * Time.deltaTime),
            transform.position.z);

        if (maxRange != Vector2.zero || minRange != Vector2.zero)
        {
            pos.x = Mathf.Clamp(pos.x, minRange.x, maxRange.x);
            pos.y = Mathf.Clamp(pos.y, minRange.y, maxRange.y);
        }

        gameObject.transform.position = pos;
    }

    public static void CameraShake_c(float duration, float range, float power)
    {
        Shake_event(duration, range, power);
    }
    //晃動鏡頭
    public void CameraShake(float duration, float range, float power)
    {
        //StartCoroutine(Shake_coro(0.08f, 2f, 0.25f)); //建議數值
        if (!isShaking)
        {
            StartCoroutine(Shake_coro(duration, range, power));
        }
    }
    IEnumerator Shake_coro(float duration, float range, float power)
    {
        isShaking = true;
        //while (duration-- > 0)
        while (duration > 0)
        {
            Vector3 s = transform.position;
            s.x += Mathf.Lerp(0, UnityEngine.Random.Range(-range, range), power);
            s.y += Mathf.Lerp(0, UnityEngine.Random.Range(-range, range), power);
            s.z = transform.position.z;
            transform.position = s;

            duration -= Time.fixedUnscaledDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        isShaking = false;
    }

    private void OnDrawGizmos()
    {

        if (maxRange != Vector2.zero)
        {
            //Gizmos.DrawWireCube((maxRange-minRange)*0.5f,);

            //Gizmos.DrawLine(minRange + offset, maxRange + offset);

            Vector2 min = minRange + offset + (Vector2)transform.position; ;
            Vector2 max = maxRange + offset + (Vector2)transform.position; ;
            Gizmos.DrawLine(new Vector2(min.x, min.y), new Vector2(max.x, min.y));//底邊
            Gizmos.DrawLine(new Vector2(max.x, min.y), new Vector2(max.x, max.y));//右上
            Gizmos.DrawLine(new Vector2(min.x, max.y), new Vector2(max.x, max.y));//上邊
            Gizmos.DrawLine(new Vector2(min.x, min.y), new Vector2(min.x, max.y));//左上
        }
#if UNITY_EDITOR
        Vector3 pos = transform.position;

        if (maxRange != Vector2.zero || minRange != Vector2.zero)
        {
            pos.x = Mathf.Clamp(pos.x, minRange.x, maxRange.x);
            pos.y = Mathf.Clamp(pos.y, minRange.y, maxRange.y);
        }

        gameObject.transform.position = pos;
#endif
    }
}
