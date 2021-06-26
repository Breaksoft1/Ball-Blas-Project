
using System;

public class FormatUtil
{
	public static string FormatTimeLeagues(long timeSecond)
	{
		long num = 0L;
		long num2;
		long num3;
		long num4;
		if (timeSecond >= 86400L)
		{
			num = timeSecond / 86400L;
			num2 = (timeSecond - num * 60L * 60L * 24L) / 3600L;
			num3 = (timeSecond - num * 60L * 60L * 24L - num2 * 60L * 60L) / 60L;
			num4 = timeSecond - num * 60L * 60L * 24L - num2 * 60L * 60L - num3 * 60L;
		}
		else if (timeSecond >= 3600L)
		{
			num2 = timeSecond / 3600L;
			num3 = (timeSecond - num2 * 60L * 60L) / 60L;
			num4 = timeSecond - num2 * 60L * 60L - num3 * 60L;
		}
		else if (timeSecond >= 60L)
		{
			num2 = 0L;
			num3 = timeSecond / 60L;
			num4 = timeSecond - num3 * 60L;
		}
		else
		{
			num2 = 0L;
			num3 = 0L;
			num4 = timeSecond;
		}
		string text = num4 + string.Empty;
		string text2 = num3 + string.Empty;
		string text3 = num2 + string.Empty;
		if (text.Length == 0)
		{
			text = "0";
		}
		if (text2.Length == 0)
		{
			text2 = "00";
		}
		else if (text2.Length == 1)
		{
			text2 = string.Empty + text2;
		}
		if (text3.Length == 0)
		{
			text3 = "00";
		}
		else if (text3.Length == 1)
		{
			text3 = "0" + text3;
		}
		string result = string.Empty;
		if (num > 0L)
		{
			result = string.Concat(new object[]
			{
				num,
				"D ",
				num2,
				"H"
			});
		}
		else if (num2 == 0L && num3 == 0L)
		{
			result = text + " SECOND";
		}
		else if (num2 == 0L && num3 >= 1L)
		{
			result = text2 + " MINUTE";
		}
		else
		{
			result = text3 + "H " + text2 + "M";
		}
		return result;
	}

	public static string FormatTime(long timeSecond)
	{
		long num = 0L;
		long num2;
		long num3;
		long num4;
		if (timeSecond >= 86400L)
		{
			num = timeSecond / 86400L;
			num2 = (timeSecond - num * 60L * 60L * 24L) / 3600L;
			num3 = (timeSecond - num * 60L * 60L * 24L - num2 * 60L * 60L) / 60L;
			num4 = timeSecond - num * 60L * 60L * 24L - num2 * 60L * 60L - num3 * 60L;
		}
		else if (timeSecond >= 3600L)
		{
			num2 = timeSecond / 3600L;
			num3 = (timeSecond - num2 * 60L * 60L) / 60L;
			num4 = timeSecond - num2 * 60L * 60L - num3 * 60L;
		}
		else if (timeSecond >= 60L)
		{
			num2 = 0L;
			num3 = timeSecond / 60L;
			num4 = timeSecond - num3 * 60L;
		}
		else
		{
			num2 = 0L;
			num3 = 0L;
			num4 = timeSecond;
		}
		string text = num4 + string.Empty;
		string text2 = num3 + string.Empty;
		string text3 = num2 + string.Empty;
		if (text.Length == 0)
		{
			text = "00";
		}
		else if (text.Length == 1)
		{
			text = "0" + text;
		}
		if (text2.Length == 0)
		{
			text2 = "00";
		}
		else if (text2.Length == 1)
		{
			text2 = "0" + text2;
		}
		if (text3.Length == 0)
		{
			text3 = "00";
		}
		else if (text3.Length == 1)
		{
			text3 = "0" + text3;
		}
		string result = string.Empty;
		if (num > 0L)
		{
			result = string.Concat(new object[]
			{
				num,
				"D ",
				num2,
				"H"
			});
		}
		else if (num2 == 0L && num3 == 0L)
		{
			result = "00:" + text + string.Empty;
		}
		else if (num2 == 0L)
		{
			result = text2 + ":" + text + string.Empty;
		}
		else
		{
			result = text3 + "H " + text2 + "M";
		}
		return result;
	}

	public static string FormatHighScore(long score)
	{
		if (score < 10000L)
		{
			return FormatUtil.FormatMoneyDetail(score);
		}
		return FormatUtil.FormatMoney(score);
	}

	public static string FormatMoney(long money)
	{
		string arg = string.Empty;
		float num = (float)money;
		if ((double)num >= Math.Pow(10.0, 12.0))
		{
			num /= (float)((long)Math.Pow(10.0, 12.0));
			arg = "T";
		}
		else if ((double)num >= Math.Pow(10.0, 9.0))
		{
			num /= (float)((long)Math.Pow(10.0, 9.0));
			arg = "B";
		}
		else if ((double)num >= Math.Pow(10.0, 6.0))
		{
			num /= (float)((long)Math.Pow(10.0, 6.0));
			arg = "M";
		}
		else if ((double)num >= Math.Pow(10.0, 3.0) && (double)num < Math.Pow(10.0, 6.0))
		{
			num /= (float)((long)Math.Pow(10.0, 3.0));
			arg = "K";
		}
		return FormatUtil.Floor2(num) + arg;
	}

	private static float Floor2(float f)
	{
		return (float)Math.Floor((double)(f * 10f)) / 10f;
	}

	public static string FormatMoneyDetail(long money)
	{
		if (money < 0L)
		{
			money = 0L;
		}
		string text = string.Empty;
		string text2 = string.Empty;
		text = money + string.Empty;
		for (int i = 0; i < text.Length; i++)
		{
			text2 += text[text.Length - i - 1];
			if ((i + 1) % 3 == 0 && i < text.Length - 1)
			{
				text2 += ",";
			}
		}
		text = string.Empty;
		for (int j = 0; j < text2.Length; j++)
		{
			text += text2[text2.Length - j - 1];
		}
		return text;
	}
}
