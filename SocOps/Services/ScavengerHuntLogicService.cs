using SocOps.Data;
using SocOps.Models;

namespace SocOps.Services;

public class ScavengerHuntLogicService
{
    private static readonly Random _random = new();

    /// <summary>
    /// Shuffle an array using Fisher-Yates algorithm
    /// </summary>
    private static List<T> ShuffleArray<T>(List<T> array)
    {
        var shuffled = new List<T>(array);
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }
        return shuffled;
    }

    /// <summary>
    /// Generate a new scavenger hunt list with shuffled questions (no free space)
    /// </summary>
    public static List<HuntTask> GenerateHunt()
    {
        var shuffledQuestions = ShuffleArray(Questions.QuestionsList);
        var hunt = new List<HuntTask>();

        for (int i = 0; i < shuffledQuestions.Count; i++)
        {
            hunt.Add(new HuntTask
            {
                Id = i,
                Question = shuffledQuestions[i],
                IsChecked = false
            });
        }

        return hunt;
    }

    /// <summary>
    /// Toggle an item's checked state - returns new list (immutable pattern)
    /// </summary>
    public static List<HuntTask> ToggleItem(List<HuntTask> items, int itemId)
    {
        var updated = new List<HuntTask>();
        foreach (var item in items)
        {
            if (item.Id == itemId)
            {
                updated.Add(new HuntTask
                {
                    Id = item.Id,
                    Question = item.Question,
                    IsChecked = !item.IsChecked
                });
            }
            else
            {
                updated.Add(new HuntTask
                {
                    Id = item.Id,
                    Question = item.Question,
                    IsChecked = item.IsChecked
                });
            }
        }
        return updated;
    }

    /// <summary>
    /// Check if all items are completed
    /// </summary>
    public static bool IsHuntComplete(List<HuntTask> items)
    {
        return items.All(item => item.IsChecked);
    }

    /// <summary>
    /// Get progress as (checked count, total count, percentage)
    /// </summary>
    public static (int CheckedCount, int TotalCount, float Percentage) GetProgress(List<HuntTask> items)
    {
        int checkedCount = items.Count(item => item.IsChecked);
        int totalCount = items.Count;
        float percentage = totalCount > 0 ? (checkedCount / (float)totalCount) * 100 : 0;

        return (checkedCount, totalCount, percentage);
    }
}
