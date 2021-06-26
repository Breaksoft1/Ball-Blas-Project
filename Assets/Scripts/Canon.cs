
using System;
using DG.Tweening;
using Spine;
using Spine.Unity;
using UnityEngine;

public class Canon : MonoBehaviour
{
	private void Start ()
	{
		for (int i = 0; i < this.SkeletonAnimation.skeleton.slots.Count; i++) {
			if (this.SkeletonAnimation.skeleton.slots.Items [i].ToString ().Equals ("Wheel_L")) {
				this.LeftWheel = this.SkeletonAnimation.skeleton.slots.Items [i];
			}
			if (this.SkeletonAnimation.skeleton.slots.Items [i].ToString ().Equals ("Wheel_R")) {
				this.RightWheel = this.SkeletonAnimation.skeleton.slots.Items [i];
			}
			if (this.SkeletonAnimation.skeleton.slots.Items [i].ToString ().Equals ("laser_l")) {
				this.LeftArm = this.SkeletonAnimation.skeleton.slots.Items [i];
			}
			if (this.SkeletonAnimation.skeleton.slots.Items [i].ToString ().Equals ("laser_r")) {
				this.RightArm = this.SkeletonAnimation.skeleton.slots.Items [i];
			}
		}
		this.PlayController = UnityEngine.Object.FindObjectOfType<PlayController> ();
		this._canonScale = this.PlayController.FrameGame.HorzExtent / (this.BoxCollider2D.bounds.size.x * 4f);
		base.transform.localScale = Vector3.one * this._canonScale;
		this.Reset ();
	}

	private void Update ()
	{
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.gameObject.tag.Equals ("ball") || other.gameObject.tag.Equals ("bomb")) && base.gameObject.activeInHierarchy && this.PlayController.GameStatus == PlayController.Game_Status.PLAYING && this.TimeImmortal <= 0.1f) {
			this.PlayController.GameOver ();
			GameController.AudioController.PlayDeadVibrate ();
		}
	}

	private void FixedUpdate ()
	{
		this.Shoot ();
		this.Movement ();
		this.TimeImmortal -= Time.deltaTime;
		if (this.TimeImmortal <= 0f) {
			this.TimeImmortal = 0f;
			this.PlayController.Shield.SetActive (false);
		} else {
			this.PlayController.Shield.SetActive (true);
			this.PlayController.Shield.transform.position = base.transform.position;
		}
	}

	public void SpawnBullet (Vector3 position, int num)
	{
		for (int i = 0; i < num; i++) {
			Bullet component = TuNDPool.Spawn (this.Bullet, this.PlayController.FrameGame.transform).GetComponent<Bullet> ();
			component.SpriteRenderer.sprite = this.PlayController.BulletSprite;
			component.transform.localScale = Vector3.one * this._canonScale;
			component.transform.position = new Vector2 (position.x - (float)(num - 1) * component.Width / 2f + (float)i * component.Width, position.y);
			component.SetInfo (this, this.PlayController.FrameGame.VertExtent);
		}
	}

	private void Movement ()
	{
		if (this.PlayController.GameStatus == PlayController.Game_Status.PLAYING || this.PlayController.GameStatus == PlayController.Game_Status.WAIT) {
			if (Input.GetMouseButton (0)) {
				this._destination = new Vector2 (Mathf.Clamp (Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y)).x, -this.PlayController.FrameGame.HorzExtent + this.BoxCollider2D.size.x / 2f * this._canonScale, this.PlayController.FrameGame.HorzExtent - this.BoxCollider2D.size.x / 2f * this._canonScale), base.transform.position.y);
			}
			base.transform.position = new Vector2 (Mathf.Lerp (base.transform.position.x, this._destination.x, this._speed * Time.deltaTime), base.transform.position.y);
		}
		this.LeftWheel.bone.rotation = -this.LeftWheel.bone.GetWorldPosition (base.transform).x * 140f;
		this.RightWheel.bone.rotation = -this.RightWheel.bone.GetWorldPosition (base.transform).x * 140f;
	}

	private void Shoot ()
	{
		if (this.PlayController.GameStatus == PlayController.Game_Status.PLAYING) {
			if (Input.GetMouseButton (0)) {
				if (Time.time > this._timeSpawn) {
					this._timeSpawn = Time.time + 1f / Mathf.Min (this.FireSpeed, this.MaxSpeed);
					int num = (int)Mathf.Max ((this._oddSpeed + this.FireSpeed) / this.MaxSpeed, 1f);
					this._oddSpeed = Mathf.Max (0f, this._oddSpeed + this.FireSpeed - this.MaxSpeed * (float)num);
					this.SpawnBullet (this.BulletEmiter.position, num);
					if (!this._shoot) {
						this.SetAnim (this.AnimName);
						if (this.CanonPlugin != null) {
							this.CanonPlugin.SetVisible (true);
						}
					}
					this._shoot = true;
				}
			} else {
				if (this._shoot) {
					this.SetAnim (this.IdleName);
					if (this.CanonPlugin != null) {
						this.CanonPlugin.SetVisible (false);
					}
				}
				this._shoot = false;
			}
		} else {
			this._timeSpawn = 0f;
			if (this._shoot) {
				this.SetAnim (this.IdleName);
				if (this.CanonPlugin != null) {
					this.CanonPlugin.SetVisible (false);
				}
			}
			this._shoot = false;
		}
	}

	public void SetAnim (string animName)
	{
		try {
			this.SkeletonAnimation.AnimationState.SetAnimation (0, animName, true);
		} catch (Exception ex) {
			this.SkeletonAnimation.AnimationState.ClearTracks ();
		}
	}

	public void Reset ()
	{
		base.transform.DOLocalMoveX (0f, 0.2f, false);
		this._destination = base.transform.position;
		this.FirePower = Preference.Instance.DataGame.FirePower;
		this.FireSpeed = (float)Preference.Instance.DataGame.FireSpeed;
		this._shoot = false;
		this.SetAnim (this.IdleName);
		if (this.CanonPlugin != null) {
			this.CanonPlugin.SetVisible (false);
		}
	}

	public void PowerUp ()
	{
		base.transform.DOShakeScale (0.15f, 0.2f, 10, 90f, true);
		this.PlayController.EffectController.StartPowerUpEff (base.transform.position);
	}

	public int CanonId;

	public CanonPlugin CanonPlugin;

	public string AnimName;

	public string IdleName;

	public float FirePower = 1f;

	public float FireSpeed = 100f;

	public Vector2 BulletVelocity;

	public float MaxSpeed = 40f;

	private Vector2 _destination;

	private float _speed = 30f;

	public BoxCollider2D BoxCollider2D;

	[HideInInspector]
	public PlayController PlayController;

	public GameObject Bullet;

	public Transform BulletEmiter;

	private float _timeSpawn;

	private float _oddSpeed;

	public SkeletonAnimation SkeletonAnimation;

	public float _canonScale;

	private bool _shoot;

	public Slot LeftArm;

	public Slot RightArm;

	public Slot LeftWheel;

	public Slot RightWheel;

	public float TimeImmortal;
}
