using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared;
using ExileCore.Shared.Cache;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Helpers;
using JM.LinqFaster;
using SharpDX;
using Input = ExileCore.Input;
using Vector2N = System.Numerics.Vector2;

namespace SimpleInformation
{
    public class SimpleInformation : BaseSettingsPlugin<SimpleInformationSettings>
    {
        private string areaName = "";

        private Dictionary<int, float> ArenaEffectiveLevels = new Dictionary<int, float>
        {
            {71, 70.94f},
            {72, 71.82f},
            {73, 72.64f},
            {74, 73.4f},
            {75, 74.1f},
            {76, 74.74f},
            {77, 75.32f},
            {78, 75.84f},
            {79, 76.3f},
            {80, 76.7f},
            {81, 77.04f},
            {82, 77.32f},
            {83, 77.54f},
            {84, 77.7f}
        };

        private TimeCache<bool> CalcXp;
        private bool CanRender;
        private DebugInformation debugInformation;
        private Vector2N drawTextVector2;
        private string latency = "";
        private string ping = "";
        private RectangleF leftPanelStartDrawRect = RectangleF.Empty;
        private TimeCache<bool> LevelPenalty;
        private double levelXpPenalty, partyXpPenalty;
        private float percentGot;
        private double partytime = 4000;
        private DateTime startTime, lastTime;
        private long startXp, getXp, xpLeftQ;
        private float startY;
        private double time;
        private string Time = "";
        private string timeLeft = "";
        private TimeSpan timeSpan;
        private string xpRate = "";
        private string xpText = "";
        private string playerLevelText = "";

        public float GetEffectiveLevel(int monsterLevel)
        {
            return Convert.ToSingle(-0.03 * Math.Pow(monsterLevel, 2) + 5.17 * monsterLevel - 144.9);
        }

        public override void OnLoad()
        {
            Order = -50;
        }

        public override bool Initialise()
        {
            Input.RegisterKey(Keys.F10);

            Input.ReleaseKey += (sender, keys) =>
            {
                if (keys == Keys.F10) Settings.Enable.Value = !Settings.Enable;
            };

            GameController.LeftPanel.WantUse(() => Settings.Enable);
            CalcXp = new TimeCache<bool>(() =>
            {
                partytime += time;
                time = 0;
                CalculateXp();
                var areaCurrentArea = GameController.Area.CurrentArea;

                if (areaCurrentArea == null)
                    return false;

                timeSpan = DateTime.UtcNow - areaCurrentArea.TimeEntered;

                Time = AreaInstance.GetTimeString(timeSpan);
                xpText = $"{xpRate} ({percentGot:P0})".ToUpper();

                if (partytime > 4900)
                {
                    var levelPenaltyValue = LevelPenalty.Value;
                }

                return true;
            }, 1000);

            LevelPenalty = new TimeCache<bool>(() =>
            {
                partyXpPenalty = PartyXpPenalty();
                levelXpPenalty = LevelXpPenalty();
                return true;
            }, 5000);

            GameController.EntityListWrapper.PlayerUpdate += OnEntityListWrapperOnPlayerUpdate;
            OnEntityListWrapperOnPlayerUpdate(this, GameController.Player);

            debugInformation = new DebugInformation("Game FPS", "Collect game fps", false);
            return true;
        }

        private void OnEntityListWrapperOnPlayerUpdate(object sender, Entity entity)
        {
            percentGot = 0;
            xpRate = "0.00 xp/h";
            var level = GameController.Player.GetComponent<Player>()?.Level ?? 100;
            playerLevelText = $"Level: {level}";
            timeLeft = "-h -m -s to Level Up";
            getXp = 0;
            xpLeftQ = 0;
            xpText = "";

            startTime = lastTime = DateTime.UtcNow;
            startXp = entity.GetComponent<Player>().XP;
            levelXpPenalty = LevelXpPenalty();
        }

        public override void AreaChange(AreaInstance area)
        {
            LevelPenalty.ForceUpdate();
        }

        public override Job Tick()
        {
            TickLogic();
            return null;
        }

        private void TickLogic()
        {
            time += GameController.DeltaTime;
            var gameUi = GameController.Game.IngameState.IngameUi;

            if (GameController.Area.CurrentArea == null || gameUi.InventoryPanel.IsVisible || gameUi.SyndicatePanel.IsVisibleLocal)
            {
                CanRender = false;
                return;
            }

            var UIHover = GameController.Game.IngameState.UIHover;

            if (UIHover.Tooltip != null && UIHover.Tooltip.IsVisibleLocal &&
                UIHover.Tooltip.GetClientRectCache.Intersects(leftPanelStartDrawRect))
            {
                CanRender = false;
                return;
            }

            CanRender = true;

            var calcXpValue = CalcXp.Value;
            
            var areaSuffix = (GameController.Area.CurrentArea.RealLevel >= 68)
                ? $" - T{GameController.Area.CurrentArea.RealLevel - 67}"
                : "";

            areaName = $"{GameController.Area.CurrentArea.DisplayName}{areaSuffix}";
            ping = $"Ping: {GameController.Game.IngameState.ServerData.Latency}";
        }

