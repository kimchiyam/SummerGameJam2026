using UnityEngine;

public static class CountAlien
{
    private static int goodBoy = 0;
    private static int badBoy = 0;


    public static int AlienCount { get; private set; }


    public static void TrueAlien()
    {
        AlienCount++;
        goodBoy++;
    }

    public static void FalseAlien(int minus)
    {
        AlienCount -= minus;
        badBoy++;
    }

    public static void MiddleCheck()
    {

    }
}
