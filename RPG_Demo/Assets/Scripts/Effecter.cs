using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//效果
public class Effecter : MonoBehaviour
{

    public static IEnumerator AddLineParticle(GameObject target, Vector3 localPosition, float duration, float size)
    {
        GameObject effect = GameObject.Instantiate(Resources.Load<GameObject>("Effect/LineParticle"), Vector3.zero, Quaternion.identity);
        effect.transform.SetParent(target.transform);
        effect.transform.localPosition = localPosition;
        var ps = effect.GetComponent<ParticleSystem>().main;
        ps.startLifetime = duration;
        ps.startSize = size;
        yield return new WaitForSeconds(duration);

        Destroy(effect);
    }
    public static IEnumerator Shake(GameObject target, float duration, float range, float power)
    {
        Vector3 originPos = target.transform.position;
        while (duration > 0)
        {
            Vector3 s = target.transform.position;
            s.x += Mathf.Lerp(0, UnityEngine.Random.Range(-range, range), duration * power);
            s.y += Mathf.Lerp(0, UnityEngine.Random.Range(-range, range), duration * power);
            s.z = target.transform.position.z;
            target.transform.position = s;

            duration -= Time.fixedUnscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        //回復原位
        float backTime = 0;
        var curve_x = AnimationCurve.EaseInOut(target.transform.position.x, 0, 1, originPos.x);
        var curve_y = AnimationCurve.EaseInOut(target.transform.position.y, 0, 1, originPos.y);
        while (backTime < 1)
        {
            Vector2 pos = new Vector2(curve_x.Evaluate(backTime), curve_y.Evaluate(backTime));
            //target.transform.position = originPos;
            target.transform.position = pos;
            backTime += Time.fixedUnscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        target.transform.position = originPos;
    }

    public static IEnumerator TranslateMove(GameObject obj, Vector2 direction, float force, float duration)
    {
        float _t = 0;
        while (_t < duration)
        {
            obj.transform.position = (Vector2)obj.transform.position + direction * force;
            _t += Time.fixedUnscaledDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
