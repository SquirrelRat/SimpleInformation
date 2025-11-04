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
}