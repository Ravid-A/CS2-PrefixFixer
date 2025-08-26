using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.UserMessages;
using CounterStrikeSharp.API.Modules.Utils;
using Microsoft.Extensions.Logging;
using PrefixFixer.Extentions;

namespace PrefixFixer;

public class PrefixFixer : BasePlugin, IPluginConfig<Config>
{
    public override string ModuleName => "[CS2] Prefix Fixer";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "Ravid";
    public override string ModuleDescription => "Prefix fixer for Counter-Strike 2";
    
    public static PrefixFixer Instance { get; private set; } = null!;
    public Config Config { get; set; } = null!;

    public override void Load(bool hotReload)
    {
        Instance = this;

        var msgId = UserMessage.FindIdByName("TextMsg");
        
        if (msgId == -1)
        {
            Logger.LogError("Could not find TextMsg user message ID.");
            return;
        }
        
        HookUserMessage(msgId, OnTextMessage);
    }
    
    public void OnConfigParsed(Config config)
    {
        Config = config;
    }
    
    private static HookResult OnTextMessage(UserMessage um)
    {
        if (!um.HasField("dest") || um.ReadInt("dest") != 3)
        {
            return HookResult.Continue;
        }

        var msg = um.ReadString("param", 0);

        if (!msg.ContainsPrefix())
        {
            return HookResult.Continue;
        }
        
        um.SetString("param", msg.ReplacePrefixs(), 0);
        
        return HookResult.Changed;
    }
}