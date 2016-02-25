﻿using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

using Mono.Options;

namespace Demo.ServiceStubs.CommandLine
{
    class Program
    {
        static int Main(string[] args)
        {
            if (AppDomain.CurrentDomain.IsDefaultAppDomain())
            {
                return CreateNewAppDomain();
            }

            int port = 1234;

            OptionSet options = new OptionSet();
            options.Add("p|port=", (int v) => port = v);
            options.Add("h|?|help", v => ShowHelp(options));
            options.Parse(Environment.GetCommandLineArgs());

            var uri = new Uri($"http://localhost:{port}");
            using (var host = new StubServiceHost(uri))
            {
                host.Start();

                Console.WriteLine($"Listening for requests at {uri.OriginalString}");
                Console.WriteLine("Hit ENTER to quit...");
                Console.WriteLine("");
                Console.ReadLine();
            }

            return 0;
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: greet [OPTIONS]+ message");
            Console.WriteLine("Greet a list of individuals with an optional message.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        private static int CreateNewAppDomain()
        {
            Console.WriteLine("Switching to secound AppDomain for RazorEngine clean up.");
            Console.WriteLine();

            var current = AppDomain.CurrentDomain;
            var strongNames = new StrongName[0];

            var domain = AppDomain.CreateDomain(
                "MyMainDomain", null,
                current.SetupInformation, new PermissionSet(PermissionState.Unrestricted),
                strongNames);
            var exitCode = domain.ExecuteAssembly(Assembly.GetExecutingAssembly().Location);

            AppDomain.Unload(domain);
            return exitCode;
        }
    }
}
