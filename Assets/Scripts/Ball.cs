
using System;
using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy && (base.gameObject.transform.position.y < -30f || base.gameObject.transform.position.y > 30f))
		{
			this.SetHearth(0f);
		}
	}

	private void OnMouseDown()
	{
	}

	public bool OnFrameGame
	{
		get
		{
			return base.gameObject.transform.position.x > -this._frameGame.HorzExtent - this._scale.x * this.CircleCollider2D.radius && base.gameObject.transform.position.x < this._frameGame.HorzExtent + this._scale.x * this.CircleCollider2D.radius;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("ground") && this._frameGame.PlayController.GameStatus == PlayController.Game_Status.PLAYING)
		{
			this.Rigidbody2D.velocity = new Vector2(this.Rigidbody2D.velocity.x, 0f);
			this.Rigidbody2D.AddForce(new Vector2(0f, this._yForce[this.Size]));
			this._frameGame.PlayController.EffectController.StartDustEffect(this);
			Camera.main.DOShakePosition(0.2f, 0.02f * (float)(this.Size + 1), 10, 90f, true);
		}
	}

	public void InitBall(FrameGame frameGame, int size, int hearth)
	{
		this.SetFrameGame(frameGame);
		this.SpriteRenderer.sprite = frameGame.PlayController.BallSprite;
		this.SetSize(size);
		this.SetOrigilHearth(hearth);
	}

	public void InitBall(FrameGame frameGame, Vector2 position, bool isLeft, float targetX, int size, int hearth)
	{
		this.InitBall(frameGame, size, hearth);
		base.gameObject.transform.position = new Vector2(position.x - ((!isLeft) ? (-this.CircleCollider2D.radius) : this.CircleCollider2D.radius) * base.transform.localScale.x, position.y - this.CircleCollider2D.radius);
		float vX = (float)UnityEngine.Random.Range(-140, -70);
		if (isLeft)
		{
			vX = (float)UnityEngine.Random.Range(70, 140);
			targetX = Mathf.Max(targetX, position.x + this.CircleCollider2D.radius * base.transform.localScale.x);
		}
		else
		{
			targetX = Mathf.Min(targetX, position.x - this.CircleCollider2D.radius * base.transform.localScale.x);
		}
		this._moveTween = base.gameObject.transform.DOMoveX(targetX, 0.9f, false).OnComplete(delegate
		{
			this.StartRig();
			this.Rigidbody2D.AddForce(new Vector2(vX, 0f));
		});
	}

	public void StartRig()
	{
		this.CircleCollider2D.isTrigger = false;
		this.Rigidbody2D.gravityScale = this._gravity;
		this._tween = base.gameObject.transform.DORotate(new Vector3(0f, 0f, 360f), 6f, RotateMode.FastBeyond360).SetLoops(-1);
	}

	public void SetFrameGame(FrameGame frameGame)
	{
		this._frameGame = frameGame;
	}

	public void SetSize(int size)
	{
		this.Size = size;
		this._scale = Vector3.one * this._ballSize[this.Size] * this._frameGame.HorzExtent / this.CircleCollider2D.radius;
		base.transform.localScale = this._scale;
	}

	public void SetOrigilHearth(int hearth)
	{
		if (hearth < 1)
		{
			hearth = 1;
		}
		this.OrigilHearth = hearth;
		this.SetHearth((float)hearth);
	}

	public void Bleed(float power)
	{
		if (this._frameGame.PlayController.GameStatus != PlayController.Game_Status.PLAYING)
		{
			return;
		}
		this.SetHearth(this.Hearth - power);
		if (this._frameGame.PlayController.SurvivalMode)
		{
			this._frameGame.PlayController.PlayUI.AddScore((int)Mathf.Min(power, this.Hearth));
		}
		this._frameGame.PlayController.EffectController.StartBleed(this);
		GameController.AudioController.PlayOneShot("Audios/Effect/ball_break_02");
	}

	public void SetHearth(float hearth)
	{
		this.Hearth = hearth;
		if (this.Hearth <= 0f)
		{
			this.Hearth = 0f;
			this._frameGame.SplitBall(this, base.transform.position);
			return;
		}
		if (hearth >= 10000f)
		{
			this.TextMesh.transform.localScale = Vector3.one * 0.12f;
		}
		else if (hearth >= 1000f)
		{
			this.TextMesh.transform.localScale = Vector3.one * 0.14f;
		}
		else
		{
			this.TextMesh.transform.localScale = Vector3.one * 0.2f;
		}
		this.TextMesh.text = Mathf.Ceil(hearth) + string.Empty;
		int num;
		if (hearth <= 5f)
		{
			num = 0;
		}
		else if (hearth <= 10f)
		{
			num = 1;
		}
		else if (hearth <= 20f)
		{
			num = 2;
		}
		else if (hearth <= 40f)
		{
			num = 3;
		}
		else if (hearth <= 80f)
		{
			num = 4;
		}
		else if (hearth <= 160f)
		{
			num = 5;
		}
		else if (hearth <= 320f)
		{
			num = 6;
		}
		else if (hearth <= 640f)
		{
			num = 7;
		}
		else
		{
			num = 8;
		}
		this.SpriteRenderer.color = GameColor.GAME_COLOR[num];
		if (this._tweenScale != null)
		{
			this._tweenScale.Kill(false);
		}
		if (this._scale != Vector2.zero && this.Hearth < (float)this.OrigilHearth)
		{
			this._tweenScale = DOTween.Sequence().Append(base.transform.DOScale(this._scale * 1.15f, 0.02f)).Append(base.transform.DOScale(this._scale * 1f, 0.02f));
		}
	}

	public void DestroyBall()
	{
		this.SpriteRenderer.DOFade(0f, 0.3f).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}).OnUpdate(delegate
		{
			this.TextMesh.color = new Color(1f, 1f, 1f, this.SpriteRenderer.color.a);
		});
	}

	public void Resume()
	{
		this.Rigidbody2D.velocity = this._savedVelocity;
		this.CircleCollider2D.isTrigger = this._saveTrigger;
		this.Rigidbody2D.gravityScale = this._saveGravity;
		this.Rigidbody2D.isKinematic = false;
		if (this._tween != null)
		{
			this._tween.Rewind(true);
		}
		if (this._moveTween != null)
		{
			this._moveTween.PlayForward();
		}
		if (this._tweenScale != null)
		{
			this._tweenScale.PlayForward();
		}
	}

	public void Pause()
	{
		this._savedVelocity = this.Rigidbody2D.velocity;
		this._saveGravity = this.Rigidbody2D.gravityScale;
		this._saveTrigger = this.CircleCollider2D.isTrigger;
		this.CircleCollider2D.isTrigger = true;
		this.Rigidbody2D.gravityScale = 0f;
		this.Rigidbody2D.velocity = Vector2.zero;
		this.Rigidbody2D.isKinematic = true;
		if (this._tween != null)
		{
			this._tween.Pause<Tween>();
		}
		if (this._moveTween != null)
		{
			this._moveTween.Pause<Tween>();
		}
		if (this._tweenScale != null)
		{
			this._tweenScale.Pause<Sequence>();
		}
	}

	public SpriteRenderer Eye;

	private float _gravity = 0.7f;

	private float[] _ballSize = new float[]
	{
		0.14f,
		0.2f,
		0.3f,
		0.44f
	};

	public int Size;

	public float Hearth;

	public int OrigilHearth;

	public SpriteRenderer SpriteRenderer;

	public TextMesh TextMesh;

	private float[] _yForce = new float[]
	{
		456.5f,
		498f,
		539.5f,
		581f
	};

	public int IndexBall;

	public Rigidbody2D Rigidbody2D;

	public CircleCollider2D CircleCollider2D;

	private FrameGame _frameGame;

	private Tween _moveTween;

	private Tween _tween;

	private Vector2 _scale;

	private Sequence _tweenScale;

	private Vector3 _savedVelocity;

	private bool _saveTrigger;

	private float _saveGravity;
}
