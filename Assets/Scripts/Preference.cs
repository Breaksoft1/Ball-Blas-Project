
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public class Preference
{
	private Preference()
	{
		this.LoadData();
	}

	public static Preference Instance
	{
		get
		{
			if (Preference._instance == null)
			{
				Preference._instance = new Preference();
			}
			return Preference._instance;
		}
	}

	private void LoadData()
	{
		if (PlayerPrefs.HasKey(this.DATA))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(this.DataGame.GetType());
			StringReader textReader = new StringReader(PlayerPrefs.GetString(this.DATA));
			this.DataGame = (DataGame)xmlSerializer.Deserialize(textReader);
			if (this.DataGame.CannonStatuses.Length < 10)
			{
				CannonStatus[] array = new CannonStatus[10];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new CannonStatus();
					array[i].Id = i;
					array[i].IsOpen = false;
					array[i].NumTry = 0;
				}
				for (int j = 0; j < this.DataGame.CannonStatuses.Length; j++)
				{
					array[j] = this.DataGame.CannonStatuses[j];
				}
				this.DataGame.CannonStatuses = array;
			}
		}
		else
		{
			this.SaveData();
		}
	}

	public void SaveData()
	{
		XmlSerializer xmlSerializer = new XmlSerializer(this.DataGame.GetType());
        Utf8StringWriter stringWriter = new Utf8StringWriter();
        xmlSerializer.Serialize(stringWriter, this.DataGame);
		PlayerPrefs.SetString(this.DATA, stringWriter.ToString());
	}

	public string DATA = "JumpBall";

	public DataGame DataGame = new DataGame();

	private static Preference _instance;

    public sealed class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }

}
