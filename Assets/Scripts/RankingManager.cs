using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RankEntry
{
    public string name;
    public int score;
    public string date;
}

[Serializable]
class RankList
{
    public List<RankEntry> entries = new List<RankEntry>();
}

public static class RankingManager
{
    const int MaxEntries = 10;

    static string Key(string difficulty) => "ranking_" + difficulty;

    public static List<RankEntry> Load(string difficulty)
    {
        string json = PlayerPrefs.GetString(Key(difficulty), "");
        if (string.IsNullOrEmpty(json)) return new List<RankEntry>();
        return JsonUtility.FromJson<RankList>(json).entries;
    }

    // Returns the 1-based position reached, or -1 if it didn't make the top 10.
    public static int Submit(string difficulty, string playerName, int score)
    {
        List<RankEntry> list = Load(difficulty);

        RankEntry entry = new RankEntry
        {
            name = string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName,
            score = score,
            date = DateTime.Now.ToString("dd/MM/yyyy")
        };

        list.Add(entry);
        list.Sort((a, b) => b.score.CompareTo(a.score));
        if (list.Count > MaxEntries) list.RemoveRange(MaxEntries, list.Count - MaxEntries);

        int position = list.IndexOf(entry);

        PlayerPrefs.SetString(Key(difficulty), JsonUtility.ToJson(new RankList { entries = list }));
        PlayerPrefs.Save();

        return position >= 0 ? position + 1 : -1;
    }

    public static void Clear(string difficulty)
    {
        PlayerPrefs.DeleteKey(Key(difficulty));
    }
}
