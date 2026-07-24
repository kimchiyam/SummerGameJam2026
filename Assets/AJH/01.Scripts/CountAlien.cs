using UnityEngine;

public static class CountAlien
{
    private static int goodBoy = 0;
    private static int badBoy = 0;

    public static int AlienCount { get; private set; }
    public static int TrueAlienCount => goodBoy;   // 진짜
    public static int FalseAlienCount => badBoy;    // 가짜

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

    public static void MiddleCheck() { }

    // 하루 단위로 리셋할지, 총합만 유지할지는 취향
    public static void ResetDay()
    {
        goodBoy = 0;
        badBoy = 0;
        // AlienCount = 0;  // 총합도 매일 리셋하려면 주석 해제
    }
}