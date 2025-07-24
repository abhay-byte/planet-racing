using System;
using System.Collections.Generic;
using alias = System;

public class PRUtils
{
	private PRUtils()
	{
	}

    public static PRUtils Instance = new PRUtils();

    public static readonly alias.Random randomNumbers = new alias.Random();

    public K GetFromMap<T, K>(Dictionary<T, K> map, T key )
    {
		if(map.ContainsKey(key))
        {
			return map[key];
        } else
        {
            return default;
        }
    }

    public static string CurrencyFormater(string value)
    {
        string formatedValue = "";
        int i = value.Length; int j = 1;
        while(0 < i)
        {

            formatedValue = value[i-1] + formatedValue;
            if (j % 3 == 0 && (value.Length >j))
            {
                formatedValue = "," + formatedValue;
            }
            i--; j++;
        }

        return formatedValue;
    }

    public static T GetSingle<T>(List<T> list)
    {
        return list[randomNumbers.Next(0, list.Count)];
    }
    public static T GetSingle<T>(T[] list)
    {
        return list[randomNumbers.Next(0, list.Length)];
    }

}