        private void CalculateXp()
        {
            var level = GameController.Player.GetComponent<Player>()?.Level ?? 100;

            if (level >= 100)
            {
                
                xpRate = "0.00 xp/h";
                timeLeft = "--h--m--s";
                return;
            }

            long currentXp = GameController.Player.GetComponent<Player>().XP;
            getXp = currentXp - startXp;
            var rate = (currentXp - startXp) / (DateTime.UtcNow - startTime).TotalHours;
            xpRate = $"{ConvertHelper.ToShorten(rate, "0.00")} xp/h";

            if (level >= 0 && level + 1 < Constants.PlayerXpLevels.Length && rate > 1)
            {
                var xpLeft = Constants.PlayerXpLevels[level + 1] - currentXp;
                xpLeftQ = xpLeft;
                var time = TimeSpan.FromHours(xpLeft / rate);
                timeLeft = $"{time.Hours:0}h {time.Minutes:00}m {time.Seconds:00}s to Level Up";

                if (getXp == 0)
                    percentGot = 0;
                else
                {
                    percentGot = getXp / ((float) Constants.PlayerXpLevels[level + 1] - (float) Constants.PlayerXpLevels[level]);
                    if (percentGot < -100) percentGot = 0;
                }
            }
        }

        private double LevelXpPenalty()
        {
            var arenaLevel = GameController.Area.CurrentArea.RealLevel;
            var characterLevel = GameController.Player.GetComponent<Player>()?.Level ?? 100;


            if (arenaLevel > 70 && !ArenaEffectiveLevels.ContainsKey(arenaLevel))
            {
                
                ArenaEffectiveLevels.Add(arenaLevel, GetEffectiveLevel(arenaLevel));
            }
            var effectiveArenaLevel = arenaLevel < 71 ? arenaLevel : ArenaEffectiveLevels[arenaLevel];
            var safeZone = Math.Floor(Convert.ToDouble(characterLevel) / 16) + 3;
            var effectiveDifference = Math.Max(Math.Abs(characterLevel - effectiveArenaLevel) - safeZone, 0);
            double xpMultiplier;

            xpMultiplier = Math.Pow((characterLevel + 5) / (characterLevel + 5 + Math.Pow(effectiveDifference, 2.5)), 1.5);

            if (characterLevel >= 95) 
                xpMultiplier *= 1d / (1 + 0.1 * (characterLevel - 94));

            xpMultiplier = Math.Max(xpMultiplier, 0.01);

            return xpMultiplier;
        }

        private double PartyXpPenalty()
        {
            var entities = GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Player];

            if (entities.Count == 0)
                return 1;

            var levels = entities.Select(y => y.GetComponent<Player>()?.Level ?? 100).ToList();
            var characterLevel = GameController.Player.GetComponent<Player>()?.Level ?? 100;
            var partyXpPenalty = Math.Pow(characterLevel + 10, 2.71) / levels.SumF(level => Math.Pow(level + 10, 2.71));
            return partyXpPenalty * levels.Count;
        }

        private ColorScheme GetColorScheme()
        {
            var scheme = (ColorSchemeList)Enum.Parse(typeof(ColorSchemeList), Settings.ColorScheme.Value);
            switch (scheme)
            {
                case ColorSchemeList.SolarizedDark:
                    return new SolarizedDarkColorScheme();
                case ColorSchemeList.Dracula:
                    return new DraculaColorScheme();
                case ColorSchemeList.Inverted:
                    return new InvertedColorScheme();
                default:
                    return new DefaultColorScheme();
            }
        }

        public override void Render()
        {
            if (!CanRender)
                return;
            var origStartPoint = GameController.LeftPanel.StartDrawPoint;
            var colorScheme = GetColorScheme();

            var allItems = new[]
            {
                (playerLevelText, colorScheme.XphGetLeft),
                (Time, colorScheme.Timer),
                (ping, colorScheme.Ping),
                (areaName, colorScheme.Area),
                (timeLeft, colorScheme.TimeLeft),
                (xpText, colorScheme.Xph)
            };

            var padding = 2;
            var totalTextWidth = allItems.Sum(x => Graphics.MeasureText(x.Item1).X) + (allItems.Length - 1) * Graphics.MeasureText(" | ").X;
            var barWidth = Settings.BarWidth.Value;
            if (totalTextWidth > barWidth)
            {
                
                totalTextWidth = barWidth;
            }

            var maxHeight = allItems.Max(x => Graphics.MeasureText(x.Item1).Y);

            var drawPoint = new Vector2N(
                (GameController.Window.GetWindowRectangle().Width - barWidth) / 2 + Settings.DrawXOffset.Value,
                origStartPoint.Y);
            leftPanelStartDrawRect = new RectangleF(drawPoint.X, drawPoint.Y, 1, 1);

            var bounds = new RectangleF(drawPoint.X - padding, drawPoint.Y - padding, barWidth + padding * 2, maxHeight + padding * 2);
            var backgroundColor = colorScheme.Background;
            backgroundColor.A = (byte)Settings.BackgroundAlpha.Value;
            Graphics.DrawBox(bounds, backgroundColor);

            var textDrawPoint = new Vector2N(drawPoint.X + (barWidth - totalTextWidth) / 2, drawPoint.Y);

            for (int i = 0; i < allItems.Length; i++)
            {
                var (text, color) = allItems[i];
                var textSize = Graphics.DrawText(text, textDrawPoint, color);
                textDrawPoint.X += textSize.X;
                
                if (i < allItems.Length - 1)
                {
                    var separatorSize = Graphics.DrawText(" | ", textDrawPoint, colorScheme.Timer); 
                    textDrawPoint.X += separatorSize.X;
                }
            }
            
            GameController.LeftPanel.StartDrawPoint = new Vector2(origStartPoint.X, origStartPoint.Y + maxHeight + 10);
        }
    }
}