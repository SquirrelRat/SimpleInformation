using SharpDX;

namespace SimpleInformation
{
    public abstract class ColorScheme
    {
        public abstract Color Background { get; }
        public abstract Color Timer { get; }
        public abstract Color Fps { get; }
        public abstract Color Ping { get; }
        public abstract Color Area { get; }
        public abstract Color TimeLeft { get; }
        public abstract Color Xph { get; }
        public abstract Color XphGetLeft { get; }
    }

    public class DefaultColorScheme : ColorScheme
    {
        public override Color Background => new Color(0, 0, 0, 255);
        public override Color Timer => new Color(220, 190, 130, 255);
        public override Color Fps => new Color(220, 190, 130, 255);
        public override Color Ping => new Color(220, 190, 130, 255);
        public override Color Area => new Color(140, 200, 255, 255);
        public override Color TimeLeft => new Color(220, 190, 130, 255);
        public override Color Xph => new Color(220, 190, 130, 255);
        public override Color XphGetLeft => new Color(220, 190, 130, 255);
    }


    public class SolarizedDarkColorScheme : ColorScheme
    {
        public override Color Background => new Color(0x00, 0x2b, 0x36, 0xff);
        public override Color Timer => new Color(0x26, 0x8b, 0xd2, 0xff);
        public override Color Fps => new Color(0x85, 0x99, 0x00, 0xff);
        public override Color Ping => new Color(0x85, 0x99, 0x00, 0xff);
        public override Color Area => new Color(0xcb, 0x4b, 0x16, 0xff);
        public override Color TimeLeft => new Color(0xdc, 0x32, 0x2f, 0xff);
        public override Color Xph => new Color(0xd3, 0x36, 0x82, 0xff);
        public override Color XphGetLeft => new Color(0x6c, 0x71, 0xc4, 0xff);
    }

    public class DraculaColorScheme : ColorScheme
    {
        public override Color Background => new Color(0x28, 0x2a, 0x36, 0xff);
        public override Color Timer => new Color(0xbd, 0x93, 0xf9, 0xff);
        public override Color Fps => new Color(0x50, 0xfa, 0x7b, 0xff);
        public override Color Ping => new Color(0x50, 0xfa, 0x7b, 0xff);
        public override Color Area => new Color(0xff, 0xb8, 0x6c, 0xff);
        public override Color TimeLeft => new Color(0xff, 0x55, 0x55, 0xff);
        public override Color Xph => new Color(0xff, 0x79, 0xc6, 0xff);
        public override Color XphGetLeft => new Color(0x8b, 0xe9, 0xfd, 0xff);
    }


    public class InvertedColorScheme : ColorScheme
    {
        public override Color Background => new Color(0xE0, 0xE0, 0xE0, 0xff);
        public override Color Timer => Color.Black;
        public override Color Fps => Color.Black;
        public override Color Ping => Color.Black;
        public override Color Area => Color.Black;
        public override Color TimeLeft => Color.Black;
        public override Color Xph => Color.Black;
        public override Color XphGetLeft => Color.Black;
    }

    public class Cyberpunk2077ColorScheme : ColorScheme
    {
        public override Color Background => new Color(30, 45, 60, 255); 
        public override Color Timer => new Color(255, 97, 89, 255);     
        public override Color Fps => new Color(255, 97, 89, 255);    
        public override Color Ping => new Color(255, 97, 89, 255);    
        public override Color Area => new Color(76, 176, 165, 255);   
        public override Color TimeLeft => new Color(255, 97, 89, 255);     
        public override Color Xph => new Color(255, 97, 89, 255);    
        public override Color XphGetLeft => new Color(255, 97, 89, 255);   
    }

    // New UI-inspired color schemes (10 total)
    public class OverwatchColorScheme : ColorScheme
    {
        public override Color Background => new Color(18, 22, 41, 255);
        public override Color Timer => new Color(30, 144, 255, 255);
        public override Color Fps => new Color(30, 144, 255, 255);
        public override Color Ping => new Color(30, 144, 255, 255);
        public override Color Area => new Color(0, 191, 255, 255);
        public override Color TimeLeft => new Color(0, 214, 144, 255);
        public override Color Xph => new Color(255, 215, 0, 255);
        public override Color XphGetLeft => new Color(32, 178, 170, 255);
    }

    public class MinecraftColorScheme : ColorScheme
    {
        public override Color Background => new Color(20, 40, 20, 255);
        public override Color Timer => new Color(76, 175, 80, 255);
        public override Color Fps => new Color(76, 175, 80, 255);
        public override Color Ping => new Color(76, 175, 80, 255);
        public override Color Area => new Color(102, 187, 106, 255);
        public override Color TimeLeft => new Color(255, 153, 51, 255);
        public override Color Xph => new Color(255, 223, 0, 255);
        public override Color XphGetLeft => new Color(139, 195, 74, 255);
    }

    public class ValorantColorScheme : ColorScheme
    {
        public override Color Background => new Color(25, 25, 60, 255);
        public override Color Timer => new Color(199, 0, 255, 255);
        public override Color Fps => new Color(199, 0, 255, 255);
        public override Color Ping => new Color(199, 0, 255, 255);
        public override Color Area => new Color(111, 45, 168, 255);
        public override Color TimeLeft => new Color(255, 0, 128, 255);
        public override Color Xph => new Color(0, 255, 170, 255);
        public override Color XphGetLeft => new Color(255, 204, 255, 255);
    }

    public class HaloColorScheme : ColorScheme
    {
        public override Color Background => new Color(20, 20, 20, 255);
        public override Color Timer => new Color(0, 153, 255, 255);
        public override Color Fps => new Color(0, 153, 255, 255);
        public override Color Ping => new Color(0, 153, 255, 255);
        public override Color Area => new Color(0, 122, 204, 255);
        public override Color TimeLeft => new Color(0, 255, 255, 255);
        public override Color Xph => new Color(255, 215, 0, 255);
        public override Color XphGetLeft => new Color(135, 206, 250, 255);
    }

    public class MonochromeColorScheme : ColorScheme
    {
        public override Color Background => new Color(24, 24, 24, 255);
        public override Color Timer => Color.Gray;
        public override Color Fps => Color.Gray;
        public override Color Ping => Color.Gray;
        public override Color Area => new Color(170, 170, 170, 255);
        public override Color TimeLeft => Color.White;
        public override Color Xph => new Color(200, 200, 200, 255);
        public override Color XphGetLeft => Color.White;
    }
}
