using SocOps.Models;
using System.Text.Json;
using Microsoft.JSInterop;

namespace SocOps.Services;

public class BingoGameService
{
    private const string STORAGE_KEY = "bingo-game-state";
    private const int STORAGE_VERSION = 1;

    private readonly IJSRuntime _jsRuntime;

    public GameState CurrentGameState { get; private set; } = GameState.Start;
    public List<BingoSquareData> Board { get; private set; } = new();
    public BingoLine? WinningLine { get; private set; }
    public HashSet<int> WinningSquareIds => BingoLogicService.GetWinningSquareIds(WinningLine);
    public bool ShowBingoModal { get; private set; }
    public List<HuntTask> ScavengerHuntItems { get; private set; } = new();
    public bool ShowScavengerHuntCompletionModal { get; private set; }

    public event Action? OnStateChanged;

    public BingoGameService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        await LoadGameStateAsync();
    }

    public void StartGame()
    {
        Board = BingoLogicService.GenerateBoard();
        WinningLine = null;
        CurrentGameState = GameState.Playing;
        ShowBingoModal = false;
        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void HandleSquareClick(int squareId)
    {
        Board = BingoLogicService.ToggleSquare(Board, squareId);

        // Check for bingo after toggling
        if (WinningLine == null)
        {
            var bingo = BingoLogicService.CheckBingo(Board);
            if (bingo != null)
            {
                WinningLine = bingo;
                CurrentGameState = GameState.Bingo;
                ShowBingoModal = true;
            }
        }

        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void ResetGame()
    {
        CurrentGameState = GameState.Start;
        Board = new();
        WinningLine = null;
        ShowBingoModal = false;
        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void DismissModal()
    {
        ShowBingoModal = false;
        NotifyStateChanged();
    }

    public void StartScavengerHunt()
    {
        ScavengerHuntItems = ScavengerHuntLogicService.GenerateHunt();
        CurrentGameState = GameState.ScavengerHunt;
        ShowScavengerHuntCompletionModal = false;
        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void HandleScavengerHuntItemClick(int itemId)
    {
        ScavengerHuntItems = ScavengerHuntLogicService.ToggleItem(ScavengerHuntItems, itemId);

        // Check if hunt is complete
        if (ScavengerHuntLogicService.IsHuntComplete(ScavengerHuntItems))
        {
            CurrentGameState = GameState.ScavengerHuntComplete;
            ShowScavengerHuntCompletionModal = true;
        }

        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void DismissScavengerHuntCompletion()
    {
        CurrentGameState = GameState.Start;
        ScavengerHuntItems = new();
        ShowScavengerHuntCompletionModal = false;
        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();

    private async Task LoadGameStateAsync()
    {
        try
        {
            var saved = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", STORAGE_KEY);
            if (!string.IsNullOrEmpty(saved))
            {
                var data = JsonSerializer.Deserialize<StoredGameData>(saved);
                if (data != null && data.Version == STORAGE_VERSION)
                {
                    CurrentGameState = data.GameState;
                    Board = data.Board;
                    WinningLine = data.WinningLine;
                    ScavengerHuntItems = data.ScavengerHuntItems ?? new();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load game state: {ex.Message}");
        }
    }

    private async Task SaveGameStateAsync()
    {
        try
        {
            var data = new StoredGameData
            {
                Version = STORAGE_VERSION,
                GameState = CurrentGameState,
                Board = Board,
                WinningLine = WinningLine,
                ScavengerHuntItems = ScavengerHuntItems
            };
            var json = JsonSerializer.Serialize(data);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", STORAGE_KEY, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save game state: {ex.Message}");
        }
    }

    private class StoredGameData
    {
        public int Version { get; set; }
        public GameState GameState { get; set; }
        public List<BingoSquareData> Board { get; set; } = new();
        public BingoLine? WinningLine { get; set; }
        public List<HuntTask> ScavengerHuntItems { get; set; } = new();
    }
}
