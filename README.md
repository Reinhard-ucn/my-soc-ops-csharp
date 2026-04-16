🌐 [Português (BR)](README.pt_BR.md) | [Español](README.es.md)

# Soc Ops

Social Bingo game for in-person mixers. Find people who match the questions and get 5 in a row!

🎮 **[Play the Game](https://dotnet-presentations.github.io/vscode-github-copilot-agent-lab/)** • 📚 **[Lab Guide](workshop/)**

---

## ✅ Pre-Commit Checklist

Before committing, run these checks from `SocOps/`:

```bash
# 1. Lint (C# code style)
dotnet format --verify-no-changes --verbosity diagnostic

# 2. Build (compile with no warnings)
dotnet build

# 3. Test (run any test suites)
dotnet test
```

---

## 🚀 Quick Start

**Prerequisites**: [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or higher

```bash
cd SocOps
dotnet run
```

Open http://localhost:5166 in your browser.

---

## 📚 Lab Modules

- [Overview & Checklist](workshop/00-overview.md)
- [Setup & Context Engineering](workshop/01-setup.md)
- [Design-First Frontend](workshop/02-design.md)
- [Custom Quiz Master](workshop/03-quiz-master.md)
- [Multi-Agent Development](workshop/04-multi-agent.md)

---

## 🔗 Codespaces

1. Click **Code** → **Codespaces** → **Create codespace on main**
2. Wait for devcontainer setup
3. Run `cd SocOps && dotnet run`

Deploys automatically to GitHub Pages on push to `main`.
