---
name: AGENTS
description: Multi-agent collaborative development workflows for the Soc Ops project
---

# Soc Ops Multi-Agent Development Workflows

This document defines specialized agents that work together on different aspects of the Soc Ops Blazor project. These workflows support the 5-part workshop guide and enable sophisticated collaborative development patterns.

---

## 🏗️ Agent Ecosystem

### Core Agents

| Agent | Purpose | Trigger | Output |
|-------|---------|---------|--------|
| **Setup Agent** | Bootstrap development environment | `/setup` | Build passing, dev server running |
| **Design Agent** | UI/UX improvements | `/design [component] with [aesthetic]` | Beautiful, distinctive components |
| **Refactor Agent** | Code quality improvements | `/refactor [area]` | Cleaner, more consistent code |
| **Quiz Master** | Question theming | `/quiz-master [theme]` | Custom question sets |

### Specialized Workflows

| Workflow | Agents Involved | Use Case | Workshop Part |
|----------|-----------------|----------|---|
| **Scavenger Hunt** | Tester → Developer → Reviewer | TDD cycle (Red→Green→Refactor) | Part 04 |
| **Pixel Jam** | Designer → Animator → Accessibility Expert | Rapid UI iteration | Part 04 |
| **Code Review** | Reviewer → Refactor Agent → Tests | PR validation | Ongoing |

---

## 🔄 Workflow Patterns

### Pattern 1: Scavenger Hunt (TDD)

**Goal**: Implement features through test-driven development with Red → Green → Refactor cycles.

**Participants**:
1. **Tester Agent** — Writes failing test/spec
2. **Developer Agent** — Makes minimum code to pass
3. **Refactor Agent** — Improves code quality

**Workflow**:
```
Tester writes failing requirement
  ↓
Developer implements minimum solution
  ↓
Tester verifies it passes
  ↓
Refactor agent improves code
  ↓
All quality checks pass
  ↓
Repeat for next feature
```

**Example Task**:
```
Run: /scavenger-hunt
Goal: Add player statistics tracking
Requirement: "BingoSquare should track total games played"
Acceptance: "PlayerStats shows games completed and win percentage"
```

**Agents Collaborate**:
- Tester: "Write test that fails without tracking"
- Developer: "Add minimal stats to GameService"
- Refactor: "Extract stats to separate service, add localStorage persistence"
- Tester: "Verify stats persist across browser refresh"

### Pattern 2: Pixel Jam (Design-Driven)

**Goal**: Rapid UI iteration with focus on polish, animation, and accessibility.

**Participants**:
1. **Designer** — Vision and component updates
2. **Animator** — Motion and transitions
3. **A11y Expert** — Accessibility validation

**Workflow**:
```
Designer sketches new aesthetic
  ↓
Designer implements in components
  ↓
Animator adds smooth transitions
  ↓
A11y expert validates contrast, keyboard nav, ARIA
  ↓
Design agent refines
  ↓
Repeat for next component
```

**Example Task**:
```
Run: /pixel-jam
Theme: "Cyberpunk neon aesthetic"
Components: StartScreen, BingoBoard, BingoModal
Focus: "Make every interaction feel snappy and futuristic"
```

**Agents Collaborate**:
- Designer: "Redesign StartScreen with neon borders and dark background"
- Animator: "Add glow effects and entrance animations"
- A11y: "Verify contrast ratio meets WCAG AA, keyboard navigation works"
- Designer: "Polish transitions based on performance feedback"

### Pattern 3: PR Review Automation

**Goal**: Automated validation of pull requests against project standards.

**Trigger**: New PR created or `/review` command

**Checks**:
- ✅ Build passes with 0 warnings
- ✅ No new anti-patterns introduced
- ✅ Code style compliance (naming, imports)
- ✅ Component lifecycle correct (IAsyncDisposable)
- ✅ State management flows through services
- ✅ CSS uses utilities before custom styles
- ✅ Documentation updated

**Example Output**:
```
✅ BUILD: Passed (0 warnings)
⚠️ ANTI-PATTERNS: Found 1 issue
   - BingoSquare.razor line 42: Direct Board mutation
   
✅ NAMING: Compliant
✅ LIFECYCLE: Proper cleanup implemented
❌ DOCUMENTATION: Missing comment on refactored method

Summary: Request changes
- Fix direct mutation → use BingoLogicService.ToggleSquare()
- Add XML doc comment to HandleStateChange()
```

---

## 🎯 Running Multi-Agent Workflows

### Interactive Workflow

```bash
# Start Scavenger Hunt workflow
/scavenger-hunt

# Specify the feature to build
# Example: "Add game replay functionality"

# Follow the cycle: Red → Green → Refactor
# Agents guide you through each phase
```

### Parallel Workflows

Run multiple agents for different areas in parallel:

