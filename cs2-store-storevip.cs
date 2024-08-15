using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Menu;
using StoreApi;
using System.Text.Json.Serialization;

namespace Store_VIP;

public class Store_VIPConfig : BasePluginConfig
{
    [JsonPropertyName("Store_Vip_commands")]
    public List<string> StoreVIPCommands { get; set; } = ["storevip", "buyvip"];

    [JsonPropertyName("VipItems")]
    public List<VipItem> VipItems { get; set; } = new()
    {
        new VipItem
        {
            Name = "VIP Bronze",
            Options = new List<VipOption>
            {
                new VipOption { DurationType = "dias", DurationValue = 30, Price = 1000, Command = "css_vip_adduser \"{SteamID}\" \"VIP_Bronze\" \"43200\"" },
                new VipOption { DurationType = "dias", DurationValue = 60, Price = 1800, Command = "css_vip_adduser \"{SteamID}\" \"VIP_Bronze\" \"86400\"" }
            }
        },
        new VipItem
        {
            Name = "VIP Silver",
            Options = new List<VipOption>
            {
                new VipOption { DurationType = "minutes", DurationValue = 30, Price = 200, Command = "css_vip_adduser \"{SteamID}\" \"VIP_Silver\" \"30\"" },
                new VipOption { DurationType = "hours", DurationValue = 2, Price = 600, Command = "css_vip_adduser \"{SteamID}\" \"VIP_Silver\" \"120\"" },
                new VipOption { DurationType = "days", DurationValue = 7, Price = 5000, Command = "css_vip_adduser \"{SteamID}\" \"VIP_Silver\" \"10080\"" }
            }
        }
    };
}

public class VipItem
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("Options")]
    public List<VipOption> Options { get; set; } = new();
}

public class VipOption
{
    [JsonPropertyName("DurationType")]
    public string DurationType { get; set; } = string.Empty;

    [JsonPropertyName("DurationValue")]
    public int DurationValue { get; set; } = 0;

    [JsonPropertyName("Price")]
    public int Price { get; set; } = 0;

    [JsonPropertyName("Command")]
    public string Command { get; set; } = string.Empty;
}

public class Store_VIP : BasePlugin, IPluginConfig<Store_VIPConfig>
{
    public override string ModuleName => "Store Module [VIP]";
    public override string ModuleVersion => "0.1.0";
    public override string ModuleAuthor => "Nathy";

    public IStoreApi? StoreApi { get; set; }
    public Store_VIPConfig Config { get; set; } = new();

    public void OnConfigParsed(Store_VIPConfig config)
    {
        Config = config;
    }

    public override void OnAllPluginsLoaded(bool hotReload)
    {
        StoreApi = IStoreApi.Capability.Get() ?? throw new Exception("StoreApi could not be located.");
        CreateCommands();
    }

    private void CreateCommands()
    {
        foreach (var cmd in Config.StoreVIPCommands)
        {
            AddCommand($"css_{cmd}", "Open the VIP menu", Command_VIPMenu);
        }
    }

    public void Command_VIPMenu(CCSPlayerController? player, CommandInfo info)
    {
        if (player == null) return;

        var menu = new CenterHtmlMenu(Localizer["VIP Menu title"]);

        foreach (var item in Config.VipItems)
        {
            menu.AddMenuOption(Localizer["VIP Menu item", item.Name], (client, option) =>
            {
                OpenVipSubMenu(player, item);
            });
        }

        MenuManager.OpenCenterHtmlMenu(this, player, menu);
    }

    private void OpenVipSubMenu(CCSPlayerController player, VipItem item)
    {
        var subMenu = new CenterHtmlMenu(Localizer["VIP Options title"]);

        foreach (var option in item.Options)
        {
            subMenu.AddMenuOption(Localizer["VIP Options item", option.DurationValue, option.DurationType, option.Price], (client, subOption) =>
            {
                BuyVIP(player, item.Name, option);
            });
        }

        MenuManager.OpenCenterHtmlMenu(this, player, subMenu);
    }

    private void BuyVIP(CCSPlayerController player, string vipName, VipOption option)
    {
        if (StoreApi == null) throw new Exception("StoreApi could not be located.");

        int playerCredits = StoreApi.GetPlayerCredits(player);

        if (playerCredits < option.Price)
        {
            player.PrintToChat(Localizer["Prefix"] + Localizer["Not enough credits"]);
            return;
        }

        StoreApi.GivePlayerCredits(player, -option.Price);

        string steamId = player.SteamID.ToString();
        string command = option.Command.Replace("{SteamID}", steamId);

        player.PrintToChat(Localizer["Prefix"] + Localizer["VIP purchased", vipName, option.DurationValue, option.DurationType, option.Price]);
        ExecuteServerCommand(command);
    }

    private void ExecuteServerCommand(string command)
    {
        NativeAPI.IssueServerCommand(command);
        // Server.PrintToChatAll(command);
    }
}
