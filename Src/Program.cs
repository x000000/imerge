using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using CommandLine;

namespace x0.imerge
{
    static class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static int Main(string[] args)
        {
            DesktopApplicationLifetime lifetime;

            try {
                lifetime = new DesktopApplicationLifetime(ParseArgs(args, out _)) {
                    Args = args,
                    ShutdownMode = ShutdownMode.OnMainWindowClose,
                };
            } catch {
                return -1;
            }

            BuildAvaloniaApp().SetupWithLifetime(lifetime);
            return lifetime.Start(args);
        }

        private static NamedStartupOptions ParseArgs(string[] args, out List<Error>? errors)
        {
            List<Error>? errorList = null;

            object result = Parser.Default.ParseArguments<NamedStartupOptions>(args)
                    .WithNotParsed(e => errorList = new List<Error>(e));

            if (result is Parsed<NamedStartupOptions> nr) {
                errors = null;
                return nr.Value;
            }

            result = Parser.Default.ParseArguments<SimpleStartupOptions>(args)
                .WithNotParsed(errors => errorList!.AddRange(errors));

            if (result is ParserResult<SimpleStartupOptions> sr) {
                errors = null;
                return sr.Value;
            }

            errors = errorList;
            throw new ArgumentNullException();
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}