```bash
# Terminal 1: Design work
/design StartScreen with retro arcade theme

# Terminal 2: Quiz mastering
/quiz-master "Tech Life"

# Terminal 3: Code review
/review

# All work on separate areas simultaneously
# Merge when each is complete
```

### Scheduled Workflows

(Future enhancement) Set up background agents:
- **Nightly builds**: Full test suite + linting
- **PR validation**: Automatic code review on every PR
- **Documentation**: Keep README aligned with code

---

## 📚 Workshop Integration

Each workshop part aligns with specific agents and workflows:

### Part 00: Overview & Checklist
- ✅ Setup agent verification
- Read through all instructions

### Part 01: Setup & Context Engineering
- ✅ Setup agent (`/setup`)
- Generate workspace instructions
- **Agent**: Setup Agent

### Part 02: Design-First Frontend
- ✅ Design agent (`/design`)
- Full Blazor component redesign
- **Agents**: Design Agent + Refactor Agent
- **Workflow**: Single agent with feedback loops

### Part 03: Custom Quiz Master  
- ✅ Quiz Master agent (`/quiz-master`)
- Generate 3-5 custom question themes
- **Agents**: Quiz Master (solo)
- **Workflow**: Batch theme creation

### Part 04: Multi-Agent Development
- ✅ All agents together
- **Workflow 1**: Scavenger Hunt (TDD)
- **Workflow 2**: Pixel Jam (rapid design)
- **Workflow 3**: Code Review (validation)
- **Agents**: Tester, Developer, Designer, Reviewer

---

## 🛠️ Special Agent Commands

### Setup
```bash
/setup
```
Bootstraps development environment. Runs:
- Dependency verification
- Build compilation
- Dev server startup
- Browser preview

### Design
```bash
/design [component or page] with [aesthetic description]
```
Examples:
- `/design StartScreen with retro arcade theme`
- `/design BingoBoard with minimalist glass morphism`
- `/design entire app with cyberpunk neon aesthetic`

### Refactor
```bash
/refactor [scope: "all", "components", "services", or specific file]
```
Examples:
- `/refactor all` — Full codebase review
- `/refactor components` — Only Components folder
- `/refactor BingoGameService` — Specific file

### Quiz Master
```bash
/quiz-master [theme name]
```
Examples:
- `/quiz-master "Tech Life"`
- `/quiz-master "Office Humor"`
- `/quiz-master "Deep Chat with personality focus"`

---

## 📋 Quality Standards for All Agents

Every agent MUST verify before completion:

```
Build & Compile
  ✅ dotnet build passes
  ✅ 0 warnings
  ✅ 0 errors
  ✅ All imports used

Code Quality
  ✅ Naming conventions followed
  ✅ No anti-patterns
  ✅ Comments clear and helpful
  ✅ Component lifecycle correct

State Management
  ✅ Board updates via services
  ✅ Components subscribed to events
  ✅ Resources cleaned up in DisposeAsync()

Styling
  ✅ Uses CSS utilities first
  ✅ No hardcoded colors
  ✅ Responsive and accessible

Testing
  ✅ Game flow still works
  ✅ No breaking changes
  ✅ localStorage persistence verified
```

---

## 🚀 Recommended Workflow Progression

**Week 1**: Individual agents
1. Setup agent (`/setup`) - 5 min
2. Design agent (`/design StartScreen`) - 30 min
3. Refactor agent (`/refactor components`) - 20 min

**Week 2**: Quiz mastering
1. Quiz Master (`/quiz-master Tech Life`) - 15 min
2. Quiz Master (`/quiz-master Personality`) - 15 min
3. Design agent (`/design StartScreen with themed aesthetic`) - 30 min

**Week 3**: Multi-agent collaboration
1. Scavenger Hunt (`Red → Green → Refactor` cycle) - 60 min
2. Pixel Jam (Rapid design iteration) - 60 min
3. Code Review (`/review` all PRs) - 30 min

---

## 📖 Reference Documentation

- **Main instructions**: [.github/copilot-instructions.md](.github/copilot-instructions.md)
- **Design guidelines**: [.github/instructions/frontend-design.instructions.md](.github/instructions/frontend-design.instructions.md)
- **CSS utilities**: [.github/instructions/css-utilities.instructions.md](.github/instructions/css-utilities.instructions.md)
- **Design prompt**: [.github/prompts/design.prompt.md](.github/prompts/design.prompt.md)
- **Refactor prompt**: [.github/prompts/refactor.prompt.md](.github/prompts/refactor.prompt.md)
- **Quiz Master prompt**: [.github/prompts/quiz-master.prompt.md](.github/prompts/quiz-master.prompt.md)

---

**Status**: Multi-agent infrastructure documented. Ready to run workflows! 🚀
