//Handles the history of rolls

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO Separate History from GUI function?
public class History : MonoBehaviour {

	public GameObject infoPrefab;
	public GameObject historyPanel;
	public RectTransform display;

	public int MaxLength
	{
		get
		{
			return maxLength;
		}
		set
		{
			if(value >= minMaxLength)
			{
				maxLength = value;
			}
		}
	}

	private List<Roll> rollList;
	private int minMaxLength = 5;

	[SerializeField] private int maxLength;

	public void AddRoll(int rolled, int kept, int crits, int result)
	{
		Roll roll = new Roll(rolled, kept, crits, result);
		rollList.Add(roll);
		AddEntryToPanel(roll);
		UpdateList();
	}

	public Roll GetRoll(int index)
	{
		return rollList[index].copy();
	}

	public void ClearHistory()
	{
		rollList.Clear();
	}

	public void SetPanelActive(bool active)
	{
		historyPanel.SetActive(active);
	}

	void Start()
	{
		rollList = new List<Roll>();
		maxLength = (maxLength < minMaxLength) ? minMaxLength : maxLength;
		UpdateList();
	}

	private void AddEntryToPanel(Roll roll)
	{
		GameObject entry = GameObject.Instantiate(infoPrefab);
		Text[] labels = entry.GetComponentsInChildren<Text>();
		//German Time:
		//labels[0].text = roll.TimeStamp.Hour.ToString() + ":" + ((roll.TimeStamp.Minute < 10) ? "0" : "") + roll.TimeStamp.Minute.ToString();
		labels[0].text = roll.TimeStamp.ToShortTimeString();
		labels[2].text = roll.Rolled.ToString();	//2 steps because of separators
		labels[4].text = roll.Kept.ToString();
		labels[6].text = roll.Crits.ToString();
		labels[8].text = roll.Result.ToString();
		entry.transform.parent = display;
		entry.GetComponent<RectTransform>().localScale = Vector3.one;
		entry.name = "Entry " + roll.TimeStamp.TimeOfDay;
		entry.SetActive(true);
	}

	private void UpdateList()
	{
		while(rollList.Count > maxLength)
		{
			rollList.RemoveAt(0);
			GameObject.Destroy(display.GetChild(0).gameObject);
		}
		display.sizeDelta = new Vector2 (display.sizeDelta.x, rollList.Count * infoPrefab.GetComponent<RectTransform>().sizeDelta.y);
		display.transform.parent.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
	}

}

//Contains data for one roll
public class Roll{

	private int rolled;
	private int kept;
	private int crits;
	private int result;
	private DateTime timeStamp;

	public Roll(int roll, int kep, int crit, int res)
	{
		timeStamp = System.DateTime.Now;
		rolled = roll;
		kept = kep;
		crits = crit;
		result = res;
	}

	public Roll(Roll orig)
	{
		timeStamp = orig.timeStamp;
		rolled = orig.rolled;
		kept = orig.kept;
		crits = orig.crits;
		result = orig.result;
	}

	public int Rolled
	{
		get
		{
			return rolled;
		}
	}
	public int Kept
	{
		get
		{
			return kept;
		}
	}
	public int Crits
	{
		get
		{
			return crits;
		}
	}
	public int Result
	{
		get
		{
			return result;
		}
	}

	public DateTime TimeStamp
	{
		get
		{
			return timeStamp;
		}
	}

	public Roll copy()
	{
		return new Roll(this);
	}

}
