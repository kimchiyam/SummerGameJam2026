using UnityEngine;

public static class CountAlien
{
    private static int goodBoy = 0;
    private static int badBoy = 0;

    public static int AlienCount { get; private set; }
    public static int TrueAlienCount => goodBoy;   // 진짜
    public static int FalseAlienCount => badBoy;   // 가짜

    public static void TrueAlien()
    {
        AlienCount++;
        goodBoy++;
    }

    public static void FalseAlien(int minus)
    {
        AlienCount = Mathf.Max(0, AlienCount - minus); // 음수 방지
        badBoy++;
    }

    public static void MiddleCheck() { }

    public static void ResetDay()
    {
        goodBoy = 0;
        badBoy = 0;
        // AlienCount = 0;
    }
}