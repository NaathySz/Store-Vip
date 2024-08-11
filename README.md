# [Store module]
Store module that lets players buy different VIP tiers with custom durations and prices
# Config
Config will be auto generated. Default:
```json
{
  "Store_Vip_commands": [
    "storevip",
    "buyvip"
  ],
  "VipItems": [
    {
      "Name": "VIP Bronze", // Shown in main menu
      "Options": [
        {
          "Days": 30, // Shown in submenu to inform days
          "Price": 1000, // Credit cost
          "Command": "css_vip_adduser \"{SteamID}\" \"VIP_Bronze\" \"43200\"" // Command to add the vip.
        },
        {
          "Days": 60,
          "Price": 1800,
          "Command": "css_vip_adduser \"{SteamID}\" \"VIP_Bronze\" \"86400\""
        }
      ]
    },
    {
      "Name": "VIP Silver",
      "Options": [
        {
          "Days": 30,
          "Price": 2000,
          "Command": "css_vip_adduser \"{SteamID}\" \"VIP_Silver\" \"43200\""
        },
        {
          "Days": 60,
          "Price": 3500,
          "Command": "css_vip_adduser \"{SteamID}\" \"VIP_Silver\" \"86400\""
        }
      ]
    }
  ],
  "ConfigVersion": 1
}
```
[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/L4L611665R)
