using McMaster.Extensions.CommandLineUtils;
using System;
using System.Drawing;
using System.Net;
using System.Reflection;

namespace Cololight.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            PrintHeader();

            try
            {
                int returnCode = 0;
                var app = new CommandLineApplication();

                app.HelpOption();
                var optionHost = app.Option("-a|--address <IPADDRESS>", "The IP address of the Cololight device you want to instruct", CommandOptionType.SingleValue);
                var optionOn = app.Option<bool>("-o|--on <BOOL>", "True to turn the light on, false to turn it off", CommandOptionType.SingleOrNoValue);
                var optionColour = app.Option<KnownColor>("-c|--colour <COLOUR>", "Sets the base colour to one of the System.Drawing.Color types", CommandOptionType.SingleOrNoValue);
                var optionRgb = app.Option("-rgb|--rgb <RRGGBB>", "Sets the base colour a custom HTML defined colour", CommandOptionType.SingleOrNoValue);
                var optionEffect = app.Option<CololightController.CololightController.Effects>("-e|--effect <EFFECT>", "Sets the animated Effect of the lights", CommandOptionType.SingleOrNoValue);
                var optionBrightness = app.Option<int>("-b|--brightness <N>", "Sets the brightness as a percentage between 0 and 100", CommandOptionType.SingleOrNoValue);

                app.OnExecute(() =>
                {
                    CololightController.CololightController controller = new CololightController.CololightController();

                    if (optionHost.HasValue())
                    {
                        IPAddress address = IPAddress.Parse(optionHost.Value());
                        System.Console.WriteLine(string.Format("Sending commands to IP Address {0}", address.ToString()));
                        controller.Host = address;
                    }
                    else
                    {
                        throw new ArgumentNullException("--address");
                    }

                    if (optionColour.HasValue() && returnCode == 0)
                    {
                        var colour = Color.FromKnownColor(optionColour.ParsedValue);
                        System.Console.WriteLine(string.Format("Setting Colour to {0}", colour.Name)); ;                        
                        returnCode = controller.SetColour(colour);
                    }

                    if (optionRgb.HasValue() && returnCode == 0)
                    {
                        var rgb = optionRgb.Value();
                        System.Console.WriteLine(string.Format("Setting Colour to #{0}", rgb));
                        returnCode = controller.SetColour(rgb);
                    }

                    if (optionEffect.HasValue() && returnCode == 0)
                    {
                        var effect = optionEffect.ParsedValue;
                        System.Console.WriteLine(string.Format("Setting Effect to {0}", effect));
                        returnCode = controller.SetEffect(effect);
                    }

                    if (optionBrightness.HasValue() && returnCode == 0)
                    {
                        var brightness = optionBrightness.ParsedValue;

                        if (brightness < 0)
                            brightness = 0;
                        else if (brightness > 100)
                            brightness = 100;

                        System.Console.WriteLine(string.Format("Setting brightness to {0}", brightness));
                        returnCode = controller.SetBrightness(brightness);
                    }

                    if (optionOn.HasValue() && returnCode == 0)
                    {
                        var on = optionOn.HasValue() ? optionOn.ParsedValue : true;

                        if (on)
                        {
                            System.Console.WriteLine("Turning On");
                            returnCode = controller.TurnOn();
                        }
                        else
                        {
                            System.Console.WriteLine("Turning Off");
                            returnCode = controller.TurnOff();
                        }
                    }

                    if (returnCode == -1)
                    {
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("ERROR: Unable to find device on the network");
                        System.Console.ResetColor();
                    }

                    return returnCode;
                });

                return app.Execute(args);
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(ex.Message);
                System.Console.ResetColor();
                return -2;
            }
        }   
        
        private static void PrintHeader()
        {
            System.Console.ResetColor();

            System.Console.WriteLine("");

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Write("=");

            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write("=");

            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.Write("=");

            System.Console.Write(" Cololight Controller v{0} ", Assembly.GetEntryAssembly().GetName().Version);

            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.Write("=");

            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write("=");

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Write("=");

            System.Console.ResetColor();

            System.Console.Write("\r\n");
        }
    }
}
