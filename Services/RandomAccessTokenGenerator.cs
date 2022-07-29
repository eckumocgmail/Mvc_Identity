using System;

public class RandomAccessTokenGenerator
{
    private static string Punct = @"~!@#$%^&*()_+=[]{}'""<>";
    private static string Numbers = "0123456789";
    private static string Ru = "ёЁйцукенгшщзхъфывапролджэячсмитьбю"+ ("ёЁйцукенгшщзхъфывапролджэячсмитьбю".ToUpper());
    private static string En = "qwertyuiopasdfghjklzxcvbnm" + ("qwertyuiopasdfghjklzxcvbnm".ToUpper());
  
    private static Random R = new Random();

    public static int GetRandomInt(int from, int to)
    {
        R.NextDouble();
        R.NextDouble();
        return (int)(Math.Floor(R.NextDouble() * (to - from)) + from);
    }

    public static int GetRandomInt( int to) => GetRandomInt(0, to);
 
    /// <param name="length"> Длина токена </param>
    /// <param name="uppercase"> Количество символов в верхнем регистре</param>
    /// <returns></returns>
    public static string GetRandomEn( int length, int uppercase )
    {
        string result = "";
        while(result.Length < length)
        {
            result += "qwertyuiopasdfghjklzxcvbnm"[GetRandomInt("qwertyuiopasdfghjklzxcvbnm".Length)];
        }
        int p = 0;
        do
        {
            p = GetRandomInt(result.Length);
        } while ((result[p] + "").ToUpper() == (result[p] + ""));
        
        return result.Substring(0,p)+((result[p]+"").ToUpper())+result.Substring(p+1);
    }

    public static string GetRandomNumbers(int length)
    {
        string result = "";
        while (result.Length < length)
        {
            result += Numbers[GetRandomInt(Numbers.Length)];
        }         
        return result;
    }


    public string GetRandomToken()
    {
        return GetRandomEn(4, 2) + GetRandomNumbers(2) +
            GetRandomEn(2, 4) + GetRandomNumbers(2);
    }



}
