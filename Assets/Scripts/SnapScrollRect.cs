
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SnapScrollRect : ScrollRect
{
	private new void Start()
	{
	}

	private void Update()
	{
	}

	public void SetEndCallBack(SnapScrollRect.CallBack callBack)
	{
		this._callBack = callBack;
	}

	public override void OnBeginDrag(PointerEventData eventData)
	{
		base.OnBeginDrag(eventData);
		this.oldAnchored = base.content.anchoredPosition;
	}

	public void SetIndex(int index)
	{
		this.Index = index;
		base.content.anchoredPosition = new Vector2((float)this.Index * base.content.rect.width / 5f, base.content.anchoredPosition.y);
		if (this._callBack != null)
		{
			this._callBack();
		}
	}

	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);
		int num = (int)base.content.rect.width / 5;
		if (this.oldAnchored.x - base.content.anchoredPosition.x > 30f)
		{
			this.Index--;
			this.Index = (int)Mathf.Min(Mathf.Round(base.content.anchoredPosition.x / (float)num), (float)this.Index);
		}
		else if (this.oldAnchored.x - base.content.anchoredPosition.x < -30f)
		{
			this.Index++;
			this.Index = (int)Mathf.Max(Mathf.Round(base.content.anchoredPosition.x / (float)num), (float)this.Index);
		}
		this.Index = Mathf.Clamp(this.Index, -4, 0);
		if (this._tween != null)
		{
			this._tween.Kill(false);
		}
		this._tween = base.content.DOAnchorPosX((float)(this.Index * num), 0.3f, false);
		if (this._callBack != null)
		{
			this._callBack();
		}
	}

	private SnapScrollRect.CallBack _callBack;

	public int Index;

	private Vector3 oldAnchored;

	private Tween _tween;

	public delegate void CallBack();
}
