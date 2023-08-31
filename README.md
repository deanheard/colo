# Colo
.NET 6 library and application for controlling Cololight Pro hardware from the command line/shell.

## Background
Cololight Pro is a wifi-aware lighting system that is controlled by an application on your Android or iOS device. It's similar in concept to a Nanoleaf. "Colo" is a .NET solution that can be run from the Command Line on Windows, MacOS and Linux. Being Command Line based means you can integrate it into most scripting solutions on any of those platforms.

## Usage
The help can be accessed by running `colo --help` at the command line/shell.
```
Usage:  [options]

Options:
  -?|-h|--help              Show help information
  -a|--address <IPADDRESS>  The IP address of the Cololight device you want to instruct
  -o|--on[:<BOOL>]          True to turn the light on, false to turn it off
  -c|--colour[:<COLOUR>]    Sets the base colour to one of the System.Drawing.Color types
                            Allowed values are: ActiveBorder, ActiveCaption, ActiveCaptionText, AppWorkspace, Control,
                            ControlDark, ControlDarkDark, ControlLight, ControlLightLight, ControlText, Desktop,
                            GrayText, Highlight, HighlightText, HotTrack, InactiveBorder, InactiveCaption,
                            InactiveCaptionText, Info, InfoText, Menu, MenuText, ScrollBar, Window, WindowFrame,
                            WindowText, Transparent, AliceBlue, AntiqueWhite, Aqua, Aquamarine, Azure, Beige, Bisque,
                            Black, BlanchedAlmond, Blue, BlueViolet, Brown, BurlyWood, CadetBlue, Chartreuse, Chocolate,
                            Coral, CornflowerBlue, Cornsilk, Crimson, Cyan, DarkBlue, DarkCyan, DarkGoldenrod, DarkGray,
                            DarkGreen, DarkKhaki, DarkMagenta, DarkOliveGreen, DarkOrange, DarkOrchid, DarkRed,
                            DarkSalmon, DarkSeaGreen, DarkSlateBlue, DarkSlateGray, DarkTurquoise, DarkViolet, DeepPink,
                            DeepSkyBlue, DimGray, DodgerBlue, Firebrick, FloralWhite, ForestGreen, Fuchsia, Gainsboro,
                            GhostWhite, Gold, Goldenrod, Gray, Green, GreenYellow, Honeydew, HotPink, IndianRed, Indigo,
                            Ivory, Khaki, Lavender, LavenderBlush, LawnGreen, LemonChiffon, LightBlue, LightCoral,
                            LightCyan, LightGoldenrodYellow, LightGray, LightGreen, LightPink, LightSalmon,
                            LightSeaGreen, LightSkyBlue, LightSlateGray, LightSteelBlue, LightYellow, Lime, LimeGreen,
                            Linen, Magenta, Maroon, MediumAquamarine, MediumBlue, MediumOrchid, MediumPurple,
                            MediumSeaGreen, MediumSlateBlue, MediumSpringGreen, MediumTurquoise, MediumVioletRed,
                            MidnightBlue, MintCream, MistyRose, Moccasin, NavajoWhite, Navy, OldLace, Olive, OliveDrab,
                            Orange, OrangeRed, Orchid, PaleGoldenrod, PaleGreen, PaleTurquoise, PaleVioletRed,
                            PapayaWhip, PeachPuff, Peru, Pink, Plum, PowderBlue, Purple, Red, RosyBrown, RoyalBlue,
                            SaddleBrown, Salmon, SandyBrown, SeaGreen, SeaShell, Sienna, Silver, SkyBlue, SlateBlue,
                            SlateGray, Snow, SpringGreen, SteelBlue, Tan, Teal, Thistle, Tomato, Turquoise, Violet,
                            Wheat, White, WhiteSmoke, Yellow, YellowGreen, ButtonFace, ButtonHighlight, ButtonShadow,
                            GradientActiveCaption, GradientInactiveCaption, MenuBar, MenuHighlight
  -rgb|--rgb[:<RRGGBB>]     Sets the base colour a custom HTML defined colour
  -e|--effect[:<EFFECT>]    Sets the animated Effect of the lights
                            Allowed values are: Club80s, CherryBlossom, CocktailParade, Instagrammer, Pensieve,
                            Savasana, Sunrise, TheCircus, Unicorns, Christmas, RainbowFlow, MusicMode
  -b|--brightness[:<N>]     Sets the brightness as a percentage between 0 and 100
  ```
  
  The ```build.bat``` file will publish self contained applications for Windows x64, Linux x64 and MacOS x64 platforms.
