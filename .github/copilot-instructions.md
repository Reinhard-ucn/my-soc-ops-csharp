# Soc Ops — Copilot Workspace Instructions

**Project**: Social Bingo game built with Blazor WebAssembly (.NET 10)  
**Purpose**: In-person mixer icebreaker where players find people matching questions to achieve 5-in-a-row bingo  
**Framework**: [Blazor WebAssembly](https://learn.microsoft.com/en-us/aspnet/core/blazor/), [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

---

## 🚀 Quick Start

```bash
# Build the project
dotnet build SocOps/SocOps.csproj

# Run dev server (opens at http://localhost:5166)
dotnet run --project SocOps
```

**Key task**: Already running? Check terminal output for port number.

---

## 🏗️ Architecture Overview

```
SocOps/
├── Program.cs                    # DI setup, service registration
├── App.razor                     # Root component
├── _Imports.razor                # Global usings
├── Components/                   # Reusable UI components
│   ├── BingoBoard.razor          # 5x5 grid component
│   ├── BingoSquare.razor         # Individual square with click handlers
│   ├── BingoModal.razor          # Question modal dialog
│   ├── GameScreen.razor          # Active game view
│   └── StartScreen.razor         # Initial player setup screen
├── Pages/                        # Routable pages
│   ├── Home.razor                # Entry point
│   ├── Counter.razor, Weather.razor  # Template examples
│   └── NotFound.razor            # 404 handler
├── Models/                       # Data structures
│   ├── GameState.cs              # Enum: Start, Playing, Bingo
│   ├── BingoLine.cs              # Winner validation (row/col/diag)
│   └── BingoSquareData.cs        # {Id, Question, IsMarked}
├── Services/                     # Business logic
│   ├── BingoGameService.cs       # State management + event notifications
│   └── BingoLogicService.cs      # Static game logic (board gen, bingo check)
├── Data/                         # Static content
│   └── Questions.cs              # Bingo question pool
├── Layout/                       # Navigation & layout
│   ├── MainLayout.razor
│   └── NavMenu.razor
├── Properties/                   # Build metadata
│   └── launchSettings.json
└── wwwroot/                      # Static assets
    ├── index.html                # Main HTML shell
    ├── css/
    │   └── app.css               # Utility classes (Tailwind-like)
    └── lib/bootstrap/            # Bootstrap CDN (optional)
```

---

## 🎯 Key Concepts

### State Management

The project uses **event-driven state management** with localStorage persistence:

1. **BingoGameService** (scoped service)
   - Owns game state: `CurrentGameState`, `Board`, `WinningLine`
   - Publishes `OnStateChanged` event when state mutates
   - Persists to localStorage via JSInterop

2. **Component Subscription Pattern**
   ```csharp
   @implements IAsyncDisposable

   private async Task OnInitializedAsync()
   {
       GameService.OnStateChanged += StateHasChanged;
       await GameService.InitializeAsync();
   }

   async ValueTask IAsyncDisposable.DisposeAsync()
   {
       GameService.OnStateChanged -= StateHasChanged;
   }
   ```

3. **Immutable Board Updates**
   - `BingoLogicService` returns new board state (functional style)
   - Avoid mutating board directly; always call service methods

### Game Flow

```
StartScreen (PlayerName input)
    ↓ [StartGame()]
GameScreen (Display board + modal)
    ↓ [Click square]
BingoSquare → HandleSquareClick()
    ↓ [Check bingo]
BingoLogicService.CheckBingo() → BingoLine?
    ↓ if winning line
GameState.Bingo (Show modal, disable board)
```

### Models Reference

```csharp
public enum GameState { Start, Playing, Bingo }

public record BingoSquareData(int Id, string Question, bool IsMarked);

public record BingoLine(LineType Type, int Index);
public enum LineType { Row, Column, DiagonalDown, DiagonalUp }
```

---

## 📋 Development Conventions

### C# Code Style

- **PascalCase** for all public members (properties, methods, events)
- **camelCase** for private fields and local variables
- Use **nullable reference types** (enabled in .csproj: `<Nullable>enable</Nullable>`)
- Prefer **implicit usings** (enabled: `<ImplicitUsings>enable</ImplicitUsings>`)

**Example**:
```csharp
public class BingoGameService
{
    private readonly IJSRuntime _jsRuntime;  // camelCase for private
    public GameState CurrentGameState { get; private set; }  // PascalCase for public
    
    public event Action? OnStateChanged;  // Event naming: On[Action]
}
```

### Component Conventions

- **Component name** matches filename: `BingoSquare.razor` → `<BingoSquare />`
- **Parameters** are public properties with `[Parameter]` attribute
- **Lifecycle**: Use `OnInitializedAsync()` for async init, unsubscribe in `DisposeAsync()`
- **Event handlers**: Named `Handle[Event]` pattern (e.g., `HandleSquareClick`)

### Razor Template Best Practices

- Use **directive attributes** instead of string interpolation for binding
  ```razor
  <!-- ✓ Good -->
  <button @onclick="@((ChangeEventArgs e) => HandleChange(e.Value))">

  <!-- ✗ Avoid -->
  <button onclick="@("javascript:...")">
  ```

- Keep markup simple; move complex logic to C# code-behind (`@code` block)
- Avoid nested conditionals: extract to helper methods

---

## 🎨 Styling & Design

### CSS Utilities

The project uses **custom CSS utility classes** (Tailwind-like) in `wwwroot/css/app.css`:

- **Layout**: `.flex`, `.flex-col`, `.grid`, `.grid-cols-5`, `.items-center`, `.justify-center`
- **Spacing**: `.p-1` through `.p-6`, `.mb-2` through `.mb-8`, `.mx-auto`, `.gap-2`
- **Colors**: `.bg-accent` (primary), `.bg-marked` (green for selected), `.text-gray-*`
- **Typography**: `.text-xs` through `.text-5xl`, `.font-semibold`, `.font-bold`
- **Borders**: `.border`, `.rounded`, `.rounded-lg`, `.shadow-sm`, `.shadow-xl`

**See detailed guidance**: [.github/instructions/css-utilities.instructions.md](.github/instructions/css-utilities.instructions.md)

### Frontend Design

When redesigning UI components or adding new features:
- Avoid generic "AI slop" aesthetics
- Commit to cohesive design (typography, color theory, motion)
- Use CSS animations for high-impact moments (page load, bingo reveal)
- Choose distinctive fonts over generic choices (Inter, Arial, etc.)

**See detailed guidance**: [.github/instructions/frontend-design.instructions.md](.github/instructions/frontend-design.instructions.md)

---

## 🎮 Mario Bros Design System

The game uses a **retro 8-bit Mario Bros aesthetic** with pixel-art styling, chunky typography, and playful animations.

### Color Palette

Define colors via CSS custom properties in `:root` (see `wwwroot/css/app.css`):

```css
:root {
    --mario-red: #e63946;        /* Primary actions, danger */
    --mario-green: #06a77d;      /* Success, marked squares */
    --mario-gold: #ffd60a;       /* Bonus, bingo celebration */
    --mario-blue: #457b9d;       /* Secondary, pipe theme */
    --mario-black: #1a1a1a;      /* Text, outlines, shadows */
    --mario-white: #ffffff;      /* Backgrounds, text */
    --mario-shadow: #4a4a4a;     /* Depth effect */
}
```

**Usage in components:**
- Red (`.bg-mario-red`, `.text-mario-red`): Start button, danger states
- Green (`.bg-mario-green`, `.text-mario-green`): Marked squares, success feedback
- Gold (`.bg-mario-gold`, `.text-mario-gold`): Bingo celebration, highlights
- Blue (`.bg-mario-blue`, `.text-mario-blue`): Header, free space, secondary elements
- Black (`.text-mario-black`): Primary text, borders
- White (`.text-mario-white`, `.bg-mario-white`): Backgrounds, high contrast text

### Pixel-Art Utilities

**Thick Borders** (`.pixel-border`):
```css
border: 4px solid var(--mario-black);
```
Creates authentic chunky 8-bit square outlines. Use on all clickable elements and cards.

**Pixel Shadows** (`.pixel-shadow`, `.pixel-shadow-lg`):
```css
.pixel-shadow { box-shadow: 4px 4px 0px var(--mario-shadow); }
.pixel-shadow-lg { box-shadow: 6px 6px 0px var(--mario-shadow); }
```
Displaced shadow effect for 3D depth. Larger games use `-lg`, buttons use standard.

**Brick Background** (`.bg-brick`):
```css
background-image: linear-gradient(135deg, #d2691e 25%, transparent 25%), 
                  linear-gradient(225deg, #d2691e 25%, transparent 25%), ...
background-color: #8b4513;
```
Retro brown brick pattern. Use on full-screen backgrounds (StartScreen, GameScreen).

### Component Styling Guidelines

#### StartScreen
- **Title**: Large/bold (`text-5xl font-bold`), white text on brick background, with `pixel-shadow-lg`
- **Subtitle**: Gold text, caps, letter-spacing 0.05em
- **Card**: White bg, `pixel-border`, `pixel-shadow`, black inner text
- **Button**: Red bg, white text, caps, `pixel-border`, `pixel-shadow`, no rounded corners

#### GameScreen
- **Header**: Pipe blue background (`bg-mario-blue`), white text, `pixel-border` bottom
- **Back button**: White text, no border, hover brightens text
- **Background**: Brick pattern (`.bg-brick`)
- **Bingo indicator**: Gold background with black text and border

#### BingoBoard
- **Grid spacing**: `gap-2` (wider gaps than default for pixel clarity)
- **Aspect ratio**: `aspect-square` (1:1 ratio for playfield)

#### BingoSquare (Button states)
- **Unmarked**: 
  - White bg, black `pixel-border`, black text
  - Hover: `brightness-95` (darkens slightly)
  
- **Marked (non-winning)**:
  - Green bg (`bg-mario-green`), white text, `pixel-shadow`
  - Checkmark (✓): Top-right corner
  - Animation: None (static)
  
- **Marked (winning line)**:
  - Gold bg (`bg-mario-gold`), black text, `pixel-shadow`
  - Applies `.animate-block-bounce` (elastic pop effect)
  
- **Free space**:
  - Blue bg (`bg-mario-blue`), white text, bold caps
  - Disabled state (no click handler)

#### BingoModal (Bingo celebration)
- **Background**: Gold (`bg-mario-gold`)
- **Text**: Black, bold, caps, letter-spacing
- **Border**: `pixel-border` (black)
- **Shadow**: `pixel-shadow-lg`
- **Emoji**: Game controller (🎮) or trophy
- **Coins**: 3× coin emoji (💰) with `animate-coin-pop` and staggered delays
- **Button**: Red with white text, `pixel-border`, `pixel-shadow`

### Animation Specifications

**blockBounce** (0.3s, elastic):
```css
@keyframes blockBounce {
    0% { transform: scale(1); }
    50% { transform: scale(1.12); }
    100% { transform: scale(1); }
}
```
Applied to marked winning squares on click. Creates satisfying spring effect.

**coinPop** (0.6s, ascending fade):
```css
@keyframes coinPop {
    0% { opacity: 1; transform: scale(0.3) translateY(0); }
    100% { opacity: 0; transform: scale(1.2) translateY(-40px); }
}
```
Applied to coin emojis in bingo modal. Stagger with `animation-delay: 0.1s`, `0.2s`, etc.

**pixelFlash** (0.2s, white overlay):
```css
@keyframes pixelFlash {
    0% { opacity: 0.8; }
    100% { opacity: 0; }
}
```
Applied to modal backdrop on bingo win (celebration flash).

### Typography

- **Font family**: System fonts (`system-ui, -apple-system, sans-serif`) for performance
- **Weight**: Bold (700) for all interactive text, headings
- **Letter spacing**: Add `style="letter-spacing: 0.05em"` or `0.1em` to titles for pixel feel
- **Case**: Use CAPS for titles, buttons, and emphasis (e.g., "SOC OPS", "START GAME")

### Extending the Theme

**Adding new colors:**
1. Add CSS variable to `:root`:
   ```css
   --mario-new: #hexcode;
   ```
2. Create utility classes:
   ```css
   .bg-mario-new { background-color: var(--mario-new); }
   .text-mario-new { color: var(--mario-new); }
   ```
3. Use in components: `class="bg-mario-new text-mario-white"`

**Adding new animations:**
1. Define `@keyframes` in `app.css`
2. Create animation class:
   ```css
   .animate-my-effect {
       animation: myKeyframe 0.5s cubic-bezier(...);
   }
   ```
3. Apply in Razor: Bind via `@GetCssClasses()` method or inline

**Maintaining consistency:**
- Always use pixel borders on clickable elements (buttons, cards)
- Pair shadows with borders (no floating elements without structure)
- Animate on interaction (click, hover), not idle states
- Keep animations under 0.6s for snappy feel
- Use `cubic-bezier(0.68, -0.55, 0.265, 1.55)` for elastic bounces

---

## ✅ Quality Checklist

Before committing changes:

- [ ] **Build passes**: `dotnet build SocOps/SocOps.csproj`
- [ ] **No compiler warnings**: Check build output for `warning CS*`
- [ ] **No unused imports**: Clean up `using` statements
- [ ] **State management preserved**: Board updates go through service methods
- [ ] **Components clean up**: Event subscriptions removed in `DisposeAsync()`
- [ ] **CSS follows conventions**: Use existing utilities before adding new styles
- [ ] **Component names match files**: `BingoSquare.cs` → `<BingoSquare />`

---

## 📚 Workshop & Learning Path

The project includes a comprehensive lab guide for learning Copilot workflows:

| Part | Focus | Files |
|------|-------|-------|
| [00 Overview](workshop/00-overview.md) | Intro & checklist | — |
| [01 Setup](workshop/01-setup.md) | Context engineering (you are here!) | — |
| [02 Design](workshop/02-design.md) | UI redesign with Plan Mode | — |
| [03 Quiz Master](workshop/03-quiz-master.md) | Custom quiz themes | — |
| [04 Multi-Agent](workshop/04-multi-agent.md) | Collaborative development | — |

**Start**: Read [workshop/GUIDE.md](workshop/GUIDE.md) for structure and tips.

---

## 🔧 Troubleshooting

### Port Already in Use

If `http://localhost:5166` fails, check `SocOps/Properties/launchSettings.json`:
```json
"applicationUrl": "http://localhost:5166"
```
Or run with custom port: `dotnet run --project SocOps -- --urls "http://localhost:5555"`

### Build Fails with WASM Errors

```bash
# Clean build
rm -rf SocOps/bin SocOps/obj
dotnet build SocOps/SocOps.csproj
```

### localStorage Not Persisting

Check browser console for JSInterop errors. Ensure `BingoGameService.InitializeAsync()` is called in parent component.

---

## 📖 Key Files to Explore First

1. **[SocOps/Program.cs](SocOps/Program.cs)** — DI container setup
2. **[SocOps/Services/BingoGameService.cs](SocOps/Services/BingoGameService.cs)** — State management pattern
3. **[SocOps/Services/BingoLogicService.cs](SocOps/Services/BingoLogicService.cs)** — Game logic (pure functions)
4. **[SocOps/Components/GameScreen.razor](SocOps/Components/GameScreen.razor)** — Component lifecycle example
5. **[SocOps/wwwroot/css/app.css](SocOps/wwwroot/css/app.css)** — CSS utilities reference

---

## 🚫 Anti-Patterns to Avoid

| ✗ Don't | ✓ Do | Why |
|---------|-----|-----|
| Mutate `Board` directly | Call `BingoLogicService` methods | Immutability enables testing & debugging |
| State in component `@code` | Store in `BingoGameService` | Single source of truth, persist across routes |
| Forget to unsubscribe from events | Use `@implements IAsyncDisposable` | Prevents memory leaks in SPAs |
| Hardcode colors/spacing | Use CSS utility classes | Consistency, easier refactoring |
| Inline large templates | Extract components | Readability, reusability |
| Ignore TypeScript types in JSInterop | Use explicit JSON serializable models | Type safety across JS boundary |

---

## 🤝 Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for:
- CLA requirements (Microsoft projects)
- Code of Conduct
- PR guidelines

---

**Questions?** Start with [SUPPORT.md](SUPPORT.md) or file an issue with reproduction steps.
