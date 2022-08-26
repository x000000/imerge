using System.Collections.Generic;
using CommandLine;

namespace x0.imerge;

public record StartupOptions
{
#if DEBUG
    [Option('d', "devtools")]
    public string? AttachDevTools { get; set; }
#endif
}

public record NamedStartupOptions : StartupOptions
{
    [Option('b', "base",
        Required = false,
        HelpText = "The name of a temporary file containing the common base of the files to be merged, if available."
    )]
    public string? Base { get; set; }

    [Option('l', "local",
        Required = true,
        HelpText = "The name of a temporary file containing the contents of the file on the current branch."
    )]
    public string Local { get; set; }

    [Option('r', "remote",
        Required = true,
        HelpText = "The name of a temporary file containing the contents of the file from the branch being merged."
    )]
    public string Remote { get; set; }

    [Option('m', "merged",
        Required = true,
        HelpText = "The name of the file to which the merge tool should write the results of a successful merge."
    )]
    public string Merged { get; set; }
}

public record SimpleStartupOptions : StartupOptions
{
    [Value(0,
        Required = true,
        Min = 3,
        Max = 4,
        HelpText = "Input files in order: [BASE] LOCAL REMOTE MERGED"
    )]
    public IList<string> Files { get; set; }

    public static implicit operator NamedStartupOptions(SimpleStartupOptions opts)
    {
        return new NamedStartupOptions
        {
#if DEBUG
            AttachDevTools = opts.AttachDevTools,
#endif
            Merged = opts.Files[^1],
            Remote = opts.Files[^2],
            Local  = opts.Files[^3],
            Base   = opts.Files.Count == 4 ? opts.Files[0] : null,
        };
    }
}