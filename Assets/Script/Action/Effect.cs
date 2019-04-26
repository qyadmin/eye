using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Effect : MonoBehaviour
{
	[SerializeField]
	public Texture2D[] AllSprite;
    [SerializeField]
    private float times = 0.1f;
	[SerializeField]
	public RawImage effect;

    Coroutine c;
    public void Stop()
    {
        if (c != null)
            StopCoroutine(c);
    }

    public void Play()
    {
        if (c != null)
            StopCoroutine(c);
        c = StartCoroutine("PlayAnimator");
    }
    private IEnumerator PlayAnimator()
    {
        int i = 0;
        while (true)
        {
            if (AllSprite[i] == null)
                continue;
			effect.texture = AllSprite[i];
            yield return new WaitForSeconds(times);
            i++;
            if (i == AllSprite.Length - 1)
                i = 0;
           // Debug.Log("PLAYER--------------");
        }
    }
}
