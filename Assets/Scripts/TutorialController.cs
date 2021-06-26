
using System;

public class TutorialController : BaseController
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void PlayTutorial()
	{
		this._step++;
	}

	public int _step;
}
