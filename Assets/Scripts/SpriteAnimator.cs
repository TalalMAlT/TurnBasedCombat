using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour {
	public enum EStyle
	{
		Once,
		Loop,
    }

    protected Image  image;

    [SerializeField]
	protected EStyle style       = EStyle.Once;
    [SerializeField]
    protected bool   playOnAwake = false;
    [SerializeField]
    protected float  oneFrameSec = 1.0f;
    [SerializeField]
    protected Sprite[] sprites;

    protected int       frameCounter  = 0;
	protected Coroutine playCoroutine = null;

    protected bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } }

    void Awake()
    {
        image = GetComponent<Image>();
    }

	protected void OnEnable()
	{
        if( playOnAwake )
        {
            Play();
        }
	}

	protected void OnDisable ()
	{
		Stop();
	}
	
	public void SetSprites( Sprite[] sps )
	{
		sprites = sps;
	}

	public void Play()
    {
        play();
    }

    [ContextMenu("Play")]
	private void play()
    {
        //@memo アクティブじゃない時にStartCoroutineが走るとエラーなので
		if(gameObject.activeInHierarchy == false) return;

		if(playCoroutine != null) StopCoroutine(playCoroutine);

        frameCounter = 0;
        image.sprite = sprites[frameCounter];
        isPlaying = true;

		switch (style)
		{
			case EStyle.Once:
                playCoroutine = StartCoroutine( playOnceCoroutine() );
				break;
			case EStyle.Loop:
                playCoroutine = StartCoroutine( playLoopCoroutine() );
				break;
		}
    }

    [ContextMenu("Stop")]
	public void Stop()
    {
		if(playCoroutine != null) StopCoroutine(playCoroutine);
		playCoroutine = null;
        isPlaying = false;
    }

	private IEnumerator playOnceCoroutine ()
    {
		while( frameCounter < sprites.Length )
		{
            image.sprite = sprites[ frameCounter ];
            yield return new WaitForSeconds( oneFrameSec );
            frameCounter++;
		}
        isPlaying = false;
    }

	IEnumerator playLoopCoroutine()
	{
		while( true )
		{
            image.sprite = sprites[ frameCounter % sprites.Length ];
            yield return new WaitForSeconds( oneFrameSec );
            frameCounter++;
		}
	}
}
