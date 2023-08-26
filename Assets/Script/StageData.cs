using System;
using UnityEngine;
[Serializable]
public class StageData
{
    public int PlayTime;
    public int PlayedRound;
    public int Accuracy;
    public int QuestionNumber;
    public int AverageSpeed;
}

[Serializable]
public class ProblemData
{
    public StageData S01, S02, S03, S04, S05, S06, S07, S08, S09, S10, S11, S12, S13, S14;
}

[Serializable]
public class DateData
{
    public ProblemData Problem;
}