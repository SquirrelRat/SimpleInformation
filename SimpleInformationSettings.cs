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
        Inverted
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
        [Menu("Bar Width", "Sets the total width of the information bar.")]
        public RangeNode<int> BarWidth { get; set; } = new RangeNode<int>(750, 100, 2000);
        [Menu("Background Alpha", "Controls the transparency of the background of the information bar (0-255).")]
        public RangeNode<int> BackgroundAlpha { get; set; } = new RangeNode<int>(150, 0, 255);
    }
}