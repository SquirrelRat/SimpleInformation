using System;
using System.Collections.Generic;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using SharpDX;
using ExileCore.Shared.Attributes;

namespace SimpleInformation
{
    public enum ColorSchemeList
    {
        Default,
        SolarizedDark,
        Dracula,
        Inverted,
        Cyberpunk2077
    }

    public class SimpleInformationSettings : ISettings
    {
        [Menu("Enable", "Toggles the SimpleInformation plugin on or off.")]
        public ToggleNode Enable { get; set; } = new ToggleNode(true);
        [Menu("Draw X Offset", "Adjusts the horizontal position of the information display.")]
        public RangeNode<int> DrawXOffset { get; set; } = new RangeNode<int>(0, -150, 150);
        [Menu("Color Scheme", "Selects the color scheme for the information display.")]
        public ListNode ColorScheme { get; set; } = new ListNode
        {
            Value = "Dracula",
            Values = new List<string>(Enum.GetNames(typeof(ColorSchemeList)))
        };
        [Menu("Background Alpha", "Controls the transparency of the background of the information bar (0-255).")]
        public RangeNode<int> BackgroundAlpha { get; set; } = new RangeNode<int>(150, 0, 255);
        [Menu("Show Gold", "Toggles the display of the player's gold amount.")]
        public ToggleNode ShowGold { get; set; } = new ToggleNode(true);
        [Menu("Show Player Level", "Toggles the display of the player's level.")]
        public ToggleNode ShowPlayerLevel { get; set; } = new ToggleNode(true);
        [Menu("Show Area Time", "Toggles the display of the time spent in the current area.")]
        public ToggleNode ShowAreaTime { get; set; } = new ToggleNode(true);
        [Menu("Show Ping", "Toggles the display of the player's ping.")]
        public ToggleNode ShowPing { get; set; } = new ToggleNode(true);
        [Menu("Show Area Name", "Toggles the display of the current area's name.")]
        public ToggleNode ShowAreaName { get; set; } = new ToggleNode(true);
        [Menu("Show Time Left to Level", "Toggles the display of the estimated time left to level up.")]
        public ToggleNode ShowTimeLeft { get; set; } = new ToggleNode(true);
        [Menu("Show XP Rate", "Toggles the display of the player's experience gain rate.")]
        public ToggleNode ShowXpRate { get; set; } = new ToggleNode(true);
        [Menu("Show G/H", "Toggles the display of the player's gold per hour.")]
        public ToggleNode ShowGoldPerHour { get; set; } = new ToggleNode(true);
    }
}