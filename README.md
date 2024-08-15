# [Store module] VIP
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
          "DurationType": "days", // Type of duration (e.g., minutes, hours, days)
          "DurationValue": 30, // Numeric value for the duration type
          "Price": 1000, // Credit cost
          "Command": "css_vip_adduser \"{SteamID}\" \"VIP_Bronze\" \"43200\"" // Command to add the vip
        },
        {
          "DurationType": "days",
          "DurationValue": 60,
          "Price": 1800,
          "Command": "css_vip_adduser \"{SteamID}\" \"VIP_Bronze\" \"86400\""
        }
      ]
    },
    {
      "Name": "VIP Silver",
      "Options": [
        {
          "DurationType": "minutes",
          "DurationValue": 30,
          "Price": 200,
          "Command": "css_vip_adduser \"{SteamID}\" \"VIP_Silver\" \"30\""
        },
        {
          "DurationType": "hours",
          "DurationValue": 2,
          "Price": 600,
          "Command": "css_vip_adduser \"{SteamID}\" \"VIP_Silver\" \"120\""
        },
        {
          "DurationType": "days",
          "DurationValue": 7,
          "Price": 5000,
          "Command": "css_vip_adduser \"{SteamID}\" \"VIP_Silver\" \"10080\""
        }
      ]
    }
  ],
  "ConfigVersion": 1
}
```
[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/L4L611665R)
