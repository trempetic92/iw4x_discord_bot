using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WarmaneAPIBOT.API;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Echovoice.JSON;
using Discord;
using Discord.WebSocket;
using System.Diagnostics;
using Discord.Net;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using TeknoMW3_ServerList;

namespace WarmaneAPIBOT.Commands
{
    class IW5ServerCommand : ModuleBase<SocketCommandContext>
    {
        [DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi)]
        extern static int GetTickCount();

        [Command("iw5list")]
        [Alias("iw5list")]
        public async Task Handle()
        {

            var RecvAddr = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName()).First(i => i.AddressFamily == AddressFamily.InterNetwork),
                                       (Process.GetCurrentProcess().Id % 400) + 28599);

            var msClient = new TcpClient(RecvAddr);

            msClient.Connect("mw3master.teknogods.com", 27017);

            var req = new MW3MasterClientRequest();
            req.Magic4CC = 0x5448454d; // THEM
            req.Version = 0x01040215; // 2804 version
            msClient.Client.Send(req.ToBytes());
            byte[] buffer = new byte[10240];
            msClient.Client.Receive(buffer);

            var items = MW3MasterClientResponse.FromBytes(buffer);
            msClient.Close();

            var client = new UdpClient(RecvAddr);
            client.Client.Blocking = false;
            client.EnableBroadcast = true;

            Discord.IDMChannel dm = await Context.User.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync("```css\nPlease wait until all TeknoMW3 servers are listed. Thank you!```");

            var message = new MW3ServerQuery
            {
                Magic4CC = 1347374924
            };

            foreach (var server in items.Entries)
            {
                var s = new IPEndPoint(server.IpAddress, server.QPort);
                await dm.SendMessageAsync($"```css\n{s.Address.ToString()}:{server.QPort}\n```");
                message.TimeStamp = GetTickCount();

                var bytes = message.ToBytes();

                for (var i = 0; i < 5; i++)
                {
                    try
                    {
                        client.Send(bytes, bytes.Length, s);
                        break;
                    }
                    catch
                    {
                        await dm.SendMessageAsync($"```css\nConnect to {s.Address.ToString()}:{server.QPort} failed\n```");
                    }
                }

            }



        }
    }
}