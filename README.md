# 🚀 MCLauncher

> Easily install and manage multiple versions of Minecraft: Windows 10 Edition (Bedrock) side-by-side!  
> Useful for testing beta versions, official releases, or anything else—without endless uninstall/reinstall cycles.

---

## ✨ Modifications in This Fork

> **What’s new in this customized version?**

- 🪟 **MC runs outside of the Windows sandbox:**  
  Minecraft will be freed from the Windows app sandbox, allowing greater flexibility and compatibility.
- ⚡ **Automatic mod loader download and DLL injection support:**  
  The launcher automatically downloads a compatible mods loader so DLLs can be loaded easily—making modding and extension simpler than ever.

---

## ⚠️ Disclaimer

**This tool will NOT help you pirate the game.**  
A Microsoft account with valid ownership of Minecraft for Windows 10 from the Microsoft Store is required.

---

## 📝 Prerequisites

- 👤 **A Microsoft account with Minecraft for Windows 10 ownership in the Microsoft Store**
- 🛡️ **Administrator permissions** on your Windows user account (or on an accessible one)
- 🔧 **Developer mode enabled** in Windows 10 Settings (required for sideloading apps)
- 🧪 _Optional:_ If you want to use beta versions, subscribe to the Minecraft Beta program through the Xbox Insider Hub
- 📦 [Microsoft Visual C++ Redistributable](https://aka.ms/vs/16/release/vc_redist.x64.exe) installed

---

## 📦 Setup

1. Download the latest release from the [Releases](https://github.com/MCMrARM/mc-w10-version-launcher/releases) page and unzip it to any folder.
2. Run `MCLauncher.exe` to start the launcher.
3. **[In this fork]** The launcher will automatically handle mod loader download and configuration for you!

---

## 🛠️ Building From Source

- Visual Studio required, with Windows 10 SDK version **10.0.17763** and **.NET Framework 4.6.1 SDK**.
- If missing, you can install these via Visual Studio Installer.
- Open the project in Visual Studio and build directly—no manual modifications needed.

---

## ❓ FAQ

**Can I run multiple instances of Minecraft: Bedrock at the same time?**  
❌ **No, not currently.**  
You can _install_ multiple versions side-by-side, but only one can run at a time.

**Does this fork make modding easier?**  
✅ **Yes!**  
This version automatically prepares a mod loader for DLL injection, making it easy to use mods that require DLLs.

---

> ⭐️ Please star this project and contribute feedback or suggestions!
