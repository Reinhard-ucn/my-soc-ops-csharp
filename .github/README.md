# GitHub Directory - Soc Ops Workspace Configuration

This directory contains all Copilot agent configuration and workspace customization for the Soc Ops project.

## Contents

### Core Instructions
- **`copilot-instructions.md`** — Main workspace instructions for all AI agents
  - 276 lines covering architecture, conventions, state management, anti-patterns
  - Foundation for all other agent configurations

### Specialized Instructions  
- **`instructions/css-utilities.instructions.md`** — CSS utility class reference and usage
- **`instructions/frontend-design.instructions.md`** — Design guidelines avoiding generic aesthetics

### Agent Prompts
- **`prompts/setup.prompt.md`** — Environment setup and verification
- **`prompts/design.prompt.md`** — UI/design work with distinctive aesthetics
- **`prompts/refactor.prompt.md`** — Code quality and anti-pattern fixes
- **`prompts/quiz-master.prompt.md`** — Custom question theme generation

### Workflows
- **`AGENTS.md`** — Multi-agent workflow definitions
  - Scavenger Hunt (TDD Red→Green→Refactor)
  - Pixel Jam (rapid design iteration)
  - PR Review automation

### Other
- **`agents/`** — Additional agent definitions
- **`workflows/`** — GitHub Actions workflow definitions

## Quick Start

When working on Soc Ops, agents will automatically load:
1. `.github/copilot-instructions.md` (main context)
2. Specialized prompts based on task type
3. AGENTS.md for multi-agent workflows

## For Developers

- Start with `/setup` to verify environment
- Use `/design [component] with [aesthetic]` for UI work  
- Use `/refactor [scope]` for code improvements
- Use `/quiz-master [theme]` for question generation
- Use the AGENTS.md workflows for complex tasks

## For Workshop Participants

Each workshop part uses specific agents:
- **Part 01**: Setup Agent
- **Part 02**: Design Agent + Frontend Design instructions
- **Part 03**: Quiz Master Agent
- **Part 04**: All agents + multi-agent workflows

See `copilot-instructions.md` for the full workshop integration guide.

