using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace CololightController
{
    public class CololightController
    {
        private const string COMMAND_PREFIX = "535a30300000000000";
        private const string COMMAND_CONFIG = "20000000000000000000000000000000000100000000000000000004010301c";
        private const string COMMAND_EFFECT = "23000000000000000000000000000000000100000000000000000004010602ff";

        private const string COMMAND_DISTANCE = "2300000000000000000000000000000000010000000000000000000b01068700";
        private const int port = 8900;
                
        public IPAddress Host { get; set; }

        public enum Effects
        {
            Club80s,
            CherryBlossom,
            CocktailParade,
            Instagrammer,
            Pensieve,
            Savasana,
            Sunrise,
            TheCircus,
            Unicorns,
            Christmas,
            RainbowFlow,
            MusicMode
        }

        private NameValueCollection EffectsLookup = new NameValueCollection();

        public CololightController()
        {
            EffectsLookup.Add("Club80s", "049a0000");
            EffectsLookup.Add("CherryBlossom", "04940800");
            EffectsLookup.Add("CocktailParade", "05bd0690");
            EffectsLookup.Add("Instagrammer", "03bc0190");
            EffectsLookup.Add("Pensieve", "04c40600");
            EffectsLookup.Add("Savasana", "04970400");
            EffectsLookup.Add("Sunrise", "01c10a00");
            EffectsLookup.Add("TheCircus", "04810130");
            EffectsLookup.Add("Unicorns", "049a0e00");
            EffectsLookup.Add("Christmas", "068b0900");
            EffectsLookup.Add("RainbowFlow", "03810690");
            EffectsLookup.Add("MusicMode", "07bd0990");
        }

        public int TurnOn()
        {
            string command = string.Format("{0}{1}{2}", COMMAND_PREFIX, COMMAND_CONFIG, "f35");

            return SendMessage(command);
        }

        public int TurnOff()
        {
            string command = string.Format("{0}{1}{2}", COMMAND_PREFIX, COMMAND_CONFIG, "e1e");

            return SendMessage(command);
        }

        //public int DistanceTest()
        //{
        //    string command = string.Format("{0}{1}{2}", COMMAND_PREFIX, COMMAND_DISTANCE, "85008503");

        //    return SendMessage(command);
        //}

        public int SetEffect(Effects effect)
        {
            string effectValue = EffectsLookup.Get(Enum.GetName(typeof(Effects), effect));

            string command = string.Format("{0}{1}{2}", COMMAND_PREFIX, COMMAND_EFFECT, effectValue);

            return SendMessage(command);
        }

        public int SetBrightness(int brightness)
        {
            if (brightness < 0)
                brightness = 0;
            else if (brightness > 100)
                brightness = 100;

            string prefix = "f";
            string command = string.Format("{0}{1}{2}{3}", COMMAND_PREFIX, COMMAND_CONFIG, prefix, brightness.ToString("X2"));

            return SendMessage(command);
        }

        public int SetColour(System.Drawing.Color colour)
        {
            string hexColour = colour.R.ToString("X2") + colour.G.ToString("X2") + colour.B.ToString("X2");

            return this.SetColour(hexColour);
        }

        public int SetColour(string colour)
        {
            string prefix = "00";
            string command = string.Format("{0}{1}{2}{3}", COMMAND_PREFIX, COMMAND_EFFECT, prefix, colour);

            return SendMessage(command);
        }

        private int SendMessage(string message)
        {            
            if (PingHost(this.Host.ToString()))
            {
                UdpClient udpClient = new UdpClient();
                IPEndPoint ipEndPoint = new IPEndPoint(this.Host, port);

                try
                {
                    udpClient.Connect(ipEndPoint);

                    byte[] dgram = StringToByteArray(message);

                    if (dgram.Length > 0)
                    {
                        udpClient.Send(dgram, dgram.Length);
                        return 0;
                    }
                }
                catch (Exception e)
                {                    
                    Debug.WriteLine(e.ToString());
                }
                finally
                {
                    udpClient.Close();
                }
            }            

            return -1;
        }

        private bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        private byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;

            if (NumberChars % 2 == 0)
            {
                byte[] bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                return bytes;
            }
            else
            {                
                return new byte[0];
            }
        }
    }
}
