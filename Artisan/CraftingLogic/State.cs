﻿using Artisan.CraftingLogic.CraftData;
using Artisan.RawInformation.Character;
using System.Text;

namespace Artisan.CraftingLogic;

public class CraftState
{
    // player stats
    public int StatCraftsmanship;
    public int StatControl;
    public int StatCP;
    public int StatLevel;
    public bool UnlockedManipulation;
    public bool Specialist;
    public bool Splendorous;

    // recipe stats
    public bool CraftCollectible;
    public bool CraftExpert;
    public int CraftLevel; // Recipe.RecipeLevelTable.ClassJobLevel
    public int CraftDurability; // Recipe.RecipeLevelTable.Durability * Recipe.DurabilityFactor / 100
    public int CraftProgress; // Recipe.RecipeLevelTable.Difficulty * Recipe.DifficultyFactor / 100
    public int CraftProgressDivider; // Recipe.RecipeLevelTable.ProgressDivider
    public int CraftProgressModifier; // Recipe.RecipeLevelTable.ProgressModifier
    public int CraftQualityDivider; // Recipe.RecipeLevelTable.QualityDivider
    public int CraftQualityModifier; // Recipe.RecipeLevelTable.QualityModifier
    public int CraftQualityMax; // Recipe.RecipeLevelTable.Quality * Recipe.QualityFactor / 100
    public int CraftQualityMin1; // min/first breakpoint
    public int CraftQualityMin2;
    public int CraftQualityMin3;
    public float[] CraftConditionProbabilities = { }; // TODO: this assumes that new condition does not depend on prev - this is what my preliminary findings suggest (except for forced transitions)

    public static float[] NormalCraftConditionProbabilities(int statLevel) => [1, statLevel >= 63 ? 0.25f : 0.2f, 0.04f];
    public static float[] EWRelicT1CraftConditionProbabilities() => [1, 0.03f, 0, 0, 0.12f, 0.12f, 0.12f, 0, 0, 0.12f];
    public static float[] EWRelicT2CraftConditionProbabilities() => [1, 0.04f, 0, 0, 0, 0.15f, 0.12f, 0.12f, 0.15f, 0.12f];
    public static float[] EW5StarCraftConditionProbabilities() => [1, 0.04f, 0, 0, 0.12f, 0.12f, 0.10f, 0.10f, 0.12f, 0.12f];
}

public class StepState
{
    public int Index;
    public int Progress;
    public int Quality;
    public int Durability;
    public int RemainingCP;
    public Condition Condition;
    public int IQStacks;
    public int WasteNotLeft;
    public int ManipulationLeft;
    public int GreatStridesLeft;
    public int InnovationLeft;
    public int VenerationLeft;
    public int MuscleMemoryLeft;
    public int FinalAppraisalLeft;
    public int CarefulObservationLeft;
    public bool HeartAndSoulActive;
    public bool HeartAndSoulAvailable;
    public Skills PrevComboAction;

    public override string ToString()
    {
        var sb = new StringBuilder($"#{Index} {Condition}: {Progress}/{Quality}/{Durability}/{RemainingCP}; IQ={IQStacks}");
        if (WasteNotLeft > 0)
            sb.Append($", WN={WasteNotLeft}");
        if (ManipulationLeft > 0)
            sb.Append($", Manip={ManipulationLeft}");
        if (GreatStridesLeft > 0)
            sb.Append($", GS={GreatStridesLeft}");
        if (InnovationLeft > 0)
            sb.Append($", Inno={InnovationLeft}");
        if (VenerationLeft > 0)
            sb.Append($", Vene={VenerationLeft}");
        if (MuscleMemoryLeft > 0)
            sb.Append($", MuMe={MuscleMemoryLeft}");
        if (FinalAppraisalLeft > 0)
            sb.Append($", FA={FinalAppraisalLeft}");
        sb.Append($", CO={CarefulObservationLeft}, HS={(HeartAndSoulActive ? "active" : HeartAndSoulAvailable ? "avail" : "none")}; Prev={PrevComboAction}");
        return sb.ToString();
    }
}
